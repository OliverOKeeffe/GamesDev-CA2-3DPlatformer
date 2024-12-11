using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Drag your Cinemachine FreeLook Camera here
    public float sensitivity = 10f;  // Mouse sensitivity

    private float currentX = 0f;
    private float currentY = 0f;

    void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Adjust for mouse sensitivity
        currentX += mouseX * sensitivity;
        currentY -= mouseY * sensitivity;
        currentY = Mathf.Clamp(currentY, -40f, 80f);  // Limit vertical camera rotation

        // Update the Cinemachine camera's rotation
        freeLookCamera.m_XAxis.Value = currentX;
        freeLookCamera.m_YAxis.Value = currentY;
    }
}
