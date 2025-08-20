using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class UnitBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    [Header("�X�e�[�^�X")]
    public float maxHealth = 100f;
    public float currentHealth;

    public float attackPower = 10f;       // �U����
    public float defensePower = 0f;       // �h��́i�_���[�W�y���j
    public float attackRange = 2f;        // �U���˒�
    public float attackCooldown = 1.5f;   // �U���Ԋu�i�b�j
    protected bool canAttack = true;
    protected UnitBase attackTarget;

    public float moveSpeed = 3.5f;        // �ړ����x

    public UnitState currentState = UnitState.Idle;//���j�b�g�̏��

    [Header("�w�c���")]
    public int teamId = 0; // 0: �v���C���[, 1: �G

    [Header("�I�����")]
    public bool isSelected = false;

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
        float damage = Mathf.Max(1f, amount - defensePower); // �Œ�1�_���[�W
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
            // �˒����`�F�b�N
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= attackRange)
            {
                target.TakeDamage(attackPower);
                target.SetTarget(this);//����ɍU���������Ƃ�m�点��
                canAttack=false;
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

    public virtual void SetSelected(bool selected)
    {
        isSelected = selected;
        // �I�����̃G�t�F�N�g�ȂǕ\����������
    }

    public virtual void SetTarget(UnitBase newTarget)
    {

        // �Â��^�[�Q�b�g�̃C�x���g����
        if (attackTarget != null)
        {
            newTarget.OnUnitDeath -= OnTargetDeath;
        }

        attackTarget = newTarget;

        if (attackTarget != null)
        {
            attackTarget.OnUnitDeath += OnTargetDeath;
            ChangeState(UnitState.Combat);
        }
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
