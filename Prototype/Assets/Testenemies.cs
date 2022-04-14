using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Testenemies : MonoBehaviour {
    public NavMeshAgent monster;
    public Transform bungie;

    //wandering
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    public bool bungieInSightRange, bungieInAttackRange;


    // Start is called before the first frame update
    void Start() {
        bungie = GameObject.Find("thirdPersonPlayer").transform;
        monster.GetComponent<NavMeshAgent>();
    }
    void Update() {
        //Check for sight and attack range:
        bungieInSightRange = Physics.CheckSphere(transform.position, sightRange);
        bungieInAttackRange = Physics.CheckSphere(transform.position, attackRange);
        
    }
    private void Wander() {
        if (!walkPointSet) SearchWalkPoint();
        Debug.Log("searching for walkpoint");

        if (walkPointSet) {
            monster.SetDestination(walkPoint);
            Debug.Log("walkpoint is set");
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //reached the walkPoint:
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
            Debug.Log("walk point is false");
        }
    }
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f)) {
            walkPointSet = true;
            Debug.Log("walkpoint is true");
        }
    }
}
