using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public GameController controller;
    public AudioClip eatSFX;
    public AudioClip pickupSFX;
    public GameObject pickupParticles;

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
        AudioSource.PlayClipAtPoint(eatSFX, Camera.main.transform.position);
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        Instantiate(pickupParticles, transform.position, Quaternion.identity);
        controller.OnPickupCollect();
    }
}
