using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagnist : MonoBehaviour {
	public float reviveVerticalOffset = .5f;

	//最新经过的复活点【最新复活点】
	private Vector3 _latestRevivePoint;

	// Use this for initialization
	void Start () {
		//找到最左侧的revivePoint，作为最新复活点
		_latestRevivePoint = _findFirstRevivePoint();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("deadly")) {
			_die();
		}
		if(other.CompareTag("revivePoint")) {
			//当这个复活点比最新复活点靠右时，让他成为最新复活点，否则不做
			if(other.transform.position.x > _latestRevivePoint.x) {
				_latestRevivePoint = other.transform.position + Vector3.up * reviveVerticalOffset;
				//TODO 复活点动画
			}
		}
	}

	void OnCollisionEnter(Collision collisionInfo) {
		if(collisionInfo.gameObject.CompareTag("deadly")) {
			_die();
		}
	}

	private Vector3 _findFirstRevivePoint() {
		GameObject[] revivePointGOs = GameObject.FindGameObjectsWithTag("revivePoint");
		
		Vector3 mostLeftVector = Vector3.right * 10000;
		foreach(GameObject iGO in revivePointGOs) {
			if(iGO.transform.position.x < mostLeftVector.x) {
				mostLeftVector = iGO.transform.position;
			}
		}
		Debug.Log(mostLeftVector);
		return mostLeftVector;
	}

	private void _die() {
		Debug.Log("dead!");
		transform.position = _latestRevivePoint;
	}
}
