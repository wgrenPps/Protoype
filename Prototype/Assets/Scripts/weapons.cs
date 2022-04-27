using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapons : MonoBehaviour {
    public Transform anchor;
    public Transform club;
    float t;
    public float t1;
    public float t2;
    bool thing;
    bool x;
    // Start is called before the first frame update
    void Start() {
        t = 0;
    }

    // Update is called once per frame
    void Update() { 
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            thing = true;

        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            thing = true;
            x = true;

        }
        if (thing == true && x != true) {
            t += Time.deltaTime;
                if (t < t1) { 
            anchor.localRotation =  Quaternion.Euler( (30 * t) * 4f, 3f * (50 * t), 90);
            }
        } else if (thing == true && x == true) {
            t += Time.deltaTime;
            if (t < t1)
            {
                anchor.localRotation = Quaternion.Euler((20 * t) * 6f, -3f * (50 * t), -90);
            }
        }
        if (t > t2) {
            
            thing = false;
            t = 0;
            anchor.localRotation = Quaternion.Euler(-(30 * t), (20 * t), -35);
            if (x == true) {
                x = false;
            }
        }
    }
}