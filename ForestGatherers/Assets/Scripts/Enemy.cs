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
interface IAnimEventReciever {
    void OnRecieve();
}
public class Enemy : MonoBehaviour, IAnimEventReciever {
    public Rigidbody rig;

    Vector3 dir;
    public float speed = 1f;

    public Transform goal;
    NavMeshAgent agent;

    public EnemyType enemyType = EnemyType.Ground;
    public AttackMode aiState = AttackMode.Waiting;

    public float groundAttackDistance=5;

    public Animator anim;
    public bool readyToAtk;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = speed;
        HandleDifferentEnemyTypes();
        switch (aiState) {
            case AttackMode.Waiting:
                Vector3 selfPos = new Vector3(transform.position.x, transform.position.y, GameManager.m.player.position.z);
                if (Vector3.Distance(selfPos, GameManager.m.player.position)
                    < groundAttackDistance) {
                    aiState = AttackMode.Attacking;
                    if (anim)
                        anim.SetBool("awakened", true);
                }
                break;
            case AttackMode.Attacking:
                if (GameManager.m.player&& Vector2.Distance(GameManager.m.player.position, transform.position) < 5
                    && readyToAtk) {
                    anim.SetTrigger("attack");
                    readyToAtk = false;
                } else {
                    if (goal)
                    agent.destination = goal.position;
                    anim.SetBool("move", true);
                }
                if (Vector2.Distance(GameManager.m.player.position, transform.position) > 30) {
                    aiState = AttackMode.LostTarget;
                }
                break;
            case AttackMode.LostTarget:
                aiState = AttackMode.Waiting;
                break;
            default:
                break;
        }
    }

    private void HandleDifferentEnemyTypes() {
        switch (enemyType) {
            case EnemyType.Ground:
                
                break;
            case EnemyType.Tree:
                break;
            case EnemyType.Other:
                break;
            default:
                break;
        }

    }

    public void OnRecieve() {
        readyToAtk = true;
    }

    internal void StopEffect() {
        StartCoroutine(Stop(3));
    }

    private IEnumerator Stop(float v) {
        float s = speed;
        speed = 0;
        yield return new WaitForSeconds(v);
        speed = s;
    }
}
