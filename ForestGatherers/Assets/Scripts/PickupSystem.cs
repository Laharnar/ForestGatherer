using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public List<PickupItem> collectedItems = new List<PickupItem>();
    public List<PickupItem> points = new List<PickupItem>();

    public Transform housePoint;
    public int maxItemsOnMap = 3;
    public AudioController audioFx;

    private void EndGame() {
        audioFx.Init();
        Debug.Log("End game");
        GameManager.m.ShowUI("Menu1_END");
    }

    private void OnTriggerEnter(Collider collider) {
        audioFx.PlayOne(0);

        PickupItem item = collider.GetComponent<PickupItem>();
        if (item != null) {
            if (item.mode == 0 && item.isActualyPickup) {
                collectedItems.Add(item);
                item.gameObject.SetActive(false);
            }
            else if (collectedItems.Count == maxItemsOnMap) // assume it's a house
                EndGame();
        }
    }
}
