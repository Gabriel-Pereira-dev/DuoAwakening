using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class FaceCamera : MonoBehaviour
    {
        private Camera thisCamera;

        private void Awake()
        {
            thisCamera = Camera.main;
        }

        private void Update()
        {
            transform.LookAt(thisCamera.transform);
        }
    }
}