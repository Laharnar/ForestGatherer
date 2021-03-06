﻿using System;
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

    public int hp = 3;

    public int trapCount = 5;
    float trapCd = 0;

    public float horizontalSensitivity = 4f;
    public float verticalSensitivity = 4f;

    public Animator anim;

    public AudioController audioFx;

    // Start is called before the first frame update
    void Start()
    {
        audioFx.Init();
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
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * horizontalSensitivity);
        GameManager.m.cam.GetComponent<TargetCamera>().offset += new Vector3(0, Input.GetAxis("Mouse Y"), 0) * Time.deltaTime * verticalSensitivity;
        //        GameManager.m.cam.transform.Translate();
        if(audioFx.monsterSounds)
        audioFx.Play(1, true);

        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        running = false;
        if (Input.GetKey(KeyCode.LeftShift) && stanmina > 0) {
            

            running = true;
            stanmina -= Time.deltaTime;
            stanminaAvaliableForReload = false;
        }
        if (Input.GetKey(KeyCode.Space) && trapCount > 0 && Time.time > trapCd) {
            audioFx.PlayOne(2);

            trapCd = Time.time+0.33f;
            trapCount--;
            Instantiate(GameManager.m.trapPref, transform.position, new Quaternion());
        }
    }

    private void FixedUpdate() {
        if (input == Vector3.zero) {
            if (anim) anim.SetBool("walk", false);
            if (anim) anim.SetBool("run", false);

        }
        if (running == false) {
            if(anim) anim.SetBool("walk", true);
            rig.MovePosition(rig.position + transform.TransformDirection(input) * speed * Time.deltaTime);
        } else {
            if(anim) anim.SetBool("run", true);
            rig.MovePosition(rig.position + transform.TransformDirection(input) * runSpeed * Time.deltaTime);
        }
    }

    internal void GetDmg(int v) {
        audioFx.PlayOne(0);
        hp -= v;
        if (hp <= 0) {
            GameManager.OpenLoseScene();
            Destroy(gameObject);
        }
    }
}

