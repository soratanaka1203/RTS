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
                    selectedUnits.Add(unit.GetComponent<NavMeshAgent>());
                    Debug.Log("ユニットを選択: " + unit.name);
                }
                else
                {
                    unit.SetSelected(false);
                    selectedUnits.Remove(unit.GetComponent<NavMeshAgent>());
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
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
