﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grivaty : MonoBehaviour {
    Transform tf;
   // Vector3 normal;
	// Use this for initialization
	void Start () {
        
        tf = this.transform;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
       //TO-DO ss
        RaycastHit hit;

        float dis = float.MaxValue;
        //down
        if (Physics.Raycast(this.transform.position, -tf.up, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
        //up
        if (Physics.Raycast(this.transform.position, tf.up, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
        //forward
        if (Physics.Raycast(this.transform.position, tf.forward, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
        //back
        if (Physics.Raycast(this.transform.position, -tf.forward, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
        //left
        if (Physics.Raycast(this.transform.position, -tf.right, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
        //right
        if (Physics.Raycast(this.transform.position, tf.right, out hit, 1000))
        {
            Debug.DrawRay(this.transform.position, hit.normal * 1000.0f, Color.red);
            if (Vector3.Distance(hit.transform.position, this.transform.position) < dis)
                tf.up = hit.normal;
        }
		//set gravity
        Physics.gravity = -tf.up * 9.8f;
        
        Debug.Log("Change G " + Physics.gravity);
        Debug.DrawRay(this.transform.position, Physics.gravity * 1000.0f, Color.green);
		//Camera move
        if (Input.GetMouseButton(1))
        {
            float mousX = Input.GetAxis("Mouse X") * 20;
            //Camera.main.transform.RotateAround(this.transform.position, tf.up, mousX);
			Camera.main.transform.RotateAround(this.transform.position, tf.up, mousX);
        }
        //Camera.main.transform.rotation=
    }

}
