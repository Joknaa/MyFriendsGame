using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public Image[] healthPoints;

    public float health, maxHealth = 100;

    private void Start() {
        health = maxHealth;
    }

    private void Update() {
        if (health > maxHealth) health = maxHealth;
        
        for (int i = 0; i < healthPoints.Length; i++) {
            healthPoints[i].enabled = health > i * maxHealth / healthPoints.Length;
        }
    }

    public void Damage(float damagePoints) {
        if (health > 0) health -= damagePoints;
    }

    public void Heal(float healingPoints) {
        if (health < maxHealth) health += healingPoints;
    }
}