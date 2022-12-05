using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private GameObject _optionsWindow;
    [SerializeField] private VLCPlayerExample _videoPLayer;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private GameObject _connectionWindow;
    private bool _isVideoOpen;
    
    private void Start()
    {
        EventsManager.ServerConnection += VisibleMenuConnect;
        _videoPLayer = _videoPLayer.GetComponent<VLCPlayerExample>();
    }
    public void OpenOptionsWindow()
    {
        _optionsWindow.SetActive(true);
    }

    public void CloseOptionsWindow()
    {
        _optionsWindow.SetActive(false);
    }

    public void StartVideo()
    {
        _videoPLayer.Open(_videoPLayer.path);
        _isVideoOpen = false;
    }

    private void VisibleMenuConnect(bool value)
    {
        _connectionWindow.SetActive(!value);
    }
    private void Update()
    {
        if ((_videoPLayer.mediaPlayer.Time >0)&&(!_isVideoOpen))
        {
            _isVideoOpen = true;
            if(_videoPLayer.mediaPlayer.Tracks(LibVLCSharp.TrackType.Audio).Count > 0)
            {
                _musicToggle.isOn = false;
            }
        }
    }

    private void OnDestroy()
    {
        EventsManager.ServerConnection -= VisibleMenuConnect;
    }
}
