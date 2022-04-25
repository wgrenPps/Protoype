using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyEnemies : MonoBehaviour {
    GameObject bungie;
    Vector3 merph;
    Rigidbody rb;
    Vector2 force;
    bool hit;
    float t;
    void OnTriggerEnter(Collider other)
    {
        merph = bungie.transform.rotation.eulerAngles;
        hit = true;
        Debug.Log(merph);
        rb = other.transform.parent.GetComponent<Rigidbody>();
        force = new Vector2(500f * Mathf.Sin(merph.y), 500f * (1 - Mathf.Sin(merph.y)));
    }
    // Start is called before the first frame update
    void Start()
    {
        bungie = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null)
        { 
            if (hit == true)
            {
                rb.AddForce(force.x, 0, force.y);
                hit = false;
       
            }
            

        }
    }
}