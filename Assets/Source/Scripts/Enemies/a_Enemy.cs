using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class a_Enemy : MonoBehaviour, IMortal
{
    [Header("Moving")]
    [SerializeField] private float _sightDistance;
    [SerializeField] private float _speed;
    [Header("Attack")]
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _attackDamage;
    [Header("Health")]
    [SerializeField] private Transform _healthBar;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private float _maxHealth;
    [Header("Other")]
    [SerializeField] private DropItem[] _possibleDrop;
    [SerializeField] private Item _itemPrefab;

    private PlayerHealth _target;
    private Rigidbody2D _rigidbody2D;

    private bool _isFlipped;

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            _healthBarFill.fillAmount = value / _maxHealth;

            if (value <= 0)
            {
                Die();
            }
        }
    }

    public Image HealthBar => _healthBarFill;

    private float _lastAttckTime;

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Health = MaxHealth;
    }
    private void Start()
    {
        _target = FindAnyObjectByType<PlayerHealth>();
    }
    private void Update()
    {
        _rigidbody2D.velocity = Vector2.zero;
        if (Vector2.Distance(_target.transform.position, transform.position) < _attackDistance)
        {
            Attack();
            return;
        }


        if (Vector2.Distance(_target.transform.position, transform.position) < _sightDistance)
        {
            Follow();
            return;
        }
    }

    private void Follow()
    {
        _rigidbody2D.velocity = (_target.transform.position - transform.position).normalized * _speed;

        _isFlipped = _rigidbody2D.velocity.x < 0;
        transform.localScale = new Vector3(_isFlipped ? -1 : 1, 1, 1);
        _healthBar.localScale = new Vector3(_isFlipped ? -1 : 1, 1, 1);
    }

    private void Attack()
    {

        if (Time.time > _lastAttckTime + _attackDelay)
        {
            _lastAttckTime = Time.time;
            _target.Hurt(_attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _sightDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }

    public void Hurt(float damage)
    {
        Health -= damage;
    }

    private void Die()
    {
        GetItem();
        Destroy(gameObject);
    }

    private void GetItem()
    {
        Item item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);

        List<int> weightsList = new List<int>();
        int j = 0;
        foreach (DropItem dropItem in _possibleDrop)
        {
            for (int i = 0; i < dropItem.DropWeight; i++)
            {
                weightsList.Add(j);
            }
            j++;
        }
        item.SetItem(_possibleDrop[weightsList[Random.Range(0, weightsList.Count)]]);
    }
}
