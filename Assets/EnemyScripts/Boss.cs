using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyParent {
    GameObject Lightning;
    bool LightShow = false;
    bool Active = true;
    bool WhiskerTime = true;
    bool LightningTime = true;
    int rand;
    public GameObject menu;

	// Use this for initialization
	void Start () {
        attackSpeed = 6f;
        idleSpeed = 1f;
        health = 40;
        notHeavy = false;
        startUp();

        Lightning = (GameObject)Instantiate(Resources.Load("BossLight"));
        Lightning.SetActive(false);

        Active = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (dead == true)
            menu.SetActive(true);
        if (Active) {
            AttackControl();
            Active = false;

           Invoke("summonSummon", 5);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {                 //Once Player Enters Start Objective, Only Jump If Not Chasing To Prevent It From Jumping Permantley If Player Under
            Active = true;
        }
    }

    void AttackControl() {
        rand = Random.Range(0, 2);

        if (rand == 0)
            summonPink();
        else if (rand == 1)
            summonLightning();


        Invoke("AttackControl", 2f);
    }



    private void OnCollisionEnter2D(Collision2D collision) {                            //If Touch Player Get Knocked Back And Lose Health
        if (collision.gameObject.tag == "PlayerAttack") {
            if (health == 1) {
                changeState(3);
                Destroy(polygonBody);
                dead = true;

           
                Destroy(this);

            }
            else {
                changeState(1);
                //summonLightning();
                //bossSummon();
                //summonPink();
                health -= 1;
                //Invoke("normal", 1);

            }
            loseHealth();
        }
    }

    private void summonPink() {
        GameObject Blah2 = (GameObject)Instantiate(Resources.Load("PinkProjectile"));
        Blah2.transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z);

    }
    private void bossSummon() {
        changeState(2);
        Invoke("summonSummon", 1f);
        Invoke("normal", 3);

        
    }

    private void summonSummon() {
        GameObject Whisker = (GameObject)Instantiate(Resources.Load("Summon"));
        Whisker.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
        Invoke("summonSummon", 15);
    }

    private void summonLightning() {
        LightShow = true;
        Invoke("LightShowSwitch", 1f);
        Lightning.SetActive(true);
        Lightning.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        GameObject Blah = (GameObject)Instantiate(Resources.Load("Boss Projectile"));
        Blah.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y+10, playerObject.transform.position.z);

        lightControl();
        Invoke("normal", 1);
    }

    private void lightControl() {
        if (LightShow) {
            Invoke("lightControl", .1f);
            Lightning.SetActive(!Lightning.activeSelf);
        }
    }

    private void LightShowSwitch() {
        LightShow = false;
        Lightning.SetActive(false);
    }
    private void normal() {                                                            //Change State Through Here If Needed After x Seconds
        changeState(0);
    }
}
