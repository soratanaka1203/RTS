using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject resetButton;
    [SerializeField] TextMeshProUGUI resultText;

    private void Start()
    {
        resetButton.SetActive(false);
        resultText.gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        Castle.OnCastleDestroyed += OnCastleDestroyed; // イベント購読
        EnemyCastle.OnCastleDestroyed += OnCastleDestroyed; 
    }

    private void OnDisable()
    {
        Castle.OnCastleDestroyed += OnCastleDestroyed;      // 購読解除
        EnemyCastle.OnCastleDestroyed -= OnCastleDestroyed; 
    }

    private void OnCastleDestroyed(Team destroyedTeam)
    {
        if (destroyedTeam == Team.Player)
        {
            EndGame(false); // プレイヤーの負け
        }
        else if (destroyedTeam == Team.Enemy)
        {
            EndGame(true);  // プレイヤーの勝ち
        }
    }

    private void EndGame(bool playerWon)
    {
        if (playerWon) resultText.text = "You Win！";
        else resultText.text = "You Lose...";
        resultText.gameObject.SetActive(true);
        resetButton.SetActive(true);
        Time.timeScale = 0f; // 一時停止（例）
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
