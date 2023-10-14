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
            Transform transform1;
            (transform1 = transform).LookAt(thisCamera.transform);
            var position = transform1.position;
            position = new Vector3(position.x, 1.15f, position.z);
            transform1.position = position;
        }
    }
}