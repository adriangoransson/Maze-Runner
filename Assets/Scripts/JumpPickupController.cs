using UnityEngine;

public class JumpPickupController : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") {
            return;
        }

        playerController.Jumps++;

        Destroy(gameObject);
    }
}
