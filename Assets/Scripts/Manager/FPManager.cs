using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPManager : MonoBehaviour
{
    public static int score = 0;
    public static bool isGameOver = false;
    public static bool isGameStart = false;

    public void Start()
    {
        isGameStart = true;
    }

}
