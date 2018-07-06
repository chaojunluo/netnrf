z.DC["dataurl_formtype"] = {
    //绑定数据
    data: [
        { value: "text", text: "text 文本" },
        { value: "date", text: "yyyy-MM-dd 日期" },
        { value: "time", text: "mm:ss 时间" },
        { value: "datetime", text: "yyyy-MM-dd HH:mm:ss 日期时间" },
        { value: "calc", text: "calc 计算器" },
        { value: "combobox", text: "combobox 下拉列表框" },
        { value: "combotree", text: "combotree 下拉列表树" },
        { value: "modal", text: "modal 模态弹出（浏览）" },
        { value: "checkbox", text: "checkbox 复选框" },
        { value: "password", text: "password 密码框" }
    ],
    //绑定数据前回调
    init: function (obj) {
        //this和obj 都是 z.Combo构造的对象（obj参数可以不要，直接用this）
        //允许编辑，具体配置项参考 EasyUI文档
        this.editable = true; //obj.editable = true;
    }
};
z.DC["/common/querymenu?custom=m"] = {
    init: function () {
        this.panelHeight = 460;
    }
}
z.DC["dataurl_colformat"] = {
    data: [
        { value: "11", text: "yyyy-MM" },
        { value: "12", text: "HH:mm:ss" },
        { value: "13", text: "yyyy-MM-dd HH:mm:ss" },
        { value: "14", text: "yyyy-MM-dd" },
        { value: "15", text: "精确两位" },
        { value: "16", text: "确两位 带￥" },
        { value: "17", text: "1√ 0×" },
        { value: "18", text: "1× 0√" },
        { value: "19", text: "1正常 0停用" }
    ]
};
z.DC["dataurl_colalign"] = {
    data: [
        { value: 1, text: "居左" },
        { value: 2, text: "居中" },
        { value: 3, text: "居右" }
    ],
    init: function () {
        this.panelHeight = 100;
    }
}
z.DC["dataurl_formarea"] = {
    data: [
        { value: 1, text: "值一" },
        { value: 2, text: "值二" }
    ],
    init: function () {
        this.multiple = true;
        this.panelHeight = 100;
    }
};

//modal 浏览示例
z.DC["/setting/sysuser"] = {
    init: function () {
        //允许输入
        //this.editable = true;

        //确定回调事件
        this.okClick = function () {
            var wd = this.modal.find('iframe')[0].contentWindow;
            var rowData = wd.gd1.func('getSelected');

            //由于新增、编辑、公共查询都调用一个模态框，需要对确定事件做不同处理
            //this.guide 指向触发模态框的节点
            //this.guidetype 触发类型，form 表单触发，table 表格编辑触发
            switch (this.guidetype) {
                //表单输入，即新增、编辑
                case "form":
                    this.guide.val(rowData ? rowData.RoleName : '');
                    break;
                //表格编辑，公共查询
                case "table":
                    {

                    }
                    break;
            }

            this.hide();
        };
    }
};

//modal 浏览示例
z.DC["/setting/sysrole"] = {
    init: function () {
        //配置项看 z.Modal组件
        this.title = '<i class="fa fa-search blue"></i><span>选择角色<span>';
        //模态框大小 3最大 2普通 1小
        this.size = 2;
        //设置iframe最大高度
        this.heightIframe = 800;
        //OK按钮文本
        this.okText = '确定';
        //不显示右上角关闭按钮
        this.showClose = false;
        //不显示取消按钮
        this.showCancel = false;

        //确定回调
        this.okClick = function () {
            //根据模态框 this.modal 对象 找到 iframe
            var wd = this.modal.find('iframe')[0].contentWindow;
            //获取iframe里面的选中行数据
            var rowData = wd.gd1.func('getSelected');

            //由于新增、编辑、公共查询都调用一个模态框，需要对确定事件做不同处理
            //this.guide 指向触发模态框的节点
            //this.guidetype 触发类型，form 表单触发，table 表格编辑触发
            switch (this.guidetype) {
                //表单输入，即新增、编辑
                case "form":
                    this.guide.val(rowData ? rowData.RoleName : '');
                    break;
                //表格编辑，公共查询
                case "table":
                    {

                    }
                    break;
            }

            this.hide();
        };
    }
};



//载入
var gd1 = z.Grid();
gd1.url = "/common/querydata?uri=tempexample";
gd1.multiSort = true;
gd1.sortName = "TableName,ColOrder";
gd1.sortOrder = "asc,asc";
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
        title: '新增表单'
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
            title: '查看表单',
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
            title: '修改表单'
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
            url: "/Setting/SaveSysTableConfig?savetype=" + z.btnTrigger,
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
            url: "/Setting/DelSysTableConfig?id=" + rowData.ID,
            type: "post",
            success: function (data) {
                if (data == "success") {
                    gd1.load();
                } else {
                    art('fail');
                }
            }
        })
    });
});