using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static event Action NewPlayerBall;
    public static event Func<float, float, int, GameObject> AddBall;
    public static event Action<int> ChangeScore;
    public static event Action<int> StartLevel;
    public static event Action WinLevel;
   
    public static void OnNewPlayerBall()
    {
        NewPlayerBall?.Invoke();
    }

    public static GameObject OnAddBall(float x, float y, int colorBall)
    {
        return AddBall?.Invoke(x, y, colorBall);
    }

    public static void OnChangeScore(int value)
    {
        ChangeScore?.Invoke(value);
    }

    public static void OnStartLevel(int value)
    {
        StartLevel?.Invoke(value);
    }

    public static void OnWinLevel()
    {
        WinLevel?.Invoke();
    }
}
