using UnityEngine;

public class Enemy_ZombieAnim : MonoBehaviour
{
    EnemyClass enemyClass;

    private void Start()
    {
        enemyClass = GetComponent<EnemyClass>();
    }

    void AttackStart()
    {
        enemyClass.AttackPos.SetActive(true);
    }

    void AttackEnd()
    {
        enemyClass.AttackPos.SetActive(false);
    }

    void End()
    {
        enemyClass.OnAttack = false;
        enemyClass.moveSpeed = enemyClass.BaseMoveSpeed;
    }
}
