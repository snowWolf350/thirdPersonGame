using UnityEngine;

public class wall : MonoBehaviour,IDamagable
{
    [SerializeField]Transform _brokenWall;

    HealthSystem healthSystem;

    Vector3 lastDamagePosition;

    private void Start()
    {
        healthSystem = new HealthSystem(40);
        healthSystem.onDeath += HealthSystem_onDeath;
    }

    private void HealthSystem_onDeath(object sender, System.EventArgs e)
    {
        Transform brokenWall = Instantiate(_brokenWall, transform.position, Quaternion.identity);
        foreach (Transform child in brokenWall)
        {
            child.GetComponent<Rigidbody>().AddExplosionForce(100, lastDamagePosition, 2f);
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount, Vector3 damagePoint)
    {
        healthSystem.TakeDamage(damageAmount);
        lastDamagePosition = damagePoint;
    }
}
