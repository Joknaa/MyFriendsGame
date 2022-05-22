using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reality {
    public class EnemyMelee : MonoBehaviour, IEnemy {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip[] SFX;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage;
        

        [SerializeField] private int MaxHealth = 1;
        private SpriteRenderer spriteRenderer;
        private int currentHealth;
        private GameObject player;
        private bool isDead = false;
        private BoxCollider2D[] boxColliders;
        private int direction = 1;

        void Start() {
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
            boxColliders = transform.GetComponents<BoxCollider2D>();
            spriteRenderer.enabled = false;
            foreach (var boxCollider2D in boxColliders) {
                boxCollider2D.enabled = false;
            }
            
            player = GameObject.FindGameObjectWithTag("Player");
            currentHealth = MaxHealth;
            StartCoroutine(Setup());
        }

        private void Update() {
            if (!GameStateController.Instance.IsPlaying_SecondHalf()) return;
            
            transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
            spriteRenderer.flipX = direction == -1;
        }

        
        private IEnumerator Setup() {
            yield return new WaitUntil(GameStateController.Instance.IsPlaying_SecondHalf);
            spriteRenderer.enabled = true;
            foreach (var boxCollider2D in boxColliders) {
                boxCollider2D.enabled = true;
            }
        }
        
        private void OnTriggerEnter2D(Collider2D col) {
            if (col.CompareTag("RoomTransition")) {
                direction *= -1;
            }
            if (col.CompareTag("Ground")) {
                direction *= -1;
            }
            
            if (col.CompareTag("Player")) {
                col.gameObject.GetComponent<PlayerHealth>().takeDamage(damage);
            }
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
        

        public void TakeDamage(float damage) {
            this.currentHealth -= (int) damage;
            _source.PlayOneShot(SFX[0]);
            if (currentHealth <= 0 && isDead==false) {
                StartCoroutine(CommitSeppuku(1f));
            }
        }
        
    }
}