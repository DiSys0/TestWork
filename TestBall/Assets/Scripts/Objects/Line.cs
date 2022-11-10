using UnityEngine;

public class Line : MonoBehaviour
{
    private float _speed;
    private void Start()
    {
        _speed = GlobalVar.LevelDifficulty * 2;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamagable>().Damage();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;    
    }
}
