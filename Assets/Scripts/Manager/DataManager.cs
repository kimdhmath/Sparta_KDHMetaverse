using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    static DataManager dataManager;
    public static DataManager Instance { get { return dataManager; } }

    public const string TSBESTSCOREKEY = "TSBestScore";
    public const string FPBESTSCOREKEY = "FPBestScore";

    private int fpScore = 0;
    public int FPScore
    {
        get { return fpScore; }
        set
        {
            fpScore = value;
            if (fpScore > fpBestScore)
            {
                fpBestScore = fpScore;
                PlayerPrefs.SetInt(FPBESTSCOREKEY, fpBestScore);
            }
        }
    }
    private int fpBestScore = 0;
    public int FPBestScore { get { return fpBestScore; } }
    
    private int tsScore = 0;
    public int TSScore 
    {
        get { return tsScore; }
        set
        {
            tsScore = value;
            if (tsScore > tsBestScore)
            {
                tsBestScore = tsScore;
                PlayerPrefs.SetInt(TSBESTSCOREKEY, tsBestScore);
            }
        }
    }
    private int tsBestScore = 0;
    public int TSBestScore { get { return tsBestScore; } }

    private void Awake()
    {
        if (dataManager == null)
        {
            dataManager = this; 
            DontDestroyOnLoad(gameObject);
        }
        fpBestScore = PlayerPrefs.GetInt(FPBESTSCOREKEY, 0);
        tsBestScore = PlayerPrefs.GetInt(TSBESTSCOREKEY, 0);
    }

}
