using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	[SerializeField]
	SpriteRenderer muzzleFlashRend;
	public Transform BulletPrefab;
	public Transform HitPrefab;

	public float fireRate = 0;
	public float Damage = 10;
	public float hitForce = 400f;
	public float range = 10f;
	public LayerMask whatToHit;

	float timetoSpawnEffect = 0;
	float effectSpawnRate = 10;
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
			//hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(-hit.normal * hitForce);
		}

		if(Time.time >= timetoSpawnEffect) {
			Vector3 hitPos;
			Vector3 hitNormal;

			if(hit.collider == null) {
				hitPos = (mousePosition - firePointPosition) * 30;
				hitNormal = new Vector3(9999,9999,9999);
			}
			else {
				hitPos = hit.point;
				hitNormal = hit.normal;
			}

			Effect(hitPos, hit.normal);
			timetoSpawnEffect = Time.time + 1/effectSpawnRate;
		}
	}

	void Effect(Vector3 hitPos, Vector3 hitNormal) {
		Transform trail = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer>();

		if(lr != null) {
			lr.SetPosition(0, firePoint.position);
			lr.SetPosition(1, hitPos);
		}
		Destroy(trail.gameObject, 0.04f);

		if(hitNormal != new Vector3(9999,9999,9999)) {
			Transform hitParticle = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.forward, hitNormal)) as Transform;
			Destroy(hitParticle.gameObject, 1f);
		}

		StartCoroutine(MuzzleFlash(0.02f));
	}

	IEnumerator MuzzleFlash(float _duration) {
		muzzleFlashRend.color = new Color(1,1,1,1);
		muzzleFlashRend.gameObject.transform.Rotate(Random.Range(0,2)*100,0,0);
		yield return new WaitForSeconds(_duration);
		muzzleFlashRend.color = new Color(0,0,0,0);
	}


}
