using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CircuitLoader : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public AllSprites allSprites;
    public bool sandBoxMode;
    public bool tutrorialMode;
    private Vector3Int outputLocation;
    private HashSet<Vector3Int> placeholders = new HashSet<Vector3Int>();
    private int index;
    public void Awake()
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            TileBase current = tilemap.GetTile(pos);
            if(current is WireTile)
            {
                WireTile wire = ScriptableObject.CreateInstance<WireTile>();
                wire.sprites = allSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wire);
            }
            else if(current is AndTile)
            {
                AndTile and = ScriptableObject.CreateInstance<AndTile>();
                and.sprites = allSprites;
                and.sprite = allSprites.andSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), and);
            }
            else if(current is OrTile)
            {
                OrTile or = ScriptableObject.CreateInstance<OrTile>();
                or.sprites = allSprites;
                or.sprite = allSprites.orSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), or);
            }
            else if(current is NotTile)
            {
                NotTile not = ScriptableObject.CreateInstance<NotTile>();
                not.sprite = allSprites.notSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), not);
            }
            else if(current is InputOnTile)
            {
                InputOnTile inputOn = ScriptableObject.CreateInstance<InputOnTile>();
                inputOn.sprite = allSprites.inputOnSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOn);
            }
            else if(current is InputOffTile)
            {
                InputOffTile inputOff = ScriptableObject.CreateInstance<InputOffTile>();
                inputOff.sprite = allSprites.inputOffSprite;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), inputOff);
            }
            else if(current is OutputTile)
            {
                OutputTile output = ScriptableObject.CreateInstance<OutputTile>();
                output.sprites = allSprites;
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), output);
                if(!sandBoxMode)
                {
                    outputLocation = new Vector3Int(pos.x, pos.y, 0);
                }
            }
            else if(current is PlaceholderTile)
            {
                PlaceholderTile placeholder = ScriptableObject.CreateInstance<PlaceholderTile>();
                placeholder.sprites = allSprites;
                placeholder.sprite = allSprites.placeholderSprite;
                placeholders.Add(new Vector3Int(pos.x, pos.y, 0));
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), placeholder);
            }
            else
            {
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), null);
            }
        }
    }

    private void OnMouseDown() 
    {
        // Debug.Log("OnMouseDown()");
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int location = grid.WorldToCell(worldPosition);

        if(tutrorialMode)
        {
            TileBase selectedTile = tilemap.GetTile(location);
            if(selectedTile is InputTile)
            {
                if(selectedTile is InputOnTile)
                {
                    InputOffTile newTile = ScriptableObject.CreateInstance<InputOffTile>();
                    newTile.sprite = allSprites.inputOffSprite;
                    tilemap.SetTile(new Vector3Int(location.x, location.y, 0), newTile);
                }
                else
                {
                    InputOnTile newTile = ScriptableObject.CreateInstance<InputOnTile>();
                    newTile.sprite = allSprites.inputOnSprite;
                    tilemap.SetTile(new Vector3Int(location.x, location.y, 0), newTile);
                }
            }
        }
        else if (sandBoxMode || placeholders.Contains(location)) {
            switch (index)
            {
                case 0: {
                    WireTile tile = ScriptableObject.CreateInstance<WireTile>();
                    tile.sprites = allSprites;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 1: {
                    AndTile tile = ScriptableObject.CreateInstance<AndTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.andSprite;
                    tilemap.SetTile(location, tile);

                    if(!sandBoxMode)
                    {
                        gameObject.GetComponent<LevelUI>().updateScore();   
                    }
                    break;
                    
                }
                case 2: {
                    OrTile tile = ScriptableObject.CreateInstance<OrTile>();
                    tile.sprites = allSprites;
                    tile.sprite = allSprites.orSprite;
                    tilemap.SetTile(location, tile);

                    if(!sandBoxMode)
                    {
                        gameObject.GetComponent<LevelUI>().updateScore();   
                    }
                    break;
                }
                case 3: {
                    NotTile tile = ScriptableObject.CreateInstance<NotTile>();
                    tile.sprite = allSprites.notSprite;
                    tilemap.SetTile(location, tile);

                    if(!sandBoxMode)
                    {
                        gameObject.GetComponent<LevelUI>().updateScore();   
                    }
                    break;
                }
                case 4: {
                    InputOffTile tile = ScriptableObject.CreateInstance<InputOffTile>();
                    tile.sprite = allSprites.inputOffSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 5: {
                    InputOnTile tile = ScriptableObject.CreateInstance<InputOnTile>();
                    tile.sprite = allSprites.inputOnSprite;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 6: {
                    OutputTile tile = ScriptableObject.CreateInstance<OutputTile>();
                    tile.sprites = allSprites;
                    tilemap.SetTile(location, tile);
                    break;
                }
                case 7: {
                    if(sandBoxMode) {
                        Circuit.RemoveComponent(location);
                        tilemap.SetTile(location, null);
                    }
                    else {
                        PlaceholderTile tile = ScriptableObject.CreateInstance<PlaceholderTile>();
                        tile.sprite = allSprites.placeholderSprite;
                        tile.sprites = allSprites;
                        tilemap.SetTile(location, tile);
                    }
                    break;
                }
                default: break;
            }            
        }
        if(!sandBoxMode && !tutrorialMode && Circuit.circuitComponents[outputLocation].on)
        {
            bool slotsCovered = true;
            foreach(Vector3Int slot in placeholders)
            {
                slotsCovered = !tilemap.GetTile<PlaceholderTile>(slot) ? slotsCovered : false;
            }
            if(slotsCovered)
            {
                gameObject.GetComponent<LevelUI>().LevelCompleted();
            }
        }
        else if(tutrorialMode && Circuit.circuitComponents[outputLocation].on)
        {
            gameObject.GetComponent<TutorialUI>().LevelCompleted();
        }
    }
    public void WireClick() {
        index = 0;
    }

    public void AndClick() {
        index = 1;
    }

    public void OrClick() {
        index = 2;
    }

    public void NotClick() {
        index = 3;
    }

    public void OffInputClick() {
        index = 4;
    }

    public void OnInputClick() {
        index = 5;
    }

    public void OutputClick() {
        index = 6;
    }
    public void EraserClick() {
        index = 7;
    }
}
