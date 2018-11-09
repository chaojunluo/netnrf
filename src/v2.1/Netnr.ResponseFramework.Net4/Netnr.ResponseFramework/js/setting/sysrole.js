//载入
var gd1 = z.Grid();
gd1.url = "/Setting/QuerySysRole?tableName=" + z.TableName;
gd1.sortName = "CreateTime";
gd1.onDblClickRow = function (index, row) {
    //双击行模拟点编辑
    z.buttonClick('edit');
}
gd1.onBeforeLoad = function (row, param) {
    var sq = QueryWhereGet();
    param.wheres = sq.stringify();
}
gd1.load();

//查询回调
function QueryWhereCallBack() {
    gd1.pageNumber = 1;
    gd1.load();
}

//刷新
z.button('reload', function () {
    gd1.load();
});

//新增
z.button('add', function () {
    //表单标题
    z.FormTitle({
        icon: 0,
        title: '新增系统角色'
    });
    $('#fv_modal_1').modal();
});

//查看
z.button('see', function () {
    //获取选中行
    var rowData = gd1.func("getSelected");
    if (rowData) {
        //选中行回填表单
        z.FormEdit(rowData);
        //表单标题
        z.FormTitle({
            icon: 2,
            title: '查看系统角色',
            required: false
        });
        //禁用
        z.FormDisabled(true);
        //显示模态框
        $('#fv_modal_1').modal();
    } else {
        art("select");
    }
});
//关闭模态框后
$('#fv_modal_1').on('hidden.bs.modal', function () {
    //是查看时，解除禁用
    if (z.btnTrigger == "see") {
        z.FormDisabled(false);
    }
});

//修改
z.button('edit', function () {
    //获取选中行
    var rowData = gd1.func("getSelected");
    if (rowData) {
        //选中行回填表单
        z.FormEdit(rowData);
        //表单标题
        z.FormTitle({
            icon: 1,
            title: '修改系统角色'
        });
        //显示模态框
        $('#fv_modal_1').modal();
    } else {
        art("select");
    }
});

//保存
$('#fv_save_1').click(function () {
    //检测必填项
    if (z.FormRequired('red')) {
        $('#fv_save_1')[0].disabled = true;
        $.ajax({
            url: "/Setting/SaveSysRole?savetype=" + z.btnTrigger,
            type: "post",
            data: $("#fv_form_1").serialize(),
            success: function (data) {
                if (data == "success") {
                    //新增成功，重新载入
                    if (z.btnTrigger == "add") {
                        gd1.load();
                    } else {
                        //编辑成功，修改行
                        gd1.func("updateRow", {
                            index: gd1.func('getRowIndex', gd1.func('getSelected')),
                            row: z.FormToJson()
                        });
                    }
                    $('#fv_modal_1').modal('hide');
                } else {
                    art('fail');
                }
            },
            error: function () {
                art('error');
            }
        });

        $('#fv_save_1')[0].disabled = false;
    }
});

//删除
z.button('del', function () {
    var rowData = gd1.func("getSelected");
    if (!rowData) {
        art('select');
        return false;
    }
    art('确定删除选中的行', function () {
        $.ajax({
            url: "/Setting/DelSysRole?id=" + rowData.Id,
            type: "post",
            success: function (data) {
                if (data == "exists") {
                    art('角色下有用户，不能删除');
                } else if (data == "success") {
                    gd1.load();
                } else {
                    art('fail');
                }
            }
        })
    });
});

//载入导航
var cbnav = z.Combo();
cbnav.id = "#navTree";
cbnav.url = "/common/querymenu?type=all";
cbnav.type = "tree";
cbnav.checkbox = true;
cbnav.onBeforeSelect = function (node) {
    return node.children == null;
}
cbnav.onSelect = function (node) {
    cbnav.setbtn = true;
    var nodes = cbtn.func('getChildren');
    $(nodes).each(function () {
        cbtn.func('uncheck', this.target);
    });
    var btns = authButtons[node.id];
    if (btns) {
        btns = btns.split(',');
        $(nodes).each(function () {
            if (btns.indexOf(this.id) >= 0) {
                cbtn.func('check', this.target);
            }
        });
        z.TreeVagueCheck(cbtn, btns);
    }
    cbnav.setbtn = null;
}
cbnav.onComplete(function () {
    var nodes = cbnav.func('getChildren');
    $(nodes).each(function () {
        cbnav.func('uncheck', this.target);
    });
    $(nodes).each(function () {
        if (authMenus.indexOf(this.id) >= 0) {
            cbnav.func('check', this.target);
        }
    });
    z.TreeVagueCheck(cbnav, authMenus);
});



//功能按钮
var cbtn = z.Combo();
cbtn.id = "#btnTree";
cbtn.url = "/Common/QueryButtonTree";
cbtn.type = 'tree';
cbtn.checkbox = true;
cbtn.onBeforeCheck = cbtn.onBeforeSelect = function () {
    var navnode = cbnav.func('getSelected');
    if (!navnode) {
        art('选菜单后再选按钮');
        return false;
    }
}
cbtn.onSelect = function (node) {
    cbtn.func($(node.target).find('span.tree-checkbox').hasClass('tree-checkbox1') ? 'uncheck' : 'check', node.target);
}
cbtn.onCheck = function (node) {
    if (!cbnav.setbtn) {
        var ckd = cbtn.func('getChecked'), btnid = [];
        ckd = ckd.concat(cbtn.func('getChecked', 'indeterminate'));
        $(ckd).each(function () {
            btnid.push(this.id);
        })
        authButtons[cbnav.func('getSelected').id] = btnid.join(',');
    }
}

//菜单
var authMenus = null;
//菜单对应的功能按钮
var authButtons = null;

//权限设置
z.button('auth', function () {
    var rowData = gd1.func("getSelected");
    if (rowData) {
        authMenus = rowData.Menus ? rowData.Menus.split(',') : [];
        authButtons = rowData.Buttons ? $.parseJSON(rowData.Buttons) : {};
        $('#myModalAuth').modal();
        if (cbnav.data) {
            cbnav.bind();
            cbtn.bind();
        } else {
            cbnav.load();
            cbtn.load();
        }
    } else {
        art('select');
    }
});

//保存菜单及按钮
$('#btnSaveAuth').click(function () {
    var navckd = cbnav.func('getChecked');
    navckd = navckd.concat(cbnav.func('getChecked', 'indeterminate'));
    authMenus = [];
    $(navckd).each(function () {
        authMenus.push(this.id);
    });

    var rowData = gd1.func('getSelected');
    rowData.Menus = authMenus.join(',');
    rowData.Buttons = JSON.stringify(authButtons);

    $('#btnSaveAuth')[0].disabled = true;
    $.ajax({
        url: "/Setting/SaveSysRole",
        type: "post",
        data: rowData,
        success: function (data) {
            if (data == "success") {
                gd1.func("updateRow", {
                    index: gd1.func('getRowIndex', rowData),
                    row: rowData
                });
                $('#myModalAuth').modal('hide');
            } else {
                art('fail');
            }
        },
        error: function () {
            art('error');
        },
        complete: function () {
            $('#btnSaveAuth')[0].disabled = false;
        }
    });
});