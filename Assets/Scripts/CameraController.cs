using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(640, 480, FullScreenMode.Windowed);
    }
}
