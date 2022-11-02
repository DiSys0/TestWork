using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private GameObject _pauseDialog;
    [SerializeField] private GameObject _mainMenuLayer;
    [SerializeField] private LevelCreate _levelCreateObject;
    [SerializeField] private GameObject _selectLevelMenu;
    [SerializeField] private GameObject _winMenu;
    
    private void Start()
    {
        EventsManager.ChangeScore += ChangeScore;
        EventsManager.StartLevel += StartGame;
        EventsManager.WinLevel += WinGame;
        _score = _score.GetComponent<Text>();
    }

    private void ChangeScore(int value)
    {
        _score.text = (value + int.Parse(_score.text)).ToString();
    }

    private void OnDestroy()
    {
        EventsManager.ChangeScore -= ChangeScore;
        EventsManager.StartLevel -= StartGame;
        EventsManager.WinLevel -= WinGame;
    }

    private void WinGame()
    {
        _winMenu.SetActive(true);
    }
    public void ButtonPauseClick(bool isOn)
    {
        _pauseDialog.SetActive(isOn);
    }

    public void StartGame(int level)
    {
        _levelCreateObject.Init(level);
        _score.text = "0";
        _mainMenuLayer.SetActive(false);
    }

    public void RestartGame()
    {
        StartGame(0);
        _winMenu.SetActive(false);
        _pauseDialog.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        _selectLevelMenu.SetActive(false);
        _mainMenuLayer.SetActive(true);
        _pauseDialog.SetActive(false);
        _winMenu.SetActive(false);
    }

    public void SelectLevelMenuActive(bool isOn)
    {
        _selectLevelMenu.SetActive(isOn);
    }
}
