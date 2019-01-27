using UnityEngine;

public class Trap:MonoBehaviour {

    public AudioController audioFx;

    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Enemy") {
            audioFx.monsterSounds.PlayOneShot(audioFx.soundClips[0]);
            // activate trap
            other.GetComponent<Enemy>().StopEffect();
            Destroy(gameObject);
        }
    }
}
