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
        field: "exists", title: "表配置", width: 80, halign: "center", align: "center", formatter: function (value, row, index) {
            return value == 1 ? '✔' : value;
        }
    }
]];
gd1.load();

//建立表配置
$('#btnBuilder').click(function () {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        var names = [];
        $(rowData).each(function () {
            names.push(this.name)
        })

        $('#btnBuilder')[0].disabled = true;
        $.ajax({
            url: '/tool/BuildTableConfig',
            data: {
                names: names.join(','),
                cover: $('#chkCover')[0].checked ? 1 : 0
            },
            success: function (data) {
                if (data == "success") {
                    gd1.load();
                }
                art(data);
            },
            error: function () {
                art('error');
            },
            complete: function () {
                $('#btnBuilder')[0].disabled = false;
            }
        })
    } else {
        art('select');
    }
});

//表字典
var gd2 = z.Grid();
gd2.id = "#Grid2";
gd2.url = "/Tool/QueryTableInfo";
gd2.type = "propertygrid";
gd2.groupField = "表名";
gd2.autosizePid = "#PGrid2";
gd2.pagination = false;
gd2.showGroup = true;
gd2.groupFormatter = function (group, rows) {
    var row = rows[0];
    return row["表名"] + " " + row["表说明"];
}
//表头配置
gd2.columns = [[
    //propertygrid name-value 为必须字段，此处在数据源上面追加了该字段,但不显示
    { field: "name", title: "name", width: 150, hidden: 1 },
    { field: "value", title: "value", width: 150, hidden: 1 },

    { field: "表名", title: "表名", hidden: 1 },
    { field: "表说明", title: "表说明", hidden: 1 },

    { field: "字段名", title: "字段名", width: 150, halign: "center" },
    {
        field: "类型", title: "类型", width: 100, halign: "center", formatter: function (value, row, index) {
            if (value.indexOf('datetime') >= 0) {
                value = "datetime";
            }
            if (value.indexOf('timestamp') >= 0) {
                value = "timestamp";
            }
            value = value.replace("(-1)", "(MAX)");
            return value;
        }
    },
    {
        field: "主键", title: "主键", width: 80, halign: "center", align: "center", formatter: function (value, row, index) {
            return value == 'YES' ? '✔' : value;
        }
    },
    {
        field: "不为空", title: "不为空", width: 80, halign: "center", align: "center", formatter: function (value, row, index) {
            return value == 'YES' ? '✔' : value;
        }
    },
    {
        field: "默认值", title: "默认值", width: 100, halign: "center", formatter: function (value, row, index) {
            if (value == 'NULL') {
                value = '';
            }
            value.replace(/\(\(.*?\)\)/g, function () {
                value = arguments[1];
            })
            return value;
        }
    },
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