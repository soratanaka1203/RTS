using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Interface;

public class UnitController : MonoBehaviour
{
    List<NavMeshAgent> selectedUnits = new List<NavMeshAgent>();

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // クリック対象からIAttackableを探す
                IAttackable target = hit.collider.GetComponent<IAttackable>();

                foreach (NavMeshAgent agent in selectedUnits)
                {
                    UnitBase unit = agent.GetComponent<UnitBase>();

                    if (target != null && target.TeamId != unit.TeamId)
                    {
                        // 敵（ユニット or 建物）を攻撃対象に
                        unit.SetTarget(target);
                        unit.ChangeState(UnitState.Combat);
                    }
                    else if (hit.collider.CompareTag("Ground"))
                    {
                        // 移動命令
                        MoveUnitsWithSpacing(hit.point, selectedUnits);
                    }
                }
            }
        }


    }


    /// <summary>
    /// ユニットが一定距離を保って移動
    /// </summary>
    public void MoveUnitsWithSpacing(Vector3 targetPosition, List<NavMeshAgent> selectedAgents)
    {
        float spacing = 1.5f; // ユニット間の距離
        int count = selectedAgents.Count;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(count)); //列
        int rows = Mathf.CeilToInt((float)count / columns);//行

        for (int i = 0; i < count; i++)
        {
            UnitBase unit = selectedAgents[i].GetComponent<UnitBase>();
            unit .ChangeState(UnitState.Moving);
            int row = i / columns;
            int col = i % columns;

            Vector3 offset = new Vector3((col - columns / 2) * spacing, 0, (row - rows / 2) * spacing);
            Vector3 finalPosition = targetPosition + offset;

            selectedAgents[i].SetDestination(finalPosition);
            unit.ChangeState(UnitState.Idle);
        }

    }


    public void AddSelectedUnit(NavMeshAgent agent)
    {
        selectedUnits.Add(agent);
    }

    public void RemoveSelectedUnit(NavMeshAgent agent)
    {
        selectedUnits.Remove(agent);
    }

    public void ClearSelectedAgent()
    {
        selectedUnits.Clear();
    }
}
