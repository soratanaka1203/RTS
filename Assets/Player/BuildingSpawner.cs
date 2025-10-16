using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private BuildingBase buildingPrefab;
    [SerializeField] private GameObject previewObject;
    [SerializeField] private LayerMask groundLayer; // 地面レイヤー
    private bool isBuildMode = false;

    void Start()
    {
        previewObject.SetActive(false);
    }

    void Update()
    {
        if (!isBuildMode) return;

        // UI上をクリックしていたらスルー（EventSystemを使う）
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            previewObject.transform.position = hit.point; 

            if (Input.GetMouseButtonDown(0))
            {
                //コインを払えたら建設する
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

    // UIボタンから呼び出す関数
    public void EnterBuildMode()
    {
        if (ResourceManager.Instance.coinAmount >= buildingPrefab.cost)
        {
            previewObject.SetActive(true);
            isBuildMode = true;
            Debug.Log("建設モード開始");
        }
    }
}
