using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileScript : MonoBehaviour {
    Animator projectile;
	// Use this for initialization
	void Start () {
        projectile = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Floor") {
            projectile.SetInteger("State", 1);
            Invoke("delete", .5f);
        }
        if(collision.gameObject.tag == "Player") {
            Invoke("delete", .125f);
        }

    }

    void delete() {
        Destroy(gameObject);
    }
}
