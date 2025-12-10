using UnityEngine;
using UnityEngine.InputSystem;

public class TetrominoController : MonoBehaviour
{
    [Header("Cài đặt Di chuyển")]
    public float fallTime = 1.0f;

    private float previousTime;

    void Start()
    {
        previousTime = Time.time;

        // Set độ ưu tiên hiển thị cho các tetromino
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = 10;
        }

        // Check Game Over ngay khi sinh ra
        if (!Board.IsValidPosition(transform))
        {
            Debug.Log("GAME OVER");
            // Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. Xử lý dịch Trái
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!Board.IsValidPosition(transform))
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        // 2. Xử lý dịch Phải
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            transform.position += new Vector3(1, 0, 0);
            if (!Board.IsValidPosition(transform))
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }

        // 3. Xử lý xoay
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            transform.Rotate(0, 0, 90);
            if (!Board.IsValidPosition(transform))
            {
                transform.Rotate(0, 0, -90);
            }
        }

        // 4. Xử lý rơi xuống
        if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame || Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!Board.IsValidPosition(transform))
            {
                transform.position -= new Vector3(0, -1, 0);
                this.enabled = false; // Vô việu hóa input di chuyển
                Board.AddToGrid(transform); // Add tetromino vào data
                Board.HandleAllLines(); // Giải quyết các dòng nếu có full

                if (FindFirstObjectByType<Spawner>() != null)
                {
                    FindFirstObjectByType<Spawner>().SpawnNewTetromino();
                }
            }   
            previousTime = Time.time;
        }
    }
}