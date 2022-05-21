using Reality;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image[] healthPoints;

    private float health, maxHealth;
    private PlayerHealth playerHealth;

    private void Start() {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        maxHealth = playerHealth.MaxHealth;

        health = maxHealth;
    }

    private void Update() {
        health = playerHealth.currentHealth;

        if (health > maxHealth) {
            health = maxHealth;
            return;
        }

        for (int i = 0; i < healthPoints.Length; i++) {
            healthPoints[i].enabled = health > i * maxHealth / healthPoints.Length;
        }
    }
}