using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class photonHandler : MonoBehaviour {

	public PhotonButton photonBtutton;
	public GameObject mainPlayer;

	private void Awake() {
		DontDestroyOnLoad(this.transform);
	}

	public void createNewRoom() {
		PhotonNetwork.CreateRoom(photonBtutton.createRoomInput.text, new RoomOptions() { MaxPlayers = 4}, null);
	}

	public void joinOrCreateRoom() {
		RoomOptions roomOptions = new RoomOptions();
		roomOptions.MaxPlayers = 4;
		PhotonNetwork.JoinOrCreateRoom(photonBtutton.joinRoomInput.text, roomOptions, TypedLobby.Default);
	}

	private void OnJoinedRoom() {
		moveScene();
		Debug.Log("You just joined the room!");
	}

	public void moveScene() {
		PhotonNetwork.LoadLevel("BlackMamba");
	}

	private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode) {
		if(scene.name == "BlackMamba") {
			spwanPlayer();
		}
	} 

	private void spwanPlayer() {
		PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
	}
}
