
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour
{
       
    [Inject] private ScoreController _scoreController;
    [Inject] private BlockSpawner _blockSpawner;
    [SerializeField] private TextMeshProUGUI resultText;

    
    public bool GameStarted { get; private set;}

    private void Start()    
    {
        StartGame();
    }
    public void StartGame()
    {
        resultText.text = "";
        _scoreController.ResetScore();
        _blockSpawner.SpawnField();

        GameStarted = true;

    }
    public void Win()
    {
        GameStarted = false;
        resultText.text = "You Win!";

    }
    public void Lose()
    {
        GameStarted = false;
        resultText.text = "You Lose!";

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
