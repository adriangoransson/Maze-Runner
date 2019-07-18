using UnityEngine;

public class JumpPickupController : MonoBehaviour
{
    public AudioClip audioClip;

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

        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        playerController.Jumps++;
        Destroy(gameObject);
    }
}
