using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour {
    public NavMeshAgent monster;
    public Transform bungie;
    public GameObject Player;
    public int powerA;
    public GameObject stick;
    
    public LayerMask whatIsGround, whatIsBungie;

    public int enemyHealth;

    //wandering
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool bungieInSightRange, bungieInAttackRange;

    //item drops stuff
    public GameObject drop; //coin GameObject


    //Player Health 
    public HealthTrack helth;

    //door opener
    DoorOpen doortracker;

    // knockback
    public destroyEnemies knock;
    public Rigidbody rb;


    void Start() {
        //enemyHealth = 5;
        helth = Player.GetComponent<HealthTrack>();
        doortracker = Player.GetComponent<DoorOpen>();
        knock = stick.GetComponent<destroyEnemies>();
    }

    private void Awake() {
        bungie = GameObject.Find("thirdPersonPlayer").transform;
        stick = GameObject.Find("STICC");
        Player = GameObject.Find("Bungie");
        monster = GetComponent<NavMeshAgent>();
    }
   

    private void Update() {
        //Check for sight and attack range:
        bungieInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBungie);
        bungieInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsBungie);

        if (!bungieInSightRange && !bungieInAttackRange) Wander();
        if (bungieInSightRange && !bungieInAttackRange) Chase();
        if (bungieInAttackRange && bungieInSightRange) Attack();
    }

    private void FixedUpdate() {
        if (enemyHealth == 0) {
            doortracker.enemiesDead++;
            Destroy(this.gameObject);
        }
        if (knock.hit == true)
        {
            rb.AddForce(-knock.force.x, 0, -knock.force.y);
            knock.hit = false;
        }
    }

    private void Wander() {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet) {
            monster.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //reached the walkPoint:
        if (distanceToWalkPoint.magnitude < 1f) {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint() {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)) {
            walkPointSet = true;
        }
    }

    private void Chase() {
        monster.SetDestination(bungie.position);
    }

    private void Attack() {
        monster.SetDestination(transform.position);

        if (!alreadyAttacked) {
            helth.healthBar -= powerA;
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack() {
        alreadyAttacked = false;
    }

    //when enemy gets destroyed it becomes a coin
    private void OnDestroy() {
        //this is not neccesary. Will keep just in case
        //Instantiate(drop, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), drop.transform.rotation);
    }

    
}