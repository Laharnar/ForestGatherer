using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChoices : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    [ContextMenu("Pick tree")]
    private void PickOneTree() {
        PickOneTree(UnityEngine.Random.Range(0, transform.childCount));
    }

    private void PickOneTree(int v) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(v).gameObject.SetActive(true);
    }
}
