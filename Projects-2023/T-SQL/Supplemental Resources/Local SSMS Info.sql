
--Get Info about local SSMS 
SELECT 
    SERVERPROPERTY('ProductVersion') AS ProductVersion,
    SERVERPROPERTY('ProductLevel') AS ProductLevel,
    SERVERPROPERTY('ProductMajorVersion') AS ProductMajorVersion,
    SERVERPROPERTY('ProductMinorVersion') AS ProductMinorVersion,
    SERVERPROPERTY('ProductName') AS ProductName,
    SERVERPROPERTY('Edition') AS Edition,
    SERVERPROPERTY('BuildClrVersion') AS BuildClrVersion,
    SERVERPROPERTY('SqlCharSet') AS SqlCharSet,
    SERVERPROPERTY('Collation') AS Collation,
    SERVERPROPERTY('BuildNumber') AS BuildNumber,
    SERVERPROPERTY('SqlSortOrder') AS SqlSortOrder,
    SERVERPROPERTY('FileDescription') AS FileDescription,
    SERVERPROPERTY('FileVersion') AS FileVersion,
    SERVERPROPERTY('ProductBuildType') AS ProductBuildType,
    SERVERPROPERTY('SqlBuildNumber') AS SqlBuildNumber,
    SERVERPROPERTY('BuildType') AS BuildType,
    SERVERPROPERTY('SqlSortOrder') AS SqlSortOrder,
    SERVERPROPERTY('SqlCharSet') AS SqlCharSet,
    SERVERPROPERTY('SqlCharSetName') AS SqlCharSetName,
    SERVERPROPERTY('SqlSortOrderName') AS SqlSortOrderName,
    SERVERPROPERTY('SqlSortOrderName') AS SqlSortOrderName,
    SERVERPROPERTY('SqlCharSetName') AS SqlCharSetName,
	SERVERPROPERTY('ComputerNamePhysicalNetBIOS'),
	SERVERPROPERTY('MachineName') as 'MachineName'


SELECT
SUM (user_object_reserved_page_count)*8 as usr_obj_kb,
SUM (internal_object_reserved_page_count)*8 as internal_obj_kb,
SUM (version_store_reserved_page_count)*8 as version_store_kb,
SUM (unallocated_extent_page_count)*8 as freespace_kb,
SUM (mixed_extent_page_count)*8 as mixedextent_kb
FROM sys.dm_db_file_space_usage

SELECT * FROM fn_virtualservernodes();

SELECT
	@@VERSION AS 'SQL Server Version',
	@@SERVICENAME


BEGIN 
	--Run this command to view current size of the transaction logs and how much space is being used.
	DBCC SQLPERF(logspace)

	--Run this CMD to view more info
	DBCC LOGINFO

	--Back transaction log
	BACKUP LOG DBUtil TO DISK = 'C:\Backup\DBUtil.trn'
END