using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
	public Transform target;
    public float smoothTime = 0.3F;
	public Vector3 offset = new Vector3(-2, 2.6f, -6.6f);

    private SceneController sceneController;
    private float yVelocity = 0.0F;
	private Vector3 posWithoutOffset;

	// Use this for initialization
	void Start () {
        target = GameInitializer.axiceGO.transform;
	}
	
    void Update() {
        float newPositionX = Mathf.SmoothStep(posWithoutOffset.x, target.position.x, smoothTime);
        float newPositionY = Mathf.SmoothStep(posWithoutOffset.y, target.position.y, smoothTime);
		posWithoutOffset = new Vector3(newPositionX, newPositionY, 0);
        transform.position = posWithoutOffset + offset;
    }

}
