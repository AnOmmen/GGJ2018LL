using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    GameObject target;

    public Vector3 point;
    public Vector3 cameraPos;

    // Use this for initialization
    void Start () 
    {
        point = target.transform.position;
        cameraPos = this.transform.position;
        transform.LookAt(point);
	}
	
	// Update is called once per frame
	void Update () 
    {
        point = target.transform.position;
        point.y = 0.5f;
        cameraPos.z = target.transform.position.z - 5;
        cameraPos.x = target.transform.position.x;
        //cameraPos.y = target.transform.position.y;


        this.transform.position = cameraPos;
        transform.LookAt(point);
	}
}
