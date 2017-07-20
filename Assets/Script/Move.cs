using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {
    float force = 10;
    float minForce = 1f, maxForce = 100f;
    float ratio = 0, ratioPerSec = 0.5f;
    bool hitting = false;
    public Scrollbar scbar;
    Image scbarc;
    Vector3 normal = Vector3.down;

    // Use this for initialization
    void Start()
    {
        scbarc = scbar.GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                hitting = true;
            }
        }
        if (Input.GetButton("Fire1") && hitting)
        {
            ratio += ratioPerSec * Time.deltaTime;
            if (ratio >= 1) ratio = 0;
        }
        if (Input.GetButtonUp("Fire1") && hitting)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                force = minForce + maxForce * ratio;
                this.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * force);
            }
            hitting = false;
        }
        if (!hitting) ratio -= ratioPerSec * Time.deltaTime;
        if (ratio > 1) ratio = 0;
        if (ratio < 0) ratio = 0;
        scbarc.color = Color.HSVToRGB(ratio, 1, 1);
        scbar.size = ratio;
    }
}
