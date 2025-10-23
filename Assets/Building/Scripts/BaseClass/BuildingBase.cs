using UnityEngine;
using static Interface;

public abstract class BuildingBase : MonoBehaviour, IAttackable
{
    [Header("�����̐ݒ�")]
    public string buildingName;
    public int cost = 100;
    public float buildTime = 5f;
    public float maxHealth = 1500;
    protected float currentHealth;

    protected bool isConstructing;

    public Team TeamId { get; set; } = Team.Player;
    public bool IsAlive => currentHealth > 0;
    public Vector3 Position => transform.position;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        isConstructing = true;
    }

    protected virtual void Update()
    {
        if (isConstructing)
        {
            ConstructBuilding();
        }
    }

    /// <summary>
    /// ���ݏ����i���Ԍo�߂�HP���񕜂��Ă����C���[�W�j
    /// </summary>
    protected virtual void ConstructBuilding()
    {
        currentHealth += Mathf.CeilToInt(maxHealth * Time.deltaTime / buildTime);

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            isConstructing = false;
            OnConstructed();
        }
    }

    /// <summary>
    /// ���݊���������Ăяo�����
    /// </summary>
    protected abstract void OnConstructed();

    /// <summary>
    /// �_���[�W���󂯂�
    /// </summary>
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroyBuilding();
        }
    }

    /// <summary>
    /// ��������ꂽ�Ƃ�
    /// </summary>
    protected virtual void DestroyBuilding()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// ���݂̌��ݏ󋵂�Ԃ��iUI�p�Ȃǁj
    /// </summary>
    public float GetBuildProgress()
    {
        return (float)currentHealth / maxHealth;
    }
}
