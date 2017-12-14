using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	// private SceneController sceneController = null;
	private GameObject _axiceGO = null;
	private Flatable _axiceFlatable = null;
	private int _curFingerId = 0;
	private bool _touching = false;
	private int _touchingSection = -1;
	private SmoothRotatableTarget _sro;

	public static float ScreenSectionWidth = 0;
	public const int SECTION_LEFT = 0;
	public const int SECTION_CENTRE = 1;
	public const int SECTION_RIGHT = 2;

	// Use this for initialization
	void Start () {
		// sceneController = this.GetComponent<SceneController>();
		_axiceGO = GameInitializer.axiceGO;
		_sro = GameInitializer.sroTF.GetComponent<SmoothRotatableTarget>();
		_axiceFlatable = _axiceGO.GetComponent<Flatable>();
		ScreenSectionWidth = Screen.width/3;

		//如果是支持触摸的设备
		if(Input.touchSupported) {
			//TODO
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//能省不？
		if(Input.touchCount > 0) {

			/* 这一段保证touch是之前新触摸的同一根手指 */
			//怎么给struct初始化？
			Touch touch = Input.GetTouch(0);
			if(!_touching) {
			/* 新触摸 */
				touch = Input.GetTouch(0);
				_curFingerId = touch.fingerId;
				_touchingSection = getSection(touch);
				_touching = true;
			} else {
			/* 有一根手指正在触摸 */
				//找到新触摸
				foreach(Touch iTouch in Input.touches) {
					if(iTouch.fingerId == _curFingerId) {
						touch = iTouch;
						break;
					}
				}
			}
			
			/* 注意began不一定是新触摸，因为可能新触摸的同时又放上了第二根手指 */
			switch(touch.phase) {
				case TouchPhase.Began:
					if(_touchingSection == SECTION_CENTRE) {
						_sro.beginRotating();

					} else {
						_axiceFlatable.beginDown();
					}
					break;

				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					if(_touchingSection == SECTION_CENTRE) {
						_sro.rotating(touch.deltaPosition);
					} else {
						_axiceFlatable.downing();
					}
					break;

				case TouchPhase.Ended:
					if(_touchingSection == SECTION_CENTRE) {
						_sro.endRotating();
					} else {
						bool isForward = _touchingSection == SECTION_RIGHT;
						_axiceFlatable.endDown(isForward);
					}
					_touching = false;
					break;
			}
		} else {
			if(Input.GetButtonDown("Forward") || Input.GetButtonDown("Backward")) {
				_axiceFlatable.beginDown();
			}
			if(Input.GetButton("Forward") || Input.GetButton("Backward")) {
				_axiceFlatable.downing();
			}
			if(Input.GetButtonUp("Forward")) {
				_axiceFlatable.endDown(true);
			} else if(Input.GetButtonUp("Backward")) {
				_axiceFlatable.endDown(false);
			}
		}

	}

	/* 得到触摸点在屏幕x轴的哪个区域，三等分，返回值为0，1，2，代表从左向右 */
	private int getSection(Touch touch) {
		return (int)(touch.position.x / ScreenSectionWidth);
	}


}
