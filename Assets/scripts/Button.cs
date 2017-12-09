using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
	高 - 关 - false
	低 - 开 - true

	有三种按钮：
					判断标准			初始状态	
	1.可toggle		toggle真				指定
	2.自动跳起		toggle假且autoUp真		false
	3.起不来一次性	toggle假且autoUp假		false
 */
public class Button : MonoBehaviour {
	public float moveDuration = .2f;
	//是否可以toggle（false为只可以按下）(如果是，则初始状态只为false)
	public bool toggleSwitch = true;
	//是否会自动弹起
	public bool autoUp = false;
	//初始状态
	public bool initStatus = false;


	public event EventHandler<ButtonEventArgs> buttonChange;

	/* 按钮状态，默认关 */
	private bool _status;
	private float _startTime;
	private Vector3 _newPos;

	private float _fromY;
	private float _targetY;
	private float _bottomY = .2f;
	private float _upY = .175f;
	private bool _isGoing = false;
	private bool _isGoingDown = true;


	// Use this for initialization
	void Start () {
		_fromY = transform.position.y;
		_newPos = transform.position;
		startGo(initStatus);
	}
	
	// Update is called once per frame
	void Update () {
		if(_isGoing) {
			float newY;
			float linearProportion = (Time.time - _startTime) / moveDuration;
			
			newY = Mathf.SmoothStep(_fromY, _targetY, linearProportion);

			if(newY != _targetY) {
			/* 下降没结束 */
				_newPos.y = newY;
				transform.position = _newPos;
			} else {
			/* 下降结束 */
				_isGoing = false;
				_triggerListeners();
				_status = _isGoingDown;
			}
		
		}

	}

	//当被碰到
	void OnTriggerEnter() {
		if(_isGoing) return;
		Debug.Log("enter");
		if(toggleSwitch) {
			/* 1.toggle */
			//只要碰，就切换
			toggle();
		} else {
			if(autoUp) {
				/* 2.autoUp */
				//只要碰，肯定下
				startGo(true);
			} else {
				/* 3.justOnce */
				//只有在上面，碰了才下
				if(_status) {
					startGo(true);
				}
			}
		}
		
	}

	void OnTriggerExit(Collider collisionInfo) {
		if(_isGoing) return;
		Debug.Log("exit");
		if(!toggleSwitch) {
			if(autoUp) {
				/* 2.autoUp */
				startGo(false);
			}
		}
	}

	public void toggle() {
		//注意toggle是向现在的反方向
		if(_status) {
			startGo(false);
		} else {
			startGo(true);
		}
	}
	public void startGo(bool goingDown) {
		_isGoing = true;
		_isGoingDown = goingDown;
		_startTime = Time.time;

		if(goingDown) {
			_fromY = _upY;
			_targetY = _bottomY;
		} else {
			_fromY = _bottomY;
			_targetY = _upY;
		}
	}

	/* 触发监听器 */
	private void _triggerListeners() {
		if(buttonChange != null) {
			//这里暂时写死为true，因为暂时接收事件放不需要知道按钮状态是啥
			buttonChange(this, new ButtonEventArgs(true));
		}
	}
}
