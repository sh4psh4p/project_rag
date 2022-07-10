using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMug : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject, Random.Range(5f, 8f));
        }
    }
}
