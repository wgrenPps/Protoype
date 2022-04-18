using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFab : MonoBehaviour {
    public Transform Enemies;
    public int population = 10;

    void Start() {
        for (int i = 0; i < population; i++) {
            Instantiate(Enemies);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
