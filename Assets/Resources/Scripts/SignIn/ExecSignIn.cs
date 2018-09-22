using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExecSignIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// TODO: RealtimeDB
//		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vrmuploader.firebaseio.com/");
//		FirebaseDatabase.DefaultInstance
//			.GetReference("vrms")
//			.GetValueAsync().ContinueWith(task=>{
//				if(task.IsFaulted){
//					Debug.LogError("失敗");
//				}
//				else if(task.IsCompleted){
//					DataSnapshot snapshot = task.Result;
//					// TODO: ここを現在のuidに変更する
//					string json = snapshot.Child("A8Q3h0UUFIWXpc2PtfKWzYOKmC82").GetRawJsonValue();
//					string url = JsonUtility.FromJson<VrmUrl>(json).url;
//					Debug.Log("Read: "+ url);
//				}
//			});
	}

	// サインイン処理を実行
	public void OnClick() {
		string email = GameObject.FindGameObjectWithTag("Email").GetComponent<Text>().text;
		string pass = GameObject.FindGameObjectWithTag("Pass").GetComponent<Text>().text;
		
		FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, pass).ContinueWith(
			task => {
				if (task.IsCanceled) {
					Debug.LogError("サインインがキャンセルされた");
					return;
				}

				if (task.IsFaulted) {
					Debug.LogError("サインイン失敗: " + task.Exception);
					return;
				}

				// サインイン成功
				FirebaseUser newUser = task.Result;
				Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
				
				SceneManager.LoadSceneAsync("OnlineMain");
			});
	}
}
