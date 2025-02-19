using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    //FPFPFPFPFPFPFPFPFP
    private int fpScore = 0;
    private bool isFPGameStart = false;
    public bool isFPFirstStart = true;
    //FPFPFPFPFPFPFPFPFP

    [SerializeField]private BaseUIManager uiManager;

    public static GameManager Instance { get { return gameManager; } }

    private void Awake()
    {
        gameManager = this;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void ExitMiniGame()
    {
        SceneManager.LoadScene("MetarverseScene");
    }


    //FPFPFPFPFPFPFPFPFP
    public void FPUISet()
    {
        uiManager = FindObjectOfType<FPUIManager>();
    }

    public void FPStartGame()
    {
        fpScore = 0;
        uiManager.SetPlayGame();
        Time.timeScale = 1;
    }

    public void FPAddScore()
    {
        fpScore++;
    }

    public int GetFPScore()
    {
        return fpScore;
    }

    public void FPGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //FPFPFPFPFPFPFPFPFPFPFPFP
}
