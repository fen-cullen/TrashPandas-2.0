using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupControl : MonoBehaviour
{
    public GameController controller;
    public AudioClip pickupSFX;
    public GameObject pickupParticles;

    void Start()
    {
        if (controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        controller.OnHealthPickup();
    }
}
