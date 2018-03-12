using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {
    Rigidbody2D _rigidbody;
    Animator _animator;
    public bool airborn;
    public int health;
    public int mana;
    public bool shield;
    float manaTimer;
    float animationTimer;
    bool knockback;
    bool updateOn;
    LayerMask floorLayer;
    public static float JUMPFORCE = 30.0f;
    public static float HORIZONTALFORCE = 20.0f;
    public static float WEIGHT = 2;
    public static float GRAVITY = 3;
    public static int STARTHEALTH = 3;
    public static int STARTMANA = 100;
    public Stack healthList;
    public ArrayList inActiveHealth = new ArrayList();
    public GameObject itemprefab;
    public Transform Healthbar;
    public Transform Inventory;
    public Slider manabar;
    public GameObject menu;
    private PlayerShield ps;
    private Blink pb;
    private Projectile pp;
    public float timeLeft;
    public Tutorial tuts;


    // Use this for initialization
    void Start () {
        
        
        timeLeft = 60;
        updateOn = true;
        knockback = false;
        floorLayer = LayerMask.GetMask("Floor");
        //healthList= new Stack(GameObject.FindGameObjectsWithTag("Healthbar"));
        manaTimer = 0;
        animationTimer = 0;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Idle2", false);
         airborn = false;
        _rigidbody.transform.localScale = new Vector3(2f, 2f, 0f);
        _rigidbody.mass = WEIGHT;
        _rigidbody.gravityScale = GRAVITY;
        health = STARTHEALTH;
        mana = STARTMANA;
        shield = false;
        manabar.value = getMana();
        for(int i = 0; i < health; i++)
        {
            GameObject testing = Instantiate(itemprefab);
            testing.transform.SetParent(Healthbar.transform, false);
        }
        ps = GetComponentInChildren<PlayerShield>();
        pb = GetComponentInChildren<Blink>();
        pp = GetComponentInChildren<Projectile>();

        string scene = SceneManager.GetActiveScene().name;
        if (scene != "game")
        {
            GameObject tmp = Instantiate(GameObject.FindGameObjectWithTag("item1"));
            tmp.transform.SetParent(Inventory.transform, false);
            pp.itemFound = true;
            GameObject.Find("item1").GetComponent<Image>().enabled = true;

            GameObject tmp1 = Instantiate(GameObject.FindGameObjectWithTag("item2"));
            tmp1.transform.SetParent(Inventory.transform, false);
            ps.itemFound = true;
            GameObject.Find("item2").GetComponent<Image>().enabled = true;

            GameObject tmp2 = Instantiate(GameObject.FindGameObjectWithTag("item3"));
            tmp2.transform.SetParent(Inventory.transform, false);
            pb.itemFound = true;
            GameObject.Find("item3").GetComponent<Image>().enabled = true;


        }
    }

    // Update is called once per frame
    void Update () {

        if (updateOn)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                _animator.SetTrigger("Death");
                _rigidbody.Sleep();
                Invoke("respawn", .6f);
                updateOn = false;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.4f, floorLayer);
            if (hit.collider != null)
            {
                _animator.SetBool("Jumping", false);
                airborn = false;
            }
            else
            {
                airborn = true;
            }
            manabar.value = getMana();
            if (!knockback)
                _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
            if (!airborn && Input.GetKeyDown(KeyCode.UpArrow) && !knockback &&  _rigidbody.velocity.y < 5)
            {
                _rigidbody.AddForce(Vector2.up * JUMPFORCE, ForceMode2D.Impulse);
                _animator.SetBool("Jumping", true);
                _animator.Play("", 0, 0f);
            }

            if (Input.GetKey(KeyCode.RightArrow) && !knockback)
            {
                _rigidbody.AddForce(Vector2.right * HORIZONTALFORCE, ForceMode2D.Impulse);
                _rigidbody.transform.localScale = new Vector3(2f, _rigidbody.transform.localScale.y, 0f);
                _animator.SetBool("Running", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && !knockback)
            {
                _rigidbody.AddForce(Vector2.left * HORIZONTALFORCE, ForceMode2D.Impulse);
                _rigidbody.transform.localScale = new Vector3(-2f, _rigidbody.transform.localScale.y, 0f);
                _animator.SetBool("Running", true);
            }

            else
            {
                _animator.SetBool("Running", false);
            }
            if (manaTimer > .5)
            {
                if (shield)
                {
                    if (mana >= 25)
                        mana -= 25;
                    else
                    {
                        GetComponentInChildren<PlayerShield>().RemoveShield();
                    }
                }
                else if ((mana + 10) <= 100)
                    mana += 20;
                else
                    mana = 100;
                manaTimer = 0;
            }
            manaTimer = manaTimer + Time.deltaTime;

            if (animationTimer > 5)
            {
                _animator.SetBool("Idle2", !_animator.GetBool("Idle2"));
                animationTimer = 0;
            }
            else
                animationTimer = animationTimer + Time.deltaTime;
        }
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (updateOn)
        {
            if (collision.gameObject.name == "shoot")
            {
                GameObject tmp = Instantiate(GameObject.FindGameObjectWithTag("item1"));
                tmp.transform.SetParent(Inventory.transform, false);
                pp.itemFound = true;
                GameObject.Find("item1").GetComponent<Image>().enabled = true;
                Destroy(collision.gameObject);
                if (tuts.tut2b == false)
                    tuts.tutorialWindow(2);
            }
            if (collision.gameObject.name == "shield")
            {
                GameObject tmp = Instantiate(GameObject.FindGameObjectWithTag("item2"));
                tmp.transform.SetParent(Inventory.transform, false);
                ps.itemFound = true;
                GameObject.Find("item2").GetComponent<Image>().enabled = true;
                Destroy(collision.gameObject);
                if (tuts.tut3b == false)
                    tuts.tutorialWindow(3);
            }
            if (collision.gameObject.name == "warp")
            {
                GameObject tmp = Instantiate(GameObject.FindGameObjectWithTag("item3"));
                tmp.transform.SetParent(Inventory.transform, false);
                pb.itemFound = true;
                GameObject.Find("item3").GetComponent<Image>().enabled = true;
                Destroy(collision.gameObject);
                if (tuts.tut4b == false)
                    tuts.tutorialWindow(4);
            }
            if (collision.gameObject.tag == "Enemy" && knockback == false)
            {
                if (!shield)
                {
                    health--;
                    Destroy(Healthbar.transform.GetChild(Healthbar.transform.childCount - 1).gameObject);
                    knockback = true;
                }
                Vector2 direction = (Vector2)transform.position - (Vector2)collision.transform.position;
                direction = direction.normalized;
                Debug.Log(direction);
                _rigidbody.AddForce(direction * 20, ForceMode2D.Impulse);
                knockback = true;
                if (health <= 0)
                {
                    _animator.SetTrigger("Death");
                    _rigidbody.Sleep();
                    Invoke("respawn", .6f);
                    updateOn = false;
                }
                Invoke("knockbackDone", .2f);
            }
            if (collision.gameObject.tag == "End")
            {

                //menu.SetActive(true);
                SceneManager.LoadScene("level2");


            }
            if (collision.gameObject.tag == "bossLoad")
            {

                //menu.SetActive(true);
                SceneManager.LoadScene("boss");


            }
            if (collision.gameObject.tag == "heart")
            {
                AddHealth();
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "End")
        {

            
            menu.SetActive(true);

        }
    }
    private void knockbackDone()
    {
        knockback = false;
    }
    public void respawn()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Start();
        updateOn = true;
        foreach (GameObject i in inActiveHealth)
            i.SetActive(true);
        healthList = new Stack(inActiveHealth);
        inActiveHealth = new ArrayList();
        health = STARTHEALTH;
        mana = STARTMANA;
        airborn = false;
        shield = false;
        
        
        


    }
    public void AddHealth()
    {
        GameObject testing = Instantiate(itemprefab);
        testing.transform.SetParent(Healthbar.transform, false);
    }
    public float getMana()
    {
        return mana / 100.0f;
    }
    

}
