using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    public float amount;
    public float maxAmount;
    public float smoothAmount;

    public Vector3 initialPosition;

    public bool aiming;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (aiming == false)
        {
            float movementX = -Input.GetAxis("Mouse X") * amount;
            float movementY = -Input.GetAxis("Mouse Y") * amount;
            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
        }

        if (aiming == true)
        {
            float movementX = -Input.GetAxis("Mouse X") * amount / 5;
            float movementY = -Input.GetAxis("Mouse Y") * amount / 5;
            movementX = Mathf.Clamp(movementX, -maxAmount / 5, maxAmount / 5);
            movementY = Mathf.Clamp(movementY, -maxAmount / 5, maxAmount / 5);

            Vector3 finalPosition = new Vector3(movementX, movementY, 0);

            transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
        }
    }
}
