using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
	public bool AutoUp = false;
	public float moveDuration = .2f;

	/* 按钮状态，默认关 */
	private bool _status = false;
	/* Machine是Button的listener */
	private List<ButtonListener> _listenerList = new List<ButtonListener>();
	
	private bool _startGoingDown = false;
	private float _startTime;
	private float _fromY;
	private float _targetY = .175f;
	private Vector3 _newPos;


	// Use this for initialization
	void Start () {
		_fromY = transform.position.y;
		_newPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//按钮下降
		if(_startGoingDown) {
			float linearProportion = (Time.time - _startTime) / moveDuration;
			float newY = Mathf.SmoothStep(_fromY, _targetY, linearProportion);
			// Debug.Log(newY+" "+_fromY+" "+_targetY);
			if(newY != _targetY) {
				_newPos.y = newY;
				transform.position = _newPos;
			} else {/* 下降结束 */
				_startGoingDown = false;
				_triggerListeners();
			}
		}
	}

	//当被碰到，就会变成打开
	void OnCollisionEnter() {
		if(_status == false) {
			_status = !_status;

			//开始下降
			_startGoingDown = true;
			_startTime = Time.time;
		}
	}

	/* 触发监听器 */
	private void _triggerListeners() {
		foreach(ButtonListener listener in _listenerList) {
			listener.onButtonChange(_status);
		}
	}

	public void addButtonListener(ButtonListener listener) {
		_listenerList.Add(listener);
	}
}
