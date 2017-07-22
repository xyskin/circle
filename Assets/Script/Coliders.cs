using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coliders : MonoBehaviour {

    public Slider health;
	// Use this for initialization
	void Start () {
        health.value = 100;
	}
	
	// Update is called once per frame
	void Update () {
        health.value -= Time.deltaTime;
        health.GetComponentsInChildren<Image>()[1].color = new Color(Time.deltaTime/100,health.value,0,1);
        if (health.value == 0)
            Time.timeScale = 0;
        
    }

    void OnTriggerEnter(Collider o)
    {
        if (o.tag != "Platform")
        {
            Destroy(o.gameObject, 0);
            health.value += 5;
        }
    }
}
