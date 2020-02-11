using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public GameController controller;

    void Start()
    {
        if (controller == null) {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        controller.OnPickupCollect();
    }
}
