using UnityEngine;

public class DynamiteController : MonoBehaviour
{
    public float blastRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") {
            return;
        }

        // Boom!
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider c in colliders) {
            if (c.tag == "Wall") {
                Destroy(c.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
