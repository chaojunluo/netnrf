-- 删除所有表(仅生成删除表SQL语句，还需要执行)

SELECT CONCAT('drop table ', TABLE_NAME, ';')
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_SCHEMA = '@DataBaseName';