using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    public NavMeshAgent monster;
    public bool wandering = true;

    void Start() {
        monster = this.GetComponent<NavMeshAgent>();
    }

    void Attack (Vector3 center, float radius) {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        float dist;
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.gameObject.CompareTag("Player")) {
                dist = Vector3.Distance(center, hitCollider.transform.position);
            }
        }
    }
}
