using UnityEngine;

public class Board : MonoBehaviour
{
    // Set up kích thước lưới (trong data)
    public static int width = 10;
    public static int height = 20;

    // Set up độ lệch
    public static float offsetX = 4.5f;
    public static float offsetY = 9.5f;

    // Mảng 2 chiều lưu trữ các khối gạch ĐÃ CỐ ĐỊNH
    public static Transform[,] grid = new Transform[width, height];

    void Awake()
    {
        // Reset lại mảng dữ liệu thành rỗng mỗi khi Game bắt đầu lại
        grid = new Transform[width, height];
    }

    // --- HÀM KIỂM TRA VỊ TRÍ ---
    public static bool IsValidPosition(Transform tetromino)
    {
        foreach (Transform child in tetromino)
        {
            int x = Mathf.RoundToInt(child.position.x + offsetX);
            int y = Mathf.RoundToInt(child.position.y + offsetY);

            // Kiểm tra biên
            if (x < 0 || x >= width || y < 0 || y>= height) {
                Debug.Log($"LỖI VA CHẠM: Child[{child.position.x}, {child.position.y}] và Grid[{x}, {y}]");
                return false;
            }
            
            // Kiểm tra va chạm với gạch cũ
            if (y < height && grid[x, y] != null)
            {
                Debug.Log($"LỖI VA CHẠM: Gạch tại Grid[{x}, {y}] đã có người đứng!");
                return false;
            }
        }
        return true;
    }

    // --- HÀM LƯU VÀO LƯỚI ---
    public static void AddToGrid(Transform tetromino)
    {
        foreach (Transform child in tetromino)
        {
            int x = Mathf.RoundToInt(child.position.x + offsetX);
            int y = Mathf.RoundToInt(child.position.y + offsetY);

            if (y < height && x >= 0 && x < width && y >= 0)
            {
                grid[x, y] = child;
            }
        }
    }

    #region XỬ LÝ DÒNG
    public static bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] == null) return false;
        }
        return true;
    }

    public static void DeleteLine(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public static void MoveLineDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (grid[x, y] != null)
            {
                // Cập nhật data cho Line
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Cập nhật visual cho Line
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void MoveAllLinesDown(int y)
    {
        for (int i = y; i < height; i++)
        {
            MoveLineDown(i);
        }
    }

    public static void HandleAllLines()
    {
        int linesCleared = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                MoveAllLinesDown(y + 1);
                y--;
                linesCleared++;
            }
        }
        if (linesCleared > 0 && GameManager.instance != null)
        {
            GameManager.instance.AddScore(linesCleared);
        }
    }
    #endregion XỬ LÝ DÒNG


}