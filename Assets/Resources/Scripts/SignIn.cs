using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;

public class SignIn : MonoBehaviour {

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

		// TODO: Auth
		FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync("b@example.com", "testtest").ContinueWith(
			task => {
				if (task.IsCanceled) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
					return;
				}

				if (task.IsFaulted) {
					Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
					return;
				}

				// Firebase user has been created.
				Firebase.Auth.FirebaseUser newUser = task.Result;
				Debug.LogFormat("Firebase user created successfully: {0} ({1})",
					newUser.DisplayName, newUser.UserId);
			});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
