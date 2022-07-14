using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool paused;

    public GameObject PauseMenuObject;

    private PlayerMovement playerMovement;

    private PlayerShoot playerShoot;

    private Rigidbody rb;

    void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();

        playerShoot = transform.GetComponent<PlayerShoot>();

        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            PauseMenuObject.SetActive(paused);

            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;

            Cursor.visible = paused;

            playerMovement.enabled = !paused;

            playerShoot.enabled = !paused;

            Time.timeScale = paused ? 0 : 1;
        }
    }
}
