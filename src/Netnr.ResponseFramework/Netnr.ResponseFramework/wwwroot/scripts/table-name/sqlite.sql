-- 查询数据库所有表名

SELECT name FROM sqlite_master WHERE type='table' and name!='sqlite_sequence' ORDER BY name;