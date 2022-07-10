using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject cam;

    public GameObject BrokenMug;

    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Mug"))
            {
                Instantiate(BrokenMug, hit.transform.position, Quaternion.identity);

                Collider[] colliders = Physics.OverlapSphere(hit.transform.position, 1f);

                foreach (Collider collider in colliders)
                {
                    Rigidbody rb = collider.transform.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddExplosionForce(2f, hit.point, 1f);
                    }
                }

                Destroy(hit.transform.gameObject);
            }
        }
    }
}
