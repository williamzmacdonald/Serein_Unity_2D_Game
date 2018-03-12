using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform player;
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;
    public float dis;
    public float distanceCamera;
    
    // Use this for initialization
    void Start () {
        distanceCamera = 4.0f;
        minX = GameObject.Find("LeftWall").transform.position.x + 9f;
        maxX = GameObject.Find("RightWall").transform.position.x - 9f;
        this.transform.transform.position = new Vector3(player.position.x, this.transform.position.y, this.transform.position.z);
        
        
        

    }
	
	// Update is called once per frame
	void LateUpdate () {



        
        
        this.transform.transform.position = new Vector3(player.position.x+2, this.transform.position.y, this.transform.position.z);
        if (this.transform.position.x<minX)
        {
           this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
        }
        if (this.transform.position.x > maxX)
        {
            this.transform.position= new Vector3(maxX, this.transform.position.y, this.transform.position.z);
        }
        if (Vector3.Distance(transform.position, player.position) > distanceCamera)
        {
        //var move = Vector3.MoveTowards(transform.position, new Vector3(player.position.x-dis, player.position.y - dis, transform.position.z), distanceCamera);
       // Debug.Log(move);
        //    transform.position =move;
            transform.position = new Vector3(transform.position.x, player.position.y - dis,transform.position.z);
        }
        


    }
       
}
