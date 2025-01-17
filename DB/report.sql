﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 10.0.225.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 11/8/2024 6:53:35 AM
-- Server version: 8.0.39
--

--
-- Disable foreign keys
--
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

--
-- Set SQL mode
--
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

--
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8mb4';

--
-- Set default database
--
USE report;

--
-- Drop procedure `Proc_AllTable_Count`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_Count;

--
-- Drop procedure `Proc_AllTable_Delete`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_Delete;

--
-- Drop procedure `Proc_AllTable_DeleteList`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_DeleteList;

--
-- Drop procedure `Proc_AllTable_GetAll`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_GetAll;

--
-- Drop procedure `Proc_AllTable_GetAllByColumnName`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_GetAllByColumnName;

--
-- Drop procedure `Proc_AllTable_GetByCode`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_GetByCode;

--
-- Drop procedure `Proc_AllTable_GetById`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_GetById;

--
-- Drop procedure `Proc_AllTable_GetListByColumnValues`
--
DROP PROCEDURE IF EXISTS Proc_AllTable_GetListByColumnValues;

--
-- Drop procedure `Proc_Report_Filter`
--
DROP PROCEDURE IF EXISTS Proc_Report_Filter;

--
-- Drop procedure `Proc_Report_Insert`
--
DROP PROCEDURE IF EXISTS Proc_Report_Insert;

--
-- Drop procedure `Proc_Report_Update`
--
DROP PROCEDURE IF EXISTS Proc_Report_Update;

--
-- Drop table `report`
--
DROP TABLE IF EXISTS report;

--
-- Set default database
--
USE report;

