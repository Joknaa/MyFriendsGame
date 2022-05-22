using System;
using UnityEngine;

namespace Reality {
    public class CameraController : MonoBehaviour {
        [SerializeField] private float transitionSmoothing = 0.1f;
        private float cameraNewPosition_X;
        private float cameraPosition_X;
        private Vector3 cameraPosition;
        private Transform playerTransform;
        private void Start() {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cameraNewPosition_X = playerTransform.position.x;
        }

        private void LateUpdate() {
            cameraNewPosition_X = playerTransform.position.x;
            
            cameraPosition_X = Mathf.Lerp(cameraPosition_X, cameraNewPosition_X, transitionSmoothing);

            if (cameraPosition_X < 15) cameraPosition_X = 15;
            if (cameraPosition_X > 254) cameraPosition_X = 254;
            
            cameraPosition = transform.position;
            transform.position = new Vector3(cameraPosition_X, cameraPosition.y, cameraPosition.z);
            
        }

        public void SetCameraPosition(float CameraTranslation) {
            cameraNewPosition_X = cameraPosition_X + CameraTranslation;
        }
        
    }
}