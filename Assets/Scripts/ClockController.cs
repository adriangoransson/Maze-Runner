using UnityEngine;

public class ClockController : MonoBehaviour
{
    public AudioClip audioClip;

    private GameController controller;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        controller = go.GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") {
            return;
        }

        AudioSource.PlayClipAtPoint(audioClip, transform.position);

        controller.DecreaseTime();
        Destroy(gameObject);
    }
}
