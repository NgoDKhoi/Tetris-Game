# 🗺️ Lộ Trình Phát Triển Game Tetris (Unity C#)

Đây là kế hoạch chi tiết chia làm 4 giai đoạn để hoàn thành game Tetris từ những Prefab có sẵn.

## 🏗️ Phần 1: Cơ chế Cơ bản (Movement & Spawning)
*Mục tiêu: Làm cho các khối gạch xuất hiện và di chuyển được.*

- [ ] **Tạo Spawner (Bộ sinh khối):**
    - Tạo script `Spawner.cs`.
    - Viết hàm `SpawnNewTetromino()` để sinh ngẫu nhiên 1 trong 7 prefab tại đỉnh màn hình.
- [ ] **Xử lý Input (Điều khiển):**
    - Tạo script `Piece.cs`.
    - Nhận nút bấm (Mũi tên Trái, Phải) để thay đổi `transform.position`.
    - Nhận nút (Mũi tên Lên) để `transform.Rotate` (Xoay 90 độ).
- [ ] **Tự động rơi (Auto Fall):**
    - Sử dụng bộ đếm thời gian (`Time.time`) để khối tự động rơi xuống 1 ô sau mỗi khoảng thời gian (ví dụ: 1 giây).

## 🧠 Phần 2: Hệ thống Lưới & Va chạm (Core Grid Logic)
*Mục tiêu: Xử lý logic gạch, tuyệt đối không dùng Physics/Rigidbody của Unity để tránh lỗi vật lý không mong muốn.*

- [ ] **Thiết lập Lưới ảo (The Grid):**
    - Hình dung lưới toạ độ chuẩn (thường là Rộng 10 x Cao 20).
    - Tạo logic để lưu trữ trạng thái: Tại toạ độ (x,y) này đã có gạch chưa?
- [ ] **Kiểm tra biên (Boundary Check):**
    - Viết hàm `IsValidMove()` để kiểm tra trước khi di chuyển.
    - Chặn không cho gạch đi ra ngoài mép trái (x < 0) hoặc mép phải (x > 9).
    - Chặn không cho gạch đi xuyên xuống dưới đáy sàn (y < 0).
- [ ] **Kiểm tra va chạm gạch (Block Collision):**
    - Cập nhật hàm `IsValidMove()`: Trả về `false` nếu vị trí mới dự định đi tới trùng với một khối gạch cũ đã nằm yên.

## ♻️ Phần 3: Vòng lặp Game (Game Loop)
*Mục tiêu: Xử lý chuỗi sự kiện xảy ra khi một khối gạch chạm đất.*

- [ ] **Khóa gạch (Locking):**
    - Khi gạch chạm đáy hoặc chạm gạch khác và không thể đi xuống nữa -> Dừng di chuyển.
    - Vô hiệu hóa script điều khiển của khối đó.
    - Lưu vị trí của 4 ô vuông con (children) vào hệ thống Lưới ảo (Grid).
- [ ] **Xử lý hàng (Line Clearing):**
    - Quét lưới từ dưới lên trên.
    - Nếu phát hiện hàng nào đã lấp đầy (đủ 10 ô) -> Xóa các GameObject gạch ở hàng đó.
    - **Gravity (Rơi bù):** Kéo tất cả các hàng nằm phía trên hàng vừa xóa tụt xuống 1 nấc để lấp chỗ trống.
- [x] **Spawn Next (Lượt mới):**
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