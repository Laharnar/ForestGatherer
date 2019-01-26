using UnityEngine;

public class AnimationEventTriggers:MonoBehaviour {



    public void DoDmgToPlayer() {
        GameManager.m.player.GetComponent<PlayerMovement>().GetDmg(1);
        //specific*
        gameObject.GetComponentInParent<IAnimEventReciever>().OnRecieve();
    }
}