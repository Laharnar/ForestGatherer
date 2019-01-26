using UnityEngine;

public class Trap:MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Enemy") {
            // activate trap
            other.GetComponent<Enemy>().StopEffect();
            Destroy(gameObject);
        }
    }
}
