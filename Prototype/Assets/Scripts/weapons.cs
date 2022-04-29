using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapons : MonoBehaviour {
    public Transform anchor;
    public Transform club;
    float t;
    public float t1;
    public float t2;
    bool attack1;
    bool attack2;

    void Start() {
        t = 0;
    }

    void Update() {
        //left click attack
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            attack1 = true;
        }

        //right click attack
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            attack2 = true;
        }

        //left click attack movement:
        if (attack1 == true) {
            t += Time.deltaTime;
            if (t < t1) { 
            anchor.localRotation =  Quaternion.Euler( (30 * t) * 4f, 20f * (50 * t), 90);
            }
            //right click attack movement:        
        } else if (attack2 == true) {
            t += Time.deltaTime;
            if (t < t1) {
                anchor.localRotation = Quaternion.Euler((20 * t) * 6f, -20f * (50 * t), -90);
            }
        }

        //attack cooldown
        if (t > t2) {
            attack1 = false;
            t = 0;
            anchor.localRotation = Quaternion.Euler(-(30 * t), (20 * t), -35);
            attack2 = false;
        }
    }
}