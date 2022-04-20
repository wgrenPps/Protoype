using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mepf : MonoBehaviour {
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Bad")) {
            Destroy(other.transform.parent.gameObject);
        }
    }
}