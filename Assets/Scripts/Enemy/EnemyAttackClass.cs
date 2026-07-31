using UnityEngine;

public class EnemyAttackClass : MonoBehaviour
{
    [SerializeField] private EnemyStatus enemyStatus;
    [SerializeField] private EnemyClass enemyclass;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            var player = other.gameObject.GetComponent<Player>();
            Instantiate(enemyclass.HitEffect,player.HitPos.transform.position, Quaternion.identity);
            player.CurrentHP -= enemyStatus.Damage;
        }
    }
}
