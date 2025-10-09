using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Interface;
using static UnityEngine.GraphicsBuffer;

public class UnitBase : MonoBehaviour, IAttackable
{
    protected NavMeshAgent agent;

    [Header("�X�e�[�^�X")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float attackPower = 10f;       // �U����
    public float defensePower = 0f;       // �h��́i�_���[�W�y���j
    public float attackRange = 4f;        // �U���˒�
    public float attackCooldown = 1.5f;   // �U���Ԋu�i�b�j
    protected bool canAttack = true;
    protected IAttackable attackTarget;
    public float moveSpeed = 3.5f;        // �ړ����x
    public UnitState currentState = UnitState.Idle;//���j�b�g�̏��

    public Team TeamId { get;  set; } = Team.Player;
    public bool IsAlive => currentHealth > 0;
    public Vector3 Position => transform.position;

    //[Header("�I�����")]
    //public bool isSelected = false;

    // ���S���ɒʒm����C�x���g
    public event Action<UnitBase> OnUnitDeath;


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
                if (attackTarget != null && agent.isOnNavMesh)
                {
                    MoveTo(attackTarget.Position);
                    Attack(attackTarget);
                }
                else
                {
                    ChangeState(UnitState.Idle); // �^�[�Q�b�g�����Ȃ���Αҋ@�ɖ߂�
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

    // IAttackable�̎���
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
            SetTarget(attacker); // �������������ꍇ
        }
        ChangeState(UnitState.Combat);

    }


    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


    public virtual void Attack(IAttackable target)
    {
        if (canAttack && target != null && TeamId != target.TeamId)
        {
            float distance = Vector3.Distance(transform.position, (target as MonoBehaviour).transform.position);
            if (distance <= attackRange)
            {
                target.TakeDamage(attackPower);

                canAttack = false;
                StartCoroutine(StartCooldown());
            }
        }
    }

    protected virtual void Die()
    {
        // ���S�C�x���g���Ăяo��
        OnUnitDeath?.Invoke(this);
        // ���S���o�Ȃǁi�h���N���X�Ŋg���\�j
        Destroy(gameObject);
    }

    //public virtual void SetSelected(bool selected)
    //{
    //    // �����ȊO�͖���
    //    if (TeamId != Team.Player) return;

    //    isSelected = selected;
    //}


    public virtual void SetTarget(IAttackable newTarget)
    {
        // �Â��^�[�Q�b�g�̃C�x���g����
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
        // �^�[�Q�b�g�������đҋ@�ɖ߂�
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
    Idle,       // �ҋ@��
    Moving,     // �ړ���
    Combat,     // �퓬�Ԑ��i�G���U�����Ă��� or �U���Ώۂ�T���Ă���j
    Dead        // ���S
}

public enum Team
{
    Neutral,
    Player,
    Enemy
}

