using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkProjectileScript : MonoBehaviour {
    GameObject player;
    Animator projectile;
    Vector3 Target;
    // Use this for initialization
    void Start() {
        projectile = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        Target = new Vector3(transform.position.x-player.transform.position.x, transform.position.y-player.transform.position.y, transform.position.z);
        
        //Target = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, transform.position - Target, 10 * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        projectile.SetInteger("State", 1);
        Invoke("delete", .5f);

    }

    void delete() {
        Destroy(gameObject);
    }
}
