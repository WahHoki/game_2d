using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton agar mudah diakses oleh Player
    public static GameManager Instance { get; private set; }

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("GAME OVER! Pemain mengenai jebakan atau jatuh.");
        
        // Restart level setelah 1.5 detik
        Invoke("RestartLevel", 1.5f); 
    }

    public void LevelComplete()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("MENANG! Lanjut ke level berikutnya.");
        
        // Restart level (Bisa diganti dengan memuat Scene berikutnya)
        Invoke("RestartLevel", 2f); 
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
