using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    public void EndCameraAnim()
    {
        Debug.LogWarning("End Camera animation.");
        playerCamera.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

    }
}
