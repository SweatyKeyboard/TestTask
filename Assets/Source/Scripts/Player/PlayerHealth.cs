using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IMortal
{
    [SerializeField] private Image _healthbar;
    [SerializeField] private float _maxHealth;
    [SerializeField] private GameObject _deadWindow;

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            _healthbar.fillAmount = value / _maxHealth;

            if (value <= 0)
            {
                _deadWindow.SetActive(true);
                StartCoroutine(
                    DelayedInvokeWithIntArgument(SceneController.OpenScene, 0, 2f));
            }
        }
    }

    public Image HealthBar => _healthbar;

    public float MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    private void Awake()
    {
        Health = MaxHealth;
    }

    public void Hurt(float damage)
    {
        Health -= damage;
    }

    private IEnumerator DelayedInvokeWithIntArgument(System.Action<int> method, int argument, float delay)
    {
        yield return new WaitForSeconds(delay);
        method?.Invoke(argument);
    }
}
