USE [master]
GO
CREATE DATABASE [QLNhansu]
GO
USE [QLNhansu]
GO


CREATE TABLE [dbo].[Phongban]
(
    MaPB CHAR(2) NOT NULL PRIMARY KEY,
    TenPB NVARCHAR(30) NOT NULL
)

CREATE TABLE [dbo].[Nhanvien]
(
    MaNV CHAR(6) NOT NULL PRIMARY KEY,
    TenNV NVARCHAR(20),
    Ngaysinh DATETIME,
    MaPB CHAR(2) REFERENCES [dbo].[Phongban](MaPB)
)

INSERT INTO [dbo].[Phongban] (MaPB, TenPB)
VALUES ('01', N'Kinh doanh'), ('02', N'Kế Toán');

INSERT INTO [dbo].[Nhanvien] (MaNV, TenNV, Ngaysinh, MaPB)
VALUES 
('NV0001', N'Trần Văn Nam', '1980-08-15', '01'),
('NV0002', N'Nguyễn Thị Yến', '1981-05-25', '02'),
('NV0003', N'Lý Kim Tuyền', '1979-08-10', '01');

