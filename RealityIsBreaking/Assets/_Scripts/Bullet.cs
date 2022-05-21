using System;
using UnityEngine;

namespace Reality {
    public class Bullet : MonoBehaviour {

        private GameObject bulletInstance;
        
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Ground")) {
                print("Bullet hit ground");
                Destroy(bulletInstance);
            }
            // If collided with enemy, kill it
        }
        
        public void SetInstance(GameObject instance) => this.bulletInstance = instance;

    }
}