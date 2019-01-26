using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rig;

    Vector3 input;
    public float speed = 1f;

    public float runSpeed = 2f;
    bool running = false;

    public float stanmina = 10;
    private bool stanminaAvaliableForReload;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StanminaReLoader());
    }

    IEnumerator StanminaReLoader() {
        float maxStanmina = stanmina;
        while (true) {
            if (!stanminaAvaliableForReload) {
                yield return new WaitForSeconds(2);
                stanminaAvaliableForReload = true;
            }
            // reload stanmina
            if (stanminaAvaliableForReload) {
                yield return new WaitForSeconds(0.3f);
                if(stanmina < maxStanmina)
                    stanmina++;
                else {
                    stanmina = maxStanmina;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        running = false;
        if (Input.GetKey(KeyCode.Space) && stanmina > 0) {
            running = true;
            stanmina -= Time.deltaTime;
            stanminaAvaliableForReload = false;
        }
    }

    private void FixedUpdate() {
        if (running == false) {
            rig.MovePosition(rig.position + input * speed * Time.deltaTime);
        } else {
            rig.MovePosition(rig.position + input * runSpeed * Time.deltaTime);
        }
    }
}

