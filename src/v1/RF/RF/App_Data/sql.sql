-- NuGet 控制台命令安装 Sqlite.core 版本的命令：
--Install-Package System.Data.SQLite.Core

-- Web.Config配置 sqlite 连接字符串
-- 项目App_Data目录路径：Data Source=|DataDirectory|\sqlite.db;Pooling=true;


--用于快速填充列配置表
--查询表 获取注释、列名
SELECT RANK()over(order by c.[name]) AS ID, t.[name] AS vtable,c.[name] AS col_fiels,cast(ep.[value] as nvarchar(200)) AS col_title,
		'100' AS col_order,'left' AS col_align,'100' as col_width,'' as col_format,'1' as col_hidden
FROM sys.tables AS t  INNER JOIN sys.columns   AS c ON t.object_id = c.object_id  
LEFT JOIN sys.extended_properties AS ep   
ON ep.major_id = c.object_id AND ep.minor_id = c.column_id WHERE ep.class =1   
AND t.name='sys_role'


