using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    [Header("ステータス")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float attackPower = 10f;       // 攻撃力
    public float defensePower = 0f;       // 防御力（ダメージ軽減）
    public float attackRange = 2f;        // 攻撃射程
    public float attackCooldown = 1.5f;   // 攻撃間隔（秒）
    protected bool canAttack = true;
    protected UnitBase attackTarget;

    public float moveSpeed = 3.5f;        // 移動速度

    public UnitState currentState = UnitState.Idle;//ユニットの状態



    [Header("陣営情報")]
    public int teamId = 0; // 0: プレイヤー, 1: 敵

    [Header("選択状態")]
    public bool isSelected = false;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        agent.speed = moveSpeed;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                break;

            case UnitState.Combat:
                MoveTo(attackTarget.gameObject.transform.position);
                Attack(attackTarget);
                break;

            case UnitState.Moving:
                break;

            case UnitState.Dead:
                break;
        }
    }

    public virtual void MoveTo(Vector3 position)
    {
        if (agent != null)
            agent.SetDestination(position);
    }

    public virtual void TakeDamage(float amount)
    {
        ChangeState(UnitState.Combat);
        float damage = Mathf.Max(1f, amount - defensePower); // 最低1ダメージ
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    public virtual void Attack(UnitBase target)
    {
        if (canAttack && target != null)
        {
            ChangeState(UnitState.Combat);
            // 射程内チェック
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= attackRange)
            {
                target.TakeDamage(attackPower);
                target.attackTarget = this;//相手に攻撃したことを知らせる
                canAttack=false;
                StartCoroutine(StartCooldown());
            }
        }
    }





    protected virtual void Die()
    {
        // 死亡演出など（派生クラスで拡張可能）
        Destroy(gameObject);
    }

    public virtual void SetSelected(bool selected)
    {
        isSelected = selected;
        // 選択時のエフェクトなど表示もここで
    }

    public void ChangeState(UnitState state)
    {
        currentState = state;
    }


}

public enum UnitState
{
    Idle,       // 待機中
    Moving,     // 移動中
    Combat,     // 戦闘態勢（敵を攻撃している or 攻撃対象を探している）
    Dead        // 死亡
}
