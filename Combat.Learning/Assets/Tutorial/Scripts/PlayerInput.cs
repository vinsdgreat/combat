using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    private PlayerMovement c_movement;  //Reference to PlayerMovement script
    //private PlayerShoot c_shoot;
    private bool isJumping;             //To determine if the player is jumping
	
	void Awake()
    {
        //References
        c_movement = GetComponent<PlayerMovement>();
        //c_shoot = GetComponent<PlayerShoot>();
	}
	
	void Update ()
    {
        //If he is not jumping...
	    if (!isJumping)
        {
            //See if button is pressed...
            isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        }	

        /*if (Input.GetButtonDown("Fire1") && c_shoot.fireRate == 0) {
            c_shoot.OnShoot();
        }
        if (Input.GetButton("Fire1") && c_shoot.fireRate > 0) {
            c_shoot.OnShoot();
        }*/
	}

    private void FixedUpdate()
    {
        //Get horizontal axis
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //Call movement function in PlayerMovement
        c_movement.Move(h, isJumping);
        //Reset
        isJumping = false;
    }
}
