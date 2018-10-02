-- 查询数据库所有表名

SELECT name FROM @DataBaseName..SysObjects Where XType = 'U' ORDER BY Name