using UnityEngine;

/*
 * Bounds for maze to check player win condition.
 */
public class BoundsChecker : MonoBehaviour
{
    private GameController controller;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        controller = go.GetComponent<GameController>();
    }

    private void OnTriggerExit(Collider other)
    {
        controller.WinGame();
    }
}
