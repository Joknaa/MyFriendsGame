using System;
using UnityEngine;

namespace Reality {
    public class PlayerCollisions : MonoBehaviour{
        private void OnTriggerEnter2D(Collider2D collidedWith) {
            if (collidedWith.CompareTag("Chest")) {
                GameStateController.Instance.SetState_PhoneCall();
            }
        }
    }
}