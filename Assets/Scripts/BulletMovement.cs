using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public float speed=40.0f;

    float elapsed;
    // Use this for initialization
	void Start () {
        elapsed = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        elapsed += Time.fixedDeltaTime;

        transform.position += transform.forward * speed * Time.fixedDeltaTime;
		if (elapsed>3)
        {
            GameObject.Destroy(gameObject);
        }
     
	}
}
