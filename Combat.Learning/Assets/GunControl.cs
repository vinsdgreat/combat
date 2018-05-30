using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

	public int rotationOffset = 90;
	
	// Update is called once per frame
	void Update () {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		difference.Normalize();

		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		float limitRotZ = Mathf.Clamp(rotZ, -45, 45);
		//transform.rotation = Quaternion.Euler(0f, 0f, limitRotZ + rotationOffset);

		transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, limitRotZ);

		Debug.Log(rotZ);
	}
}
