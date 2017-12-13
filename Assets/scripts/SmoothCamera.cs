using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
	public Transform target;
    public float smoothTime = 0.3F;
	public Vector3 offset = new Vector3(-2, 2.6f, -6.6f);

    //追踪目标缓动后的位置
	private Vector3 smoothTargetPos;

	// Use this for initialization
	void Start () {
        target = GameInitializer.axiceGO.transform;
	}
	
    void Update() {
        float newPositionX = Mathf.SmoothStep(smoothTargetPos.x, target.position.x, smoothTime);
        float newPositionY = Mathf.SmoothStep(smoothTargetPos.y, target.position.y, smoothTime);
		smoothTargetPos = new Vector3(newPositionX, newPositionY, 0);
        transform.position = smoothTargetPos + offset;
        GameInitializer.sceneFacilitiesGO.GetComponent<TextMesh>().text = newPositionX+" "+smoothTargetPos.x+" "+transform.position.x;
    }

}
