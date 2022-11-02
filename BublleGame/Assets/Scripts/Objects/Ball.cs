using UnityEngine;

public class Ball : base_Ball
{
    [SerializeField] GameObject _parentPrefab;
  

    private void OnEnable()
    {
        ConnectBall();
    }
    public void ConnectBall()
    {
        if (transform.position.y == 9)
        {
            gameObject.AddComponent<FixedJoint2D>();
        }
        RaycastHit2D[] _hits = Physics2D.CircleCastAll(transform.position, 1f, Vector2.zero, 0f, _ballLayer);
        if (_hits.Length > 1)
        {
            for (int i = 0; i < _hits.Length; i++)
            {
                if (gameObject != _hits[i].collider.gameObject)
                {
                    if (transform.position.y < 9)
                    {
                        gameObject.AddComponent<FixedJoint2D>().connectedBody = _hits[i].transform.GetComponent<Rigidbody2D>();
                    }
                    if(_hits[i].transform.GetComponent<base_Ball>().Color == _color)
                    {
                        if (transform.parent == null)
                        {
                            transform.parent = _hits[i].transform.parent;    
                        }else
                        {
                            if (transform.parent != _hits[i].transform.parent)
                            {
                                Transform destroyParent = _hits[i].transform.parent;
                                int childCount = destroyParent.childCount;
                                for (int j = 0; j < childCount; j++)
                                {
                                    destroyParent.GetChild(0).parent = transform.parent;   
                                }
                                Destroy(destroyParent.gameObject);
                            }
                        }
                    }
                }    
            }
        }
        if (transform.parent == null)
        {
            GameObject parentBall = Instantiate(_parentPrefab, Vector2.zero, Quaternion.identity);
            transform.parent = parentBall.transform;
        }
     
    }
}
