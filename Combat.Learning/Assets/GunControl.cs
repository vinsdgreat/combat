using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

	public int rotationOffset = 90;
	PlayerMovement c_movement;
	[SerializeField]
	private GameObject player; 
	private Transform torsoBoneTransform;

	public void Awake() {
		c_movement = GetComponent<PlayerMovement>();
		torsoBoneTransform = player.transform.Find("Bones/Hip Bone/Torso Bone");
		//torsoBoneTransform = torsoBone.transform;
	}
	
	// Update is called once per frame
	void Update () {

		int inverter = c_movement.m_FacingRight == true ? 1 : -1;
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - torsoBoneTransform.position;
		difference.Normalize();

		float rotZ = Mathf.Atan2(difference.y, difference.x*inverter) * Mathf.Rad2Deg;
		float limitRotZ = Mathf.Clamp(rotZ, -45, 45);
		//transform.rotation = Quaternion.Euler(0f, 0f, limitRotZ + rotationOffset);

		torsoBoneTransform.localEulerAngles = new Vector3(torsoBoneTransform.localEulerAngles.x, torsoBoneTransform.localEulerAngles.y, limitRotZ);

	}
}
