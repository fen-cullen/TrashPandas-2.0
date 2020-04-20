using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalScoreKeeper : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static int totalScore;

    void Start()
    {
        scoreText.text = "Trash Collected: " + totalScore + "!";
    }
}
