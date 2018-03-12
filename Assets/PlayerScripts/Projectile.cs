using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Projectile : MonoBehaviour {
    public Rigidbody2D ProjectilePrefab;
    public GameObject projectileSpawn;
    float timePassed;
    bool startTimer;
    public bool itemFound;
    Animator _animator;
    private Player p;
    // Use this for initialization
    void Start ()
    {
        if (SceneManager.GetActiveScene().name != "game")
        {
            itemFound = true;
        }
        else
            itemFound = false;
        _animator = projectileSpawn.GetComponent<Animator>();
        timePassed = 1;
        startTimer = false;
        p = GameObject.Find("Player").GetComponent<Player>();
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Alpha1) && timePassed >= .25 && p.mana > 30 && itemFound)
        {
            Rigidbody2D clone;
            clone = Instantiate(ProjectilePrefab, projectileSpawn.transform.position, Quaternion.identity);
            if(GetComponentInParent<Rigidbody2D>().transform.localScale.x < 0)
                clone.GetComponent<SpriteRenderer>().flipX = true;
            clone.gameObject.layer = 1;
            clone.AddForce(new Vector2(GetComponentInParent<Rigidbody2D>().transform.localScale.x * 1.25f, 0f), ForceMode2D.Impulse);
            startTimer = true;
            timePassed = 0;
            p.mana = p.mana - 30;
            _animator.SetTrigger("Projectile");
            if (!p.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                p.GetComponentInParent<Animator>().SetBool("Attack", true);
            }
                       

        }
        else if(startTimer)
        {
            timePassed = timePassed + Time.deltaTime;
        }
        if(p.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            p.GetComponentInParent<Animator>().SetBool("Attack", false);
        }
    }
   
 
}
