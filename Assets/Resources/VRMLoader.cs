using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrmLoader : MonoBehaviour {
	void Start() {
		LoadVrmForWeb();
	}

	// ローカルからVRMを読み込む
	private GameObject LoadVrmForLocal() {
		var path="Assets/Resources/Alicia/AliciaSolid.vrm";
		return VRM.VRMImporter.LoadFromPath(path);
	}

	// WebからVRMを読み込む
	private void LoadVrmForWeb() {
		var path = "https://s3-ap-northeast-1.amazonaws.com/vrm-list/okijo.vrm";

		StartCoroutine(LoadVrmCoroutine(path, go =>
		{
			SetProperties(go);
		}));
	}

	// 読み込んだVRMのプロパティ設定を行う
	private void SetProperties(GameObject go) {
		// 角度を設定して、Playerオブジェクトの子にセット
		var player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<CharacterController>().enabled = true;
		go.transform.parent = player.transform;
		go.transform.rotation = Quaternion.Euler(0, 180, 0);
		
		// アニメーター動的セット
		go.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load ("Character"));
	}
	
	// コルーチンでVRMを読み込む
	IEnumerator LoadVrmCoroutine(string path, System.Action<GameObject> onLoaded) {
		var www = new WWW(path);
		yield return www;
		VRM.VRMImporter.LoadVrmAsync(www.bytes, onLoaded);
	}
}
