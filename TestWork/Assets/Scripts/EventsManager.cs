using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public static event Action<bool> ServerConnection;
    public static event Action<string> StartServer; 
    public static event Action<float> ChangeOdometer;

    public static void OnServerConnection(bool value)
    {
        ServerConnection?.Invoke(value);
    }

    public static void OnStartServer(string adress)
    {
        StartServer?.Invoke(adress);
    }
    public static void OnChangeOdometer(float value)
    {
        ChangeOdometer?.Invoke(value);
    }

}
