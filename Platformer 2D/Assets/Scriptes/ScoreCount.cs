using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    public int score;
    public static ScoreCount Instance;
    private void Start()
    {
        Instance = this;
        score = 0;
        scoreText.text = score.ToString();
    }
    public void AddCrystal(int count)
    {
        score += count;
        scoreText.text = score.ToString();
    }
}
