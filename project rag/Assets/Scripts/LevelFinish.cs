using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public int NextLevel;

    public LevelSettings LevelSettings;

    void Start()
    {
        LevelSettings = GameObject.FindObjectOfType<LevelSettings>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player") == true)
        {
            float time = col.transform.GetComponent<PlayerManager>().time;

            if (time < LevelSettings.BronzeTime)
            {
                PlayerPrefs.SetInt("Level" + (NextLevel - 1).ToString() + "Bronze", 1);

                Debug.Log("test");
            }

            if (time < LevelSettings.SilverTime)
            {
                PlayerPrefs.SetInt("Level" + (NextLevel - 1).ToString() + "Silver", 1);
            }

            if (time < LevelSettings.GoldTime)
            {
                PlayerPrefs.SetInt("Level" + (NextLevel - 1).ToString() + "Gold", 1);

                Debug.Log("test");
            }

            if (NextLevel != 6)
            {
                PlayerPrefs.SetInt("Level" + NextLevel.ToString() + "Unlocked", 1);

                SceneManager.LoadScene("Level" + NextLevel.ToString());
            }

            else
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }
}
