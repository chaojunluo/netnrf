//关系符
z.DC["dataurl_cq_relation"] = {
    data: [
        { value: "Equal", text: "等于" },
        { value: "Contains", text: "包含" },
        { value: "LessThanOrEqual", text: "小于等于" },
        { value: "GreaterThanOrEqual", text: "大于等于" },
        { value: "BetweenAnd", text: "两值之间" },
        { value: "Clear", text: "" }
    ],
    init: function () {
        this.panelHeight = 185;
        this.formatter = function (row) {
            if (row.value == "Clear") {
                return "<span class='red'>（清空条件）</span>";
            }
            return row.text;
        };
        this.onClick = function (record) {
            if (record.value == "Clear") {
                var ei = gdquery.ei;
                setTimeout(function () {
                    //结束编辑
                    if (gdquery.ei != null) {
                        gdquery.func('endEdit', gdquery.ei);
                        gdquery.ei = null;
                    }
                }, 50);

                setTimeout(function () {
                    var rowData = gdquery.func('getSelected');
                    rowData.relation = null;
                    rowData.value1 = null;
                    rowData.value2 = null;
                    gdquery.func('updateRow', { index: ei, row: rowData });
                }, 80);
            }
        };
        this.onSelect = function (record) {
            if (record.value == "BetweenAnd") {
                var rowData = gdquery.func('getSelected');
                rowData.value2 = "";
                gdquery.func('updateRow', { index: gdquery.ei, row: rowData });
            }
        }
    }
}

//查询条件值格式化
function col_custom_value12(value, row) {
    if (row.relation == "Clear") {
        return "";
    }
    switch (row.FormType) {
        case "password":
            if (value != undefined) {
                var c = [], len = value.length;
                while (len) {
                    c.push('★');
                    len -= 1;
                }
                return c.join('');
            }
            break;
        case "checkbox":
            if (value != undefined) {
                var icon = value == "1" ? "fa-check-square-o" : "fa-square-o";
                return '<span class="fa ' + icon + ' fa-2x text-muted"></span>';
            }
            break;
        case "combobox":
            try {
                var node = findTreeNode(z.DC[String(row.FormUrl).toLowerCase()].data, value, "value");
                if (node) {
                    return node.text;
                }
            } catch (e) { }
            break;
        case "combotree":
            try {
                var node = findTreeNode(z.DC[String(row.FormUrl).toLowerCase()].data, value, "id");
                if (node) {
                    return node.text;
                }
            } catch (e) { }
            break;
    }
    return value;
}

/**
 * 递归搜索值
 * @param {any} data 数据源（children 上下级结构）
 * @param {any} value 搜索的值
 * @param {any} key 匹配对象，默认value
 */
function findTreeNode(data, value, key) {
    key = key || "value";
    var i = 0, len = data.length;
    for (; i < len; i++) {
        var node = data[i], child = node.children;
        if (node[key] == value) {
            return node;
        }
        if (child) {
            node = arguments.callee(child, value, key);
            if (node != undefined) {
                return node;
            }
        }
    }
}

