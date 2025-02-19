using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    //FPFPFPFPFPFPFPFPFP
    private int fpScore = 0;
    private int fpBestScore = 0;
    private const string FPBESTSCOREKEY = "FPBestScore";
    private bool isFPGameStart = false;
    public static bool isFPFirstStart = true;
    [SerializeField] private FPUIManager fpUIManager;
    //FPFPFPFPFPFPFPFPFP

    [SerializeField]private BaseUIManager uiManager;

    public static GameManager Instance { get { return gameManager; } }

    private void Awake()
    {
        gameManager = this;
    }

    private void Start()
    {
        fpBestScore = PlayerPrefs.GetInt(FPBESTSCOREKEY, 0);
        if(fpUIManager != null)
        {
            fpUIManager.TextBestScore(fpBestScore);
        }
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
        fpUIManager = FindObjectOfType<FPUIManager>();
    }

    public void FPStartGame()
    {
        fpScore = 0;
        fpUIManager.SetPlayGame();
        Time.timeScale = 1;
    }

    public void FPAddScore()
    {
        fpScore++;
        fpUIManager.UpdateScore(fpScore);
    }

    public int GetFPScore()
    {
        return fpScore;
    }

    public void FPGameOver()
    {
        FPUpdateBestScore();
        fpUIManager.SetGameOver();
    }

    void FPUpdateBestScore()
    {
        if (fpBestScore < fpScore)
        {
            Debug.Log("최고 점수 갱신");
            fpBestScore = fpScore;

            PlayerPrefs.SetInt(FPBESTSCOREKEY, fpBestScore);
        }

        if (fpUIManager != null)
        {
            fpUIManager.TextBestScore(fpBestScore);
            fpUIManager.TextScore(fpScore);
        }
    }
    //FPFPFPFPFPFPFPFPFPFPFPFP
}
