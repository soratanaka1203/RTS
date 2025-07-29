using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    List<NavMeshAgent> selectedUnits = new List<NavMeshAgent>();

    void Update()
    {
        // ���N���b�N�Ń��j�b�g��I��
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
                    Debug.Log("���j�b�g��I��: " + unit.name);
                }
                else
                {
                    unit.SetSelected(false);
                    RemoveSelectedUnit(unit.GetComponent<NavMeshAgent>());
                    Debug.Log("���j�b�g�I��������: " + unit.name);
                }
            }
        }

        // �E�N���b�N�ňړ�����
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
