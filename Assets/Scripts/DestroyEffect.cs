using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float destroyDelay = 3;

    private float time;

    void Update()
    {
        time += Time.deltaTime;

        if (time > destroyDelay) {
            Destroy(gameObject);
        }
    }
}
