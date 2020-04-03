using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 1;
    public int currHealth;
    public AudioClip deathSFX;

    void Start()
    {
        currHealth = startingHealth;
    }

    public void TakeDamage(int amt)
    {
        if (currHealth > 0)
        {
            currHealth -= amt;
        }

        if (currHealth <= 0)
        {
            //EnemyDies();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Enemy hit?");
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Enemy take damage");
            TakeDamage(1);
        }
    }
}
