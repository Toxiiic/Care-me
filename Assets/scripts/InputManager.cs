using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	// private SceneController sceneController = null;
	private GameObject axiceGO = null;
	private Flatable axiceFlatable = null;

	// Use this for initialization
	void Start () {
		// sceneController = this.GetComponent<SceneController>();
		axiceGO = GameInitializer.axiceGO;
		axiceFlatable = axiceGO.GetComponent<Flatable>();

		//如果是支持触摸的设备
		if(Input.touchSupported) {
			//TODO
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Forward") || Input.GetButtonDown("Backward")) {
			axiceFlatable.beginDown();
		}
		if(Input.GetButton("Forward") || Input.GetButton("Backward")) {
			axiceFlatable.downing();
		}
		if(Input.GetButtonUp("Forward") || Input.GetButtonUp("Backward")) {
			if(Input.GetButtonUp("Forward")) {
				axiceFlatable.endDown(true);
			} else {
				axiceFlatable.endDown(false);
			}
		}
	}
}
