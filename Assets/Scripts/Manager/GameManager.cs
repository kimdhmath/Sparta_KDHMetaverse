using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneState
{
    Meta,
    FP,
    TS
}


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

    //TSTSTSTSTSTSTSTS
    private int tsScore = 0;
    private int tsBestScore = 0;
    private const string TSBESTSCOREKEY = "TSBestScore";
    private bool isTSGameStart = false;
    public static bool isTSFirstStart = true;
    [SerializeField] private TSUIManager tsUIManager;
    //TSTSTSTSTSTSTSTS

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

        tsBestScore = PlayerPrefs.GetInt(TSBESTSCOREKEY, 0);
        if (tsUIManager != null)
        {
            tsUIManager.TextBestScore(tsBestScore);
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void SceneLoad(SceneState sceneState)
    {
        switch(sceneState)
        {
            case SceneState.Meta:
                SceneManager.LoadScene("MetarverseScene");
                break;
            case SceneState.FP:
                SceneManager.LoadScene("FlappyPlaneScene");
                break;
            case SceneState.TS:
                SceneManager.LoadScene("TheStackScene");
                break;
        }
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

    //TSTSTSTSTSTSTSTS
    public void TSUISet()
    {
        tsUIManager = FindObjectOfType<TSUIManager>();
    }

    public void TSStartGame()
    {
        tsScore = 0;
        tsUIManager.SetPlayGame();
        Time.timeScale = 1;
    }

    public void TSUpadateScore(int score)
    {
        tsScore = score;
        tsUIManager.UpdateScore(score);
    }

    public int GetTSScore()
    {
        return tsScore;
    }

    public void TSGameOver()
    {
        TSUpdateBestScore();
        tsUIManager.SetGameOver();
    }

    public void TSUpdateBestScore()
    {
        if (tsBestScore < tsScore)
        {
            Debug.Log("최고 점수 갱신");
            tsBestScore = tsScore;

            PlayerPrefs.SetInt(TSBESTSCOREKEY, tsBestScore);
        }

        if (tsUIManager != null)
        {
            tsUIManager.TextBestScore(tsBestScore);
            tsUIManager.TextScore(tsScore);
        }
    }
    //TSTSTSTSTSTSTSTS
}
