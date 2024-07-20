using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            Debug.LogError("Player GameObject is null");
            return;
        }
        playerDamageable = player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnPlayerHealthChanged(playerDamageable.Health, playerDamageable.MaxHealth);
    }

    private void OnPlayerHealthChanged(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        healthText.text = $"Health : {(int)currentHealth} / {(int)maxHealth}";
    }

    private void OnEnable()
    {
        playerDamageable.HealthChangeEvent.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.HealthChangeEvent.RemoveListener(OnPlayerHealthChanged);
    }
}
