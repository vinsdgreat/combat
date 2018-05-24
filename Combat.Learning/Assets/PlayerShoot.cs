using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField]
	GameObject bulletPrefab;
	[SerializeField]
	Transform weaponTip;

	public float fireRate = 5f;
	public float damage = 25f;
	public LayerMask whatToHit;
	public float range = 10f;

	float timeFire = 0;
	PlayerMovement c_movement;

	public void Awake() {
		c_movement = GetComponent<PlayerMovement>();
	}

	public void OnShoot() {
		if (fireRate == 0) {
			Shoot();
		}
		else {
			if(Time.time > timeFire) {
				timeFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot() {
		Vector2 firePos = new Vector2(weaponTip.position.x, weaponTip.position.y);
		Vector2 dir = (c_movement.m_FacingRight) ? Vector2.right : Vector2.left;
		RaycastHit2D hit = Physics2D.Raycast(firePos, dir, range, whatToHit);
		Debug.DrawRay(firePos, dir * range, Color.blue, 1f);
	}
}
