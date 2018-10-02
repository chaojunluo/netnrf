//载入
var gd1 = z.Grid();
gd1.url = "/Setting/QuerySysTableConfig?tableName=systableconfig"
gd1.multiSort = true;
gd1.sortName = "TableName,ColOrder";
gd1.sortOrder = "asc,asc";
gd1.load();

//刷新
z.button('reload', function () {
    gd1.load();
});