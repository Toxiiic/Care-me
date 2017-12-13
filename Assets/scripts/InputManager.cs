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
		

		if(Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);
			switch(touch.phase) {
				case TouchPhase.Began:
					axiceFlatable.beginDown();
					break;
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					axiceFlatable.downing();
					break;
				case TouchPhase.Ended:
					bool isForward = touch.position.x > Screen.width/2;
					axiceFlatable.endDown(isForward);
					Debug.Log("touch up");
					break;
			}
		} else {
			if(Input.GetButtonDown("Forward") || Input.GetButtonDown("Backward")) {
				axiceFlatable.beginDown();
			}
			if(Input.GetButton("Forward") || Input.GetButton("Backward")) {
				axiceFlatable.downing();
			}
			if(Input.GetButtonUp("Forward")) {
				axiceFlatable.endDown(true);
				Debug.Log("button Forward");
			} else if(Input.GetButtonUp("Backward")) {
				axiceFlatable.endDown(false);
				Debug.Log("button Backward");
			}
		}

	}


}
