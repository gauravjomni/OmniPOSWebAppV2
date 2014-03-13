
IF NOT EXISTS(SELECT name FROM sys.syslogins WHERE name = 'DBUserName' )
CREATE login DBUserName WITH password = 'DBPassword', DEFAULT_DATABASE=DBNAME
 
GO
USE DBNAME
GO

 If not exists (SELECT P1.name, P2.name 
 FROM sys.database_role_members AS S 
 inner JOIN sys.database_principals AS P1
 ON S.role_principal_id = P1.principal_id
 AND P1.type = 'R'
 inner JOIN sys.database_principals AS P2
 ON S.member_principal_id = P2.principal_id
AND P2.type IN ('S', 'U') where  P2.name = 'DBUserName' and P1.name = 'db_owner')
begin
Exec sp_grantdbaccess @loginame='DBUserName'
end
Exec sp_addrolemember @rolename='db_owner', @membername='DBUserName'

Exec sp_addrolemember @rolename='db_datareader', @membername='DBUserName'

Exec sp_addrolemember @rolename='db_datawriter', @membername='DBUserName'
