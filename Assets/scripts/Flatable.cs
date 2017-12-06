using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatable : MonoBehaviour {
	public float thrust = 1;
	//回弹
	public float resetDuration = 5.0F;
	public Transform flatableTf = null;
	//水平方向前进的比例
	public float horiScale = 0.2f;

	private Rigidbody rb;

	private float startTime = 0;
	private float durationNow = 0;
	private float horizontalScale = 1;
	private float verticalScale = 1;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		Debug.Log("fsdafsfsdf"+rb);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	必须按照以下顺序调用函数，才可以实现人物跳跃：

	 */
	public void beginDown () {
		rb.isKinematic = true;
		startTime = Time.time;
	}
	public void downing () {
		durationNow = Time.time - startTime;
		horizontalScale = (Mathf.Sqrt(durationNow) + 1);
		verticalScale = Mathf.Pow(10, -durationNow);
		flatableTf.localScale = new Vector3(horizontalScale, verticalScale, horizontalScale);
	}
	/*
		参数为是否是向前
	*/
	public void endDown (bool isForward) {
		Debug.Log("up~~");

		Vector3 horiVector = isForward ? Vector3.right : Vector3.left;
		rb.isKinematic = false;
		rb.AddForce((Vector3.up + horiVector*horiScale) * thrust * durationNow, ForceMode.Impulse);

		flatableTf.localScale = Vector3.one;
	}
	

}
