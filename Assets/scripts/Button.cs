using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
	public bool AutoUp = false;

	private bool _status = false;
	private Machine Machines = [];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//当被碰到，就会触发状态变化
	void OnCollisionEnter(Collision collision) {
		_status = !_status;

	}
}
