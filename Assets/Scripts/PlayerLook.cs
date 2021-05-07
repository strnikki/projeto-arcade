using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLook : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 50f;

    [SerializeField] Transform playerBody;
    [SerializeField] Slider sensitivitySlider;
    [SerializeField] TMP_Text sensitivityText;

    private float xRotation = 0f;

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        sensitivitySlider.value = mouseSensitivity;
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
    }

    public void UpdateSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
        sensitivityText.text = "" + mouseSensitivity;
    }
}
