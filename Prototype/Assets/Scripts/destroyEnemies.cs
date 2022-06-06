using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemies : MonoBehaviour {
    GameObject anchor;
    Vector3 merph;
    Rigidbody rb;
    public Vector2 force;
    public bool hit;
    float t;
    EnemyControl HP;

    public AudioClip impact;
    AudioSource audioSource;


    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("monster")) { 
          audioSource = other.transform.parent.GetComponent<AudioSource>();
          audioSource.PlayOneShot(impact, 0.7F);

            merph = anchor.transform.rotation.eulerAngles;
            hit = true; // gets hit 
            HP = other.transform.parent.GetComponent<EnemyControl>(); //aquiring health from other script
            HP.enemyHealth -= 1; // lose health
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