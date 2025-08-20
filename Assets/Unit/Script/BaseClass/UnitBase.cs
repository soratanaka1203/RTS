using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    [Header("�X�e�[�^�X")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackPower = 10f;

    [Header("�w�c���")]
    public int teamId = 0; // 0: �v���C���[, 1: �G

    [Header("�I�����")]
    public bool isSelected = false;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    public virtual void MoveTo(Vector3 position)
    {
        if (agent != null)
            agent.SetDestination(position);
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // ���S���o�Ȃǁi�h���N���X�Ŋg���\�j
        Destroy(gameObject);
    }

    public virtual void SetSelected(bool selected)
    {
        isSelected = selected;
        // �I�����̃G�t�F�N�g�ȂǕ\����������
    }
}
