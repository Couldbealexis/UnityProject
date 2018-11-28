using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour {

    public GameObject prefab;
    public Transform head;
    public Score scoreScript;

	void Start () {
	}
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(scoreScript.gameStatus == 2){
                SceneManager.LoadScene("HelloVR");
            }else{
                GameObject.Instantiate(prefab, head.position, head.rotation);
            }
        }
	}
}
