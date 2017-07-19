using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour {
    public LayerMask RayCastLayerMask;
    public Rigidbody rb;
    public GameObject sphere;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        sphere = GameObject.Find("Sphere");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.transform.position = sphere.transform.position;
        RaycastHit hitDown;
        if (Physics.BoxCast(transform.position, Vector3.one, Vector3.down,out hitDown,transform.rotation,0.1f))
        {
            Debug.Log(hitDown.collider.name);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hitDown.normal);
        }
    }
}
