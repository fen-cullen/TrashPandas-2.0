using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    //float tempTimer = 0;

    public Sprite h0;
    public Sprite h1;
    public Sprite h2;
    public Sprite h3;
    public Sprite h4;

    public int numHearts = 4;

    public AudioSource hurtSoundEffect;
    public GameObject playerDeathObject;
    public GameObject deathMessage;

    // Start is called before the first frame update
    void Start()
    {
        this.numHearts = 4;
        //this.tempTimer = 0;
        this.UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        if (this.numHearts > 0)
        {
            this.numHearts -= amount;
            hurtSoundEffect.Play();

            if(this.numHearts <= 0)
            {
                Death();
            }
            this.UpdateHearts();
        }
    }

    public void Heal(int amount)
    {
        if (this.numHearts > 0)
        {
            this.numHearts += amount;
            if (this.numHearts > 4)
            {
                this.numHearts = 4;
            }
            this.UpdateHearts();
        }
    }

    private void UpdateHearts()
    {
        var temp = this.GetComponent<Image>();
        switch (this.numHearts)
        {
            case (0):
                temp.sprite = this.h0;
                print("player died");
                return;
            case (1):
                temp.sprite = this.h1;
                return;
            case (2):
                temp.sprite = this.h2;
                return;
            case (3):
                temp.sprite = this.h3;
                return;
            case (4):
                temp.sprite = this.h4;
                return;
            default:
                temp.sprite = this.h4;
                return;
        }
    }

    public void Death() {
        deathMessage.SetActive(true);

        GameObject p = GameObject.Find("Player");
        Instantiate(playerDeathObject, p.transform.position, p.transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("Player"));

        Invoke("ReloadScene", 5f);
    }

    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
