using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static event Action ChangeDifficulLevel;
    public static event Action EndGame;

    public static void OnChangeDifficulLevel()
    {
        ChangeDifficulLevel?.Invoke();
    }

    public static void OnEndGame()
    {
        EndGame?.Invoke();
    }
}
