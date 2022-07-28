using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContoller : MonoBehaviour
{
    public Enemy enemyData;

    public NavMeshAgent agent;
    public MeshFilter meshFilter;
    public Animator animator;
    public Transform[] waypoints;

    EnemyType type;
    public EnemyState state;

    int destPoint = 0;
    float shootCooldown = 0f;

    float detectionTimer = 0f;
    float stunnedTime = 0f;
    float distractedTime = 0f;

    Transform lastPlayerPosition = null;
    Vector3 startPosition;
    Quaternion startRotation;

    // Perception Cone
    float radius;
    float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;
    public GameObject playerRefs;


    // Start is called before the first frame update
    void Start()
    {
        name = enemyData.enemyName;
        type = enemyData.type;
        meshFilter.mesh = enemyData.enemyMesh;
        agent.speed = enemyData.agentSpeed;
        radius = enemyData.coneRadius;
        angle = enemyData.coneAngle;

        state = EnemyState.IDLE;
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (waypoints.Length != 0)
        {
            state = EnemyState.PATROLING;
            GoNextPatrolPoint();
        }

        playerRefs = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(PerceptionConeRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (state != EnemyState.DEATH)
        {
            if (state != EnemyState.STUNNED)
            {
                if (canSeePlayer)
                {
                    agent.isStopped = true;

                    if (detectionTimer <= enemyData.maxDetectionTime)
                        detectionTimer += Time.deltaTime;

                    if (detectionTimer > enemyData.detectionTime)
                    {
                        state = EnemyState.SHOOTING;
                        Shoot();
                        agent.speed = enemyData.agentSpeedRun;
                    }
                }
                else
                {
                    agent.isStopped = false;

                    switch (state)
                    {
                        case EnemyState.SHOOTING:
                            if (type != EnemyType.UNDISTRACTABLE)
                            {
                                state = EnemyState.SEARCHING;
                                agent.SetDestination(lastPlayerPosition.position);
                            }
                            else
                            {
                                state = EnemyState.CAUTIOUS;
                                LookAtPos(lastPlayerPosition);
                            }
                            break;
                        case EnemyState.CAUTIOUS:
                            if (detectionTimer <= 0)
                            {
                                detectionTimer = 0;

                                state = EnemyState.IDLE;
                                agent.speed = enemyData.agentSpeed;
                                transform.SetPositionAndRotation(startPosition, startRotation);
                            }
                            break;
                        case EnemyState.SEARCHING:

                            if (detectionTimer <= 0)
                            {
                                detectionTimer = 0;

                                if (waypoints.Length != 0)
                                {
                                    state = EnemyState.PATROLING;
                                    agent.speed = enemyData.agentSpeed;
                                    agent.SetDestination(waypoints[destPoint].position);
                                }
                                else
                                {
                                    agent.SetDestination(startPosition);
                                    agent.speed = enemyData.agentSpeed;
                                    if (Vector3.Distance(transform.position, agent.destination) <= 2)
                                    {
                                        state = EnemyState.IDLE;
                                        transform.SetPositionAndRotation(startPosition, startRotation);
                                    }
                                }
                            }
                            break;
                        case EnemyState.PATROLING:
                            Patrol();
                            break;
                        case EnemyState.DISTRACTED:
                            distractedTime -= Time.deltaTime;

                            if (distractedTime <= 0)
                            {
                                distractedTime = 0;

                                if (waypoints.Length != 0)
                                {
                                    state = EnemyState.PATROLING;
                                    agent.speed = enemyData.agentSpeed;
                                    agent.SetDestination(waypoints[destPoint].position);
                                }
                                else
                                {
                                    state = EnemyState.IDLE;
                                    transform.SetPositionAndRotation(startPosition, startRotation);
                                }
                            }
                            break;
                    }

                    if (detectionTimer > 0)
                        detectionTimer -= Time.deltaTime;
                }

                if (shootCooldown > 0)
                    shootCooldown -= Time.deltaTime;
            }
            else
            {
                if (state == EnemyState.STUNNED)
                {
                    stunnedTime -= Time.deltaTime;

                    if (stunnedTime <= 0)
                    {
                        stunnedTime = 0;

                        if (waypoints.Length != 0)
                        {
                            state = EnemyState.PATROLING;
                            agent.speed = enemyData.agentSpeed;
                            agent.SetDestination(waypoints[destPoint].position);
                        }
                        else
                            state = EnemyState.IDLE;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void Patrol()
    {
        if (agent.hasPath)
        {
            if (Vector3.Distance(transform.position, agent.destination) <= 2)
                GoNextPatrolPoint();
        }
        else
            agent.SetDestination(waypoints[destPoint].position);
    }

    void GoNextPatrolPoint()
    {
        agent.SetDestination(waypoints[destPoint].transform.position);
        destPoint = (destPoint + 1) % waypoints.Length;
    }

    void Shoot()
    {
        if (shootCooldown <= 0)
        {
            GameObject bullet = Instantiate(enemyData.bulletPrefab, transform.position, transform.rotation);
            Vector3 shootDir = (lastPlayerPosition.position - transform.position).normalized;
            bullet.GetComponent<EnemyBullet>().SetUp(shootDir);

            shootCooldown = enemyData.shootCooldown;
        }
    }

    IEnumerator PerceptionConeRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            PerceptionConeCheck();
        }
    }

    void PerceptionConeCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            lastPlayerPosition = target;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    void LookAtPos(Transform playerPos)
    {
        // TODO CHANGE ROTATION TO POSITION
    }

    void Stun(float time)
    {
        state = EnemyState.STUNNED;
        stunnedTime = time;
    }

    void Distacted(float time, Transform playerPos)
    {
        state = EnemyState.DISTRACTED;
        distractedTime = time;
        LookAtPos(playerPos);
    }
}
