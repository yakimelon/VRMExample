using System.Collections;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class VrmLoader : MonoBehaviour {
	private string _userId;
	
	void Start() {
		_userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		LoadVrmForWeb();
	}

	// ローカルからVRMを読み込む
	private GameObject LoadVrmForLocal() {
		var path="Assets/Resources/Alicia/AliciaSolid.vrm";
		return VRM.VRMImporter.LoadFromPath(path);
	}

	// FirebaseからVRMを読み込む
	private void LoadVrmForWeb() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://vrmuploader.firebaseio.com/");
		FirebaseDatabase.DefaultInstance
			.GetReference("vrms")
			.GetValueAsync().ContinueWith(task=>{
				if(task.IsFaulted){
					Debug.LogError("読み込み失敗");
				}
				else if(task.IsCompleted){
					DataSnapshot snapshot = task.Result;

					string json = snapshot.Child(_userId).GetRawJsonValue();
					string url = JsonUtility.FromJson<VrmUrl>(json).url;
					Debug.Log("Read: "+ url);
					
					StartCoroutine(LoadVrmCoroutine(url, go =>
					{
						SetProperties(go);
					}));
				}
			});
	}

	// 読み込んだVRMのプロパティ設定を行う
	private void SetProperties(GameObject go) {
		// 角度を設定して、Playerオブジェクトの子にセット
		go.transform.parent = transform;
		go.transform.position = transform.position;
		go.transform.rotation = transform.rotation;
		
		// アニメーター動的セットし、CharacterControllerを有効化
		GetComponent<Animator>().avatar = Instantiate(go.GetComponent<Animator>().avatar);
		GetComponent<CharacterController>().enabled = true;
	}
	
	// コルーチンでVRMを読み込む
	IEnumerator LoadVrmCoroutine(string path, System.Action<GameObject> onLoaded) {
		var www = new WWW(path);
		yield return www;
		VRM.VRMImporter.LoadVrmAsync(www.bytes, onLoaded);
	}
}
