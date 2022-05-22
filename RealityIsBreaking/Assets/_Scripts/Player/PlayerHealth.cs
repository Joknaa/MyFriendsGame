using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reality {
    public class PlayerHealth : MonoBehaviour {
        
        public int MaxHealth = 8;
        public int currentHealth;
        private bool isImmune = false;
        public float immunityTimer = 0.5f;
        
        private SpriteRenderer spriteRenderer;
        
        private void Start() {
            spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
            currentHealth = MaxHealth;
            print("Player Health: " + currentHealth);
            
        }

        public void takeDamage(float damage) {
            print(currentHealth);
            
            if (!isImmune) {
                currentHealth -= (int) damage;
                StartCoroutine(waitForImmunity());
            }

            if (currentHealth <= 0) {
                OnPlayerDeath();
            }
        }


        private void OnPlayerDeath() {
            GameStateController.Instance.SetState_GameOver();
        }

        private IEnumerator waitForImmunity() {
            isImmune = true;
            // while immune, blink the player like SuperMario :p 
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(immunityTimer / 4);
            spriteRenderer.enabled = true;  
            yield return new WaitForSeconds(immunityTimer / 4);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(immunityTimer / 4);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(immunityTimer / 4);
            isImmune = false;
        }
    }
}