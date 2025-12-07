using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Danh sách các khối Tetromino")]
    // Mảng này sẽ chứa 7 Prefab (I, J, L, O, S, T, Z)
    public GameObject[] tetrominoes;
    
    void Start()
    {
        SpawnNewTetromino();
    }

    // Hàm sinh ra khối mới
    public void SpawnNewTetromino()
    {
        int randomIndex = Random.Range(0, tetrominoes.Length);

        // Bước 2: Tạo bản sao (Instantiate) của Prefab được chọn
        // - tetrominoes[randomIndex]: Prefab cần tạo
        // - transform.position: Tạo ngay tại vị trí của Spawner
        // - Quaternion.identity: Giữ nguyên góc xoay gốc (không xoay thêm)
        Instantiate(tetrominoes[randomIndex], transform.position, Quaternion.identity);
    }
}
