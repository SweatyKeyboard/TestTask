using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifetime;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _rigidbody2D.velocity = transform.right * _speed;
    }

    private void Update()
    {        
        _lifetime -= Time.deltaTime;

        if (_lifetime <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out a_Enemy enemy))
        {
            enemy.Hurt(_damage);
            Destroy(gameObject);
        }
    }

}