--
-- Create table `report`
--
CREATE TABLE report (
  ReportId char(36) NOT NULL,
  ProvinceId varchar(255) DEFAULT NULL,
  ProvinceName varchar(255) DEFAULT NULL,
  DistrictId varchar(255) DEFAULT NULL,
  DistrictName varchar(255) DEFAULT NULL,
  WardId varchar(255) DEFAULT NULL,
  WardName varchar(255) DEFAULT NULL,
  Address varchar(255) DEFAULT NULL,
  CropStageId char(36) DEFAULT NULL,
  CropStageName varchar(255) DEFAULT NULL,
  PestStageId char(36) DEFAULT NULL,
  PestStageName varchar(255) DEFAULT NULL,
  CropId char(36) DEFAULT NULL,
  CropName varchar(255) DEFAULT NULL,
  PestId char(36) DEFAULT NULL,
  PestName varchar(255) DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(255) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ReportName varchar(255) DEFAULT NULL,
  ReportDate datetime DEFAULT NULL,
  ModifiedBy varchar(255) DEFAULT NULL,
  PRIMARY KEY (ReportId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 1489,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
ROW_FORMAT = DYNAMIC;

DELIMITER $$

--
-- Create procedure `Proc_Report_Update`
--
CREATE PROCEDURE Proc_Report_Update (IN v_ReportId char(36), IN v_ProvinceId varchar(255), IN v_ProvinceName varchar(255), IN v_DistrictId varchar(255), IN v_DistrictName varchar(255), IN v_WardId varchar(255), IN v_WardName varchar(255), IN v_Address varchar(255), IN v_CropStageId char(36), IN v_PestStageId char(36), IN v_ModifiedDate datetime, IN v_ModifiedBy varchar(255), IN v_ReportName varchar(255), IN v_ReportDate datetime, IN v_id char(36), IN v_CropStageName varchar(255), IN v_PestStageName varchar(255), IN v_PestId char(36), IN v_PestName varchar(255), IN v_CropId char(36), IN v_CropName varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure cập nhật thông tin dự đoán.'
BEGIN
  UPDATE report r
  SET ReportId = v_ReportId,
      ProvinceId = v_ProvinceId,
      ProvinceName = v_ProvinceName,
      DistrictId = v_DistrictId,
      DistrictName = v_DistrictName,
      WardId = v_WardId,
      WardName = v_WardName,
      Address = v_Address,
      CropStageId = v_CropStageId,
      CropStageName = v_CropStageName,
      PestStageId = v_PestStageId,
      PestStageName = v_PestStageName,
      CropId = v_CropId,
      CropName = v_CropName,
      PestId = v_PestId,
      PestName = v_PestName,
      ModifiedDate = v_ModifiedDate,
      ReportName = v_ReportName,
      ReportDate = v_ReportDate,
      ModifiedBy = v_ModifiedBy
  WHERE ReportId = v_id;
END
$$

--
-- Create procedure `Proc_Report_Insert`
--
CREATE PROCEDURE Proc_Report_Insert (IN v_ReportId char(36), IN v_ProvinceId varchar(255), IN v_ProvinceName varchar(255), IN v_DistrictId varchar(255), IN v_DistrictName varchar(255), IN v_WardId varchar(255), IN v_WardName varchar(255), IN v_Address varchar(255), IN v_CropStageId char(36), IN v_PestStageId char(36), IN v_CreatedDate datetime, IN v_CreatedBy varchar(255), IN v_ReportName varchar(255), IN v_ReportDate datetime, IN v_CropStageName varchar(255), IN v_PestStageName varchar(255), IN v_PestId char(36), IN v_PestName varchar(255), IN v_CropId char(36), IN v_CropName varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure thêm báo cáo.'
BEGIN
  INSERT INTO report (ReportId, ProvinceId, ProvinceName, DistrictId, DistrictName, WardId, WardName, Address, CropStageId, CropStageName, PestStageId, PestStageName, CropId, CropName, PestId, PestName, CreatedDate, CreatedBy, ReportName, ReportDate)
    VALUES (v_ReportId, v_ProvinceId, v_ProvinceName, v_DistrictId, v_DistrictName, v_WardId, v_WardName, v_Address, v_CropStageId, v_CropStageName, v_PestStageId, v_PestStageName, v_CropId, v_CropName, v_PestId, v_PestName, v_CreatedDate, v_CreatedBy, v_ReportName, v_ReportDate);
END
$$

--
-- Create procedure `Proc_Report_Filter`
--
CREATE PROCEDURE Proc_Report_Filter (IN v_ProvinceId varchar(255), IN v_DistrictId varchar(255), IN v_WardId varchar(255), IN v_PageSize int, IN v_PageNumber int, IN v_ReportName varchar(255), IN v_ReportStartDate datetime, IN v_ReportEndDate datetime, IN v_CropId char(36))
SQL SECURITY INVOKER
COMMENT 'Procedure lọc thông tin báo cáo theo điều kiện và trả về tổng số lượng.'
BEGIN
  DECLARE startIndex int;
  SET startIndex = (v_PageNumber - 1) * v_PageSize;

  -- Truy vấn dữ liệu với phân trang
  SELECT
    *
  FROM report p
  WHERE (v_ProvinceId IS NULL
  OR v_ProvinceId = ''
  OR p.ProvinceId = v_ProvinceId)
  AND (v_DistrictId IS NULL
  OR v_DistrictId = ''
  OR p.DistrictId = v_DistrictId)
  AND (v_WardId IS NULL
  OR v_WardId = ''
  OR p.WardId = v_WardId)
  AND (v_CropId IS NULL
  OR v_CropId = ''
  OR p.CropId = v_CropId)
  AND (v_ReportName IS NULL
  OR v_ReportName = ''
  OR p.ReportName LIKE v_ReportName)
  AND (v_ReportStartDate IS NULL
  OR v_ReportEndDate IS NULL
  OR (p.ReportDate >= v_ReportStartDate
  && p.ReportDate <= v_ReportEndDate))
  ORDER BY p.ReportDate
  LIMIT startIndex, v_PageSize;

  -- Truy vấn tổng số lượng bản ghi
  SELECT
    COUNT(*) AS Total
  FROM report p
  WHERE (v_ProvinceId IS NULL
  OR v_ProvinceId = ''
  OR p.ProvinceId = v_ProvinceId)
  AND (v_DistrictId IS NULL
  OR v_DistrictId = ''
  OR p.DistrictId = v_DistrictId)
  AND (v_WardId IS NULL
  OR v_WardId = ''
  OR p.WardId = v_WardId)
  AND (v_CropId IS NULL
  OR v_CropId = ''
  OR p.CropId = v_CropId)
  AND (v_ReportName IS NULL
  OR v_ReportName = ''
  OR p.ReportName LIKE v_ReportName)
  AND (v_ReportStartDate IS NULL
  OR v_ReportEndDate IS NULL
  OR (p.ReportDate >= v_ReportStartDate
  && p.ReportDate <= v_ReportEndDate))
  ;
END
$$

--
-- Create procedure `Proc_AllTable_GetListByColumnValues`
--
CREATE PROCEDURE Proc_AllTable_GetListByColumnValues (IN `tableName` varchar(255), IN `columnName` varchar(255), IN `values` text)
SQL SECURITY INVOKER
COMMENT 'Procedure lấy ra danh sách bản ghi theo một chuỗi các giá trị của một trường.'
BEGIN
  SET @query = CONCAT('SELECT * FROM ', tableName, ' entity WHERE entity.', columnName, ' IN (', `values`, ')');
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_GetById`
--
CREATE PROCEDURE Proc_AllTable_GetById (IN `tableName` varchar(255), IN `tableId` varchar(255), IN `id` char(36))
SQL SECURITY INVOKER
COMMENT 'Procedure lấy một bản ghi theo id.'
BEGIN
  SET @query = CONCAT('SELECT * FROM ', tableName, ' entity WHERE entity.', tableId, ' = \'', id, '\'');
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_GetByCode`
--
CREATE PROCEDURE Proc_AllTable_GetByCode (IN `tableName` varchar(255), IN `tableCode` varchar(255), IN `code` varchar(20))
SQL SECURITY INVOKER
COMMENT 'Procedure lấy bản ghi theo mã code.'
BEGIN
  SET @query = CONCAT('SELECT * FROM ', tableName, ' entity WHERE entity.', tableCode, ' = \'', code, '\'');
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_GetAllByColumnName`
--
CREATE PROCEDURE Proc_AllTable_GetAllByColumnName (IN `tableName` varchar(255), IN `columnName` varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure lấy tất cả bản ghi của một bảng nhưng chỉ lấy một cột.'
BEGIN
  SET @query = CONCAT('SELECT ', columnName, ' FROM ', tableName);
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_GetAll`
--
CREATE PROCEDURE Proc_AllTable_GetAll (IN `tableName` varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure lấy tất cả bản ghi trong 1 bảng.'
BEGIN
  SET @query = CONCAT('SELECT * FROM ', tableName);
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_DeleteList`
--
CREATE PROCEDURE Proc_AllTable_DeleteList (IN `tableName` varchar(255), IN `tableId` varchar(255), IN `ids` text)
SQL SECURITY INVOKER
COMMENT 'Procedure xóa nhiều bản ghi theo id.'
BEGIN
  SET @query = CONCAT('DELETE FROM ', tableName, ' WHERE ', tableId, ' IN (', ids, ')');
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_Delete`
--
CREATE PROCEDURE Proc_AllTable_Delete (IN `tableName` varchar(255), IN `tableId` varchar(255), IN `id` char(36))
SQL SECURITY INVOKER
COMMENT 'Procedure xóa 1 bản ghi.'
BEGIN
  SET @query = CONCAT('DELETE FROM ', tableName, ' WHERE ', tableId, ' = \'', id, '\'');
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

--
-- Create procedure `Proc_AllTable_Count`
--
CREATE PROCEDURE Proc_AllTable_Count (IN `tableName` varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure để đếm số bản ghi trong 1 bảng.'
BEGIN
  SET @query = CONCAT('SELECT COUNT(1) FROM ', tableName);
  PREPARE stmt FROM @query;
  EXECUTE stmt;

  /* giải phóng tài nguyên */
  DEALLOCATE PREPARE stmt;
END
$$

DELIMITER ;

-- 
-- Dumping data for table report
--
INSERT INTO report VALUES
('206e18d7-7c73-40fd-8664-05396b619ab4', '48', 'Thành phố Đà Nẵng', '497', 'Huyện Hòa Vang', '20293', 'Xã Hòa Bắc', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-06 23:05:32', 'Trần Thế Anh', NULL, 'ab', '2024-11-06 23:05:32', NULL),
('3e0ebf84-834b-454e-b874-12ec4f9846cc', '26', 'Tỉnh Vĩnh Phúc', '251', 'Huyện Yên Lạc', '09061', 'Xã Trung Kiên', NULL, 'f4c768f6-885a-49c3-96a1-4916fcc59964', 'Cây non', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-06 06:59:36', 'Trần Thế Anh', NULL, 'ttanh', '2024-11-06 06:59:36', NULL),
('4680442e-2c1b-42a6-a6a4-e00612af6f19', '83', 'Tỉnh Bến Tre', '835', 'Huyện Bình Đại', '29086', 'Xã Bình Thới', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-07 06:18:30', 'Trần Thế Anh', NULL, 'faf', '2024-11-07 06:18:30', NULL),
('496b1f0f-c1c3-49ee-99e3-5850cf61e6f3', '26', 'Tỉnh Vĩnh Phúc', '251', 'Huyện Yên Lạc', '09061', 'Xã Trung Kiên', NULL, 'f4c768f6-885a-49c3-96a1-4916fcc59964', 'Cây non', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-12 07:19:10', 'Trần Thế Anh', NULL, 'tta', '2024-11-12 07:19:10', NULL),
('4cf91d3f-fefa-430a-a0d2-c907f65f64b0', '01', 'Thành phố Hà Nội', '017', 'Huyện Đông Anh', '00505', 'Xã Cổ Loa', '50 Đường Nguyễn Văn Huyên', '5dd69ca8-ca71-43a1-83ed-d0a1a0e47fab', 'Cây trưởng thành', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', 'cd401245-e815-40ff-a51e-fbe3b8218b32', 'Sâu keo mùa thu', '2024-10-31 21:32:59', 'Trần Thế Anhh', '2024-10-31 22:04:44', 'tta', '2024-10-31 21:32:59', 'USER UPDATE'),
('59133303-084f-4aac-a1f9-2674fa7dc925', '06', 'Tỉnh Bắc Kạn', '063', 'Huyện Bạch Thông', '02002', 'Xã Cao Sơn', NULL, 'f4c768f6-885a-49c3-96a1-4916fcc59964', 'Cây non', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-07 06:18:15', 'Trần Thế Anh', NULL, 'hh', '2024-11-07 06:18:15', NULL),
('97385d96-3188-4f57-b5a6-b9e57fa84a4d', '48', 'Thành phố Đà Nẵng', '497', 'Huyện Hòa Vang', '20323', 'Xã Hòa Châu', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-06 23:05:41', 'Trần Thế Anh', NULL, 'babb', '2024-11-06 23:05:41', NULL),
('98ee19cb-b7b9-4c93-af3e-cf2947d532aa', '92', 'Thành phố Cần Thơ', '925', 'Huyện Cờ Đỏ', '31273', 'Xã Đông Hiệp', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-06 23:05:15', 'Trần Thế Anh', NULL, 'a', '2024-11-06 23:05:15', NULL),
('bf9c18cc-fcce-458b-a142-9903afdf5a5f', '42', 'Tỉnh Hà Tĩnh', '440', 'Huyện Đức Thọ', '18229', 'Thị trấn Đức Thọ', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-07 06:19:12', 'Trần Thế Anh', NULL, 'aa', '2024-11-07 06:19:12', NULL),
('c0687f3c-67ef-44e3-95a5-5414b41112b8', '48', 'Thành phố Đà Nẵng', '492', 'Quận Hải Châu', '20239', 'Phường Hải Châu II', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-07 06:18:50', 'Trần Thế Anh', NULL, 'bb', '2024-11-07 06:18:50', NULL),
('ca6bbf07-1d5c-40d1-8729-6a739f047bd7', '01', 'Thành phố Hà Nội', '005', 'Quận Cầu Giấy', '00169', 'Phường Quan Hoa', NULL, '5dd69ca8-ca71-43a1-83ed-d0a1a0e47fab', 'Cây trưởng thành', '1000eeb2-d8e3-4f29-b9f7-51d768eff2f8', 'Sâu non 6', '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', 'cd401245-e815-40ff-a51e-fbe3b8218b32', 'Sâu keo mùa thu', '2024-10-30 22:08:05', 'Trần Thế Anh', NULL, 'Thế Anh Tr', '2024-10-30 22:08:05', NULL),
('df112495-6671-44d4-8313-712ef7f614b1', '92', 'Thành phố Cần Thơ', '926', 'Huyện Phong Điền', '31303', 'Xã Giai Xuân', NULL, '6b0025cb-7e13-4f50-9008-69e46e283c86', 'Cây con', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-06 23:05:52', 'Trần Thế Anh', NULL, 'hh', '2024-11-06 23:05:52', NULL),
('ecc2ed9e-4615-4258-8e79-dbbdfb649917', '31', 'Thành phố Hải Phòng', '312', 'Huyện An Dương', '11581', 'Thị trấn An Dương', NULL, 'f4c768f6-885a-49c3-96a1-4916fcc59964', 'Cây non', NULL, NULL, '961f6ec9-8959-42f6-a558-a774a4afa7a1', 'Cây ngô', NULL, NULL, '2024-11-07 06:18:05', 'Trần Thế Anh', NULL, 'ah', '2024-11-07 06:18:05', NULL);

--
-- Restore previous SQL mode
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

--
-- Enable foreign keys
--
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;