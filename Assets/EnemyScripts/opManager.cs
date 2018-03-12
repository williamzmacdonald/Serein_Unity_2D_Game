using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opManager : MonoBehaviour {
    private void OnTriggerExit2D(Collider2D collision) {                                //If Enemy Leaves Zone While Hasing Chasing Move Back
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<WolfEnemy>().reDirect();
        }
    }
}
