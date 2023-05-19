using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shotPos;
    [SerializeField] private float _shotDelay;
    [SerializeField] private Image _shotIcon;

    [SerializeField] private Transform _playerTransform;

    private float _lastShotTime;

    private void Update()
    {
        if (Time.time < _lastShotTime + _shotDelay)
        {
            _shotIcon.fillAmount = (Time.time - _lastShotTime) / (_shotDelay);
        }
        else
        {
            _shotIcon.fillAmount = 1;
        }
    }
    public void Shot()
    {
        if (Time.time > _lastShotTime + _shotDelay)
        {
            _lastShotTime = Time.time;
            Instantiate(
                _bulletPrefab,
                _shotPos.position,
                transform.rotation * Quaternion.Euler(0,0, _playerTransform.localScale.x < 0 ? 180 : 0));
        }

    }
}