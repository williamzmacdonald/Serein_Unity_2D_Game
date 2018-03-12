using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjColl : MonoBehaviour {
    Animator _animator;
    Rigidbody2D _body;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
       
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Enemy")
        {
            _animator.SetTrigger("ProjectileHit");
            Destroy(_body);
            Destroy(GetComponent<PolygonCollider2D>());
            Invoke("deleteProjectile", .42f);
        }
        
    }

    void deleteProjectile()
    {
        Destroy(gameObject);
    }
}
