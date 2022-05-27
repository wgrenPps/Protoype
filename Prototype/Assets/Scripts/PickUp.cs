using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    public GameObject stick;

    void Start()
    {
        stick.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            stick.SetActive(true);
        }
    }
}
