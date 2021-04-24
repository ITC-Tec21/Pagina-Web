using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject screen;
    public bool tutorialMode;

    public void TogglePause()
    {
        SceneManager.LoadScene("Pause Scene");
    }
}
