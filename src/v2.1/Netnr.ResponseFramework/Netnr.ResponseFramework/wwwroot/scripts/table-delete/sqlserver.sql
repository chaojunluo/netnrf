-- 删除所有表

DECLARE @sql VARCHAR(8000);
WHILE EXISTS (SELECT name FROM sysobjects WHERE type = 'U')
BEGIN
    SELECT @sql = 'DROP TABLE ' + name
    FROM sysobjects
    WHERE (type = 'U');

    PRINT (@sql);

    EXEC (@sql);
END;