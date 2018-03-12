using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour {
    public float attackSpeed;                                                           //Speed When Chasing
    public float idleSpeed;                                                             //Speed When Idle
    public int health;                                                                  //Amount Of Hits To Destroy
    public int direction;

    public Rigidbody2D rigidBody;                                                       //Objects
    public PolygonCollider2D polygonBody;
    public CircleCollider2D playerDetect;
    public Vector3 startPosition;
    public GameObject playerObject;
    public Animator animate;
    public SpriteRenderer sp;

    public bool chasing;                                                                //Variables To Influence Pattern
    public bool returning;
    public bool knockBack;
    public bool idle;
    public bool jump;
    public bool lunge;
    public bool notHeavy;
    public bool dead;

    public void startUp() {
        GameObject mySprite = this.gameObject;                                          //Get Object Components
        animate = GetComponent<Animator>();
        rigidBody = mySprite.GetComponent<Rigidbody2D>();
        polygonBody = mySprite.GetComponent<PolygonCollider2D>();
        playerDetect = mySprite.GetComponent<CircleCollider2D>();
        playerObject = GameObject.FindWithTag("Player");
        sp = mySprite.GetComponent<SpriteRenderer>();
        Invoke("setSpawn", 2.5f);                                                       //After 2.5 Seconds Set Home Location 2.5 Seconds Incase Spawned In Air To Allow It To Land
        direction = 1;

        chasing = returning = knockBack = idle = jump = lunge = dead = false;                  
        Physics2D.IgnoreLayerCollision(0, 0, true);                                     //Prevent Enemies From Colliding, Mostly Due To Golem Being Huge
    }
    public void loseHealth() {                                                          //Lose Health, If No More Health Delete
        health -= 1;

        if (notHeavy) {                                                                 //Knockbaced If Type Not Heavy Such As Wolf 
            knockBack = true;
            Invoke("resumeChase", 1.5f);                                                //After Knocked Back Return To Chasing If Not Heavy
        }

        if (health <= 0)                                                                //If Dead Delay Destruction To Display Death Sprite
            Invoke("destroyObject", 1.5f);
    }

    public void flipSprite() {                                                          //Flip Sprite Apperance
        sp.flipX = !sp.flipX;
        direction *= -1;
    }

    public void changeState(int X) {                                                    //Change Animation State
        animate.SetInteger("State", X);
    }

    public void destroyObject() {                                                       //Destroy After Time
        Destroy(this.gameObject);
    }

    public void resumeChase() {                                                         //Return To Chasing
        chasing = true;
        knockBack = false;
        idle = false;
    }

    public void setSpawn() {                                                            //Set Current Position To Spawn Location And Start Moving
        startPosition = transform.position;
        idle = true;
    }

    public void reDirect() {                                                            //Move In Opposite Direction
        idleSpeed *= -1;
    }

    public void resetJump() {                                                           //Reset Jump Ability
        jump = false;
    }

    public void resetAttack() {                                                         //Reset Special Attack
        lunge = false;
    }
}
