using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehavior : MonoBehaviour {

    Transform player_camera_transform;

    UnityEngine.Events.UnityAction on_player_move;

    // Use this for initialization
    void Start () {
        player_camera_transform = GameObject.Find("Player").transform.Find("Camera");
        on_player_move = new UnityEngine.Events.UnityAction(OnPlayerMove);
        player_camera_transform.GetComponentInParent<PlayerController>().on_player_move.AddListener(on_player_move);
    }

    void OnPlayerMove()
    {
        this.transform.LookAt(player_camera_transform);
    }

}
