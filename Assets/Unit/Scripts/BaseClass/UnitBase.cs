using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Interface;
using static UnityEngine.GraphicsBuffer;

public class UnitBase : MonoBehaviour, IAttackable
{
    protected NavMeshAgent agent;

    [Header("ステータス")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float attackPower = 10f;       // 攻撃力
    public float defensePower = 0f;       // 防御力（ダメージ軽減）
    public float attackRange = 4f;        // 攻撃射程
    public float attackCooldown = 1.5f;   // 攻撃間隔（秒）
    protected bool canAttack = true;
    protected IAttackable attackTarget;   //攻撃目標
    public float moveSpeed = 3.5f;        // 移動速度
    public UnitState currentState = UnitState.Idle;//ユニットの状態

    public Team TeamId { get;  set; } = Team.Player;
    public bool IsAlive => currentHealth > 0;
    public Vector3 Position => transform.position;

    // 死亡時に通知するイベント
    public event Action<UnitBase> OnUnitDeath;


    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
        agent.speed = moveSpeed;
    }

    protected virtual void FixedUpdate()
    {
        switch (currentState)
        {
            case UnitState.Idle:
                break;

            case UnitState.Combat:
                if (attackTarget != null && agent.isOnNavMesh)
                {
                    MoveTo(attackTarget.Position);
                    Attack(attackTarget);
                }
                else
                {
                    ChangeState(UnitState.Idle); // ターゲットがいなければ待機に戻す
                }
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

    // IAttackableの実装
    public virtual void TakeDamage(float amount)
    {
        ApplyDamage(amount, null);
    }

    private void ApplyDamage(float amount, IAttackable attacker)
    {
        float damage = Mathf.Max(1f, amount - defensePower);
        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            Die();
        }
        else if (attacker != null)
        {
            SetTarget(attacker); // 反撃させたい場合
        }
        ChangeState(UnitState.Combat);

    }


    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    [SerializeField] private LayerMask attackableMask;
    public virtual void Attack(IAttackable target)
    {
        if (canAttack && target != null && TeamId != target.TeamId)
        {
            Vector3 direction = (target.Position - transform.position).normalized;

            // Ray を飛ばして、間に障害物やターゲットがあるか調べる
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, attackRange, attackableMask))
            {
                // Ray がターゲットに当たったら攻撃
                IAttackable hitTarget = hit.collider.GetComponentInParent<IAttackable>();
                if (hitTarget == target)
                {
                    target.TakeDamage(attackPower);

                    canAttack = false;
                    StartCoroutine(StartCooldown());
                }
            }
        }
    }

    protected virtual void Die()
    {
        // 死亡イベントを呼び出す
        OnUnitDeath?.Invoke(this);
        // 死亡演出など（派生クラスで拡張可能）
        Destroy(gameObject);
    }

    public virtual void SetTarget(IAttackable newTarget)
    {
        // 古いターゲットのイベント解除
        if (attackTarget != null && attackTarget is UnitBase oldUnit)
        {
            oldUnit.OnUnitDeath -= OnTargetDeath;
        }

        attackTarget = newTarget;

        if (attackTarget != null && attackTarget is UnitBase unitTarget)
        {
            unitTarget.OnUnitDeath += OnTargetDeath;
        }

        ChangeState(UnitState.Combat);
    }


    private void OnTargetDeath(UnitBase deadUnit)
    {
        // ターゲット解除して待機に戻す
        if (attackTarget == deadUnit)
        {
            attackTarget = null;
            ChangeState(UnitState.Idle);
        }
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
    Combat,     // 戦闘態勢
    Dead        // 死亡
}

public enum Team
{
    Neutral,
    Player,
    Enemy
}

