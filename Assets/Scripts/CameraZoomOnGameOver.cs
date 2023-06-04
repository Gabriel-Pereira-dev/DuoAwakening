using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomOnGameOver : MonoBehaviour
{
    private CinemachineFollowZoom thisCinemachineFollowZoom;
    public float zoomSpeed = 3f;
    public float m_Width = 2f;
    // Start is called before the first frame update
    void Awake()
    {
        thisCinemachineFollowZoom = GetComponent<CinemachineFollowZoom>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (GameManager.Instance.isGameOver && thisCinemachineFollowZoom.m_Width != m_Width)
        // {
        //     thisCinemachineFollowZoom.m_Width -= Mathf.Clamp(zoomSpeed * Time.deltaTime, m_Width, 12f);
        // }
    }
}
