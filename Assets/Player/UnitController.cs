using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
                GameObject clickedObject = hit.collider.gameObject;

                foreach (NavMeshAgent agent in selectedUnits)
                {
                    UnitBase unit = agent.GetComponent<UnitBase>();
                    if (clickedObject.CompareTag("Enemy"))
                    {
                        // �G���U���Ώۂɂ���
                        UnitBase enemy = clickedObject.GetComponent<UnitBase>();
                        if (enemy != null)
                        {
                            unit.SetTarget(enemy);        // �퓬�Ԑ��ɓ���
                            unit.ChangeState(UnitState.Combat);
                        }
                    }
                    else if (clickedObject.CompareTag("Ground"))
                    {
                        unit.ChangeState(UnitState.Moving);
                        // �ړ�����
                        MoveUnitsWithSpacing(hit.point, selectedUnits);
                    }
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
            UnitBase unit = selectedAgents[i].GetComponent<UnitBase>();
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
