using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButton : MonoBehaviour {

	public InputField createRoomInput, joinRoomInput;
	public photonHandler pHandler;

	public void onClickCreateRoom() {
		pHandler.createNewRoom();
	}

	public void onClickJoinRoom() {
		pHandler.joinOrCreateRoom();
	}

}
