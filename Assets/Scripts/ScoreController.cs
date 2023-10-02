using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreCount;
    private int _points = 0;
    public void AddPoints()
    {
        _points += 1;
        SetPoints(_points);
    }

    public void ResetScore()
    {
        SetPoints(0);
    }

    private void SetPoints(int points)
    {
        scoreCount.text = points.ToString();
    }
}
