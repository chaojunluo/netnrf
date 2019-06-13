-- 查询系统表生成表配置(用于快速初始化表配置)

SELECT
	'' AS "Id",
	C.relname AS "TableName",
	A.attname AS "ColField",
CASE WHEN col_description ( A.attrelid, A.attnum ) IS NULL THEN
		A.attname ELSE col_description ( A.attrelid, A.attnum ) 
	END AS "DvTitle",
CASE WHEN col_description ( A.attrelid, A.attnum ) IS NULL THEN
		A.attname ELSE col_description ( A.attrelid, A.attnum ) 
	END AS "ColTitle",
	120 AS "ColWidth",
	1 AS "ColAlign",
	0 AS "ColHide",
	A.attnum AS "ColOrder",
	0 AS "ColFrozen",
	0 AS "ColFormat",
	0 AS "ColSort",
	1 AS "ColExport",
	0 AS "ColQuery",
	'' AS "ColRelation",
	'' AS "FormUrl",
	'text' AS "FormType",
	1 AS "FormArea",
	1 AS "FormSpan",
	0 AS "FormHide",
	A.attnum AS "FormOrder",
	0 AS "FormRequired",
	'' AS "FormPlaceholder",
	'' AS "FormValue",
	'' AS "FormText",
CASE WHEN SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) IS NOT NULL THEN
		SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) 
		WHEN SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) IS NULL 
		AND concat_ws (
			'',
			T.typname,
			SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\(.*\)' )) = 'int4' THEN
			'11' 
			WHEN SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) IS NULL 
			AND concat_ws (
				'',
				T.typname,
				SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\(.*\)' )) = 'timestamp' THEN
				'23' 
				WHEN SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) IS NULL 
				AND concat_ws (
					'',
					T.typname,
					SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\(.*\)' )) = 'date' THEN
					'11' 
					WHEN SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\d+' ) IS NULL 
					AND concat_ws (
						'',
						T.typname,
						SUBSTRING ( format_type ( A.atttypid, A.atttypmod ) FROM '\(.*\)' )) = 'time' THEN
						'9' ELSE'-1' 
					END AS "FormMaxlength" 
				FROM
					pg_class AS C,
					pg_attribute AS A,
					pg_type T 
				WHERE
					C.relname = '@TableName' 
					AND A.atttypid = T.oid 
				AND A.attrelid = C.oid 
	AND A.attnum >0