-- 查询数据库所有表名

SELECT tablename
FROM pg_tables
WHERE tablename NOT LIKE 'pg%'
      AND tablename NOT LIKE 'sql_%'
ORDER BY tablename;