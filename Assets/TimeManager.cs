using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public void SetTime(float updateTime, float fixedTime)
    {
        Time.timeScale = updateTime;
        Time.fixedDeltaTime = fixedTime;
    }
}
