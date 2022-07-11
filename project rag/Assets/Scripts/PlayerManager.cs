using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float time = 0f;

    public TMP_Text TimerText;

    void Start()
    {
        
    }

    void Update()
    {
        time += 1 * Time.deltaTime;

        int minutes = Mathf.FloorToInt(time / 60.0f);
        int seconds = Mathf.FloorToInt(time - minutes * 60);

        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
