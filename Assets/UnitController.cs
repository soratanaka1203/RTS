using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    List<NavMeshAgent> selectedUnits = new List<NavMeshAgent>();

    void Update()
    {
        // 左クリックでユニットを選択
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                UnitBase unit = hit.collider.GetComponent<UnitBase>();
                if (unit == null) return;

                if (!unit.isSelected)
                {
                    unit.SetSelected(true);
                    AddSelectedUnit(unit.GetComponent<NavMeshAgent>());
                    Debug.Log("ユニットを選択: " + unit.name);
                }
                else
                {
                    unit.SetSelected(false);
                    RemoveSelectedUnit(unit.GetComponent<NavMeshAgent>());
                    Debug.Log("ユニット選択を解除: " + unit.name);
                }
            }
        }

        // 右クリックで移動命令
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (NavMeshAgent agent in selectedUnits)
                {
                    MoveUnitsWithSpacing(hit.point, selectedUnits);
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
            int row = i / columns;
            int col = i % columns;

            Vector3 offset = new Vector3((col - columns / 2) * spacing, 0, (row - rows / 2) * spacing);
            Vector3 finalPosition = targetPosition + offset;

            selectedAgents[i].SetDestination(finalPosition);
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
