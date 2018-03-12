using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : EnemyParent {
    void Start () {
        attackSpeed = 3f;
        idleSpeed = 1f;
        health = 10;
        notHeavy = false;
        startUp();
    }

	void Update () {
        if (!dead) {
            if (chasing == true) {                                                              //Flip Sprite If Wrong             
                if ((transform.position.x - playerObject.transform.position.x > 0 && direction == -1) || (transform.position.x - playerObject.transform.position.x < 0 && direction == 1))
                    flipSprite();

                transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, attackSpeed * Time.deltaTime);

                //Perform Attack If Close Enough
                if (Mathf.Abs(transform.position.x - playerObject.transform.position.x) < 3f && lunge == false) {
                    if (transform.position.x < playerObject.transform.position.x)
                        rigidBody.AddForce(Vector3.right * 150);                                //Slightly Nudge To Help Land HIt
                    else
                        rigidBody.AddForce(Vector3.left * 150);
                    changeState(2);                                                             //Attack Animation
                    rigidBody.AddForce(Vector3.up * 50);

                    lunge = true;
                    Invoke("resetAttack", 3f);
                    Invoke("changeState", 1f);
                }
            }
            else {                                                                              //Move Toward Player 
                transform.position = Vector2.MoveTowards(transform.position, startPosition, idleSpeed * Time.deltaTime);
            }
        }
    }

    private void changeState() {                                                            //Change State Through Here If Needed After x Seconds
        animate.SetInteger("State", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && chasing == false) {                     //Once Player Enters Start Objective, Only Jump If Not Chasing To Prevent It From Jumping Permantley If Player Under
            chasing = true;
            changeState(1);
            idle = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {                                    //Player Out Of Bounds, Stop Chasing Return To Start Position
        if (collision.gameObject.tag == "Player") {
            chasing = false;
            returning = true;
            idle = true;
            changeState(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {                                //If Touch Player Get Knocked Back And Lose Health
        rigidBody.AddForce(Vector3.up * 25);
  
        if (collision.gameObject.tag == "PlayerAttack") {
            if (health == 1) {
                changeState(5);
                Destroy(polygonBody);
                dead = true;
                polygonBody = gameObject.AddComponent<PolygonCollider2D>();

            }
            else {
                changeState(3);
                Invoke("normal", 1f);
            }
            loseHealth();
        }
    }

    private void normal() {                                                            //Change State Through Here If Needed After x Seconds
        changeState(1);
    }
}
