using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RainbowText : MonoBehaviour
{
    private TMP_Text text;

    public float number;

    public bool flip;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flip == false)
        {
            number += 0.001f;

            if (number > 1)
            {
                flip = true;
            }
        }

        if (flip == true)
        {
            number -= 0.001f;

            if (number < 0)
            {
                flip = false;
            }
        }

        text.color = Color.HSVToRGB(number, 1, 1);
    }
}
