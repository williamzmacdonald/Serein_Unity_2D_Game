using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class videoStream : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        
        Time.timeScale = 0;
	}
    
    
	
	// Update is called once per frame
	void Update () {

        
    }
    public void unPause()
    {
        Time.timeScale = 1;
    }
}
