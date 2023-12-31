CREATE PROCEDURE [dbo].[SaveFirebaseUser] 
	@documentId varchar(50),
	@email varchar(50),
	@id varchar(100),
	@name varchar(50),
	@photoPath varchar(max)
AS
BEGIN
	declare @userExists as INT
	set @userExists = (select COUNT(*) from FirebaseUsers as count where documentId = @documentId)

	IF(@userExists = 1) 
		BEGIN
			UPDATE [dbo].[FirebaseUsers]
			 SET [email] = @email		  
			  ,[name] = @name
			  ,[photoPath] = @photoPath
			 WHERE documentId = @documentId
		END
	ELSE
		INSERT INTO [dbo].[FirebaseUsers]
			   ([documentId]
			   ,[email]
			   ,[id]
			   ,[name]
			   ,[photoPath])
		 VALUES
			   (@documentId
			   ,@email
			   ,@id
			   ,@name
			   ,@photoPath)	
END
