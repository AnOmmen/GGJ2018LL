using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharScript : MonoBehaviour {

    public float timeStamp;
    public bool inputChanged;
    public float facing = 180.0f;

    /*This is attacted to the player sprite
        set player properties in inspector window:

        Transform:
            Position: y = 0.5
            Scale: x = 5, y = 5

        SpriteRenderer:
            Sprite: Knob (just for testing)
            
    */
    // Use this for initialization
    void Start () 
    {
        timeStamp = 1000.0f;
	}
	
	// Update is called once per frame
    void Update () 
    {
        if (Input.inputString != "") { Debug.Log(Input.inputString); }

		Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        
        if (Input.GetKey(KeyCode.W))
        {
           // print('W');

            position.z += 0.5f;

            if (this.transform.rotation.y < 0.9999f)
            {
                rotation *= Quaternion.Euler(0, 60, 0);
                if (this.transform.rotation.y > 0.5f)
                {
                    /*Pretty sure we can change this to a different sprite instead of just a color change*/
                    this.GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }

            inputChanged = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
          //  print('A');
           // Vector3 position = this.transform.position;

            position.x -= 0.5f;

            inputChanged = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
           // print('S');
            //Vector3 position = this.transform.position;

            position.z -= 0.5f;
            if (this.transform.rotation.y > 0.05f)
            {
                rotation *= Quaternion.Euler(0, -60, 0);
                if (this.transform.rotation.y < 0.5f)
                {
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
           

            inputChanged = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
           // print('D');
            //Vector3 position = this.transform.position;

            position.x += 0.5f;

            inputChanged = true;
        }

        if (this.transform.position.y < 0.55f && inputChanged)
        {
            position.y = 2.0f;
            inputChanged = false;
        }
        else
        {
            position.y = 0.5f;
            inputChanged = false;
        }

        this.transform.position = Vector3.Lerp(this.transform.position, position, 0.1f);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, 0.2f);
		
    }
}
