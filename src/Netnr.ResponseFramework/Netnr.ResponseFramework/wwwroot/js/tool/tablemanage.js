//载入列表
var gd1 = z.Grid();
gd1.id = "#Grid1";
gd1.autosizePid = "#PGrid1";
gd1.url = "/Tool/QueryTableConfig";
gd1.singleSelect = false;
gd1.pagination = false;
gd1.fitColumns = true;
gd1.checkbox = true;

//表头配置
gd1.columns = [[
    { field: "name", title: "表名", width: 150, halign: "center" },
    {
        field: "exists", title: "表配置", width: 60, halign: "center", align: "center", formatter: function (value, row, index) {
            return value == 1 ? '已生成' : value;
        }
    }
]];
gd1.load();

//建立表配置
function builder(maketype) {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        art("警告：确定生成？", function () {
            var names = [];
            $(rowData).each(function () {
                names.push(this.name)
            })

            $.ajax({
                url: '/tool/BuildTableConfig',
                data: {
                    names: names.join(','),
                    maketype: maketype
                },
                success: function (data) {
                    if (data == "success") {
                        gd1.load();
                    }
                    art(data);
                },
                error: function () {
                    art('error');
                }
            })
        })
    } else {
        art('select');
    }
}

//导出表设计
function exportexcel() {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        var names = [];
        $(rowData).each(function () {
            names.push(this.name)
        })
        location.href = "/tool/ExportTableInfo?names=" + names.join(',');
    } else {
        art('select');
    }
}

//表字典
var gd2 = z.Grid();
gd2.id = "#Grid2";
gd2.url = "/Tool/QueryTableInfo";
gd2.autosizePid = "#PGrid2";
gd2.pagination = false;
gd2.fitColumns = true;

gd2.showGroup = true;
gd2.groupField = "表名";
gd2.view = groupview;
gd2.groupFormatter = function (value, rows) {
    var row = rows[0];
    return row["表名"] + " " + row["表说明"];
}

//表头配置
gd2.columns = [[
    { field: "表名", title: "表名", hidden: 1 },
    { field: "表说明", title: "表说明", hidden: 1 },

    { field: "字段名", title: "字段名", width: 150, halign: "center" },
    { field: "类型", title: "类型", width: 100, halign: "center" },
    { field: "主键", title: "主键", width: 60, halign: "center", align: "center" },
    { field: "不为空", title: "不为空", width: 60, halign: "center", align: "center" },
    { field: "默认值", title: "默认值", width: 80, halign: "center", align: "center" },
    { field: "注释", title: "注释", width: 150, halign: "center" }
]];
gd2.onBeforeLoad = function (row, param) {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        var names = [];
        $(rowData).each(function () {
            names.push(this.name)
        })
        param.names = names.join(',');
    }
}
$('#btnQuery').click(function () {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        gd2.load();
    } else {
        art('select');
    }
})