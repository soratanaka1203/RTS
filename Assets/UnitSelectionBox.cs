using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionBox : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private RectTransform selectionBoxUI; // UIで表示する選択矩形
    [SerializeField] private Canvas canvas;                // UIキャンバス（Screen Space - Overlay 推奨）
    [SerializeField] private Camera mainCamera;

    private Vector2 startPos;
    private UnitController unitController;

    void Awake()
    {
        selectionBoxUI.gameObject.SetActive(false);
        unitController = GetComponent<UnitController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            selectionBoxUI.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;
            UpdateSelectionBox(startPos, currentPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnitsInRectangle(startPos, Input.mousePosition);
            selectionBoxUI.gameObject.SetActive(false);
        }
    }

    void UpdateSelectionBox(Vector2 start, Vector2 end)
    {
        Vector2 center = (start + end) / 2;
        selectionBoxUI.position = center;

        Vector2 size = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
        selectionBoxUI.sizeDelta = size;
    }

    void SelectUnitsInRectangle(Vector2 screenStart, Vector2 screenEnd)
    {
        unitController.ClearSelectedAgent();
        Vector2 min = Vector2.Min(screenStart, screenEnd);
        Vector2 max = Vector2.Max(screenStart, screenEnd);

        foreach (var unit in FindObjectsOfType<UnitBase>())
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(unit.transform.position);
            if (screenPos.z > 0 && screenPos.x >= min.x && screenPos.x <= max.x && screenPos.y >= min.y && screenPos.y <= max.y)
            {
                unit.SetSelected(true);
                unitController.AddSelectedUnit(unit.GetComponent<UnityEngine.AI.NavMeshAgent>());
            }
        }
    }
}
