using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] Transform playerBody;
    [SerializeField] ParticleSystem sparks;
    [SerializeField] float shootingCooldown = .5f;
    [SerializeField] float weaponDamage = 10f;
    [SerializeField] float weaponImpact = 100f;

    private Animator gunAnimator;
    private Camera cam;

    private float xRotation = 0f;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gunAnimator = GetComponentInChildren<Animator>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Shoot();
    }

    private void Shoot()
    {
        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            gunAnimator.SetTrigger("Shooting");
            sparks.Play();
            canShoot = false;
            StartCoroutine(ShootTimer());
            
            RaycastHit hit;

            if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 1000f))
            {
                Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if(target != null)
                {
                    target.TakeDamage(transform.forward * weaponImpact, weaponDamage);
                }
            }
        }
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shootingCooldown);
        canShoot = true;
    }
}
