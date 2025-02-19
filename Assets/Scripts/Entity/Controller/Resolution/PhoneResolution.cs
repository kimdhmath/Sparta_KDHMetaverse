using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneResolution : MonoBehaviour
{
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(760, 1280, true);
    }
}
