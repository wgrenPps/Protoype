using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemies : MonoBehaviour {
    GameObject anchor;
    Vector3 merph;
    Rigidbody rb;
    Vector2 force;
    bool hit;
    float t;
    EnemyControl HP;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bad")) { 
        merph = anchor.transform.rotation.eulerAngles;
        hit = true; // gets hit
        HP = other.transform.parent.GetComponent<EnemyControl>(); //aquiring health from other script
        HP.health -= 1; // lose health
        rb = other.transform.parent.GetComponent<Rigidbody>();
        force = new Vector2(3f * 500f * Mathf.Sin(merph.y), 3f * 500f * (1 - Mathf.Sin(merph.y)));
        }
    }

    void Start() {
        anchor = GameObject.FindWithTag("Anchor");
    }

    void FixedUpdate() {
        if (rb != null) { 
            if (hit == true) {
                rb.AddForce(-force.x, 0, -force.y);
                hit = false;
            }
        }
    }
}