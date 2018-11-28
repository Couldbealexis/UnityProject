using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform playerTf;
    Vector3 playerVector;
    
    // Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        playerTf.position += playerVector;
		
	}
    public void moveXin(float x)
    {
        playerVector= new Vector3(x, 0, 0);

    }
    public void moveZin(float z)
    {
        playerVector= new Vector3(0, 0, z);

    }
    public void moveOut()
    {
        playerVector= new Vector3(0, 0, 0);

    }
 }