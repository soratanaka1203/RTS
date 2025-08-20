using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    [Header("ステータス")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float attackPower = 10f;

    [Header("陣営情報")]
    public int teamId = 0; // 0: プレイヤー, 1: 敵

    [Header("選択状態")]
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
        // 死亡演出など（派生クラスで拡張可能）
        Destroy(gameObject);
    }

    public virtual void SetSelected(bool selected)
    {
        isSelected = selected;
        // 選択時のエフェクトなど表示もここで
    }
}
