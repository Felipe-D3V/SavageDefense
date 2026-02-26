using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 10;
    private GameObject owner;

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy") || other.CompareTag("TankEnemy") || other.CompareTag("GreaterEnemy"))
        {
            other.GetComponent<EnemyLogic>()?.TomarDano();
        }
    }
}