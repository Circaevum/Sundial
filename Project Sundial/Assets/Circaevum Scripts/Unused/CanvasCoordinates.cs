using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCoordinates : MonoBehaviour {
    public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z+140f);
        //transform.rotation = new Quaternion(0,player.rotation.y,0,0);
        Debug.Log(transform.position);
	}
}
