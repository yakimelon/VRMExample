using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;

public class OnlineManager : MonoBehaviour {
	void Start () {
		PhotonNetwork.ConnectUsingSettings(null);
	}
	
	// ロビーへ入室すると呼び出される
	void OnJoinedLobby() {
		Debug.Log("ロビーへ入室しました");
		// どこかのルームへ接続
		PhotonNetwork.JoinRandomRoom();
	}
	
	// ルームの入室へ失敗すると呼び出される
	void OnPhotonRandomJoinFailed() {
		Debug.Log("ルーム入室が失敗");
		// 自分でルームを作成して入室
		PhotonNetwork.CreateRoom(null);
	}
	
	// ルームへ入室すると呼び出される
	void OnJoinedRoom() {
		Debug.Log("ルームへ入室しました");

		PhotonNetwork.Instantiate(
			"Player",
			new Vector3(0, 0, 0),
			Quaternion.Euler(0, 180, 0), 
			0
		);
	}
}
