using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField] private Transform _ball;
    private LineRenderer _lineDirectionShoot;
    private RaycastHit2D _hit;
    private float _maxDistanceLine = 10f;
    private Vector2 _playerBallPosition;
    private Vector2 _direction;
    private Vector2 _point;
    private bool _activeClick;

    private void Start()
    {
        _playerBallPosition = new Vector2(0, -7);
        _lineDirectionShoot = GetComponent<LineRenderer>();
        _lineDirectionShoot.SetPosition(0, _playerBallPosition);
    }
    void RayCreate()
    {
        if(_direction.y > _playerBallPosition.y)
        {
        _direction -= _playerBallPosition;
        _hit = Physics2D.Raycast(_playerBallPosition, _direction, 20f);
        if (_hit)
        {
            _point = _hit.point;
            Vector2 reflect = Vector2.Reflect(_direction.normalized, _hit.normal);
            if (_hit.transform.tag != "Ball")
            {
                if (_hit.distance > _maxDistanceLine)
                {
                    _lineDirectionShoot.positionCount = 2;
                    _lineDirectionShoot.SetPosition(1, _direction.normalized * _maxDistanceLine + _playerBallPosition );
                }else
                {
                    _lineDirectionShoot.positionCount = 3;
                    _lineDirectionShoot.SetPosition(1, _hit.point );
                    _lineDirectionShoot.SetPosition(2, (reflect * (_maxDistanceLine - _hit.distance)) + _hit.point);
                }
            }else
            {
                _lineDirectionShoot.positionCount = 2;
                _lineDirectionShoot.SetPosition(1, _point);    
            }       
        } 
        }else
        {
            _lineDirectionShoot.positionCount = 1;
            _direction = Vector2.zero;
        }
    }

    private void RunBall()
    {
        if (_activeClick)
        {
            if (_direction != Vector2.zero)
            {
                _lineDirectionShoot.positionCount = 1;
                _ball.GetComponent<player_Ball>().OnRun(_direction.normalized);
            }
            _activeClick = false;
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void Update()
    {
        if (((_ball.position.x == _playerBallPosition.x)&&(_ball.position.y == _playerBallPosition.y))&&(!IsPointerOverUIObject()))
        {
            #if UNITY_ANDROID
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _direction = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    RayCreate();      
                    _activeClick = true;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    RunBall();
                }
            }
            #endif

            #if UNITY_EDITOR_WIN
            if (Input.GetMouseButton(0))
            {
                _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);   
                RayCreate();
                _activeClick = true;
            }
    
            if (Input.GetMouseButtonUp(0))
            {
                RunBall();
            }
            #endif    
        }
    }

}
