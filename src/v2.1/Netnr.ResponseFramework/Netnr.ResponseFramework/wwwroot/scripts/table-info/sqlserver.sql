-- 查询系统表生成表字典

SELECT 表名 = d.name,
       表说明 = ISNULL(f.value, ''),
       字段名 = a.name,
       类型 = b.name + '(' + CONVERT(VARCHAR(10), COLUMNPROPERTY(a.id, a.name, 'PRECISION')) + ')',
       主键 = CASE
                WHEN EXISTS
                     (
                         SELECT 1
                         FROM sysobjects
                         WHERE xtype = 'PK'
                               AND name IN (
                                               SELECT name
                                               FROM sysindexes
                                               WHERE indid IN (
                                                                  SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid
                                                              )
                                           )
                     ) THEN
                    'YES'
                ELSE
                    ''
            END,
       不为空 = CASE
                  WHEN a.isnullable = 1 THEN
                      ''
                  ELSE
                      'YES'
              END,
       默认值 = CASE
                 WHEN e.text IS NULL THEN
                     'NULL'
                 ELSE e.text
             END,
       注释 = ISNULL(g.[value], '')
FROM syscolumns a
    LEFT JOIN systypes b
        ON a.xtype = b.xusertype
    INNER JOIN sysobjects d
        ON a.id = d.id
           AND d.xtype = 'U'
           AND d.name != 'dtproperties'
    LEFT JOIN syscomments e
        ON a.cdefault = e.id
    LEFT JOIN sys.extended_properties g
        ON a.id = g.major_id
           AND a.colid = g.minor_id
    LEFT JOIN sys.extended_properties f
        ON d.id = f.major_id
           AND f.minor_id = 0
WHERE d.name IN ('@TableName')
ORDER BY d.name,
         a.colorder;