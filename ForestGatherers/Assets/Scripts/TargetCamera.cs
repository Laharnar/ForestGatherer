using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (target.position.y + offset.y < 1) {
            offset.y = 1 - target.position.y;
        }
        if (target.position.y + offset.y > 10) {
            offset.y = 10 - target.position.y;
        }

        transform.position = target.position+ offset;
        transform.forward = target.parent.position - transform.position;
    }
}
