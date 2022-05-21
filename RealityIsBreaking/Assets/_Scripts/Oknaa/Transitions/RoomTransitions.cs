using Camera;
using UnityEngine;

namespace Transitions {
    public class RoomTransitions : MonoBehaviour {
        [Header("Camera & Position Variables: ")]
        public float CameraTranslation;

        public Vector3 PlayerTranslation;
        private CameraController CameraScript;

        private void Start() {
            CameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.CompareTag("Player")) {
                CameraScript.SetCameraPosition(CameraTranslation);
                other.transform.position += PlayerTranslation;
            }
        }
    }
}