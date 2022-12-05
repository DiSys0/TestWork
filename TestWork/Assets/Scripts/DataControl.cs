using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataControl : MonoBehaviour
{
    [SerializeField] private Text _textServer;
    [SerializeField] private Text _textPort;
    [SerializeField] private Text _textVideo;
    [SerializeField] private VLCPlayerExample _videoPlayer;
    private string [] _dataCongigFile;
    private string _serverAdress;
    private string _portAdress;
    private string _videoAdress;
    private void Start()
    {
        _textServer = _textServer.GetComponent<Text>();
        _textPort = _textPort.GetComponent<Text>();
        _textVideo = _textVideo.GetComponent<Text>();
        _videoPlayer = _videoPlayer.GetComponent<VLCPlayerExample>();
        LoadDataFromConfigFile();    
    }

    private void LoadDataFromConfigFile()
    {
        _dataCongigFile = Resources.Load<TextAsset>("config").text.Split("\n"[0]);
        if (_dataCongigFile.Length >= 3)
        {
            _serverAdress = GetValueFromString(_dataCongigFile[0]);
            _portAdress = GetValueFromString(_dataCongigFile[1]);
            _videoAdress = GetValueFromString(_dataCongigFile[2]);
        }

        SendData();
    }

    private string GetValueFromString(string param)
    {
        return  param.Substring(param.IndexOf(':') + 1).Trim();
    }

    private void SendData()
    {
        _textServer.text = "Сервер: " + _serverAdress;
        _textPort.text = "Порт: " + _portAdress; 
        _textVideo.text = "Видео: " + _videoAdress;
        _videoPlayer.path = _videoAdress;
        EventsManager.OnStartServer("ws://" + _serverAdress + ":" + _portAdress + "/ws");
    }
  
}
