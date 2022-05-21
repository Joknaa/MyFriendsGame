using System;
using UnityEngine;

namespace Reality {
    public class Bullet : MonoBehaviour {

        private GameObject bulletInstance;
        public bool isPlayerBullet;
        
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("Ground")) {
                //print("Bullet hit ground");
                Destroy(bulletInstance);
            }


            if (isPlayerBullet && col.CompareTag("Enemy"))
            {
                //print("Bullet hit Enemy");
                Destroy(this.gameObject);

                col.gameObject.GetComponent<Enemy_5>().takeDamage();
            }

            if (!isPlayerBullet && col.CompareTag("Player"))
            {
                print("Bullet hit Player");
                Destroy(this.gameObject);

                col.gameObject.GetComponent<PlayerController>().takeDamage();
            }
        }
        
        public void SetInstance(GameObject instance) => this.bulletInstance = instance;

    }
}