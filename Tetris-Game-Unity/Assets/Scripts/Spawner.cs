using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Danh sách các khối Tetromino")]
    public GameObject[] tetrominoes;

    void Start()
    {
        SpawnNewTetromino();
    }

    public void SpawnNewTetromino()
    {
        
        if (tetrominoes.Length == 0) return;

        int randomIndex = Random.Range(0, tetrominoes.Length);

        // Lấy vị trí của Spawner làm gốc
        // KHÔNG cộng trừ gì ở đây cả. Muốn chỉnh lệch, hãy chỉnh tọa độ của GameObject Spawner ngoài Scene.
        Vector3 spawnPosition = transform.position;

        Instantiate(tetrominoes[randomIndex], spawnPosition, Quaternion.identity);
    }
}