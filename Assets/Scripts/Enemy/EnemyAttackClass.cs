using UnityEngine;

public class EnemyAttackClass : MonoBehaviour
{
    [SerializeField] private EnemyStatus enemyStatus;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            var player = other.gameObject.GetComponent<Player>();
            player.CurrentHP -= enemyStatus.Damage;
        }
    }
}
