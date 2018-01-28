using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    

    public Vector3 point;


    // Use this for initialization
    void Start () 
    {
        
    }
    
    // Update is called once per frame
    void Update () 
    {
		point = this.transform.parent.position;
		point.y = 1f;
        //cameraPos.z = this.transform.parent.position.z - 2;
        //cameraPos.x = this.transform.parent.position.x;
        //cameraPos.y = target.transform.position.y;



        //his.transform.position = cameraPos;
        transform.LookAt(point);
	}
}
