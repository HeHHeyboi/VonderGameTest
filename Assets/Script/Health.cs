using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public TMP_Text healthText;
	public int maxHealth = 100;
	public int currentHealth;

	public UnityEvent OnDie = new();

	void Start()
	{
		currentHealth = maxHealth;
		UpdateHealthText();
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			OnDie?.Invoke();
		}
		UpdateHealthText();
	}

	public void UpdateHealthText()
	{
		healthText.text = $"Health: {currentHealth}/{maxHealth}";
	}
}
