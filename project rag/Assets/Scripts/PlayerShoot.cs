using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject cam;

    public GameObject BrokenMug;

    public CameraRecoil CameraRecoil;

    public WeaponSwitching WeaponSwitching;

    public WeaponSway WeaponSway;

    public float aimSpeed;

    public GameObject SelectedWeaponObject;

    public Weapon SelectedWeapon;

    public float TimeSinceLastShot;

    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastShot += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedWeapon.FullAuto == false)
            {
                Shoot();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (SelectedWeapon.FullAuto == true)
            {
                Shoot();
            }
        }

        if (Input.GetMouseButton(1))
        {
            Aim(true);

            WeaponSway.aiming = true;
        }

        else
        {
            Aim(false);

            WeaponSway.aiming = false;
        }
    }

    void Shoot()
    {
        if (CanShoot())
        {
            CameraRecoil.Recoil();

            TimeSinceLastShot = 0;

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

    void Aim(bool aiming)
    {
        if (aiming == true)
        {
            SelectedWeaponObject.transform.localPosition = Vector3.Lerp(SelectedWeaponObject.transform.localPosition, SelectedWeapon.ADSPosition, aimSpeed * Time.deltaTime);
        }

        else
        {
            SelectedWeaponObject.transform.localPosition = Vector3.Lerp(SelectedWeaponObject.transform.localPosition, SelectedWeapon.StartPosition, aimSpeed * Time.deltaTime);
        }
    }

    public bool CanShoot() => TimeSinceLastShot > 1f / (SelectedWeapon.Firerate / 60);
}
