using UnityEngine;
using UnityEngine.Events;

/*
 * Bounds for maze to check player win condition.
 */
public class BoundsChecker : MonoBehaviour
{
    public UnityEvent onExit;

    private void OnTriggerExit(Collider other)
    {
        onExit.Invoke();
    }
}
