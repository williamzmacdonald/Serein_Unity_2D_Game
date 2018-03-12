using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WolfEnemy : EnemyParent {
    private void Start() {
        attackSpeed = 6f;
        idleSpeed = 1f;
        health = 3;
        notHeavy = true;
        startUp();
    }
    void Update() {
        if (!dead) {
            if (chasing && knockBack == false) {                                            //Move Toward Player While Chasing Him Unless Knocked Back Recently
                                                                                            //If Looking Wronge Direction Flip
                if ((transform.position.x - playerObject.transform.position.x > 0 && direction == 1) || (transform.position.x - playerObject.transform.position.x < 0 && direction == -1))
                    flipSprite();

                transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, attackSpeed * Time.deltaTime);

                                                                                            //If Vertical Distance Between Player And Wolf Too Big Jump, Such As If On Platform
                if (playerObject.transform.position.y - transform.position.y > 2f && jump == false && rigidBody.velocity.y < 10) {
                    rigidBody.AddForce(Vector3.up * 300);
                    jump = true;
                    Invoke("resetJump", 3f);
                }                                                                           //If Wolf Gets Close Enough Lunge forward
                if (Mathf.Abs(transform.position.x - playerObject.transform.position.x) < 3f && lunge == false) {
                    if (transform.position.x < playerObject.transform.position.x)
                        rigidBody.AddForce(Vector3.right * 300);
                    else
                        rigidBody.AddForce(Vector3.left * 300);
                    rigidBody.AddForce(Vector3.up * 150);
                    lunge = true;
                    Invoke("normal", 1f);
                    changeState(1);
                    Invoke("resetLunge", 3f);
                }
            }
            else if (returning && knockBack == false) {                                     //If Lost Player Return To Spawn Zone
                transform.position = Vector2.MoveTowards(transform.position, startPosition, attackSpeed * Time.deltaTime);
                if (transform.position == startPosition) {
                    returning = false;
                    idle = true;
                }
            }
            else if (idle) {                                                                //If Idle Just Move In Bubble
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + idleSpeed, transform.position.y), Mathf.Abs(idleSpeed) * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player" && chasing == false) {                 //Once Player Enters Start Objective, Only Jump If Not Chasing To Prevent It From Jumping Permantley If Player Under
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 5);
            chasing = true;
            Invoke("resetJump", 3f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {                                //Player Out Of Bounds, Stop Chasing Return To Start Position
        if (collision.gameObject.tag == "Player") {
            knockBack = false;
            chasing = false;
            returning = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {                            //If Touch Player Get Knocked Back And Lose Health
        if (collision.gameObject.tag == "PlayerAttack") {
            if (health == 1) {
                changeState(3);
                Destroy(polygonBody);
                dead = true;
                polygonBody = gameObject.AddComponent<PolygonCollider2D>();

            }
            else {
                changeState(2);
                Invoke("normal", 1f);
            }
            loseHealth();
        }
        if(collision.gameObject.tag == "Player")
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)collision.transform.position;
            direction = direction.normalized;
            rigidBody.AddForce(direction * 5, ForceMode2D.Impulse);
            knockBack = true;
            Invoke("resumeChase", 1f);                                                    //After Knocked Back Return To Chasing

        }
    }
    private void normal() {                                                            //Change State Through Here If Needed After x Seconds
        changeState(0);
    }

}
