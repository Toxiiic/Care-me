using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonListener : MonoBehaviour {
	public Button controlButton;

	// Use this for initialization
	void Start () {
		/* 如果没连接按钮就保持指定状态，链接了按钮就听话 */
		if(controlButton == null) {
			ifNoButton();
		} else {
			controlButton.addButtonListener(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//必须重写这俩！
	virtual public void onButtonChange(bool status) {
		Debug.Log("triggered");
	}
	virtual public void ifNoButton() {}
	
}
