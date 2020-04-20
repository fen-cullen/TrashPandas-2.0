using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int totalTrashCollected;
    private int score;
    private int remaining;
    private GameObject[] pickups;
    public Text scoreDisplay;
    public Text winMessage;
    public HealthController playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        //this.pickups = GameObject.FindGameObjectsWithTag("pickup");
        this.remaining = GameObject.FindGameObjectsWithTag("pickup").Length;
        //this.scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Collected: " + score + " / " + remaining;
    }

    public void TakeDamage(int amount) {
        playerHealth.TakeDamage(amount);
    }

    public void LoadNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnPickupCollect ()
    {
        //--this.remaining;
        ++this.score;
        ++TotalScoreKeeper.totalScore;

        if (score == remaining) {
            winMessage.gameObject.SetActive(true);
            Invoke("LoadNextLevel", 5f);
        }
    }

    public void OnHealthPickup()
    {
        this.playerHealth.Heal(1);
    }
}
