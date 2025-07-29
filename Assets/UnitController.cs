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
                    MoveUnitsWithSpacing(hit.point, selectedUnits);
                }
            }
        }
    }


    /// <summary>
    /// ���j�b�g����苗����ۂ��Ĉړ�
    /// </summary>
    public void MoveUnitsWithSpacing(Vector3 targetPosition, List<NavMeshAgent> selectedAgents)
    {
        float spacing = 1.5f; // ���j�b�g�Ԃ̋���
        int count = selectedAgents.Count;
        int columns = Mathf.CeilToInt(Mathf.Sqrt(count)); //��
        int rows = Mathf.CeilToInt((float)count / columns);//�s

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
