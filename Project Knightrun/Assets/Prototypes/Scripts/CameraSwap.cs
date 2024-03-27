using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

public class CameraSwap : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject SideCamera;

    private void Start()
    {
        MainCamera.SetActive(true);
        SideCamera.SetActive(false);
    }

    [ProButton]
    public void CameraSwap2D()
    {
        MainCamera.SetActive(false);
        SideCamera.SetActive(true);
    }

    [ProButton]
    public void CameraSwap3D()
    {
        SideCamera.SetActive(false);
        MainCamera.SetActive(true);
    }
}
