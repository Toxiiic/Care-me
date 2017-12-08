using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBase : ButtonListener {
	public GameObject lazerGO;
	//如果连接到按钮，就跟下面的状态无关，而是听按钮的话
	// public Button controlButton = null;
	//如果不链接到按钮，就保持这个状态
	public bool staticStatus = true;

	private bool _status = false;
	private GameObject _lazerGO;
	//要保证它的长度正好是1个单位（粗细不用是1个单位）,且那个方向的scale也是1
	private Vector3 _lazerScale;

	// Use this for initialization
	void Start () {
		_lazerGO = Instantiate(lazerGO);
		_lazerScale = _lazerGO.transform.localScale;
		_lazerGO.transform.position = this.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_status) {
			Debug.DrawRay(transform.position, transform.forward);
			RaycastHit hit;
			//射到了
			if(Physics.Raycast(transform.position, transform.forward, out hit)) {
				_lazerScale.z = hit.distance;
			} else { //没射到
				_lazerScale.z = 30;
			}
			_lazerGO.transform.localScale = _lazerScale;
		}
	}

	override public void onButtonChange(bool status) {
		_status = status;
		Debug.Log("status");
	}

	override public void ifNoButton() {
		_status = staticStatus;
	}

}
