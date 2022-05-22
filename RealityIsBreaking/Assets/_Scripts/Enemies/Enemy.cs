using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reality {
    public class Enemy : MonoBehaviour {
        public float timeBetweenEachBullet;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletForce = 40f;

        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] SFX;

        [SerializeField] private int MaxHealth = 1;
        private SpriteRenderer spriteRenderer;
        private int currentHealth;
        private GameObject player;
        private bool isDead = false;

        void Start() {
            spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            player = GameObject.FindGameObjectWithTag("Player");
            currentHealth = MaxHealth;
            StartCoroutine(Shoot());
        }


        private IEnumerator Shoot() {
            yield return new WaitUntil(GameStateController.Instance.IsPlaying_SecondHalf);
            spriteRenderer.enabled = true;
            
            while (true) {
                if (!GameStateController.Instance.IsPlaying()) yield return new WaitUntil(GameStateController.Instance.IsPlaying);
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

        IEnumerator CommitSeppuku(float Timer) {
            _source.PlayOneShot(SFX[1]);
            isDead = true;
            

            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(Timer / 9);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(Timer / 9);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(Timer / 9);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(Timer / 9);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(Timer / 9);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(Timer / 9);

            Destroy(this.gameObject);
        }

        private float getShootingDirection() {
            Vector2 lookDir = (Vector2) player.transform.position - (Vector2) transform.position;
            return Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        }

        public void takeDamage(float damage) {
            this.currentHealth -= (int) damage;
            _source.PlayOneShot(SFX[0]);
            if (currentHealth <= 0 && isDead==false) {
                StartCoroutine(CommitSeppuku(1f));
            }
        }
    }
}