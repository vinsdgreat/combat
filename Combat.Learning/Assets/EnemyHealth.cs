using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float baseHealth = 100f;

	[SerializeField]
	float currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = baseHealth;
	}
	
	public void Damage(float damage) {
		currentHealth -= damage;
		if(currentHealth <= 0) {
			Die();
		}
	}

	void Die() {
		Debug.Log("the enemy just died");
	}
}
