using System.Collections.Generic;
using UnityEngine;

public class LevelCreate : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private GameObject _playerBall;
    private string[] _levelData;
    private List<GameObject> _balls;
    private int _currentLevel;
    private int _ballsCount;

    private void Start()
    {
        EventsManager.NewPlayerBall += NewPlayerBall;
        EventsManager.AddBall += CreateBall;
        EventsManager.ChangeScore += ChangeBallsCount;
    }

    public void Init(int level)
    {
        ClearLevel();
        if (level != 0)
        {
            _currentLevel = level;
        }
        if (level == -1)
        {
            GenerateLevel();
        }else
        {
            _levelData = Resources.Load<TextAsset>("Levels/" + _currentLevel).text.Split("\n"[0]);   
        }
        CreateBalls();
        ActiveBalls();
        NewPlayerBall();
    }

    private void ClearLevel()
    {
        GameObject[] ballParents = GameObject.FindGameObjectsWithTag("BallsParent");
        for (int i = 0; i < ballParents.Length; i++)
        {
            Destroy(ballParents[i].gameObject);
        }   
    }
    private void GenerateLevel()
    {
        int linesCount = Random.Range(8, 12);
        _levelData = new string[linesCount];
        for (int i = 0; i < linesCount; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                _levelData[i] += Random.Range(0,4);    
            }    
        }
    }
    private void ActiveBalls()
    {
        for (int i = 0; i < _balls.Count; i++)
        {
            _balls[i].SetActive(true);
        }
    }
    private GameObject CreateBall(float x, float y, int colorBall)
    {
        GameObject ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        ball.transform.position = new Vector2(x, y);
        ball.GetComponent<base_Ball>().Init(colorBall);  
        _ballsCount ++;     
        return ball;
    }

    private void CreateBalls()
    {
        _balls = new List<GameObject>();
        for (int i = 0; i < _levelData.Length; i++)
        {
            float x = (i % 2) == 0? -4.5f: -5f;
            string currentLine = _levelData[i].Trim();
            for (int j = 0; j < currentLine.Length; j++)
            {
                if (currentLine[j].ToString() != "0")
                {
                    _balls.Add(CreateBall(x+j, 9 - (Mathf.Sqrt(3)/2)*i, int.Parse(currentLine[j].ToString())));
                }
            }
        }
        _ballsCount = _balls.Count;
    }

    private void NewPlayerBall()
    {
        _playerBall.SetActive(false);
        _playerBall.GetComponent<base_Ball>().Init(Random.Range(1,4));
        _playerBall.SetActive(true);
    }

    private void ChangeBallsCount(int value)
    {
        if (value != -1)
        {
            _ballsCount --;
        }
        if (_ballsCount == 0) EventsManager.OnWinLevel();
    }
    private void OnDestroy()
    {
        EventsManager.NewPlayerBall -= NewPlayerBall;
        EventsManager.AddBall -= CreateBall;
        EventsManager.ChangeScore -= ChangeBallsCount;
    }
}