var gdquery = z.Grid();
gdquery.id = "#GridQuery_" + z.TableIndex;
gdquery.autosizePid = "#PGridQuery_" + z.TableIndex;
gdquery.autosize = "p";
gdquery.fitColumns = true;
gdquery.striped = true;
gdquery.pagination = false;
gdquery.onClickCell = function (index, field, value) {
    setTimeout(function () {
        var row, allowedit = true;
        if ("value1,value2".indexOf(field) >= 0) {
            row = gdquery.func('getSelected');
            if (field == "value2" && row.relation != "BetweenAnd") {
                allowedit = false;
            }
        }
        if (allowedit) {
            z.GridEditor(gdquery, index, field, row)
        } else {
            //结束编辑
            if (gdquery.ei != null) {
                gdquery.func('endEdit', gdquery.ei);
                gdquery.ei = null;
            }
        }
    }, 10);
}
gdquery.data = [];
gdquery.columns = [[
    { field: "field", title: "键", width: 100, halign: "center", hidden: true },
    { field: "title", title: "条件名称", width: 100, halign: "center" },
    {
        field: "relation", title: "关系符", width: 80, halign: "center", FormType: "combobox", FormUrl: "dataurl_cq_relation", formatter: function (value) {
            if (value == "Clear") {
                return "";
            }
            $(z.DC["dataurl_cq_relation"].data).each(function () {
                if (this.value == value) {
                    value = this.text;
                    return false;
                }
            });
            return value;
        }
    },
    { field: "value1", title: "值1", width: 100, halign: "center", FormType: "text", formatter: col_custom_value12 },
    { field: "value2", title: "值2", width: 100, halign: "center", FormType: "text", formatter: col_custom_value12 }
]];
$('#fv_form_' + z.TableIndex).find('label.control-label').each(function () {
    var that = $(this), txt = that.next().find('input[data-query=1]');
    if (txt.length) {
        var ft = txt.attr('data-type'), fu = txt.attr('data-url');
        var row = { field: txt[0].name, title: that.html(), FormType: ft, FormUrl: fu };
        gdquery.data.push(row);
    }
});

//显示时
$('#fq_modal_' + z.TableIndex).on('shown.bs.modal', function () {
    if (!gdquery.isnotfirst) {
        gdquery.isnotfirst = 1;

        gdquery.bind();

        z.GridEditorBlank(gdquery);
        $(window).resize(function () {
            AutoHeightGrid(gdquery);
        });
    }
    AutoHeightGrid(gdquery);
});

//重置
$('#fq_clear_' + z.TableIndex).click(function () {
    $(gdquery.data).each(function (i) {
        this.relation = null;
        this.value1 = null;
        this.value2 = null;
    });
    gdquery.bind();
});

//查询
z.button('query', function () {
    z.GridLoading(gdquery);
    $('#fq_modal_' + z.TableIndex).modal();
});

//获取查询条件
function QueryWhereGet() {
    var items = [], sq = z.SqlQuery();
    $(gdquery.data).each(function (index, obj) {
        $(z.DC["dataurl_cq_relation"].data).each(function () {
            if (this.value == obj.relation) {
                var item = sq.item();

                item.field = obj.field;
                item.relation = obj.relation;

                if (obj.relation == "BetweenAnd") {
                    if (obj.value1 != "" && obj.value2 != "") {
                        item.value = [obj.value1, obj.value2];
                        items.push(item);
                    }
                } else {
                    if (obj.FormType == "checkbox") {
                        item.value = obj.value1 == "1" ? "1" : "IS NULL 0";
                        items.push(item);
                    } else {
                        if (obj.value1 != "") {
                            item.value = obj.value1;
                            items.push(item);
                        }
                    }
                }
                return false;
            }
        })
    });
    if (items.length) {
        if (items.length > 1) {
            sq.wheres.andor = "and";
            sq.wheres.items = items;
        } else {
            sq.wheres = items[0];
        }
    }
    return sq;
}

//查询确定
$('#fq_ok_' + z.TableIndex).click(QueryWhereOk);
$('#fq_okclone_' + z.TableIndex).click(QueryWhereOk);
function QueryWhereOk() {
    //有值1无关系符提示
    var errs = [];
    $(gdquery.data).each(function () {
        if (this.relation == undefined || this.relation == "") {
            if ("checkbox".indexOf(this.FormType) >= 0) {
                if (this.value1 != undefined) {
                    errs.push(this.title);
                }
            } else {
                if (this.value1 != undefined && this.value1 != "") {
                    errs.push(this.title);
                }
            }
        }
    });

    if (errs.length) {
        var mo = art('<div style="font-size:initial;">' + errs.join('</br>') + '</div>');
        mo.modal.find('h4.modal-title').html('<b class="red">请选择关系符</b>');
    } else {
        if (typeof QueryWhereCallBack == "function") {
            if (QueryWhereCallBack() != false) {
                $('#fq_modal_' + z.TableIndex).modal('hide');
            }
        }
    }
}