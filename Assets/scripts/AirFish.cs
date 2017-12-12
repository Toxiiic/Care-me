using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airfish : MonoBehaviour {
	// public bool forwardAtBeginning = true;
	public float speed = .3f;
	
	private Vector3 _direction = Vector3.right;

	// Use this for initialization
	void Start () {
		// _direction = forwardAtBeginning ? Vector3.right : Vector3.left;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(_direction * speed * Time.deltaTime);
	}

	// void OnCollisionEnter(Collision collision) {
	// 	foreach (ContactPoint contact in collision.contacts)    //     {
    //         Debug.DrawRay(contact.point, contact.normal, Color.white);
    //     }
	// }

	void OnTriggerEnter(Collider collider) {
		//TODO 如果碰上的是建筑，才触发，当然碰到不同的东西，出发的结果不一样
		if(collider.tag == Tags.building) {
			_direction = -_direction;
		}
	}
}
