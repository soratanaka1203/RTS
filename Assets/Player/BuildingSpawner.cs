using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private BuildingBase buildingPrefab;
    [SerializeField] private GameObject previewObject;
    [SerializeField] private LayerMask groundLayer; // �n�ʃ��C���[
    private bool isBuildMode = false;

    void Start()
    {
        previewObject.SetActive(false);
    }

    void Update()
    {
        if (!isBuildMode) return;

        // UI����N���b�N���Ă�����X���[�iEventSystem���g���j
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            previewObject.transform.position = hit.point; 

            if (Input.GetMouseButtonDown(0))
            {
                //�R�C���𕥂����猚�݂���
                if(ResourceManager.Instance.SpendCoin(buildingPrefab.cost))
                    Instantiate(buildingPrefab, hit.point, Quaternion.identity);

                previewObject.SetActive(false);
                isBuildMode = false;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            previewObject.SetActive(false);
            isBuildMode = false;
        }
    }

    // UI�{�^������Ăяo���֐�
    public void EnterBuildMode()
    {
        if (ResourceManager.Instance.coinAmount >= buildingPrefab.cost)
        {
            previewObject.SetActive(true);
            isBuildMode = true;
            Debug.Log("���݃��[�h�J�n");
        }
    }
}
