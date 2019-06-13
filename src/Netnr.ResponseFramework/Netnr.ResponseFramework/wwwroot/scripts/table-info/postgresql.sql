-- 查询系统表生成表字典(默认值需更新查询语句)

SELECT C
	.relname 表名,
	CAST ( obj_description ( relfilenode, 'pg_class' ) AS VARCHAR ) 表说明,
	A.attname 字段名,
	concat_ws (
		'',
		T.typname,
	SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\(.*\)' )) AS 类型,
CASE A.attnotnull 
		WHEN 't' THEN
		'YES' ELSE'' 
	END AS 不为空,
CASE WHEN EXISTS (
		SELECT
			pg_attribute.attname 
		FROM
			pg_constraint
			INNER JOIN pg_class ON pg_constraint.conrelid = pg_class.oid
			INNER JOIN pg_attribute ON pg_attribute.attrelid = pg_class.oid 
			AND pg_attribute.attnum = pg_constraint.conkey [ 1 ] 
		WHERE
			relname = C.relname 
			AND attname = A.attname 
			) THEN
			'YES' ELSE'' 
		END AS 主键,
		'NULL' AS 默认值,
		col_description ( A.attrelid, A.attnum ) 注释 
	FROM
		pg_class C,
		pg_attribute A,
		pg_type T 
	WHERE
		A.attnum > 0 
		AND A.attrelid = C.oid 
		AND A.atttypid = T.oid 
		AND C.relname IN ('@TableName')
ORDER BY
	C.relname,
	A.attnum