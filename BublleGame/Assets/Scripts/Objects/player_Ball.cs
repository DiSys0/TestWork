using System.Collections.Generic;
using UnityEngine;

public class player_Ball : base_Ball
{
    private float _speed;
    private bool _isEndMove;
    private bool _isMove;
    private Vector2 _currentDirection;
    private Vector2 _currentNormal;
    private Vector2 _endPosition;
    private Vector2 _startPosition;
    private float _progress;
    private RaycastHit2D _hit;

    public bool IsMove => _isMove;



  
    public void OnRun(Vector2 direction)
    {
      _currentDirection = direction;
      CheckOnDirectionBall(true);

    }
    private void Start()
    {
      //_startPosition = transform.position;
      _progress = 0;
      _speed = 25;
    }

    private void OnEnable()
    {
      _isMove = false;
      _isEndMove = false;
      _progress = 0;
      transform.position = new Vector2(0, -7);
      _startPosition = transform.position;     
    }

   
    private void CheckOnDirectionBall(bool isFirstRun)
    { 
      if (!isFirstRun)
      {
        _currentDirection = Vector2.Reflect(_currentDirection, _currentNormal);
      }
     
      _hit = Physics2D.CircleCast(transform.position, 0.5f, _currentDirection, 20f, _ballLayer);
      if (!_hit)
      {
        
        RaycastHit2D[] tempHits = Physics2D.RaycastAll(transform.position, _currentDirection);
        _hit = tempHits[tempHits.Length - 1];    
        _currentNormal = _hit.normal;
        _endPosition = _hit.point;    
      }else
      {
        Vector2 dir = (_hit.point - new Vector2(_hit.transform.position.x, _hit.transform.position.y)).normalized;
        float angle =  Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        _endPosition = new Vector2(_hit.transform.position.x, _hit.transform.position.y) + GetPosition(angle);
        _isEndMove = true;
      }
        _startPosition = transform.position;
        _isMove = true;
    }
  

    private Vector2 GetPosition(float angle)
    {
      if (angle > 180)
      {
        angle -= 360;
      }
      if ((angle >= -30)&&(angle <= 30))
      {
        return new Vector2(1,0);
      }
      if ((angle >= -90)&&(angle <= -30))
      {
        return new Vector2(0.5f, - Mathf.Sqrt(3)/2);
      }
      if ((angle >= -150)&&(angle <= -90))
      {
        return new Vector2(-0.5f, -Mathf.Sqrt(3)/2);
      }
      if (((angle >= -180)&&(angle <= -150))||((angle >= 150)&&(angle <= 180)))
      {
        return new Vector2(-1,0);
      }
      if ((angle >= 90)&&(angle <= 150))
      {
        return new Vector2(-0.5f, Mathf.Sqrt(3)/2);
      }
      if ((angle >= 30)&&(angle <= 90))
      {
        return new Vector2(0.5f, Mathf.Sqrt(3)/2);
      }

      return Vector2.zero;
    }

    private void CheckNearbyBalls()
    {
      List<Transform> hitsParents = new List<Transform>();
      RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1f, Vector2.zero, 1f, _ballLayer);
      
      for (int i = 0; i < hits.Length; i++)
      {  
        if (hits[i].transform.GetComponent<Ball>().Color == _color)
        {
          if (hitsParents.Count == 0)
          {
            hitsParents.Add(hits[i].transform.parent);
          }else
          {
            if (hitsParents[0] != hits[i].transform.parent)
            {
              hitsParents.Add(hits[i].transform.parent);
            }
          }
        }
      }
      switch (hitsParents.Count)
      {
        case 0:
          EventsManager.OnAddBall(transform.position.x, transform.position.y, _color).SetActive(true);
        break;
        case 1:
          if (hitsParents[0].childCount > 1)
          {
            DestroyBalls(hitsParents[0]);
          } else
          {
            EventsManager.OnAddBall(transform.position.x, transform.position.y, _color).SetActive(true);
          }
        break;
        case 2:
          DestroyBalls(hitsParents[0]);
          DestroyBalls(hitsParents[1]);
        break;
      }
      EventsManager.OnNewPlayerBall();
    }

    private void DestroyBalls(Transform ballsParent)
    {
      for (int i = 0; i < ballsParent.childCount; i++)
      {
        ballsParent.GetChild(i).GetComponent<IDamageble>().Damage(); 
      }
      Destroy(ballsParent.gameObject);
    }
    private void Update()
    {
      if (_isMove)
      {
         transform.position = Vector2.MoveTowards(_startPosition, _endPosition, _progress);
         _progress += Time.deltaTime * _speed;
        if (Vector2.Distance(transform.position, _endPosition) == 0)
        {
          _isMove = false;
          _progress = 0;
          if (_isEndMove)
          {
            CheckNearbyBalls();
            _isEndMove = false;
          }else
          {
            CheckOnDirectionBall(false);
          }
        }
      }
    }

    public override void Damage()
    {
        base.Damage();
        EventsManager.OnNewPlayerBall();
    }
}
