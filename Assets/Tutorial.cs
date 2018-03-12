using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {

    public bool tut1b;
    public bool tut2b;
    public bool tut3b;
    public bool tut4b;
    public GameObject tut1;
    public GameObject tut2;
    public GameObject tut3;
    public GameObject tut4;

	// Use this for initialization
    
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void tutorialWindow(int i)
    {

        GameObject tmp = new GameObject();
        switch(i)
        {
            case 1: tmp = tut1;
                tut1b = true;
                break;
            case 2: tmp = tut2 ;
                tut2b = true;
                break;
            case 3: tmp = tut3;
                tut3b = true;
                break;
            case 4: tmp = tut4;
                tut4b = true;
                break;
            default:
                break;

        }

        tmp.SetActive(true);

    }
}
