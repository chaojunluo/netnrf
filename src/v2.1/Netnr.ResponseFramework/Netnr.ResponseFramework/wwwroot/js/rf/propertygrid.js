//载入
var gd1 = z.Grid();
gd1.url = "/Setting/QuerySysTableConfig?tableName=SysTableConfig";
gd1.type = "propertygrid";
gd1.showGroup = true;
gd1.groupField = "TableName";
gd1.onBeforeBind = function (gd) {
    //propertygrid name-value 为必须字段，此处在数据源上面追加了该字段
    if (gd1.isinit) {
        var col1 = { field: "name", title: "name", hidden: 1 };
        var col2 = { field: "value", title: "value", hidden: 1 };
        gd.columns[0].splice(0, 0, col2);
        gd.columns[0].splice(0, 0, col1);
    }
}
gd1.onLoadSuccess = function () {
    gd1.func('hideColumn', gd1.groupField);
}
gd1.sortName = "TableName,ColOrder";
gd1.sortOrder = "asc,asc";
gd1.load();

//刷新
z.button('reload', function () {
    gd1.load();
});