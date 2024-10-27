using UnityEngine;

/// <summary>
/// Pumpkin attack. Automatically fires every few seconds towards the closest visible enemy
/// </summary>
public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private BulletLogic fireball;
    [SerializeField] private float timeBetweenFiring = 0.5f;
    public float maxDistance = 20f;
    public FMODUnity.EventReference sfx;

    private PlayerController player;
    private float _lastFired = -999f;
    private float _maxDistanceSqr;
    private FMOD.Studio.EventInstance _sfxInstance;

    private void Awake()
    {
        _maxDistanceSqr = maxDistance*maxDistance;
        player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _sfxInstance = FMODUnity.RuntimeManager.CreateInstance(sfx);
    }

    void Update()
    {
        if(Time.time > _lastFired + timeBetweenFiring)
        {
            GameObject closest = GetClosestEnemy();
            if(closest)
            {
                _sfxInstance.start();
                var bullet = Instantiate(fireball, transform.position, Quaternion.identity);
                bullet.AimTowards(closest);
                _lastFired = Time.time;
            }
        }
    }

    private GameObject GetClosestEnemy()
    {
        EnemyCombat[] enemies = FindObjectsOfType<EnemyCombat>();
        GameObject closest = null;
        float closestSqDist = float.MaxValue;
        for(int i = 0; i < enemies.Length; i++)
        {
            float sqDist = Vector2.SqrMagnitude(transform.position - enemies[i].transform.position);
            if(sqDist < closestSqDist && sqDist < _maxDistanceSqr)
            {
                var hit = Physics2D.Raycast(transform.position, enemies[i].transform.position - transform.position, 999f, LayerMask.GetMask("Terrain", "Enemy"));
                if(hit.rigidbody.CompareTag("Enemy"))
                {
                    closestSqDist = sqDist;
                    closest = hit.rigidbody.gameObject;
                }
            }
        }

        return closest;
    }
}
