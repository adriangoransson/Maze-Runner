using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        print("Win!");
    }
}
