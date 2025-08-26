using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int coin = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddCoin(int amount)
    {
        coin += amount;
    }

    public bool SpendCoin(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;
            return true;
        }
        return false;
    }
}
