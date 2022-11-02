using UnityEngine;

public abstract class base_Ball : MonoBehaviour, IDamageble
{
    protected Rigidbody2D _rigibody;
    protected int _color;
    protected int _ballLayer = 1 << 7;
    public int Color => _color;
    
  
    public  void Init(int color)
    {
        _color = color;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Balls/Ball" + _color);
        _rigibody = GetComponent<Rigidbody2D>();
    }

    public virtual void Damage()
    {
        if (TryGetComponent(out player_Ball player))
        {
            EventsManager.OnChangeScore(-1);
        }else
        {
            EventsManager.OnChangeScore(_color);
        }
    }
}
