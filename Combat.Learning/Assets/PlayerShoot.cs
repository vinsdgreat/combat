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
	public float hitForce = 400f;

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
		ShootRay(firePos, dir);
	}

	void ShootRay(Vector2 origin, Vector2 direction) {
		RaycastHit2D hit = Physics2D.Raycast(origin, direction, range, whatToHit);
		Vector2 hitPos;
		if(hit.collider != null) {
			
			if(hit.collider.tag == "BlackMamba") {
				Debug.Log("We have hit black mamba");
				hit.collider.gameObject.GetComponent<EnemyHealth>().Damage(damage);

				if(hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {
					hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-hit.normal * hitForce);
				}
			} else {
				Debug.Log("We have hit something else");
			}

			hitPos = hit.point;
			
		} else {
			float xVal = (c_movement.m_FacingRight) ? Vector2.right.x : Vector2.left.x;
			hitPos = new Vector2(xVal * range, origin.y);
		}
		DrawBullet(hitPos);
	}

	void DrawBullet(Vector2 hitPos) {
		GameObject bullet = Instantiate(bulletPrefab, weaponTip.position, Quaternion.identity);
		LineRenderer line = bullet.GetComponent<LineRenderer>();
		if(line != null) {
			line.SetPosition(0, weaponTip.position);
			line.SetPosition(1, hitPos);
		}

		Destroy(bullet, 0.04f);
		StartCoroutine(MuzzleFlash(0.02f));
	}

	IEnumerator MuzzleFlash(float _duration) {
		muzzleFlashRend.color = new Color(1,1,1,1);
		muzzleFlashRend.gameObject.transform.Rotate(Random.Range(0,2)*100,0,0);
		yield return new WaitForSeconds(_duration);
		muzzleFlashRend.color = new Color(0,0,0,0);
	}
}
