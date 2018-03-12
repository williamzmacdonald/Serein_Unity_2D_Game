using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShield : MonoBehaviour {
    private Player p;
    private Renderer shieldRend;
    public bool itemFound;
    // Use this for initialization
    void Start () {
        p = GameObject.Find("Player").GetComponent<Player>();
        shieldRend = gameObject.GetComponent<Renderer>();
        shieldRend.enabled = false;
        if(SceneManager.GetActiveScene().name != "game")
        {
            itemFound = true;
        }
        else
            itemFound = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha2) && p.mana > 15 && itemFound)
        {
            if (p.mana > 15 && p.shield == false)
            {
                shieldRend.enabled = true;
                p.shield = true;
            }
            else
            {
                RemoveShield();
            }
        }

    }
    public void RemoveShield()
    {
        p.shield = false;
        shieldRend.enabled = false;
    }
}
