using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType {
    Ground,
    Tree,
    Other
}
public enum AttackMode {
    Waiting,
    Attacking,
    LostTarget
}
public class Enemy : MonoBehaviour
{
    public Rigidbody rig;

    Vector3 dir;
    public float speed = 1f;

    public Transform goal;
    NavMeshAgent agent;

    public EnemyType enemyType = EnemyType.Ground;
    public AttackMode aiState = AttackMode.Waiting;

    public float groundAttackDistance=5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDifferentEnemyTypes();
        
    }

    private void HandleDifferentEnemyTypes() {
        switch (enemyType) {
            case EnemyType.Ground:
                agent.destination = GameManager.m.player.position;
                Vector3 selfPos = new Vector3(transform.position.x, transform.position.y, GameManager.m.player.position.z);
                if (Vector3.Distance(selfPos, GameManager.m.player.position)
                    < groundAttackDistance) {
                    aiState = AttackMode.Attacking;
                }
                break;
            case EnemyType.Tree:
                agent.destination = GameManager.m.player.position;
                break;
            case EnemyType.Other:
                agent.destination = goal.position;
                break;
            default:
                break;
        }

    }
}
