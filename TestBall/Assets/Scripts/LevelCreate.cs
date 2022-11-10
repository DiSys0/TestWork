using UnityEngine;

public class LevelCreate : MonoBehaviour
{
    [SerializeField] private GameObject _lineSpawner;
    [SerializeField] private GameObject _ballPrefab;
    private int _level;
    public void Init()
    {
        LoadGame();
    }
   
    private void LoadGame()
    {
        _lineSpawner.SetActive(true);
        Instantiate(_ballPrefab, _ballPrefab.transform.position, Quaternion.identity);
    }
}
