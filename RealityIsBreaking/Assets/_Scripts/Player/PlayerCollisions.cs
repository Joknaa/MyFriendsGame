using System;
using UnityEngine;

namespace Reality {
    public class PlayerCollisions : MonoBehaviour{
        private void OnTriggerEnter2D(Collider2D collidedWith) {
            if (collidedWith.CompareTag("Chest")) {
                collidedWith.enabled = false;
                GameStateController.Instance.SetState_PhoneCall();
            }
            if (collidedWith.CompareTag("Spike")) {
                GetComponent<PlayerHealth>().takeDamage(1);
            }
            if (collidedWith.CompareTag("PitFall")) {
                GetComponent<PlayerHealth>().takeDamage(10);
            }
            
            if (collidedWith.CompareTag("FinishLine")) {
                GameStateController.Instance.SetState_GameWon();
            }
        }
    }
}