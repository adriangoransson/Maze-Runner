using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speedMultiplier = 1;

    private Vector3 rotate;

    private void Start()
    {
        rotate = new Vector3(0, 50, 0);
    }

    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime * speedMultiplier, Space.World);
    }
}
