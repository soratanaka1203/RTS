using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    [SerializeField] TextMeshProUGUI coinText;

    public int coinAmount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddCoin(int amount)
    {
        coinAmount += amount;
        coinText.text = $"Coin:{coinAmount}";
    }

    public bool SpendCoin(int amount)
    {
        if (coinAmount >= amount)
        {
            coinAmount -= amount;
            coinText.text = $"Coin:{coinAmount}";
            return true;
        }
        return false;
    }
}
