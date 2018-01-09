using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {


	public float zMax,zMin, yMax,yMin,cameraMoveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3(Mathf.Clamp(transform.position.x, 10,40),transform.position.y,Mathf.Clamp(transform.position.z, 10,40));

		float xTranslation = Input.GetAxis("Horizontal") *cameraMoveSpeed;
		float zTranslation = Input.GetAxis("Vertical") * cameraMoveSpeed;

		xTranslation *= Time.deltaTime;
		zTranslation *= Time.deltaTime;

		transform.Translate(xTranslation,zTranslation,0);
	}
}
