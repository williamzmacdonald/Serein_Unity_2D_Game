using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhisker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("delete", 2f);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void delete() {
        Destroy(gameObject);
    }
}
