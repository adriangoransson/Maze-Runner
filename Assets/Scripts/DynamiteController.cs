using UnityEngine;

public class DynamiteController : MonoBehaviour
{
    public GameObject explosion;
    public float blastRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") {
            return;
        }

        // Boom!
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        Destroy(gameObject);
        foreach (Collider c in colliders) {
            if (c.tag == "Wall") {
                Destroy(c.gameObject);
                // Put the explosion on the ground
                Vector3 pos = new Vector3(c.transform.position.x, 0, c.transform.position.z);
                Instantiate(explosion, pos, c.transform.rotation);
            }
        }
    }
}
