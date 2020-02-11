using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int score;
    private int remaining;
    private GameObject[] pickups;
    public Text scoreDisplay;
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

    public void OnPickupCollect ()
    {
        //--this.remaining;
        ++this.score;
    }
}
