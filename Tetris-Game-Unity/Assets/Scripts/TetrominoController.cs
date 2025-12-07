using UnityEngine;
using UnityEngine.InputSystem;

public class TetrominoController : MonoBehaviour
{
    [Header("Cài đặt Di chuyển")]
    public float gridSize = 0.5f;
    public float fallTime = 1.0f;

    private float previousTime;
    void Start()
    {
        previousTime = Time.time;

        foreach (Transform child in transform)
        {
            // Set ưu tiên hiển thị cho các tetromino
            SpriteRenderer sprite = child.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                sprite.sortingOrder = 10;
            }
        }
    }

    void Update()
    {
        // 1. Xử lý Di chuyển Trái (Left Arrow)
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            transform.position += new Vector3(-gridSize, 0, 0);

            // TODO: Sau này sẽ thêm hàm kiểm tra va chạm (IsValidMove) ở đây
            // Nếu va chạm -> lùi lại: transform.position += new Vector3(1, 0, 0);
        }

        // 2. Xử lý Di chuyển Phải (Right Arrow)
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            transform.position += new Vector3(gridSize, 0, 0);

            // TODO: Sau này sẽ thêm hàm kiểm tra va chạm (IsValidMove) ở đây
            // Nếu va chạm -> lùi lại: transform.position += new Vector3(-1, 0, 0);
        }

        // 3. Xử lý Xoay (Up Arrow)
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {

            transform.Rotate(0, 0, 90);

            // TODO: Sau này sẽ thêm hàm kiểm tra va chạm (IsValidMove) ở đây
            // Nếu va chạm -> xoay ngược lại: transform.Rotate(0, 0, -90);
        }

        // 4. Xử lý Rơi tự động & Rơi nhanh (Down Arrow)
        if (Keyboard.current.downArrowKey.wasPressedThisFrame || Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0, -gridSize, 0);

            // Cập nhật lại thời gian để đếm lại từ đầu
            previousTime = Time.time;

            // TODO: Sau này sẽ thêm logic kiểm tra va chạm ở đây
            // Nếu chạm đáy/chạm gạch khác -> Dừng lại và gọi Spawner sinh khối mới
        }
    }

}
