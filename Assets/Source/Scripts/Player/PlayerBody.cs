using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    private Transform _target;


    [SerializeField] private Transform _rightElbow;

    private void Update()
    {
        if (_target == null)
        {
            a_Enemy enemy;
            if (enemy = FindObjectOfType<a_Enemy>())
            {
                _target = enemy.transform;
                return;
            }

            _target = transform;
        }


        RotateGunArmToTarget();
    }

    private void RotateGunArmToTarget()
    {
        Vector3 difference = _target.position - _rightElbow.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _rightElbow.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90f);
    }
}
