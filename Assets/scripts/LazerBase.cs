using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBase : MonoBehaviour {
	public GameObject lazerGO;
	//初始状态
	public bool iniStatusOn = false;
	//如果连接到按钮，就跟下面的状态无关，而是听按钮的话
	public Button controlButton = null;

	private bool _status = false;
	private GameObject _lazerGO;
	//要保证它的长度正好是1个单位（粗细不用是1个单位）,且那个方向的scale也是1
	private Vector3 _lazerScale;
	private int _raycastLayerMask;

	// Use this for initialization
	void Start () {
		/* 需要初始化的变量-否则根本不对 */
		_lazerGO = Instantiate(lazerGO, transform);
		_lazerScale = _lazerGO.transform.localScale;
		// _lazerGO.transform.position = this.transform.position;
		_lazerGO.transform.localPosition = Vector3.zero;
		_lazerGO.transform.localRotation = Quaternion.identity;
		_raycastLayerMask = LayerMask.GetMask("Default");
		
		//初始状态
		if(iniStatusOn) {
			turnOn();
		} else {
			turnOff();
		}
		/* 如果有按钮，听按钮的 */
		if(controlButton != null) {
			controlButton.buttonChange += onButtonChange;
		}

		Debug.Log("fsdf "+LayerMask.GetMask("Default"));
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward);
		if(_status) {
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, 30, _raycastLayerMask)) {
			/* 射到了 */
				_lazerScale.z = hit.distance;
				// Debug.Log("fsaf "+_lazerScale+" "+transform.gameObject.name+""+hit.transform.gameObject.name);
			} else {
			/* 啥都没射到 */
				_lazerScale.z = 30;
			}
			_lazerGO.transform.localScale = _lazerScale;
		}
	}

	public void onButtonChange(System.Object sender, ButtonEventArgs e) {
		//因为已经保持按钮状态和lazer状态一一对应，所以只需要切换即可，可以不管按钮是什么状态。
		toggleStatus();
	}

	public void turnOn() {
		_status = true;
	}
	public void turnOff() {
		_status = false;
		_lazerScale.z = 0;
		_lazerGO.transform.localScale = _lazerScale;
	}
	public void toggleStatus() {
		if(_status) {
			turnOff();
		} else {
			turnOn();
		}
	}

}
