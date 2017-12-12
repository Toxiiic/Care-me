using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	飞鱼发生器：
	相同性质：
		都有生成时间，且生成后才能发出来；发了就生成；不能发得太快，鱼走的太慢，会堆在一起
	不同种类：
		1. 生成后先憋着，死了才发，让场上保持n个活着
		2. 生成了就发
*/
public class AirfishSpawn : MonoBehaviour {
	public GameObject airfishGO;
	public float airfishSpeed = .3f;
	public float chargeTime = 3;
	public bool emitAfterDie = false;
	public float upOffset = .07f;

	// private bool _charged = false;
	private float _lastChargedTime = 0;
	private bool _lastDied = true;
	private List<Airfish> _airfishChildren = new List<Airfish>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Time.time - _lastChargedTime >= 3) {
		/* 充满了 */
			if(!emitAfterDie) {
			/* 不用等上一个屎了才发射 */
				_emit();
			} else {
			/* 得等上一个屎了才能发射 */
				if(_lastDied) {
					_emit();
				} else {
				/* 充满了，但上个还没屎，等着 */
				}
			}
		} else {
		/* 没充满 */
		}
	}

	private void _emit() {
		//在一定角度、位置生成
		Quaternion rotation = transform.rotation;
		rotation.eulerAngles = new Vector3(-90, 0, 0);
		Airfish airfish = Object.Instantiate(airfishGO, transform.position + Vector3.up*upOffset, rotation).GetComponent<Airfish>();
		//设置速度
		airfish.speed = airfishSpeed;
		//加入集合
		_airfishChildren.Add(airfish);
		//维护生成器
		_lastDied = false;
		_lastChargedTime = Time.time;
	}

}
