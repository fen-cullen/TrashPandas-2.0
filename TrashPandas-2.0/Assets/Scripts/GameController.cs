using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score;
    private int remaining;
    private GameObject[] pickups;
    private GameObject scoreDisplay;
    // Start is called before the first frame update
    void Start()
    {
        this.pickups = GameObject.FindGameObjectsWithTag("pickup");
        this.remaining = this.pickups.Length;
        this.scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPickupCollect ()
    {
        --this.remaining;
        ++this.score;
    }
}
