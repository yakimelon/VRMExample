using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrmLoader : MonoBehaviour {
	void Start () {
		var go = LoadVrmForLocal();
		SetProperties(go);
	}

	private GameObject LoadVrmForLocal() {
		var path="Assets/Resources/Alicia/AliciaSolid.vrm";
		return VRM.VRMImporter.LoadFromPath(path);
	}

	private GameObject LoadVrmForWeb() {
		var www = new WWW("https://s3-ap-northeast-1.amazonaws.com/vrm-list/okijo.vrm");
		return VRM.VRMImporter.LoadFromBytes(www.bytes);
	}

	private void SetProperties(GameObject go) {
		// 角度を設定して、Playerオブジェクトの子にセット
		var player = GameObject.FindGameObjectWithTag("Player");
		go.transform.parent = player.transform;
		go.transform.rotation = Quaternion.Euler(0, 180, 0);
		
		// アニメーター動的セット
		go.GetComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Instantiate(Resources.Load ("Character"));
	}
}
