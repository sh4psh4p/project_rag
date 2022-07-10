using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Animator MainMenu;

    public Animator LevelMenu;

    public GameObject[] LockedIcons;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        if (PlayerPrefs.GetInt("Level2Unlocked") == 1)
        {
            LockedIcons[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Level3Unlocked") == 1)
        {
            LockedIcons[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Level4Unlocked") == 1)
        {
            LockedIcons[3].SetActive(false);
        }

        if (PlayerPrefs.GetInt("Level5Unlocked") == 1)
        {
            LockedIcons[4].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        MainMenu.SetBool("Enable", true);

        LevelMenu.SetBool("Enable", true);
    }

    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void LevelMenuBack()
    {
        MainMenu.SetBool("Enable", false);

        LevelMenu.SetBool("Enable", false);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
