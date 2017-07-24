using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour {
	public int speed;
	private Transform trans;
	private float posChange;
	// Use this for initialization
	void Start () {
		trans = this.transform;
		posChange = speed * 0.01f;
	}

	// Update is called once per frame
	void Update () {

		Vector3 temp = trans.position;
		temp.y += posChange;
		trans.position = temp;
		Quaternion tempRot = trans.rotation;
		Vector3 tempAngle = tempRot.eulerAngles;
		tempAngle.y += speed;
		tempRot.eulerAngles = tempAngle;
		trans.rotation = tempRot;
		if (trans.position.y < 0.5)
			posChange = speed * 0.01f;
		if (trans.position.y > 1.5)
			posChange = -speed * 0.01f;

	}
}
