using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private RectTransform selectionBoxUI; 
    [SerializeField] private Canvas canvas;                
    [SerializeField] private Camera mainCamera;

    private Vector2 startPos;
    private UnitController unitController;
    private bool isDragging = false;

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
            isDragging = false; // まだドラッグ確定していない
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;

            // ある程度動いたら「ドラッグ扱い」にする
            if (!isDragging && Vector2.Distance(startPos, currentPos) > 10f)
            {
                isDragging = true;
                selectionBoxUI.gameObject.SetActive(true);
            }

            if (isDragging)
            {
                UpdateSelectionBox(startPos, currentPos);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                // ドラッグ選択
                SelectUnitsInRectangle(startPos, Input.mousePosition);
                selectionBoxUI.gameObject.SetActive(false);
            }
            else
            {
                // クリック選択
                ClickSelectUnit();
            }
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
            else
            {
                unit.SetSelected(false);
            }
        }
    }

    void ClickSelectUnit()
    {
        unitController.ClearSelectedAgent();

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            UnitBase unit = hit.collider.GetComponent<UnitBase>();
            if (unit != null)
            {
                unit.SetSelected(true);
                unitController.AddSelectedUnit(unit.GetComponent<UnityEngine.AI.NavMeshAgent>());
            }
        }
    }
}
