using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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
    public static bool isStop = false;

    //FPFPFPFPFPFPFPFPFP
    public static bool isFPFirstStart = true;
    [SerializeField] private FPUIManager fpUIManager;
    //FPFPFPFPFPFPFPFPFP

    //TSTSTSTSTSTSTSTS
    public static bool isTSFirstStart = true;
    [SerializeField] private TSUIManager tsUIManager;
    //TSTSTSTSTSTSTSTS

    public static GameManager Instance { get { return gameManager; } }

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (fpUIManager != null)
        {
            fpUIManager.TextBestScore(DataManager.Instance.FPBestScore);
        }

        if (tsUIManager != null)
        {
            tsUIManager.TextBestScore(DataManager.Instance.TSBestScore);
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
        DataManager.Instance.FPScore = 0;
        fpUIManager.SetPlayGame();
        Time.timeScale = 1;
    }

    public void FPAddScore()
    {
        fpUIManager.UpdateScore(++DataManager.Instance.FPScore);
    }

    public void FPGameOver()
    {
        FPUpdateBestScore();
        fpUIManager.SetGameOver();
    }

    public void FPUpdateBestScore()
    {
        if (fpUIManager != null)
        {
            fpUIManager.TextBestScore(DataManager.Instance.FPBestScore);
            fpUIManager.TextScore(DataManager.Instance.FPScore);
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
        DataManager.Instance.TSScore = 0;
        tsUIManager.SetPlayGame();
        Time.timeScale = 1;
    }

    public void TSUpadateScore(int score)
    {
        DataManager.Instance.TSScore = score;
        tsUIManager.UpdateScore(score);
    }

    public void TSGameOver()
    {
        TSUpdateBestScore();
        tsUIManager.SetGameOver();
    }

    public void TSUpdateBestScore()
    {
        if (tsUIManager != null)
        {
            tsUIManager.TextBestScore(DataManager.Instance.TSBestScore);
            tsUIManager.TextScore(DataManager.Instance.TSScore);
        }
    }
    //TSTSTSTSTSTSTSTS
}
