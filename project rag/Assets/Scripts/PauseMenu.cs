using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused;

    public GameObject PauseMenuObject;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            PauseMenuObject.SetActive(paused);

            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;

            Cursor.visible = paused;
        }
    }
}
