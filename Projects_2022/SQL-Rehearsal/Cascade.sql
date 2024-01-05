
IF NOT EXISTS (
	SELECT * 
	FROM sys.foreign_keys 
	WHERE object_id = 
	OBJECT_ID('N[dbo].[FK_T_FMS_Navigation_T_FMS_Navigation]') AND parent_object_id = 
	OBJECT_ID('N[dbo].[FK_T_FMS_Navigation]'))
	ALTER TABLE [dbo].[T_FMS_Navigation] WITH CHECK ADD CONSTRAINT
	[FK_T_FMS_Navigation_T_FMS_Navigation] FOREIGN KEY ([NA_NA_UID])
	REFERENCES [dbo].[T_FMS_Navigation] ([NA_UID])
	ON DELETE CASCADE
GO

IF EXISTS (
	SELECT *
	FROM sys.foreign_keys 
	WHERE object_id = 
	OBJECT_ID('N[dbo].[FK_T_FMS_Navigation_T_FMS_Navigation]') AND parent_object_id = 
	OBJECT_ID('N[dbo].[FK_T_FMS_Navigation]'))
	ALTER TABLE [dbo].[T_FMS_Navigation] CHECK CONSTRAINT [FK_T_FMS_Navigation_T_FMS_Navigation]	
GO