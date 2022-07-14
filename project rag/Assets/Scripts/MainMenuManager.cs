using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenuManager : MonoBehaviour
{
    private bool CreditsEnabled;

    public Animator MainMenu;

    public Animator OptionsMenu;

    public Animator LevelMenu;

    public RuntimeAnimatorController MiddleToLeft;

    public RuntimeAnimatorController MiddleToRight;

    public GameObject[] LockedIcons;

    public GameObject WarningMenu;

    public Image CreditsMenu;

    public VolumeProfile PostProcessing;

    public Vignette Vignette;

    public GameObject CreditsMenu2;

    public GameObject[] BronzeCoins;

    public GameObject[] SilverCoins;

    public GameObject[] GoldCoins;

    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        Time.timeScale = 1;

        LevelMenu.SetBool("Enable", true);

        OptionsMenu.SetBool("Enable", true);

        LevelMenu.transform.GetChild(0).transform.gameObject.SetActive(false);

        OptionsMenu.transform.GetChild(0).transform.gameObject.SetActive(false);

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

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1).ToString() + "Bronze") == 1)
            {
                BronzeCoins[i].SetActive(true);
            }

            if (PlayerPrefs.GetInt("Level" + (i + 1).ToString() + "Silver") == 1)
            {
                SilverCoins[i].SetActive(true);
            }

            if (PlayerPrefs.GetInt("Level" + (i + 1).ToString() + "Gold") == 1)
            {
                GoldCoins[i].SetActive(true);
            }
        }

        for (int i = 0; i < PostProcessing.components.Count; i++)
        {
            if (PostProcessing.components[i].name == "Vignette")
            {
                Vignette = (Vignette)PostProcessing.components[i];

                ClampedFloatParameter intensity = Vignette.intensity;

                intensity.value = 0.1f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CreditsEnabled == true)
        {
            t += Time.deltaTime / 2;

            CreditsMenu.color = Color.Lerp(new Color32(0, 0, 0, 0), Color.black, t);

            ClampedFloatParameter intensity = Vignette.intensity;

            intensity.value = Mathf.Lerp(0.1f, 1f, t / 2);

            CreditsMenu2.transform.position += new Vector3(0, 100 * Time.deltaTime, 0);

            if (Input.anyKey)
            {
                CreditsEnabled = false;

                CreditsMenu.color = new Color32(0, 0, 0, 0);

                CreditsMenu.gameObject.SetActive(false);

                intensity.value = 0.1f;

                CreditsMenu2.transform.localPosition = new Vector3(0, -900, 0);

                t = 0;
            }
        }
    }

    public void Play()
    {
        LevelMenu.transform.GetChild(0).transform.gameObject.SetActive(true);

        LevelMenu.SetBool("Enable", false);

        MainMenu.runtimeAnimatorController = MiddleToLeft;

        MainMenu.SetBool("Enable", true);
    }

    public void Options()
    {
        OptionsMenu.transform.GetChild(0).transform.gameObject.SetActive(true);

        OptionsMenu.SetBool("Enable", false);

        MainMenu.runtimeAnimatorController = MiddleToRight;

        MainMenu.SetBool("Enable", true);
    }

    public void Credits()
    {
        CreditsMenu.transform.gameObject.SetActive(true);

        CreditsEnabled = true;
    }

    public void LevelMenuBack()
    {
        MainMenu.runtimeAnimatorController = MiddleToLeft;

        MainMenu.SetBool("Enable", false);

        LevelMenu.SetBool("Enable", true);
    }

    public void OptionsMenuBack()
    {
        MainMenu.runtimeAnimatorController = MiddleToRight;

        MainMenu.SetBool("Enable", false);

        OptionsMenu.SetBool("Enable", true);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level.ToString());
    }

    public void WarningMenuEnable()
    {
        WarningMenu.SetActive(true);
    }

    public void WarningMenuBack()
    {
        WarningMenu.SetActive(false);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        Application.Quit();
    }
}
