using UnityEngine;
using UnityEngine.UI;

public class SelectLevelController : UI_Controller
{
    [SerializeField] private Transform _panelButtons;
    [SerializeField] private GameObject _buttonPrefab;
    private void Start()
    {
        CreateButtonsLevel();
    }

    private void CreateButtonsLevel()
    {
        int currentLevel = 1;
        TextAsset txt = Resources.Load<TextAsset>("Levels/" + currentLevel);
        while (txt != null)
        {
            
            GameObject button = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(_panelButtons);
            button.transform.localScale = new Vector2(1,1);
            Text textButton = button.transform.GetChild(0).GetComponent<Text>();
            textButton.text = currentLevel.ToString();
            button.GetComponent<Button>().onClick.AddListener(() => EventsManager.OnStartLevel(int.Parse(textButton.text)));
            currentLevel ++;
            txt = Resources.Load<TextAsset>("Levels/" + currentLevel);
        }
      
    }
}
