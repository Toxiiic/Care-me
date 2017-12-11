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

	按钮刚开始摆放的位置，一定是up的，与初始状态无关，初始状态由代码来改变

	TODO 需要写两个瞬间变化按钮状态（不带动画）的方法，用于初始化按钮状态
 */
public class Button : MonoBehaviour {
	public float moveDuration = .2f;
	//是否可以toggle（false为只可以按下）(如果是，则初始状态只为false)
	public bool toggleSwitch = true;
	//是否会自动弹起
	public bool autoUp = false;
	//初始状态
	public bool initStatusDown = false;

	// public float speed = .1f;
	public float zOffset = .025f;


	public event EventHandler<ButtonEventArgs> buttonChange;

	/* 按钮状态 */
	private bool _status;
	private float _startTime;

	private Vector3 _fromPos;
	private Vector3 _targetPos;
	private Vector3 _bottomPos;
	private Vector3 _upPos;
	private bool _isGoing = false;
	private bool _isGoingDown = true;



	// Use this for initialization
	void Start () {
		//初始放置位置就是按钮的up位置
		_upPos = transform.position;
		//bottom位置很重要，要根据当前位置和方向计算得出
		// forwardInWorld = transform.TransformVector(Vector3.forward);
		_bottomPos = transform.position - transform.forward * zOffset;
		
		startGo(initStatusDown);
	}
	
	// Update is called once per frame
	void Update () {
		if(_isGoing) {
			float linearProportion = (Time.time - _startTime) / moveDuration;
			transform.position = Vector3.Lerp(_fromPos, _targetPos, linearProportion);
			
			/* 下降结束 */ 
			if(transform.position.Equals(_targetPos)) {
				_isGoing = false;
				_triggerListeners();
				_status = _isGoingDown;
			}
		}
	}

	//当被碰到
	void OnTriggerEnter() {
		if(_isGoing) return;
		// Debug.Log("enter");
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
		// Debug.Log("exit");
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
			_fromPos = _upPos;
			_targetPos = _bottomPos;
		} else {
			_fromPos = _bottomPos;
			_targetPos = _upPos;
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
