using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public static int pickupCount = 0;

    // house is just a trigger,, not a pickup
    public bool isActualyPickup = true;

    // 0: pickup. 1: 
    public int mode;

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < 3; i++) {
            transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).GetChild(Random.Range(0, 3)).gameObject.SetActive(true);
        pickupCount++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
