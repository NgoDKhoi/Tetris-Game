using UnityEngine;
using UnityEngine.SceneManagement; // Thư viện quản lý chuyển cảnh

public class MenuController : MonoBehaviour
{
    // Gắn hàm này vào Button "Play" ở Scene Menu
    public void buttonPlay()
    {
        // Load scene có tên là "GameScene"
        Debug.Log("Chuyển scene");
        SceneManager.LoadScene("GameScene");
    }
}