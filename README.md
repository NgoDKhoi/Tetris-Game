# 🗺️ Lộ Trình Phát Triển Game Tetris (Unity C#)

Đây là kế hoạch chi tiết chia làm 4 giai đoạn để hoàn thành game Tetris từ những Prefab có sẵn.

## 🏗️ Phần 1: Cơ chế Cơ bản (Movement & Spawning)
*Mục tiêu: Làm cho các khối gạch xuất hiện và di chuyển được.*

- [x] **Tạo Spawner (Bộ sinh khối):**
    - Tạo script `Spawner.cs`.
       + Viết hàm `SpawnNewTetromino()` để sinh ngẫu nhiên 1 trong 7 prefab tại vị trí của Spawner.
- [x] **Xử lý Input (Điều khiển):**
    - Tạo script `TetrominoController.cs`.
    - Nhận input từ bàn phím của người chơi để thay đổi `transform.position`hoặc `transform.Rotate`
- [x] **Tự động rơi (Auto Fall):**
    - Sử dụng bộ đếm thời gian (`Time.time`) để khối tự động rơi xuống 1 ô sau mỗi khoảng thời gian (ví dụ: 1 giây).

## 🧠 Phần 2: Hệ thống Lưới & Va chạm (Core Grid Logic)
*Mục tiêu: Tạo `board.cs` và xử lý logic gạch trong đó*

- [x] **Thiết lập Lưới ảo (The Grid):**
    - Tạo `board.cs`
    - Tạo mảng 2 chiều `grid = [10,20]`
- [x] **Kiểm tra biên (Boundary Check):**
    - Viết hàm `IsValidPosition()` kiểm tra vị trí có hợp lệ không ?
- [x] **Kiểm tra va chạm gạch (Block Collision):**
    - Cập nhật hàm `IsValidPosition()`: Trả về `false` nếu vị trí mới dự định đi tới trùng với một khối gạch cũ đã nằm yên.
- [x] **Khóa gạch (Locking):** 
    - Sau khi hoàn tất di chuyển thì vô hiệu hóa script điều khiển của khối đó.
    - Viết hàm `AddToGrid()` để lưu trữ các khối vào mảng grid

## ♻️ Phần 3: Vòng lặp Game (Game Loop)
*Mục tiêu: Xử lý chuỗi sự kiện xảy ra khi một khối gạch chạm đất*

- [x] **Xử lý hàng:**
    - Quét lưới từ dưới lên trên. 
    - Nếu phát hiện hàng nào đã lấp đầy -> Xóa các GameObject gạch ở hàng đó.
    - Kéo tất cả các hàng nằm phía trên hàng vừa xóa tụt xuống 1 nấc để lấp chỗ trống.
- [x] **Spaw teromino mới:**
    - Sau khi dọn hàng xong, gọi lại `Spawner` để thả khối mới.

## 🎮 Phần 4: Giao diện & Kết thúc (UI & Polish)
*Mục tiêu: Hoàn thiện trò chơi với tính năng tính điểm và thắng thua.*

- [ ] **Tính điểm (Scoring):**
    - Tạo logic tính điểm: 1 hàng = 100đ, 4 hàng (Tetris) = 800đ.
    - Hiển thị điểm số lên màn hình bằng UI Text (TextMeshPro).
- [ ] **Game Over:**
    - Kiểm tra ngay khi vừa Spawn: Nếu vị trí xuất phát đã bị chặn bởi gạch cũ -> Thông báo Thua Game.
    - Hiển thị bảng "Play Again" để reload lại màn chơi.
- [ ] **Next Piece UI (Nâng cao):**
    - Hiển thị trước hình dáng khối gạch tiếp theo ở góc màn hình để người chơi tính toán.