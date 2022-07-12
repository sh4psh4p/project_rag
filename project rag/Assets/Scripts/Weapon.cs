using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 aimPosition;
    public Vector3 defaultPosition;
    public bool fullAuto;
    public float fireRate;

    void Start()
    {
        defaultPosition = transform.localPosition;
    }
}
