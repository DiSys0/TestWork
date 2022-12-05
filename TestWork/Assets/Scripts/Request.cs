using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

public class Request : MonoBehaviour
{
    private class Json
    {
        public string operation;
        public float value;
    }
    private ClientWebSocket _clientWS;
    private Task _connect;
    private Uri _serverUri;
    private string _msg;
    private Task _send;
    private Task _get = null;
    private bool _isConnectServer;
    private Json _resultJson;
    private bool _isStartServer;

   
    private void Awake()
    {    
        EventsManager.StartServer += StartServer;
    }

    private void StartServer(string adress)
    {
        _serverUri = new Uri(adress);
        _connect = Connect(_serverUri);  
        _isStartServer = true; 
    } 

    private async Task Connect(Uri uri)
    {
        try
        {
            _clientWS = new ClientWebSocket();
            await _clientWS.ConnectAsync(uri, CancellationToken.None);  
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    private async Task Send(string message)
    {
        var encoded = Encoding.UTF8.GetBytes(message);
        var buffer = new ArraySegment<Byte>(encoded, 0, encoded.Length);

        await _clientWS.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task Get()
    {
        WebSocketReceiveResult result;
        result = null;
        ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
        try
        {
            result = await _clientWS.ReceiveAsync(bytesReceived, CancellationToken.None);   
        }
        finally
        {
            _resultJson = JsonUtility.FromJson<Json>(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            EventsManager.OnChangeOdometer(_resultJson.value);
            _get.Dispose();
        }
    }
    private  void Update()
    {
        if (_isStartServer)
        {
        if (_connect.Status == TaskStatus.RanToCompletion)
        {
            if (_clientWS.State == WebSocketState.Open)
            {
                if (!_isConnectServer)
                {
                    _isConnectServer = true;
                    EventsManager.OnServerConnection(_isConnectServer);
                }
                if (_get == null)
                {
                    _get = Get();
                }else
                {
                    if(_get.Status == TaskStatus.Faulted)
                    {
                        _get = Get();
                    }
                }
            }else
            {
                if (_isConnectServer)
                {
                    _isConnectServer = false;
                    EventsManager.OnServerConnection(_isConnectServer);
                }
            }
        }
        }
    }
    private void OnDestroy()
    {
        EventsManager.StartServer -= StartServer;
        if (_clientWS != null)
        {
            _clientWS.Dispose();
        }
    }

}
