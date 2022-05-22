using System;
using UnityEngine;

namespace Reality {
    public class Bullet : MonoBehaviour {
        private GameObject bulletInstance;
        public bool isPlayerBullet;
        public float damage = 1f;

        

        private void OnTriggerEnter2D(Collider2D collidedWith) {
            if (collidedWith.CompareTag("Ground")) {
                Destroy(bulletInstance);
            }

            if (isPlayerBullet && collidedWith.CompareTag("Enemy")) {
                collidedWith.gameObject.GetComponent<Enemy>().takeDamage(damage);
                
                Destroy(gameObject);
            }

            if (!isPlayerBullet && collidedWith.CompareTag("Player")) {
                
                collidedWith.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
                Destroy(gameObject);
            }
        }

        public void SetInstance(GameObject instance) => this.bulletInstance = instance;
    }
}