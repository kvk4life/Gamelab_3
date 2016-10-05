using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    public float timeBetweenAttack, attackRange; // the time between each attack, the range of the attack
    private float attackTimer;
    public int moveSpeed, fullMoveSpeed; // movespeed = the normal movespeed, fullmoveSpeed is for changeble movespeeds.
    private int resetMoveSpeed, counter;
    [SerializeField]
    private Transform attackTarget, wayPointTarget;
    public GameObject spawner;  
    public string[] tagList; // the tags of the minions it needs to target
    private MinionSpawner spawnClass;
    private Stats statsClass;
    public States minionState;
    public AttackTargets attackPriority; // the states of the minion

    [SerializeField]
    private List<Transform> wayPointList = new List<Transform>();
    [SerializeField]
    private List<GameObject> enemyList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> allyList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> minionList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> turretList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> championList = new List<GameObject>();
    [SerializeField]
    private List<GameObject> jungleList = new List<GameObject>();

    public enum States {
        Attack,
        FollowWayPoint
    }

    public enum AttackTargets {
        Minions,
        Turrets,
        Champions,
        JungleCamp
    }


    void Start() {
        statsClass = GetComponent<Stats>();
        spawnClass = spawner.GetComponent<MinionSpawner>();
        resetMoveSpeed = moveSpeed;
        fullMoveSpeed = moveSpeed;
        AddWayPoints();
        wayPointTarget = wayPointList[counter];
    }

    void Update() {

        //Movement of the minion
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        SetState();  //checking wich state the enemy needs to be in

        CheckState();   // checking the state of the minion

        //checking if the minion is dead or not

        if (statsClass.currentHealth < 1) {
            Destroy(gameObject);
        }


    }

    void SetState() {
       
        if (enemyList.Count == 0) {
            minionState = States.FollowWayPoint;
            return;
        }

        if (enemyList.Count > 0) {
            CheckPriority(); // checking the attack prority
            minionState = States.Attack;
            return;
        }
        
    }

    void CheckState() { 

        if (minionState == States.FollowWayPoint) {
            FollowWayPoint(); // the minion will follow the waypoints
        }

        if (minionState == States.Attack) {
            CheckForNull(); // checking if anything is null in the lists
            Attack((int)attackPriority); // the attack funtction of the minion
        }

    }

    void FollowWayPoint() {
        transform.LookAt(wayPointTarget);
        moveSpeed = fullMoveSpeed;
    }

    void AddWayPoints() { // adding all the waypoints from the spawner
        for (int i = 0; i < spawnClass.waypointList.Length; i++) {
            wayPointList.Add(spawnClass.waypointList[i]);
        }
    }

    void CheckPriority() {
        if (minionList.Count != 0) {
            attackPriority = AttackTargets.Minions;
            return;
        }

        if (turretList.Count != 0) {
            attackPriority = AttackTargets.Turrets;
            return;
        }

        if (championList.Count != 0) {
            attackPriority = AttackTargets.Champions;
            return;
        }

        if (jungleList.Count != 0) {
            attackPriority = AttackTargets.JungleCamp;
            return;
        }
    }

    void CheckForNull(){
        while (minionList.Contains(null)) {
            minionList.Remove(null);
        }

        while (turretList.Contains(null)) {
            turretList.Remove(null);
        }

        while (championList.Contains(null)) {
            championList.Remove(null);
        }

        while (jungleList.Contains(null)) {
            jungleList.Remove(null);
        }

        while (enemyList.Contains(null)) {
            enemyList.Remove(null);
        }

        while (allyList.Contains(null)) {
            allyList.Remove(null);
        }
    }

    void Attack(int number) {

        // setting the target the minion needs to attack

        if(minionList.Count == 0 && turretList.Count == 0 && championList.Count == 0 && jungleList.Count == 0) {
            return;
        }
        switch (number) {
            case 0:
                attackTarget = minionList[0].transform;
                break;
            case 1:
                attackTarget = turretList[0].transform;
                break;
            case 2:
                attackTarget = championList[0].transform;
                break;
            case 3:
                attackTarget = jungleList[0].transform;
                break;
        }

       
        transform.LookAt(attackTarget);

        float distance = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position);

        //Minion checks if its in range to attack and if its time  to attack

        if (distance <= attackRange) {
            moveSpeed = 0;

            if (attackTimer <= 0) {
                attackTarget.GetComponent<Stats>().currentHealth -= statsClass.damage;
                attackTimer = timeBetweenAttack;

            }
        }
        else {
            moveSpeed = fullMoveSpeed;
        }

        if(attackTimer >= 0) {
            attackTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider trigger) {
        //checking if it needs to change waypoints
        if (trigger.transform == wayPointList[counter]) {
            counter++;
            if (counter >= wayPointList.Count) {
                counter = 0;
            }
            wayPointTarget = wayPointList[counter];
        }
        //checking if it needs to add a enemy
        if (trigger.gameObject.GetComponent<Stats>() != null) {
            AgroAdd(trigger.gameObject);
        }

    }

    void OnTriggerExit(Collider trigger) {
        if (trigger.GetComponent<Stats>() != null) {
            AgroRemove(trigger.gameObject);
        }

    }

    void AgroRemove(GameObject target) {
        for (int i = 0; i <= tagList.Length - 1; i++) {
            if (target.transform.tag == tagList[i]) {
                if (target.gameObject.GetComponent<Stats>().teamNumber != statsClass.teamNumber) {
                    enemyList.Remove(target);
                }
                else {
                    allyList.Remove(target);
                    break;
                }
            }
        }
    }

    void AgroAdd(GameObject target) {
        //adding the enemies
        for (int i = 0; i <= tagList.Length - 1; i++) {
            if (target.transform.tag == tagList[i]) {
                if (target.gameObject.GetComponent<Stats>().teamNumber != statsClass.teamNumber) {
                    if (enemyList.Contains(target)) {
                    }
                    else {
                        enemyList.Add(target);
                        AddToEntityList(target);
                    }
                    break;
                }
                else {
                    if (allyList.Contains(target)) {
                    }
                    else {
                        allyList.Add(target);
                    }
                    break;
                }
            }
        }
    }

    void AddToEntityList(GameObject entity) {
        //adding enemy to the right list
        if (entity.transform.tag == tagList[0]) {
            minionList.Add(entity);
        }
        if (entity.transform.tag == tagList[1]) {
            turretList.Add(entity);
        }
        if (entity.transform.tag == tagList[2]) {
            championList.Add(entity);
        }
        if (entity.transform.tag == tagList[3]) {
            jungleList.Add(entity);
        }
    }
}
