using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	[SerializeField]
	GameObject bulletPrefab;
	[SerializeField]
	Transform weaponTip;
	[SerializeField]
	SpriteRenderer muzzleFlashRend;

	public float fireRate = 5f;
	public float damage = 25f;
	public LayerMask whatToHit;
	public float range = 10f;

	float timeFire = 0;
	PlayerMovement c_movement;

	public void Awake() {
		c_movement = GetComponent<PlayerMovement>();
		muzzleFlashRend.color = new Color(0,0,0,0);
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
		//RaycastHit2D hit = Physics2D.Raycast(firePos, dir, range, whatToHit);
		//Debug.DrawRay(firePos, dir * range, Color.blue, 1f);
		DrawBullet();
	}

	void DrawBullet() {
		Quaternion rot = (c_movement.m_FacingRight) ? Quaternion.Euler(0,0,0) : Quaternion.Euler(0, 180, 0);
		Instantiate(bulletPrefab, weaponTip.position, rot);
		StartCoroutine(MuzzleFlash(0.02f));
	}

	IEnumerator MuzzleFlash(float _duration) {
		muzzleFlashRend.color = new Color(1,1,1,1);
		muzzleFlashRend.gameObject.transform.Rotate(Random.Range(0,2)*100,0,0);
		yield return new WaitForSeconds(_duration);
		muzzleFlashRend.color = new Color(0,0,0,0);
	}
}
