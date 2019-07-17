using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
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

        controller.GameOver();
    }
}
