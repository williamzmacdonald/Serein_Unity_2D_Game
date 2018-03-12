using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blink : MonoBehaviour {
    Rigidbody2D playerBody;
    Player p;
    Transform ptransform;
    Transform lw;
    Transform rw;
    GameObject wp;
    GameObject wd;
    public Transform warp;
    float timePassed;
    bool startTimer;
    public bool itemFound;
    // Use this for initialization
    void Start () {
        timePassed = 0;
        startTimer = false;
        playerBody = GetComponentInParent<Rigidbody2D>();
        p = GameObject.Find("Player").GetComponent<Player>();
        rw = GameObject.Find("RightWall").GetComponent<Transform>();
        lw = GameObject.Find("LeftWall").GetComponent<Transform>();
        ptransform = GetComponentInParent<Transform>();
        wp = GameObject.Find("WarpPlayer");
        wd = GameObject.Find("WarpDestination");
        if (SceneManager.GetActiveScene().name != "game")
        {
            itemFound = true;
        }
        else
            itemFound = false;
    }

    // Update is called once per frame
    void Update () {
        Vector3 w = warp.position;
        if (Input.GetKeyDown(KeyCode.Alpha3) && p.mana > 30 && startTimer == false && itemFound)
        {
            startTimer = true;
            p.mana = p.mana - 50;
            wp.GetComponent<Animator>().SetTrigger("Warp");
            wp.GetComponent<Transform>().position = GetComponentInParent<Transform>().position;
            wd.GetComponent<Animator>().SetTrigger("Warp");
            wd.GetComponent<Transform>().position = w;
        }
        if (timePassed > .1 && startTimer == true)
        {
            if (ptransform.localScale.x > 0)
            {
                if(w.x > rw.position.x)
                    playerBody.MovePosition(new Vector2(lw.position.x - 1, ptransform.position.y));
                else
                    playerBody.MovePosition((Vector2)w);
            }
            else
            {
                if (w.x - 10 < lw.position.x)
                    playerBody.MovePosition(new Vector2(lw.position.x + 1, ptransform.position.y));
                else
                    playerBody.MovePosition((Vector2)w);
            }
            startTimer = false;
            timePassed = 0;
        }
        else if(startTimer)
        {
            timePassed = timePassed + Time.deltaTime;
        }
    }
}
