using UnityEngine;

public class Ball : MonoBehaviour, IDamagable
{
    private float _speed;
    private float _yDirection;
    private float _timerForIcreaseSpeed;

    public void Damage()
    {
        EventsManager.OnEndGame();
        Destroy(gameObject);
    }

    private void Start()
    {
        _speed = 3;
        _timerForIcreaseSpeed = 15;
        _yDirection = 0;
    }
    
    private void OnBecameInvisible()
    {
        Damage();
    }
    private void Update()
    {
        _timerForIcreaseSpeed -= Time.deltaTime;
        if (_timerForIcreaseSpeed <= 0)
        {
            _timerForIcreaseSpeed = 15;
            _speed ++;
        }
        transform.Translate(new Vector2(0,_yDirection) * _speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_yDirection < 1)
            {
                _yDirection += Time.deltaTime;
            }
        } else
        {
            if (_yDirection > -1)
            {
                _yDirection -= Time.deltaTime;
            }    
        }
    }
}
