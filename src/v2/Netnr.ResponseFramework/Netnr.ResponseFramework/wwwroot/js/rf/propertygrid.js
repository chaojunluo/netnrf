//载入
var gd1 = z.Grid();
gd1.url = "/common/querydata?uri=systableconfig";
gd1.type = "propertygrid";
gd1.showGroup = true;
gd1.groupField = "TableName";
gd1.onBeforeBind = function (gd) {
    //propertygrid name-value 为必须字段，此处在数据源上面追加了该字段
    if (!gd1.isnotfirst) {
        gd1.isnotfirst = 1;

        var col1 = { field: "name", title: "name", width: 150 };
        var col2 = { field: "value", title: "value", width: 150 };
        gd.columns[0].splice(0, 0, col2);
        gd.columns[0].splice(0, 0, col1);
    }
    var rows = [];
    $(gd.data).each(function (i) {
        this.name = "Key " + i;
        this.value = "Val " + Math.random().toString().substring(2, 6);

        //value列启用编辑
        //this.editor = "text";
    });
}
gd1.sortName = "TableName,ColOrder";
gd1.sortOrder = "asc,asc";
gd1.load();

//刷新
z.button('reload', function () {
    gd1.load();
});