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
    { field: "id", title: "表名", width: 150, halign: "center", ColQuery: 1, ColRelation: "Contains,Equal" },
    {
        field: "pid", title: "表配置", width: 60, halign: "center", align: "center", formatter: function (value, row, index) {
            return value == 1 ? '已生成' : value;
        }
    }
]];
gd1.load();

//建立表配置
function Generate(maketype) {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        art("警告：确定生成？", function () {
            var names = [];
            $(rowData).each(function () {
                names.push(this.id)
            })

            $.ajax({
                url: '/tool/BuildTableConfig',
                data: {
                    names: names.join(','),
                    maketype: maketype
                },
                dataType: 'json',
                success: function (data) {
                    if (data.code == 200) {
                        gd1.load();
                    }
                    art(data.msg);
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
            names.push(this.id)
        })
        GlobalExport("/tool/ExportTableInfo", function (data) {
            data.names = names.join(',')
        })
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
            names.push(this.id)
        })
        param.names = names.join(',');
    }
}
gd2.onComplete(function () {
    //是生成代码事件
    if (gd2.isGenerateCode) {
        codeGenerate.templateParam.PrimaryKey = null;
        $.each(gd2.data, function (i, row) {
            if (row["主键"] == "YES") {
                codeGenerate.templateParam = {
                    TableName: row["表名"],
                    TableTitle: row["表说明"] || row["表名"],
                    PrimaryKey: row["字段名"]
                }
                return false;
            }
        });
        if (codeGenerate.templateParam.PrimaryKey) {
            codeGenerate.viewCode();
        } else {
            art('未找到主键列');
        }
    }
});

$('#btnQuery').click(function () {
    var rowData = gd1.func('getSelections');
    if (rowData.length) {
        gd2.load();
    } else {
        art('select');
    }
})

//生成代码
$('#btnGenerateCode').click(function () {
    gd2.isGenerateCode = 1;
    var rowData = gd1.func('getSelections');
    if (rowData.length == 1) {
        gd2.load();
    } else if (rowData.length > 1) {
        art('请仅选择一行');
    } else {
        art('select');
    }
});

var codeGenerate = {
    //模版参数
    templateParam: {
        TableName: null,
        TableTitle: null,
        PrimaryKey: null
    },
    //显示代码
    viewCode: function () {
        gd2.isGenerateCode = 0;

        $('#fv_modal_11').modal();

        require(['vs/editor/editor.main'], function () {

            $.get('/scripts/table-code/controller.txt').then(function (res) {
                if (window.editor1) {
                    window.editor1.dispose()
                }
                window.editor1 = monaco.editor.create(document.getElementById('TabC1').children[0], {
                    value: codeGenerate.templateReplace(res),
                    language: 'csharp',
                    automaticLayout: true,
                    minimap: {
                        enabled: false
                    }
                });
            })

            $.get('/scripts/table-code/view.txt').then(function (res) {
                if (window.editor2) {
                    window.editor2.dispose()
                }
                window.editor2 = monaco.editor.create(document.getElementById('TabC2').children[0], {
                    value: codeGenerate.templateReplace(res),
                    language: 'csharp',
                    automaticLayout: true,
                    minimap: {
                        enabled: false
                    }
                });
            })

            $.get('/scripts/table-code/javascript.txt').then(function (res) {
                if (window.editor3) {
                    window.editor3.dispose()
                }
                window.editor3 = monaco.editor.create(document.getElementById('TabC3').children[0], {
                    value: codeGenerate.templateReplace(res),
                    language: 'javascript',
                    automaticLayout: true,
                    minimap: {
                        enabled: false
                    }
                });
            })
        });
    },
    //模版参数替换
    templateReplace: function (template) {
        return template.replace(/@TableName@/g, codeGenerate.templateParam.TableName)
            .replace(/@TableTitle@/g, codeGenerate.templateParam.TableTitle)
            .replace(/@PrimaryKey@/g, codeGenerate.templateParam.PrimaryKey)
    },
    //代码编辑器高度自适应
    editorAutoHeight: function () {
        var mtc = $('#myTabContent');
        mtc.children().children().height($(window).height() - mtc.offset().top - 35);
    }
}

//保存代码
$('#btnSaveGenerateCode').click(function () {
    $('#btnSaveGenerateCode')[0].disabled = true;
    $.ajax({
        url: "/Tool/SaveGenerateCode",
        type: 'post',
        data: {
            name: codeGenerate.templateParam.TableName,
            controller: editor1.getValue(),
            view: editor2.getValue(),
            javascript: editor3.getValue()
        },
        dataType: "json",
        success: function (data) {
            art(data.msg)
        },
        error: function () {
            art('error')
        },
        complete: function () {
            $('#btnSaveGenerateCode')[0].disabled = false;
        }
    })
});

//显示时、窗口大小变动时，编辑器高度自适应
$('#fv_modal_11').on("shown.bs.modal", function () {
    codeGenerate.editorAutoHeight();
})
$(window).on('resize', function () {
    codeGenerate.editorAutoHeight();
})