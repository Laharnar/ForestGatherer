using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public List<PickupItem> collectedItems = new List<PickupItem>();
    public List<PickupItem> points = new List<PickupItem>();

    public Transform housePoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Count > 0 && points[0].transform == housePoint) {
            
            EndGame();
        }
    }

    private void EndGame() {
        Debug.Log("End game");
        GameManager.m.ShowUI("Menu1_END");
    }

    private void OnTriggerEnter(Collider collider) {
        PickupItem item = collider.GetComponent<PickupItem>();
        if (item != null) {
            item.gameObject.SetActive(false);
            if (item.mode == 0)
                collectedItems.Add(item);
            else 
                points.Add(item);
        }
    }
}
