using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private a_Enemy[] _enemies;


    [SerializeField] private float _spawnZoneWidth;
    [SerializeField] private float _spawnZoneHeight;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.9f, 0.4f, 0.2f);
        Gizmos.DrawCube(
            transform.position,
            new Vector3(
                _spawnZoneWidth,
                _spawnZoneHeight,
                0));
    }

    public void Spawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn();
        }
    }
    public void
        Spawn()
    {
        Vector3 position =
            (Vector2)transform.position +
            new Vector2(
                Random.Range(-_spawnZoneWidth / 2, _spawnZoneWidth / 2),
                Random.Range(-_spawnZoneHeight / 2, _spawnZoneHeight / 2));

        Instantiate(_enemies[Random.Range(0, _enemies.Length)], position, Quaternion.identity);
    }
}
