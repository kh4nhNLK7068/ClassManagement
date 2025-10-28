# 🎓 Class Management System (Telerik Web Forms)

## 🧭 Overview
**Class Management System** là ứng dụng web quản lý lớp học được xây dựng bằng **ASP.NET Web Forms (Telerik UI)**.  
Hệ thống hỗ trợ nhiều vai trò người dùng (IT, Teacher, Student) với các chức năng quản lý, thống kê, và hiển thị dữ liệu trực quan qua **biểu đồ Dashboard**.

---

## ⚙️ Features

### 🔐 Authentication
- Đăng nhập / Đăng xuất (Login / Logout)
- Phân quyền người dùng (IT, Teacher, Student)

---

### 📊 Dashboard
Dành cho **IT** và **Teacher**:
- Biểu đồ tổng quan:
  - Số lượng **học sinh / giảng viên đã nghỉ**
  - Thống kê **lớp học full tuần** (T2→T6), **lớp học nửa tuần** (2-4-6, 3-5-7)
  - Tổng **số học sinh của tất cả lớp học**
- Biểu đồ tròn (Telerik RadHtmlChart / Kendo Chart)

---

### 👨‍🏫 Teacher
- **Tạo / Cập nhật / Hủy** môn học và lớp học
- **Tạo / Cập nhật / Vô hiệu hóa** thông tin học sinh
- Xem toàn bộ **danh sách lớp**, bao gồm:
  - Học sinh trong lớp
  - Sĩ số
  - Thời khóa biểu

---

### 💻 IT (Administrator)
- Toàn quyền hệ thống (bao gồm quyền của Teacher & Student)
- **Tạo / Cập nhật / Vô hiệu hóa** giảng viên
- **Vô hiệu hóa học sinh**
- Quản trị dữ liệu lớp học và tài khoản

---

### 🧑‍🎓 Student
- Xem danh sách **lớp học hiện tại (theo tháng)**
- Xem thông tin lớp:
  - Học sinh khác cùng lớp
  - Sĩ số
  - Thời khóa biểu
- Phân biệt các lớp học theo lịch (2-4-6, 3-5-7, full week)

---

## 🧰 Tech Stack

| Layer | Technology |
|:------|:------------|
| 🖥️ **Frontend** | ![ASP.NET](https://img.shields.io/badge/ASP.NET%20Web%20Forms-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![Telerik](https://img.shields.io/badge/Telerik%20UI-0288D1?style=for-the-badge&logo=telerik&logoColor=white) ![Bootstrap](https://img.shields.io/badge/Bootstrap-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white) |
| ⚙️ **Backend** | ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white) ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) |
| 🗃️ **Database** | ![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white) |
| 💾 **ORM / Data Access** | ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-512BD4?style=for-the-badge&logo=ef&logoColor=white) ![Dapper](https://img.shields.io/badge/Dapper-0078D7?style=for-the-badge&logo=nuget&logoColor=white) |
| 🧩 **Architecture** | Clean Architecture (3-layer: UI, Business, Data) |

---

## 🧱 System Roles

| Role | Permissions |
|------|--------------|
| 🧑‍💻 **IT** | Toàn quyền quản trị (teacher + student) |
| 👨‍🏫 **Teacher** | Quản lý lớp học, môn học, học sinh |
| 🧑‍🎓 **Student** | Xem lớp học, lịch học, bạn học cùng lớp |

---

## 🧮 Dashboard Visualization (Demo)
- Telerik **RadHtmlChart / RadChart** hiển thị biểu đồ:
  - Pie chart: tỷ lệ học sinh nghỉ / đang học
  - Column chart: số lớp full tuần vs không full tuần
  - Card summary: tổng số học sinh, tổng số lớp

---

## 🚀 Setup Guide

### 🧮 Clone repository
```bash
git clone https://github.com/kh4nhNLK7068/ClassManagement.git
```
---

## 🧑‍💻 Author

**Khanh Nguyen**
📧 [[khanhnguyennlk4198@gmail.com](mailto:khanhnguyennlk4198@gmail.com)] <br>
💼 Full-stack Developer.

---

## 🪪 License

This project is for practicing programming skills. <br>
Telerik UI libraries require a valid developer license from **Progress Telerik**.

