using UnityEngine.UI;

public interface IMortal
{
    public Image HealthBar { get; }
    public float MaxHealth { get; set; }
    public float Health { get; set; }
    public void Hurt(float damage);
}
