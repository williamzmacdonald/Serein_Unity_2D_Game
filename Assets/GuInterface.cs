using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuInterface : MonoBehaviour {

    public Player one;
    public Text text;
	// Use this for initialization
	void Start () {
        text.text = getPlayerTimer().ToString("0");
        
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = one.timeLeft.ToString();
    }
    int getPlayerTimer()
    {
        int tmp = (int)one.timeLeft;
        return tmp; 
    }
}
