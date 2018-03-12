
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuscript : MonoBehaviour {

    public Player one;
    public GameObject menu;

	// Use this for initialization
	
    public void restart(string restart)
    {
        //one.respawn();
        Application.LoadLevel(Application.loadedLevel);
        this.gameObject.SetActive(false);
      
    }
    public void exit()
    {
        Application.Quit();
    }

}
