using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private LevelCreate _levelCreate;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _endGameMenu;
    [SerializeField] private Text _gameInfo;
    private float _timerPlayGame;
    private bool _isPlayGame;

    public void StartGame()
    {
        GlobalVar.GameRoundCount ++;
        PlayerPrefs.SetInt("GameRound", GlobalVar.GameRoundCount);
        _mainMenu.SetActive(false);
        _endGameMenu.SetActive(false);
        _levelCreate.Init();
        _timerPlayGame = 0;
        _isPlayGame = true;
    }
    
    public void NextDifficul()
    {
        if (GlobalVar.LevelDifficulty == 3)
        {
            GlobalVar.LevelDifficulty = 1;
        }else
        {
            GlobalVar.LevelDifficulty ++;
        }

        EventsManager.OnChangeDifficulLevel();
    }

    private void Start()
    {
        EventsManager.EndGame += EndGame;   
        _gameInfo = _gameInfo.GetComponent<Text>(); 
    }

    private void EndGame()
    {
        _isPlayGame = false;
        _gameInfo.text = "Время игры - " + _timerPlayGame.ToString("F1") + " сек.  " + "Попытка - " + GlobalVar.GameRoundCount;
        _endGameMenu.SetActive(true);
    }
    private void Update()
    {
        if (_isPlayGame)
        {
            _timerPlayGame += Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        EventsManager.EndGame -= EndGame;
    }
}
