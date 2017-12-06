using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {
	public bool ifFinishLevel = true;
	public string targetSceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject == GameInitializer.axiceGO) {
			Debug.Log("Axice 进来了！");

			if(ifFinishLevel) {
				//应该过一会再触发
				SceneManager.LoadScene(targetSceneName);
				// int a = SceneManager.sceneCount;
			}
		}
	}

}
