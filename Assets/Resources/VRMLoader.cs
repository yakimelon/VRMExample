using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMLoader : MonoBehaviour {
	void Start () {
		var path="Assets/Resources/Alicia/AliciaSolid.vrm";
		var go=VRM.VRMImporter.LoadFromPath(path);
		go.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
		go.transform.rotation = Quaternion.Euler(0, 180, 0);
	}
}
