using UnityEngine;
using static Interface;

public abstract class BuildingBase : MonoBehaviour, IAttackable
{
    [Header("建物の設定")]
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
    /// 建設処理（時間経過でHPが回復していくイメージ）
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
    /// 建設完了したら呼び出される
    /// </summary>
    protected abstract void OnConstructed();

    /// <summary>
    /// ダメージを受ける
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
    /// 建物が壊れたとき
    /// </summary>
    protected virtual void DestroyBuilding()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 現在の建設状況を返す（UI用など）
    /// </summary>
    public float GetBuildProgress()
    {
        return (float)currentHealth / maxHealth;
    }
}
