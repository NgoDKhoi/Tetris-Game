using UnityEngine;
using TMPro;                 // Thư viện cho TextMeshPro
using UnityEngine.SceneManagement; // Thư viện để load lại Scene
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    public GameObject gamePlayPanel; // Panel chứa điểm số
    public GameObject gameOverPanel; // Panel hiện ra khi thua

    [Header("UI Elements")]
    public TextMeshProUGUI currentScoreText;    // Text hiển thị điểm lúc chơi
    public TextMeshProUGUI scoreToAddText;  // Text hiển thị điểm được cộng

    private int currentScore = 0;
    private bool isGameOver = false;

    private Coroutine hideScoreCoroutine;

    void Awake()
    {
        // Singleton pattern để gọi dễ dàng từ nơi khác
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1;
            
        // Thiết lập trạng thái ban đầu
        if (gamePlayPanel != null) gamePlayPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false); // Ẩn bảng thua đi

        if (scoreToAddText != null) scoreToAddText.gameObject.SetActive(false);

        currentScore = 0;
        UpdateScoreUI();
    }

    // --- XỬ LÝ ĐIỂM SỐ ---
    public void AddScore(int linesCleared)
    {
        if (isGameOver) return;

        // Tính điểm theo quy tắc cổ điển: 1 hàng 100, 4 hàng 800
        int pointsToAdd = 0;
        switch (linesCleared)
        {
            case 1: pointsToAdd = 100; break;
            case 2: pointsToAdd = 300; break;
            case 3: pointsToAdd = 500; break;
            case 4: pointsToAdd = 800; break;
            default: pointsToAdd = linesCleared * 100; break;
        }
        currentScore += pointsToAdd;
        UpdateScoreUI();

        if (scoreToAddText != null)
        {
            scoreToAddText.text = "+" + pointsToAdd.ToString();
            scoreToAddText.gameObject.SetActive(true); // Bật lên

            // Nếu đang có bộ đếm cũ thì dừng lại (reset timer)
            if (hideScoreCoroutine != null) StopCoroutine(hideScoreCoroutine);

            // Bắt đầu đếm ngược 1.5 giây hoặc 2 giây mới
            hideScoreCoroutine = StartCoroutine(HideScoreTextRoutine());
        }
    }

    IEnumerator HideScoreTextRoutine()
    {
        yield return new WaitForSeconds(1.5f); // Thời gian hiển thị (bạn có thể sửa thành 2.0f)

        if (scoreToAddText != null)
        {
            scoreToAddText.gameObject.SetActive(false); // Tắt đi
        }
    }

    void UpdateScoreUI()
    {
        if (currentScoreText != null) currentScoreText.text = currentScore.ToString();
    }

    // --- XỬ LÝ GAME OVER ---
    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("Game Over Activated!");

        // 1. Hiện bảng Game Over
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        // 2. Dừng Spawner lại không cho sinh khối nữa
        if (FindFirstObjectByType<Spawner>() != null)
        {
            FindFirstObjectByType<Spawner>().enabled = false;
        }

        // 3. Vô hiệu hóa khối gạch đang rơi hiện tại (nếu có)
        TetrominoController currentTetromino = FindFirstObjectByType<TetrominoController>();
        if (currentTetromino != null) currentTetromino.enabled = false;
    }

    // --- CÁC HÀM CHO BUTTON (Gắn vào nút trong Unity) ---
    public void ReplayGame()
    {
        Debug.Log("Nút Replay đã được bấm!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnMenu()
    {
        Debug.Log("Nút Menu đã được bấm!");
        SceneManager.LoadScene("MenuScene"); // Đảm bảo bạn đã tạo Scene tên là "MenuScene"
    }
}