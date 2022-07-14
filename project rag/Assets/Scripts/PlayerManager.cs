using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float time = 0f;

    public TMP_Text TimerText;

    private LevelSettings levelSettings;

    public TMP_Text LevelNameText;

    public GameObject NextLevelBlocker;

    public GameObject PreviousLevelBlocker;

    public GameObject LevelEndNextLevelBlocker;

    public GameObject LevelEndPreviousLevelBlocker;

    public GameObject LevelEndMenu;

    public TMP_Text LevelEndTimeText;

    public TMP_Text LevelEndPBTimeText;

    public TMP_Text LevelEndUnlockCoinText;

    public GameObject LevelEndNewPBText;

    public GameObject LevelEndGoldCoin;

    public GameObject LevelEndSilverCoin;

    public GameObject LevelEndBronzeCoin;

    private PlayerMovement playerMovement;

    private PlayerShoot playerShoot;

    private Rigidbody rb;

    public bool ended;

    private float t;

    void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();

        playerShoot = transform.GetComponent<PlayerShoot>();

        rb = transform.GetComponent<Rigidbody>();

        levelSettings = GameObject.FindObjectOfType<LevelSettings>();

        LevelNameText.text = "level " + levelSettings.level.ToString();

        Time.timeScale = 1;

        if (levelSettings.level == 1)
        {
            PreviousLevelBlocker.SetActive(true);

            LevelEndPreviousLevelBlocker.SetActive(true);
        }

        if (levelSettings.level == 5)
        {
            NextLevelBlocker.SetActive(true);

            LevelEndNextLevelBlocker.SetActive(true);
        }
    }

    void Update()
    {
        if (ended == false)
        {
            time += 1 * Time.deltaTime;

            int minutes = Mathf.FloorToInt(time / 60.0f);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        else
        {
            t += Time.deltaTime;

            Time.timeScale = Mathf.Lerp(1, 0, t);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level" + (levelSettings.level + 1).ToString());
    }

    public void PreviousLevel()
    {
        SceneManager.LoadScene("Level" + (levelSettings.level - 1).ToString());
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelEnd()
    {
        LevelEndMenu.SetActive(true);

        int minutes = Mathf.FloorToInt(time / 60.0f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        LevelEndTimeText.text = string.Format("time: {0:00}:{1:00}", minutes, seconds);

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;

        playerMovement.enabled = false;

        playerShoot.enabled = false;

        //Time.timeScale = 0.1f;

        ended = true;

        TimerText.transform.gameObject.SetActive(false);

        if (seconds < Mathf.FloorToInt(PlayerPrefs.GetFloat("Level" + levelSettings.level + "Time")) || PlayerPrefs.HasKey("Level" + levelSettings.level + "Time") == false)
        {
            LevelEndNewPBText.SetActive(true);
        }

        if (time < PlayerPrefs.GetFloat("Level" + levelSettings.level + "Time") || PlayerPrefs.HasKey("Level" + levelSettings.level + "Time") == false)
        {
            PlayerPrefs.SetFloat("Level" + levelSettings.level + "Time", time);
        }

        float time2 = PlayerPrefs.GetFloat("Level" + levelSettings.level + "Time");

        int minutes2 = Mathf.FloorToInt(time2 / 60.0f);
        int seconds2 = Mathf.FloorToInt(time2 - minutes2 * 60);

        LevelEndPBTimeText.text = string.Format("personal best: {0:00}:{1:00}", minutes2, seconds2);

        if (PlayerPrefs.GetInt("Level" + levelSettings.level + "Bronze") == 1)
        {
            LevelEndBronzeCoin.SetActive(true);

            LevelEndUnlockCoinText.transform.gameObject.SetActive(true);

            int minutes3 = Mathf.FloorToInt(levelSettings.SilverTime / 60.0f);
            int seconds3 = Mathf.FloorToInt(levelSettings.SilverTime - minutes3 * 60);

            LevelEndUnlockCoinText.text = string.Format("unlock silver at {0:00}:{1:00}", minutes3, seconds3);
        }

        if (PlayerPrefs.GetInt("Level" + levelSettings.level + "Silver") == 1)
        {
            LevelEndSilverCoin.SetActive(true);

            LevelEndBronzeCoin.SetActive(false);

            LevelEndUnlockCoinText.transform.gameObject.SetActive(true);

            int minutes3 = Mathf.FloorToInt(levelSettings.GoldTime / 60.0f);
            int seconds3 = Mathf.FloorToInt(levelSettings.GoldTime - minutes3 * 60);

            LevelEndUnlockCoinText.text = string.Format("unlock gold at {0:00}:{1:00}", minutes3, seconds3);
        }

        if (PlayerPrefs.GetInt("Level" + levelSettings.level + "Gold") == 1)
        {
            LevelEndBronzeCoin.SetActive(true);

            LevelEndSilverCoin.SetActive(false);
            LevelEndBronzeCoin.SetActive(false);

            LevelEndUnlockCoinText.transform.gameObject.SetActive(false);
        }
    }
}
