using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private GameObject cam;
    private CameraRecoil cameraRecoil;
    private WeaponSwitching weaponSwitching;
    private WeaponSway weaponSway;

    public GameObject BrokenMug;
    public float aimSpeed;
    [HideInInspector]
    public GameObject selectedWeaponObject;
    [HideInInspector]
    public Weapon selectedWeapon;
    public float timeSinceLastShot;

    void Start()
    {
        cam = transform.GetComponentInChildren<Camera>().gameObject;
        cameraRecoil = transform.GetComponentInChildren<CameraRecoil>();
        weaponSwitching = transform.GetComponentInChildren<WeaponSwitching>();
        weaponSway = transform.GetComponentInChildren<WeaponSway>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (selectedWeapon.fullAuto == false)
            {
                Shoot();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (selectedWeapon.fullAuto == true)
            {
                Shoot();
            }
        }

        if (Input.GetMouseButton(1))
        {
            Aim(true);

            weaponSway.aiming = true;
        }

        else
        {
            Aim(false);

            weaponSway.aiming = false;
        }
    }

    void Shoot()
    {
        if (CanShoot())
        {
            cameraRecoil.Recoil();

            timeSinceLastShot = 0;

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
            selectedWeaponObject.transform.localPosition = Vector3.Lerp(selectedWeaponObject.transform.localPosition, selectedWeapon.aimPosition, aimSpeed * Time.deltaTime);
        }

        else
        {
            selectedWeaponObject.transform.localPosition = Vector3.Lerp(selectedWeaponObject.transform.localPosition, selectedWeapon.defaultPosition, aimSpeed * Time.deltaTime);
        }
    }

    public bool CanShoot() => timeSinceLastShot > 1f / (selectedWeapon.fireRate / 60);
}
