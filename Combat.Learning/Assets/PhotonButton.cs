using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonButton : MonoBehaviour {

	public InputField createRoomInput, joinRoomInput;
	public MenuLogic mLogic;

	public void onClickCreateRoom() {
		if(createRoomInput.text.Length >= 1)
		PhotonNetwork.CreateRoom(createRoomInput.text, new RoomOptions() { MaxPlayers = 4}, null);
	}

	public void onClickJoinRoom() {
		PhotonNetwork.JoinRoom(joinRoomInput.text);
	}

	private void OnJoinedRoom() {
		mLogic.disableMenuUI();
		Debug.Log("You just joined the room!");
	}


}
