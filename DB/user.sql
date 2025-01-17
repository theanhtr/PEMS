﻿--
-- Script was generated by Devart dbForge Studio for MySQL, Version 10.0.225.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 11/8/2024 6:53:43 AM
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
USE user;

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
-- Drop procedure `Proc_User_Filter`
--
DROP PROCEDURE IF EXISTS Proc_User_Filter;

--
-- Drop procedure `Proc_User_Insert`
--
DROP PROCEDURE IF EXISTS Proc_User_Insert;

--
-- Drop procedure `Proc_User_Update`
--
DROP PROCEDURE IF EXISTS Proc_User_Update;

--
-- Drop table `user`
--
DROP TABLE IF EXISTS user;

--
-- Set default database
--
USE user;

--
-- Create table `user`
--
CREATE TABLE user (
  UserId char(36) NOT NULL,
  Username varchar(255) DEFAULT NULL,
  Password varchar(255) DEFAULT NULL,
  Fullname varchar(255) DEFAULT NULL,
  RoleID int DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(255) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(255) DEFAULT NULL,
  ProvinceId varchar(255) DEFAULT NULL,
  ProvinceName varchar(255) DEFAULT NULL,
  WardId varchar(255) DEFAULT NULL,
  WardName varchar(255) DEFAULT NULL,
  DistrictName varchar(255) DEFAULT NULL,
  DistrictId varchar(255) DEFAULT NULL,
  Address varchar(255) DEFAULT NULL,
  PhoneNumber varchar(255) DEFAULT NULL,
  PRIMARY KEY (UserId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'bảng người dùng',
ROW_FORMAT = DYNAMIC;

DELIMITER $$

--
-- Create procedure `Proc_User_Update`
--
CREATE PROCEDURE Proc_User_Update (IN v_UserId char(36), IN v_Username varchar(255), IN v_Fullname varchar(255), IN v_RoleID int, IN v_ModifiedDate datetime, IN v_ModifiedBy varchar(255), IN v_id char(36), IN v_ProvinceId varchar(255), IN v_ProvinceName varchar(255), IN v_DistrictId varchar(255), IN v_DistrictName varchar(255), IN v_WardId varchar(255), IN v_WardName varchar(255), IN v_Address varchar(255), IN v_PhoneNumber varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure cập nhật thông tin người dùng.'
BEGIN
  UPDATE user u
  SET u.UserId = v_UserId,
      u.Username = v_Username,
      u.Fullname = v_Fullname,
      u.RoleID = v_RoleID,
      u.ModifiedDate = v_ModifiedDate,
      u.ModifiedBy = v_ModifiedBy,
      u.ProvinceId = v_ProvinceId,
      u.ProvinceName = v_ProvinceName,
      u.DistrictId = v_DistrictId,
      u.DistrictName = v_DistrictName,
      u.WardId = v_WardId,
      u.WardName = v_WardName,
      u.Address = v_Address,
      u.PhoneNumber = v_PhoneNumber
  WHERE u.UserId = v_id;
END
$$

--
-- Create procedure `Proc_User_Insert`
--
CREATE PROCEDURE Proc_User_Insert (IN v_UserId char(36), IN v_Username varchar(255), IN v_Password varchar(255), IN v_Fullname varchar(255), IN v_RoleID int, IN v_CreatedDate datetime, IN v_CreatedBy varchar(255), IN v_ProvinceId varchar(255), IN v_ProvinceName varchar(255), IN v_DistrictId varchar(255), IN v_DistrictName varchar(255), IN v_WardId varchar(255), IN v_WardName varchar(255), IN v_Address varchar(255), IN v_PhoneNumber varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure thêm người dùng.'
BEGIN
  INSERT INTO user (UserId, Username, Password, Fullname, RoleID, CreatedDate, CreatedBy, ProvinceId, ProvinceName, DistrictId, DistrictName, WardId, WardName, Address, PhoneNumber)
    VALUES (v_UserId, v_Username, v_Password, v_Fullname, v_RoleID, v_CreatedDate, v_CreatedBy, v_ProvinceId, v_ProvinceName, v_DistrictId, v_DistrictName, v_WardId, v_WardName, v_Address, v_PhoneNumber);
END
$$

--
-- Create procedure `Proc_User_Filter`
--
CREATE PROCEDURE Proc_User_Filter (IN `v_pageSize` int,
IN `v_pageNumber` int,
IN `v_searchText` varchar(255))
SQL SECURITY INVOKER
COMMENT 'Procedure lọc người dùng và trả về tổng số lượng.'
BEGIN
  DECLARE startIndex int;
  SET startIndex = (v_pageNumber - 1) * v_pageSize;

  -- Truy vấn dữ liệu với phân trang
  SELECT
    u.*,
    '' AS Password
  FROM user u
  WHERE (v_searchText IS NULL
  OR (u.Username LIKE CONCAT('%', v_searchText, '%')
  OR u.Fullname LIKE CONCAT('%', v_searchText, '%')))
  LIMIT startIndex, v_pageSize;

  -- Truy vấn tổng số lượng bản ghi
  SELECT
    COUNT(*) AS Total
  FROM user u
  WHERE (v_searchText IS NULL
  OR (u.Username LIKE CONCAT('%', v_searchText, '%')
  OR u.Fullname LIKE CONCAT('%', v_searchText, '%')));
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
-- Dumping data for table user
--
INSERT INTO user VALUES
('2950cff8-8119-41e2-a1d3-e4592c2aa06a', 'expert', '$2a$11$5SrGlH/xLyrAO2pQLtMthu78OIe83FWErKsppeN90xEXGAeCCsFki', 'Chuyên gia', 2, '2024-10-22 21:06:33', 'Trần Thế Anh', '2024-10-25 13:05:53', 'USER UPDATE', '48', 'Thành phố Đà Nẵng', '20323', 'Xã Hòa Châu', 'Huyện Hòa Vang', '497', 'Xóm', '0293214242'),
('a434ab80-4605-4d9e-9a0a-cdd269dfee1c', 'farmer', '$2a$11$3/EMippSh0lcKUULahMikOmSe2OeS1JDw0XcLipu81SgN6pA6QDUq', 'NGƯỜI NÔNG DÂN CHUYÊN', 3, '2024-10-22 21:07:05', 'Trần Thế Anh', '2024-11-07 18:45:25', 'USER UPDATE', '26', 'Tỉnh Vĩnh Phúc', '09061', 'Xã Trung Kiên', 'Huyện Yên Lạc', '251', 'Xóm 2', '0293214242'),
('ef119309-03c9-4e7b-9dbf-5ea7d87843b8', 'admin', '$2a$11$TgCwE3gDHPpyxoKMy4cU4.C4RKexPRnrjoIvEI0R4ldsSmxAzcbhi', 'Trần Thế Anh', 1, NULL, NULL, '2024-11-06 20:45:44', 'USER UPDATE', '01', 'Thành phố Hà Nội', '00169', 'Phường Quan Hoa', 'Quận Cầu Giấy', '005', '130 Cầu Giấy', '0984232111');

--
-- Restore previous SQL mode
--
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

--
-- Enable foreign keys
--
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;