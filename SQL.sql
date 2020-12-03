USE WEB1020_MISACukcuk_PTDuc

CREATE SEQUENCE CustomSequence
START WITH 1
INCREMENT BY 1;

SELECT FORMAT(NEXT VALUE FOR CustomSequence, 'NV00000#');


CREATE TABLE WEB1020_MISACukcuk_PTDuc.Employee (
  EmployeeId char(36) DEFAULT FORMAT((NEXT VALUE FOR CustomSequence), 'NV00000#') COMMENT 'Id nhân viên',
  EmployeeCode varchar(20) DEFAULT NULL COMMENT 'Mã nhân viên',
  FullName varchar(100) NOT NULL COMMENT 'Tên nhân viên',
  Gender int(11) DEFAULT NULL COMMENT 'Giới tính',
  DateOfBirth date DEFAULT NULL COMMENT 'Ngày sinh',
  Email varchar(100) NOT NULL COMMENT 'Địa chỉ email',
  PhoneNumber varchar(50) NOT NULL COMMENT 'Số điện thoại',
  IdentityNumber varchar(25) NOT NULL COMMENT 'Số chứng minh nhân dân',
  IdentityDate date DEFAULT NULL COMMENT 'Ngày cấp chứng minh nhân dân',
  IdentityPlace varchar(255) DEFAULT '' COMMENT 'Nơi cấp chứng minh nhân dân',
  JoinDate date DEFAULT NULL COMMENT 'Ngày gia nhập công ty',
  PersonalTaxCode varchar(25) DEFAULT NULL COMMENT 'Mã số thuế cá nhân',
  Salary double DEFAULT NULL COMMENT 'Mức lương cơ bản',
  WorkStatus int(11) DEFAULT NULL COMMENT 'Tình trạng làm việc (0:Đang làm việc;1-Đang thử việc;2-Đã nghỉ việc)',
  PositionId varchar(36) DEFAULT '' COMMENT 'Id vị trí công việc',
  DepartmentId varchar(36) DEFAULT '' COMMENT 'Id phòng ban làm việc',
  PRIMARY KEY (EmployeeId)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 294,
CHARACTER SET utf8,
COLLATE utf8_general_ci,
COMMENT = 'Bảng thông tin nhân viên';

ALTER TABLE WEB1020_MISACukcuk_PTDuc.Employee
ADD INDEX IDX_Employee_FullName (FullName);

ALTER TABLE WEB1020_MISACukcuk_PTDuc.Employee
ADD UNIQUE INDEX UK_Employee_EmployeeCode (EmployeeCode);

ALTER TABLE WEB1020_MISACukcuk_PTDuc.Employee
ADD CONSTRAINT FK_Employee_DepartmentId2 FOREIGN KEY (DepartmentId)
REFERENCES WEB1020_MISACukcuk_PTDuc.Department (DepartmentId) ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE WEB1020_MISACukcuk_PTDuc.Employee
ADD CONSTRAINT FK_Employee_PositionId2 FOREIGN KEY (PositionId)
REFERENCES WEB1020_MISACukcuk_PTDuc.`Position` (PositionId) ON DELETE NO ACTION ON UPDATE NO ACTION;

SELECT * FROM Employee WHERE FullName = "Trần Trọng Tuấn"

  CREATE TABLE Test(
    ID char(36) DEFAULT FORMAT((NEXT VALUE FOR CustomSequence), 'NV00000#'),
  Name char (255),
  PRIMARY KEY (ID)
  )
  INSERT INTO Test VALUES ('','Duc')
;
SET @CODE = (CAST((SELECT SUBSTRING(MAX(EmployeeCode),3,8)  AS maulon FROM Employee)  AS UNSIGNED)+1);

/*SELECT SUBSTRING(MAX(EmployeeCode),3,8) FROM Employee*/ 
/*(EmployeeId, EmployeeCode, FullName, Gender, DateOfBirth, Email,
  PhoneNumber, IdentityNumber, IdentityDate, IdentityPlace, JoinDate, PersonalTaxCode, Salary, WorkStatus, PositionId, DepartmentId)*/
  SELECT MAX(EmployeeCode) FROM Employee
  SELECT CAST((SELECT SUBSTRING(MAX(EmployeeCode),3,8) FROM Employee)  AS UNSIGNED)+1 AS Code;
  INSERT INTO Employee 
   VALUES("11acd342-735a-6bbe-73de-23e1777799",@CODE   , "Duc", 1,"2010-01-12", "p@gmail.com",
  "03636265455", "113717558", "2010-01-12", "Ha Noi", "2010-01-12", "525845145", "5654555", 1, "6123f364-3fd4-3ffe-3e6b-1629b83affd2", "75cce5a6-4f7f-3671-3cf3-eb0223d0a4f7");
  CALL Proc_SearchEmployee("0105");

  SELECT
    *
  FROM Employee e,
       Department d,
       `Position` p
  WHERE e.DepartmentId = d.DepartmentId
  AND e.PositionId = p.PositionId
  AND ( e.FullName LIKE '%Mạnh%'
  or e.EmployeeCode like '%Mạnh%'
  or e.PhoneNumber like '%Mạnh%');

  CALL Proc_GetEmployeeByPosition("6123f364-3fd4-3ffe-3e6b-1629b83affd2");

  SELECT MAX(EmployeeCode) FROM Employee