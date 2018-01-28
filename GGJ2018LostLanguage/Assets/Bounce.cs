using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public float timeStamp;
    public bool inputChanged;
    public float facing = 180.0f;
    public enum MODE
    {
        FREEWALK,
        ON,
        OFF
    }
	public MODE currentMode;



    void Start () 
    {
        timeStamp = 1000.0f;
        currentMode = MODE.FREEWALK;
	}


    // Update is called once per frame
    void Update () 
    {

		Vector3 position = this.transform.position;
        
        if ((Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) && (currentMode == MODE.FREEWALK || currentMode == MODE.ON) )
        {
            inputChanged = true;
        }

        if (this.transform.position.y < 0.55f && inputChanged)
        {
            position.y = 1.5f;
            inputChanged = false;
        }
        else
        {
            position.y = 0.5f;
            inputChanged = false;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, position, 0.05f);
		
    }
}
