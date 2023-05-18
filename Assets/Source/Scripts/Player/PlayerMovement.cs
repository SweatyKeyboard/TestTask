using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Joystick _joystick;
    private Rigidbody2D _rigidbody2D;

    private Vector2 _moveDirection;
    private Vector2 MoveDirection => _moveDirection.magnitude > 1 ? _moveDirection.normalized : _moveDirection;

    private bool _isFlipped;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _moveDirection = _joystick.Direction;
        _isFlipped = _moveDirection.x < 0;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = MoveDirection * _speed;
        transform.localScale = new Vector3(_isFlipped ? -1 : 1, 1, 1);
    }

    /*public Vector2 GetCursorDirection(Transform transform)
    {
        Vector2 heading = (_mousePosition - (Vector2)transform.position);
        float distance = heading.magnitude;
        Vector2 direction = heading / distance;
        return direction;
    }*/
}
