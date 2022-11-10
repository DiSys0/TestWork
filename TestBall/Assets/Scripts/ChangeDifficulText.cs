using UnityEngine;
using UnityEngine.UI;

public class ChangeDifficulText : MonoBehaviour
{
    private string[] _namesDifficul = new string[]{"Легкая", "Средняя", "Тяжелая"};
    private Text _nameText;
    private void Start()
    {
        EventsManager.ChangeDifficulLevel += ChangeText;   
        _nameText = GetComponent<Text>(); 
        ChangeText();
    }

    private void ChangeText()
    {
        _nameText.text = _namesDifficul[GlobalVar.LevelDifficulty-1] + " сложность";
    }

    private void OnDestroy()
    {
        EventsManager.ChangeDifficulLevel -= ChangeText;
    }
 
}
