using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAudio : MonoBehaviour {
    private Rigidbody mover;
    private AudioSource noise;
    private AudioSource noise2;
    // Use this for initialization
    void Start () {
        mover = GameObject.Find("Y-Origin").GetComponent<Rigidbody>();
        noise = GameObject.Find("Moving Audio Source").GetComponent<AudioSource>();
        noise2 = GameObject.Find("Moving Audio Source (1)").GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        noise.volume = mover.velocity.magnitude/3;
        noise2.volume = mover.velocity.magnitude / 10;
    }
}
