using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protagnist : MonoBehaviour {
	//relive point
	public Vector3 relivePoint {set; get;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("deadly")) {
			Debug.Log("dead!");
			transform.position = new Vector3(0, 0.2f, 0);
		}
	}
}
