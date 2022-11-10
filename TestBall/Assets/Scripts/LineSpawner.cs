using UnityEngine;

public class LineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    private float _timerSpawner;
    private float _speedLine;
    private int _distanceSpawner;
    private void Start()
    {
        EventsManager.EndGame += EndGame;
        _speedLine = GlobalVar.LevelDifficulty * 2;
        _distanceSpawner = 10;
        _timerSpawner = _distanceSpawner/_speedLine;
        CreateLine();
    }

    private void CreateLine()
    {
       GameObject ball = Instantiate(_linePrefab, new Vector2(10, Random.Range(-5,5)), Quaternion.identity);    
       ball.transform.SetParent(transform);
    }

    private void EndGame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (_timerSpawner > 0)
        {
            _timerSpawner -= Time.deltaTime;
        }else
        {
            _timerSpawner = _distanceSpawner/_speedLine;
            CreateLine();
        }   
    }

    private void OnDestroy()
    {
        EventsManager.EndGame -= EndGame;
    }
}
