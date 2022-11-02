using UnityEngine;

public class WallDownControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IDamageble ball))
        {
            ball.Damage();
        }
    }
}
