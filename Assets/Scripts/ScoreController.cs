using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _points = 0;
    public void AddPoints()
    {
        _points++;
        SetPoints(_points);
    }

    public void ResetScore()
    {
        SetPoints(0);
    }

    private void SetPoints(int points)
    {
        scoreText.text = points.ToString();
    }
}
