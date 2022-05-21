using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reality {
    public class Enemy : MonoBehaviour {
        public float timeBetweenEachBullet;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 40f;

        [SerializeField] private int MaxHealth = 1;
        private int currentHealth;
        private GameObject player;

        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
            currentHealth = MaxHealth;
            StartCoroutine(Shoot());
        }

        private IEnumerator Shoot() {
            while (true) {
                yield return new WaitForSeconds(timeBetweenEachBullet);
                FireBullet();
            }
        }
        

        private void FireBullet() {
            float angle = getShootingDirection();

            GameObject bulletInstance = Instantiate(bulletPrefab, transform);
            bulletInstance.transform.position = transform.position;
            bulletInstance.name = "EnemyBullet";
            Bullet bulletScript = bulletInstance.AddComponent<Bullet>();
            bulletScript.SetInstance(bulletInstance);

            Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
            bulletRb.rotation = angle;
            bulletRb.AddForce(((Vector2) player.transform.position - (Vector2) transform.position).normalized * bulletForce, ForceMode2D.Impulse);
        }

        private void CommitSeppuku() {
            Destroy(this.gameObject);
        }

        private float getShootingDirection() {
            Vector2 lookDir = (Vector2) player.transform.position - (Vector2) transform.position;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }

        public void takeDamage(float damage) {
            this.currentHealth -= (int) damage;
            if (currentHealth <= 0) {
                CommitSeppuku();
            }
        }
    }
}