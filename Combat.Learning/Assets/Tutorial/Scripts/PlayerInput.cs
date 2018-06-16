using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    public bool devTesting;
    public PhotonView photonView;
    private Vector3 selfPos;
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
        if(!devTesting) {
            if(photonView.isMine) {
                checkJump();
            } else {
                smoothNetMovement();
            }
        } else {
            checkJump();
        }

        /*if (Input.GetButtonDown("Fire1") && c_shoot.fireRate == 0) {
            c_shoot.OnShoot();
        }
        if (Input.GetButton("Fire1") && c_shoot.fireRate > 0) {
            c_shoot.OnShoot();
        }*/
	}

    private void checkJump() {
        //If he is not jumping...
        if (!isJumping)
        {
            //See if button is pressed...
            isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        }	
    }

    private void FixedUpdate()
    {
        if(!devTesting) {
            if(photonView.isMine) {
                checkMovement();
            } else {
                smoothNetMovement();
            }
        } else {
            checkMovement();
        }
    }

    private void checkMovement() {
        //Get horizontal axis
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //Call movement function in PlayerMovement
        c_movement.Move(h, isJumping);
        //Reset
        isJumping = false;
    }

    private void smoothNetMovement() {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.isWriting) {
            stream.SendNext(transform.position);
        } else {
            selfPos = (Vector3)stream.ReceiveNext();
        }
    }
}
