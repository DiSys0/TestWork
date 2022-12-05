using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour
{
    private Image _lampImage;
    
    private void Start()
    {
        EventsManager.ServerConnection += ChangeStatus;
        _lampImage = transform.GetComponent<Image>();
    }

    private void ChangeStatus(bool value)
    {
        string lampColor = value? "Green":"Red";
        _lampImage.sprite = Resources.Load<Sprite>("Sprites/Lamp" + lampColor);    
    }

    private void OnDestroy()
    {
        EventsManager.ServerConnection -= ChangeStatus;    
    }
}
