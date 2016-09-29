using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinionBehavior : MonoBehaviour {

    public float timeBetweenAttack, attackRange;
    private float attackTimer;
    public int moveSpeed, fullMoveSpeed;
    private int enumNumber, resetMoveSpeed, counter;
    [SerializeField]
    private Transform currentTarget, attackTarget, wayPointTarget;
    public GameObject spawner;
    public string[] tagList;
    private bool allowAttack;
    public MinionSpawner spawnClass;
    private Stats statsClass;
    public States minionState;
    public AttackTargets attackPriority;

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
        allowAttack = true;
 
    }

    void Update() {

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (minionState == States.FollowWayPoint) {
            FollowWayPoint();
        }

        if (minionState == States.Attack) {
            Attack(enumNumber = (int)attackPriority);
        }

        if (enemyList.Count == 0) {
            int temp = (int)minionState;
            temp = 1;
            minionState = (States)temp;
        }
        else {
            int temp = (int)minionState;
            temp = 0;
            minionState = (States)temp;
        }
        CheckPriority();

        if(statsClass.currentHealth <= 0) {
            Destroy(gameObject);
        }

        /// add function null checkers;
    }


    void FollowWayPoint() {
        transform.LookAt(wayPointTarget);
        moveSpeed = fullMoveSpeed;
    }

    void AddWayPoints() {
        for (int i = 0; i < spawnClass.waypointList.Length; i++) {
            wayPointList.Add(spawnClass.waypointList[i]);
        }
    }

    void CheckPriority() {
        if (jungleList.Count > 0) {
            if (championList.Count > 0) {
                if (turretList.Count > 0) {
                    if (minionList.Count > 0) {
                        int temp = (int)attackPriority;
                        temp = 0;
                        attackPriority = (AttackTargets)temp;
                    }
                    else {
                        int temp = (int)attackPriority;
                        temp = 1;
                        attackPriority = (AttackTargets)temp;
                    }
                }
                else {
                    int temp = (int)attackPriority;
                    temp = 2;
                    attackPriority = (AttackTargets)temp;
                }
            }
            else {
                int temp = (int)attackPriority;
                temp = 3;
                attackPriority = (AttackTargets)temp;
            }
        }
    }

    void Attack(int number) {
     
        if (attackTarget != null) {
            transform.LookAt(attackTarget);

            float distance = Vector3.Distance(gameObject.transform.position, attackTarget.transform.position);

            if (distance <= attackRange) {
                allowAttack = true;
                moveSpeed = 0;
            }
            else {
                allowAttack = false;
                moveSpeed = fullMoveSpeed;
            }
        }
        else {
            switch (number) {
                case 0:
                    minionList.Remove(null);
                    break;
                case 1:
                    turretList.Remove(null);
                    break;
                case 2:
                    championList.Remove(null);
                    break;
                case 3:
                   jungleList.Remove(null);
                    break;
            }
        }

        if (allowAttack) { 
            if (attackTimer <= 0) {
                attackTimer = timeBetweenAttack;
                switch (number) {
                    case 0:
                        attackTarget = minionList[0].transform;
                        minionList[0].GetComponent<Stats>().currentHealth -= statsClass.damage;
                        break;
                    case 1:
                        attackTarget = turretList[0].transform;
                        turretList[0].GetComponent<Stats>().currentHealth -= statsClass.damage;
                        break;
                    case 2:
                        attackTarget = championList[0].transform;
                        championList[0].GetComponent<Stats>().currentHealth -= statsClass.damage;
                        break;
                    case 3:
                        attackTarget = jungleList[0].transform;
                        jungleList[0].GetComponent<Stats>().currentHealth -= statsClass.damage;
                        break;
                }
            }
            else {
                attackTimer -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter(Collider trigger) {
        if (trigger.transform == wayPointList[counter]) {
            counter++;
            if (counter >= wayPointList.Count) {
                counter = 0;
            }
            wayPointTarget = wayPointList[counter];
        }

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
