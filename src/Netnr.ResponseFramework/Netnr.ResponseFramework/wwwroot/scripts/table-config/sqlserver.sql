-- 查询系统表生成表配置(用于快速初始化表配置) 

SELECT CONVERT(VARCHAR(36), NEWID()) AS Id,
       t.[name] AS TableName,
       c.[name] AS ColField,
       ISNULL(CONVERT(NVARCHAR(200), ep.[value]), c.name) AS DvTitle,
       ISNULL(CONVERT(NVARCHAR(200), ep.[value]), c.name) AS ColTitle,
       120 AS ColWidth,
       1 AS ColAlign,
       (CASE ep.class
            WHEN 1 THEN
                0
            ELSE
                1
        END
       ) AS ColHide,
       c.column_id AS ColOrder,
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
       (CASE ep.class
            WHEN 1 THEN
                0
            ELSE
                1
        END
       ) AS FormHide,
       c.column_id AS FormOrder,
       0 AS FormRequired,
       '' AS FormPlaceholder,
       '' AS FormValue,
       '' AS FormText,
       COLUMNPROPERTY(c.object_id, c.name, 'PRECISION') AS FormMaxlength
FROM sys.tables AS t
    INNER JOIN sys.columns AS c
        ON t.object_id = c.object_id
    LEFT JOIN sys.extended_properties AS ep
        ON ep.major_id = c.object_id
           AND ep.minor_id = c.column_id
WHERE t.name = '@TableName';