using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotatableTarget : MonoBehaviour {
	public Transform target;
    public float smoothTime = 0.3F;
	public Vector3 offset = new Vector3(.1f, 0, 0);

	// public float angle = 0;
	private Vector3 smoothTargetPos;

	// Use this for initialization
	void Start () {
		target = GameInitializer.axiceGO.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float newPositionX = Mathf.SmoothStep(transform.position.x, target.position.x, smoothTime);
        float newPositionY = Mathf.SmoothStep(transform.position.y, target.position.y, smoothTime);
		smoothTargetPos = new Vector3(newPositionX, newPositionY, 0);
        transform.position = smoothTargetPos + offset;
	}
}
