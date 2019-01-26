using UnityEngine;

public class Bullet:MonoBehaviour {
    public float speed = 10f;
    public Rigidbody rig;
    private void FixedUpdate() {
        rig.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // activate trap
            other.GetComponent<PlayerMovement>().GetDmg(1);
            Destroy(gameObject);
        } else if (other.tag != "Enemy") {
            Destroy(gameObject);
        }
    }
}
