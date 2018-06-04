using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	[SerializeField]
	SpriteRenderer muzzleFlashRend;

	public float fireRate = 0;
	public float Damage = 10;
	public float hitForce = 400f;
	public float range = 10f;
	public LayerMask whatToHit;
	public Transform BulletPrefab;

	float timeToFire = 0;
	Transform firePoint;

	void Awake () {
		firePoint = transform.Find("WeaponTip");
		if (firePoint == null) {
			Debug.LogError("No firepint? WHAT?!");
		}
		muzzleFlashRend.color = new Color(0,0,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(fireRate == 0) {
			if(Input.GetButtonDown("Fire1")) {
				Shoot();
			}
		} else {
			if(Input.GetButton("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}
	}

	void Shoot() {
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2(firePoint.position.x , firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		
		Debug.DrawLine(firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if(hit.collider != null) {
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
			Debug.Log("We hit " + hit.collider.name + " and did " + Damage + " damage");
			hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-hit.normal * hitForce);
			//hitPos = hit.point;
		} else {
			//float xVal = (c_movement.m_FacingRight) ? Vector2.right.x : Vector2.left.x;
			//hitPos = new Vector2(xVal * range, origin.y);
		}

		Effect();
	}

	void Effect() {
		Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
		StartCoroutine(MuzzleFlash(0.02f));
	}

	IEnumerator MuzzleFlash(float _duration) {
		muzzleFlashRend.color = new Color(1,1,1,1);
		muzzleFlashRend.gameObject.transform.Rotate(Random.Range(0,2)*100,0,0);
		yield return new WaitForSeconds(_duration);
		muzzleFlashRend.color = new Color(0,0,0,0);
	}


}
