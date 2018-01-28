using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    float move_speed;

    [SerializeField]
    float rotation_speed;

    Vector3 translation;

    public Transform cameraTrans;

    float degrees;

    public UnityEngine.Events.UnityEvent on_player_move;

    UnityEngine.Events.UnityAction<WordObjectController> on_word_object_mouse_down;

    public bool interacting;


	

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && !interacting)
        {
			print("Entering Conversation");
            interacting = true;
            this.GetComponent<Bounce>().currentMode = Bounce.MODE.OFF;
        }
        else if (Input.GetKeyDown(KeyCode.F) && interacting)
        {
            print("Leaving Conversation");
            interacting = false;
            GetComponent<Bounce>().currentMode = Bounce.MODE.FREEWALK;
        }
        other.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void OnTriggerExit(Collider other)
	{
		other.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	} 

    void Start()
    {
        cameraTrans = GameObject.Find("Camera Focal").transform;

        interacting = false;

        translation = Vector3.zero;

        on_word_object_mouse_down = new UnityEngine.Events.UnityAction<WordObjectController>(OnWordObjectMouseDown);
        GameObject[] word_objects = GameObject.FindGameObjectsWithTag("WordObject");
        foreach (GameObject word_object in word_objects)
        {
            word_object.GetComponent<WordObjectController>().on_mouse_down.AddListener(on_word_object_mouse_down);
        }

    }

	
	void Update () {

        if (Input.anyKey && !interacting)
        {
            ProcessKeys();
        }
        
        if (!translation.Equals(Vector3.zero))
        {
            this.transform.Translate(translation.normalized * Time.deltaTime * move_speed, Space.World);
            cameraTrans.transform.Translate(translation.normalized * Time.deltaTime * move_speed, Space.World);
            translation = Vector3.zero;
            OnPlayerMove();
        }
        
        if (degrees != 0f)
        {
            if (degrees > 0f)
            {
                PositiveRotation();
            }
            else
            {
                NegativeRotation();
            }
            OnPlayerMove();
        }
        
    }

    void ProcessKeys()
    {

        if (Input.GetKey(KeyCode.A)) { StrafeLeft(); }
        if (Input.GetKey(KeyCode.D)) { StrafeRight(); }
        if (Input.GetKey(KeyCode.W)) { MoveForward(); }
        if (Input.GetKey(KeyCode.S)) { MoveBackward(); }
        if (Input.GetKeyDown(KeyCode.Q)) { RotateLeft(); }
        if (Input.GetKeyDown(KeyCode.E)) { RotateRight(); }

    }

    void MoveForward()
    {
        translation += this.transform.forward;
    }

    void MoveBackward()
    {
        translation += this.transform.forward * -1f;
    }

    void StrafeLeft()
    {
        translation += this.transform.right * -1f;
    }

    void StrafeRight()
    {
        translation += this.transform.right;
    }

    void RotateLeft()
    {
        degrees -= 45f;
        UnwrapRotation();
    }

    void RotateRight()
    {
        degrees += 45f;
        UnwrapRotation();
    }

    void UnwrapRotation()
    {
        while (degrees >= 360f || degrees <= -360f)
        {
            if (degrees >= 360f)
            {
                degrees -= 360f;
            }
            else if (degrees <= -360f)
            {
                degrees += 360f;
            }
        }
    }

    void NegativeRotation()
    {
        float rotation_degrees = rotation_speed * Time.deltaTime * -1f;
        if (degrees - rotation_degrees > 0f)
        {
            rotation_degrees = degrees;
            degrees = 0f;
        }
        else
        {
            degrees -= rotation_degrees;
        }
        this.transform.Rotate(this.transform.up, rotation_degrees);
        cameraTrans.transform.Rotate(this.transform.up, rotation_degrees);
    }

    void PositiveRotation()
    {
        float rotation_degrees = rotation_speed * Time.deltaTime;
        if (degrees - rotation_degrees < 0f)
        {
            rotation_degrees = degrees;
            degrees = 0f;
        }
        else
        {
            degrees -= rotation_degrees;
        }
        this.transform.Rotate(this.transform.up, rotation_degrees);
        cameraTrans.transform.Rotate(this.transform.up, rotation_degrees);
    }

    void OnPlayerMove()
    {
        on_player_move.Invoke();
    }

    void OnWordObjectMouseDown(WordObjectController word_object_controller)
    {
        
    }

}
