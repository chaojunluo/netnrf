-- 查询系统表生成表配置(用于快速初始化表配置)

SELECT UUID() AS Id,
       t.TABLE_NAME AS TableName,
       c.COLUMN_NAME AS ColField,
       CASE c.COLUMN_COMMENT
           WHEN '' THEN
               c.COLUMN_NAME
           ELSE
               c.COLUMN_COMMENT
       END AS DvTitle,
       CASE c.COLUMN_COMMENT
           WHEN '' THEN
               c.COLUMN_NAME
           ELSE
               c.COLUMN_COMMENT
       END AS ColTitle,
       120 AS ColWidth,
       1 AS ColAlign,
       0 AS ColHide,
       c.ORDINAL_POSITION AS ColOrder,
       0 AS ColFrozen,
       '0' AS ColFormat,
       0 AS ColSort,
       1 AS ColExport,
       0 AS ColQuery,
       '' AS ColRelation,
       '' AS FormUrl,
       'text' AS FormType,
       1 AS FormArea,
       1 AS FormSpan,
       0 AS FormHide,
       c.ORDINAL_POSITION AS FormOrder,
       0 AS FormRequired,
       '' AS FormPlaceholder,
       '' AS FormValue,
       '' AS FormText,
       CASE
           WHEN LOCATE('(', COLUMN_TYPE) > 0 THEN
               REPLACE(REPLACE(REPLACE(COLUMN_TYPE, DATA_TYPE, ''), '(', ''), ')', '')
           WHEN LOCATE('(', COLUMN_TYPE) = 0
                AND DATA_TYPE = 'date' THEN
               10
           WHEN LOCATE('(', COLUMN_TYPE) = 0
                AND DATA_TYPE = 'time' THEN
               9
           WHEN LOCATE('(', COLUMN_TYPE) = 0
                AND DATA_TYPE = 'datetime' THEN
               23
           WHEN LOCATE('(', COLUMN_TYPE) = 0
                AND DATA_TYPE = 'timestamp' THEN
               23
           ELSE
               '-1'
       END AS FormMaxlength
FROM INFORMATION_SCHEMA.TABLES t
    LEFT JOIN INFORMATION_SCHEMA.COLUMNS c
        ON c.TABLE_NAME = t.TABLE_NAME
WHERE t.TABLE_SCHEMA = '@DataBaseName'
      AND c.TABLE_SCHEMA = '@DataBaseName'
      AND t.TABLE_NAME = '@TableName';