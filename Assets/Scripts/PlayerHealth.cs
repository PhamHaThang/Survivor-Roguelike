using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [Header("Settings")]
    [SerializeField] private int maxHealth;
    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private int health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        health = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage) {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;
        UpdateUI();
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Die");
        SceneManager.LoadScene(0);
    }
    private void UpdateUI() {
        healthSlider.value = (float)health / maxHealth;
        healthText.text = health + " / " + maxHealth;
    }
}
