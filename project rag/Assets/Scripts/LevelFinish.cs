using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour
{
    public int NextLevel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player") == true)
        {
            if (NextLevel != 0)
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
