using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType {
    Ground,
    Tree
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
        // specific: enable enemy type child
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild((int)enemyType).gameObject.SetActive(true);

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
                    Attack();
                } else {
                    if (goal!=null && Vector3.Distance(goal.position, agent.destination) > 3) {
                        agent.destination = goal.position;
                        anim.SetBool("move", true);
                    }
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

    private void Attack() {
        switch (enemyType) {
            case EnemyType.Ground:
                anim.SetTrigger("attack");
                readyToAtk = false;
                break;
            case EnemyType.Tree:
                Transform t = Instantiate(GameManager.m.bulletPref, transform.position, new Quaternion());
                t.forward = (GameManager.m.player.position - transform.position);
                readyToAtk = false;
                StartCoroutine(Reload(2));
                break;
            default:
                break;
        }
    }

    private IEnumerator Reload(float cd) {
        yield return new WaitForSeconds(cd);
        readyToAtk = true;
    }

    private void HandleDifferentEnemyTypes() {
        switch (enemyType) {
            case EnemyType.Ground:
                
                break;
            case EnemyType.Tree:
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
