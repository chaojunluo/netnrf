/// <reference path="jquery.min.js" />
/// <reference path="../Content/bootstrap/js/bootstrap.min.js" />
/// <reference path="../Content/easyui/jquery.easyui-1.4.5.js" />

/*
 *  闭包函数
 *  z.Grid：DataGrid、TreeGrid
 *  z.Combo：Combotree、Combobox
 *  z.Modal：Bootstrap模态框
 */
(function () {

    var z = function (t) { return new z.prototype.init(t) };
    z.fn = z.prototype = { init: function (t) { this[0] = typeof t === "object" ? t : document.getElementById(t); return this } };
    z.prototype.init.prototype = z.prototype;

    //遍历对象 字符串或数组
    z.each = function (obj, fn) {
        //this指向obj[i]; i是第一个参数 obj[i]第二个 ...
        var i = 0, len = obj.length;
        for (; i < len; i++) { if (fn.call(obj[i], i, obj[i]) == false) { break } }
    };

    //PC、Mobile判断
    z.isPC = function () { return !navigator.userAgent.match(/(iPhone|iPod|Android|ios)/i) }

    //Grid
    z.Grid = function () { return new z.Grid.prototype.init() };
    z.Grid.fn = z.Grid.prototype = {
        init: function () { return this },
        //容器#ID 默认#Grid1
        id: "#Grid1",
        //分页 默认启用
        page: true,
        //页码 默认第一页
        pageIndex: 1,
        //页量 默认30条
        pageSize: 30,
        //总条数 默认0
        total: 0,
        //缓存 total：总条数、columns：列配置、frozens：冻结列、data：数据行
        dataCatch: { total: 0, columns: [], frozens: [], data: [] },
        //单选 默认true
        single: true,
        //复选框 默认false
        checkbox: false,
        //操作列 默认false
        control: false,
        //显示页脚 默认true
        showFooter: true,
        //请求url
        url: "",
        //序号 默认开启
        rownum: true,
        //标题列配置源
        columns: [],
        //标题已加载标记
        columnsExists: 0,
        //冻结列配置源
        frozens: [],
        //本地数据源
        data: null,
        //请求POST参数
        post: {},
        //多列排序 默认关闭
        sortMulti: false,
        //排序字段,逗号分割
        sortField: "",
        //排序方式,逗号分割，与排序字段索引对应
        sortOrder: "",
        //格式化
        format: function (field, value) { return value },
        //自适应 可选 null|xy（水平沉底）|x（水平）|y（沉底）|p（父容器完全填充）
        autosize: "xy",
        //自适应父容器 默认#myBody
        autosizePid: '#myBody',
        //模式 可选：TreeGrid、PropertyGrid 不区分大小写
        model: "DataGrid",
        //TreeGrid关联键
        treeId: "",
        //TreeGrid展开列
        treeName: "",
        //tree节点展开请求
        treeSrc: function (row) { return "" },
        //分组字段名
        groupId: "",
        //完成事件索引
        completeIndex: 0,
        //完成事件 (注意死循环，在完成事件重绑数据需加标记跳出循环绑定)
        completeE: function (fn) {
            this.completeIndex += 1;
            this["completeF" + this.completeIndex] = fn;
        },
        //单击行事件索引
        clickRowIndex: 0,
        //单击行事件
        clickRowE: function (fn) {
            this.clickRowIndex += 1;
            this["clickRowF" + this.clickRowIndex] = fn;
        },
        //双击行事件索引
        dblclickRowIndex: 0,
        //双击行事件
        dblclickRowE: function (fn) {
            this.dblclickRowIndex += 1;
            this["dblclickRowF" + this.dblclickRowIndex] = fn;
        },
        //点击单元格事件
        clickCellE: function (index, field, value) { },
        //行样式 参数：index,row
        rowStyle: null,
        //绑定数据前执行
        bindBeforeE: null,
        //加载数据源
        load: function () { z.GridLoad(this); return this },
        //刷新数据，可选页码
        F5: function (pageIndex) { arguments.length && (this.pageIndex = pageIndex); this.load(); return this },
        //本地刷新，缓存的数据重新绑定
        F5Local: function () { z.GridBind(this); return this },
        //设置选中行 索引0开始 默认第一行
        selectRow: function (index) { try { $(this.id)[this.model.toLowerCase()]('selectRow', index || 0) } catch (e) { alert(e); } return this },
        //选中行索引
        rowIndex: function () { return $(this.id).datagrid('getRowIndex', this.row()) },
        //获取选中的行
        row: function () { return z.Row(this) },
        //获取所有行
        rowAll: function () { return $(this.id).datagrid('getRows') },
        //获取页脚行
        rowFooter: function () { return $(this.id).datagrid('getFooterRows') },
        //选中行编辑
        rowEdit: function () { z.GridRowEdit(this.row()); return this },
        //更新行
        updataRow: function (row, index) { z.GridUpdataRow(this.id, row, index); return this },
        //更新页脚 数组对象
        updateFooter: function (arr) { $(this.id).datagrid('reloadFooter', arr); return this },
        //新增一行
        addRow: function (rowData, index) { z.GridAddRow(this.id, rowData, index); return this },
        //删除一行
        delRow: function (index) { z.GridDelRow(this.id, index); return this },
        //获取所有列
        columnFields: function () { return $(this.id).datagrid('getColumnFields') },
        //设置列隐藏 一个或列数组
        columnHide: function (fields) {
            var arr = typeof fields == "object" ? fields : [fields], that = this;
            $(arr).each(function () { $(that.id).datagrid('hideColumn', this); })
            return this;
        },
        //设置列显示 一个或列数组
        columnShow: function (fields) {
            var arr = typeof fields == "object" ? fields : [fields], that = this;
            $(arr).each(function () { $(that.id).datagrid('showColumn', this) })
            return this;
        },
        //获取列属性
        columnOption: function (field) { return $(this.id).datagrid('getColumnOption', field) },
        //是否允许编辑 默认tree
        editorEnable: true,
        //处于编辑的索引
        editorIndex: NaN,
        //处于编辑的列
        editorField: null,
        //开始编辑回调
        beginEditE: function () { },
        //开始编辑
        beginEdit: function (index, field, editor) {
            !isNaN(this.editorIndex) && this.endEdit(this.editorIndex);
            var fs = this.columnFields(), that = this;
            $(fs).each(function () {
                if (this == field) {
                    that.columnOption(field).editor = editor;
                } else {
                    that.columnOption(this).editor = null
                }
            });
            $(this.id).datagrid('beginEdit', index);
            this.beginEditE();
            this.editorIndex = index;
            this.editorField = field;
            return this
        },
        //结束编辑回调
        endEditE: function () { },
        //结束编辑
        endEdit: function (index) {
            try {
                $(this.id).datagrid('endEdit', index || this.editorIndex);
                this.editorIndex = NaN;
                this.editorField = null;
            } catch (e) { };
            this.endEditE();
            return this;
        },
        //计算
        calc: function (arr, mode) { return z.GridCalc(this, arr, mode) },
        //合并列 
        mergeCell: function (fields) { var that = this; setTimeout(function () { z.GridMergeCell(that, fields) }, 50); return this }
    };
    z.Grid.prototype.init.prototype = z.Grid.prototype;

    //加载Grid配置的源
    z.GridLoad = function (gd) {
        if (gd.model.toLowerCase() == "datagrid") { try { $(gd.id).datagrid("loading") } catch (e) { } }
        gd.post["sortField"] = gd.sortField;
        gd.post["sortOrder"] = gd.sortOrder;
        $.ajax({
            url: gd.url + (gd.url.indexOf('?') > -1 ? "&" : "?") + "pageIndex=" + gd.pageIndex + "&pageSize=" + gd.pageSize + "&columnsExists=" + gd.columnsExists,
            type: "POST",
            data: gd.post,
            dataType: "json",
            success: function (data) {
                gd.total = data.total || 0;
                gd.data = data.data;
                if (data.columns) {
                    var columns = [], frozens = [];
                    $(data.columns).each(function (k, v) {
                        var column = {
                            field: this.field, title: this.title,
                            width: this.width == 0 ? null : this.width, hidden: this.hide == 1 ? true : false, halign: "center",
                            align: this.align == 2 ? "center" : this.align == 3 ? "right" : "left",
                            sortable: this.sort,
                            f_type: this.f_type, f_url: this.f_url, f_area: this.f_area, f_col: this.f_col,
                            f_order: this.f_order, f_hide: this.f_hide, f_required: this.f_required,
                            formatter: function (value) { return z.GridFormat(v.format, value, v.field) }
                        }
                        if (this.frozen == "1") { frozens.push(column); }
                        else { columns.push(column); }
                    });
                    gd.columns = columns;
                    gd.frozens = frozens;
                    gd.columnsExists = 1;
                }
                gd.dataCatch.data = gd.data;
                gd.dataCatch.frozens = gd.frozens;
                gd.dataCatch.columns = gd.columns;
                gd.dataCatch.total = gd.total;
                gd.vname = data.vname;
                //大于第一页没数据 倒推一页重新加载
                if (gd.pageIndex > 1 && gd.total > 1 && gd.data.length == 0) {
                    gd.pageIndex -= 1;
                    z.GridLoad(gd);
                } else {
                    z.GridBind(gd);
                }
            },
            error: function (e) {
                if (e.status == 200 && e.readyState == 4 && gd.dataCatch != "") {
                    gd.dataCatch.total = 0;
                    gd.dataCatch.rows = [];
                    z.GridBind(gd);
                }
            }
        });
        return gd;
    }

    //判断数组
    z.isArray = function (t) { return Object.prototype.toString.call(t) === "[object Array]" };

    //绑定Grid数据源
    z.GridBind = function (gd) {
        //绑定前
        if (typeof gd.bindBeforeE == "function") {
            var rtn = gd.bindBeforeE();
            if (rtn == false) return false;
        }

        //插入操作列
        if (gd.control) {
            var has = false;
            $(gd.frozens).each(function () {
                if (this.field == "col_control") {
                    has = true;
                    return false;
                }
            });
            !has && gd.frozens.splice(0, 0, { field: 'col_control', title: '操作', align: 'center', width: 80 });
        }
        //插入复选框
        if (gd.checkbox) {
            "checkbox" in (gd.frozens[0] || {}) || gd.frozens.splice(0, 0, { field: "ck", checkbox: true });
        }

        //Grid填充对象
        var _obj = {
            data: gd.data,
            rownumbers: gd.rownum,
            singleSelect: gd.single,
            columns: z.isArray(gd.columns[0]) ? gd.columns : [gd.columns],
            showFooter: gd.showFooter,
            onClickCell: gd.clickCellE,
            onClickRow: function (index, row) {
                for (var i = 0; i < gd.clickRowIndex + 1; i++) {
                    typeof gd["clickRowF" + i] == "function" &&
                    gd["clickRowF" + i](row, index);
                }
            },
            onDblClickRow: function (index, row) {
                for (var i = 0; i < gd.dblclickRowIndex + 1; i++) {
                    typeof gd["dblclickRowF" + i] == "function" &&
                    gd["dblclickRowF" + i](row, index);
                }
            },
            frozenColumns: [gd.frozens],
            multiSort: gd.sortMulti,
            pagination: gd.page,
            onSortColumn: function (sort, order) {
                gd.sortField = sort;
                gd.sortOrder = order;
                gd.pageIndex = 1;
                z.GridLoad(gd);
            },
            rowStyler: gd.rowStyle,
            loadMsg: "加载中 ...",

            idField: gd.treeId,
            treeField: gd.treeName,
            onBeforeExpand: function (row) {
                var treeSrc = gd.treeSrc(row);
                $(this).treegrid('options').url = treeSrc;
            },
            groupField: gd.groupId,
        };
        if (gd.model.toLowerCase() == "propertygrid") {
            _obj.showGroup = true;
        }

        //绑定
        $(gd.id)[gd.model.toLowerCase()](_obj);

        //分页
        gd.page && $(gd.id).datagrid('getPager').pagination({
            pageSize: gd.pageSize,
            beforePageText: '第',
            afterPageText: '页 共 {pages} 页',
            total: gd.total,
            pageNumber: gd.pageIndex,
            layout: ['sep', 'first', 'prev', 'links', 'next', 'last', 'sep'],
            displayMsg: '当前显示 {from} - {to} 条 共 {total} 条',
            onSelectPage: function () {
                gd.pageIndex = arguments[0];
                z.GridLoad(gd);
            }
        });

        //加载完成事件
        if (z.isPC() && $(gd.id).attr('autosize') != 1) {
            z.GridAuto(gd);
            $(window).resize(function () { z.GridAuto(gd) });
            $(gd.id).attr('autosize', 1);
        }
        for (var i = 1; i < gd.completeIndex + 1; i++) {
            typeof gd["completeF" + i] == "function" &&
            gd["completeF" + i](gd);
        }

        return gd;
    }

    //Grid自适应
    z.GridAuto = function (gd) {
        if (!$(gd.id).length) { return gd; }
        //影藏滚动条
        document.documentElement.style.overflowY = "hidden";

        var h = $(window).height() - $(gd.id).parent()[0].getBoundingClientRect().top;
        h < 150 && (h = 150);
        var ro = { width: null, height: null };
        switch (gd.autosize) {
            case 'xy':
                ro.width = $(gd.autosizePid).width();
                ro.height = h - 5;
                break;
            case 'x':
                ro.width = $(gd.autosizePid).width();
                ro.height = null;
                break;
            case 'y':
                ro.width = null;
                ro.height = h - 5;
                break;
            case 'p':
                ro.width = $(gd.autosizePid).width();
                ro.height = $(gd.autosizePid).height();
                break;
            default:
                ro = null;
        }
        $(gd.id).datagrid('resize', ro);
        //显示滚动条
        document.documentElement.style.overflowY = "";

        return gd;
    }

    //Grid格式化
    z.GridFormat = function (format, value, field) {
        if (format == null) return value;
        if (format == "col_custom_") {
            try { return eval(format + field)(value); } catch (e) { return value; }
        }
        switch (Number(format)) {
            case 22:
                return value == 1 ? "冻结" : "不冻结";
                break;
            case 21:
                return value == 1 ? "隐藏" : "显示";
                break;
            case 20:
                return value == 2 ? "居中" : value == 3 ? "居右" : "居左";
                break;
            case 19:
                value = value == "1" ? '正常' : '<span style="background-color:#F89406;color:#fff;padding:4px">停用</span>';
                break;
            case 18:
                value = value == "1" ? "✘" : "✔";
                break;
            case 17:
                value = value == "1" ? "✔" : "✘";
                break;
                //精确两位  带￥
            case 16:
                if (value != undefined && value != "") {
                    value = isNaN(parseFloat(value)) ? "￥ 0.00" : '￥ ' + parseFloat(value).toFixed(2);
                }
                break;
                //精确两位
            case 15:
                if (value != undefined && value != "") {
                    value = isNaN(parseFloat(value)) ? "0.00" : parseFloat(value).toFixed(2);
                }
                break;
                //yyyy-MM-dd
            case 14:
                if (value && value.length > 10) {
                    value = value.substring(0, 10);
                    value[4] = "-";
                    value[7] = "-";
                }
                break;
                //yyyy-MM-dd HH:mm:ss
            case 13:
                if (value && value.length >= 19) {
                    value = value.substring(0, 19);
                    value[4] = "-";
                    value[7] = "-";
                }
                break;
                //HH:mm:ss
            case 12:
                if (value && value.length >= 8) {
                    value = value.substring(0, 8);
                }
                break;
                //yyyy-MM
            case 11:
                if (value && value.length > 7) {
                    value = value.substring(0, 7);
                    value[4] = "-";
                }
                break;
        }
        return value;
    }

    //获取选中的行
    z.Row = function (gd) {
        if (gd.single) { return $(gd.id).datagrid('getSelected'); } else {
            var rows = $(gd.id).datagrid('getSelections');
            (rows.length == 0 || rows[0] == undefined) && (rows = null);
            return rows;
        }
    }

    //根据选中行复制给表单
    z.GridRowEdit = function (rowData) {
        for (var i in rowData) {
            var t = $('#' + i), ptype = t.attr('ptype') || t.attr('type');
            switch (ptype) {
                case 'combobox':
                case 'combotree':
                    if (t.attr('multiple') && rowData[i]) {
                        if (typeof rowData[i] == "object") {
                            t[ptype]('setValues', rowData[i]);
                        } else {
                            var arr = [];
                            $(rowData[i].split(',')).each(function () {
                                this.length && arr.push(this);
                            })
                            t[ptype]('setValues', arr);
                        }
                    } else {
                        t[ptype]('setValue', rowData[i]);
                    }
                    break;
                case 'checkbox':
                    t[0].checked = rowData[i] == 1 ? true : false;
                    break;
                case 'file':
                    $('#hid_' + t[0].id).val(rowData[i]);
                    break;
                default:
                    try { t.val(rowData[i]); } catch (e) { }
                    break;
            }
        }
    }

    //计算列
    z.GridCalc = function (gd, arr, mode) {
        var objr = {}, ra = gd.RowAll();
        $(ra).each(function () {
            for (var i = 0; i < arr.length; i++) {
                var cc = arr[i], currv = objr[cc] || 0;
                objr[cc] = currv + (parseFloat(this[arr[i]]) || 0);
            }
        });
        return objr;
    }

    //更新一行数据 TreeGrid传展开节点的ID
    z.GridUpdataRow = function (id, rowData, index) {
        var rowid = index == undefined ? $(id).datagrid("getRowIndex", $(id).datagrid('getSelected')) : index;
        $(id).datagrid('updateRow', { index: rowid, row: rowData });
    }

    //新增一行
    z.GridAddRow = function (id, rowData, index) {
        if (arguments.length == 3) { $(id).datagrid('insertRow', { index: index + 1, row: rowData }) }
        else { $(id).datagrid('appendRow', rowData) }
    }

    //删除一行
    z.GridDelRow = function (id, index) { $(id).datagrid('deleteRow', index) }

    //合并单元格
    z.GridMergeCell = function (gd, fields) {
        fields = fields || gd.ColumnFields();
        var rows = gd.RowAll(), i = 0, j = 0, temp = {};
        for (i; i < rows.length; i++) {
            var row = rows[i];
            j = 0;
            for (j; j < fields.length; j++) {
                var field = fields[j];
                var tf = temp[field];
                if (!tf) {
                    tf = temp[field] = {};
                    tf[row[field]] = [i];
                } else {
                    var tfv = tf[row[field]];
                    if (tfv) {
                        tfv.push(i);
                    } else {
                        tfv = tf[row[field]] = [i];
                    }
                }
            }
        }
        $.each(temp, function (field, colunm) {
            $.each(colunm, function () {
                var group = this;
                if (group.length > 1) {
                    var before, after,
                    megerIndex = group[0];
                    for (var i = 0; i < group.length; i++) {
                        before = group[i];
                        after = group[i + 1];
                        if (after && (after - before) == 1) {
                            continue;
                        }
                        var rowspan = before - megerIndex + 1;
                        if (rowspan > 1) {
                            $(gd.id).datagrid('mergeCells', {
                                index: megerIndex,
                                field: field,
                                rowspan: rowspan
                            });
                        }
                        if (after && (after - before) != 1) {
                            megerIndex = after;
                        }
                    }
                }
            });
        });
    }

    //编辑Grid单元格
    z.GridCellEdit = function (gd, index, field, value) {
        if (!gd.editorEnable) { return false }
        this.combobox = function (column) {
            return {
                type: 'combobox', options: {
                    valueField: 'id',
                    textField: 'text',
                    data: z.DC[String(column.f_url).toLowerCase()] || [],
                    editable: false,
                    onSelect: function (node) {
                        $(this).combobox('hidePanel');
                        try { return eval(column.field + "_onSelect(node)"); } catch (e) { }
                    },
                    onLoadSuccess: function () {
                        var that = $(this);
                        setTimeout(function () {
                            that.next().css('height', 33).find('input').first().css('height', 33)[0].focus();
                            that.next().find('a').first().css('height', 33);
                            that.combobox('showPanel');
                        }, 10);
                    }
                }
            }
        }

        var columns = gd.columns, column, that = this;
        $(columns).each(function () { if (this.field == field) { column = this; return false; } });
        if (!column) { return false; }
        switch (column.f_type) {
            case 'combotree':
            case 'combobox':
                var lowerUrl = String(column.f_url).toLowerCase();
                if (lowerUrl in z.DC) {
                    gd.beginEdit(index, field, that[column.f_type](column));
                } else {
                    $.ajax({
                        url: lowerUrl,
                        type: "post",
                        dataType: "json",
                        data: typeof z.DC[lowerUrl + "_post"] == "function" ? z.DC[lowerUrl + "_post"]() : {},
                        success: function (data) {
                            z.DC[lowerUrl] = data;
                            gd.beginEdit(index, field, that[column.f_type](column));
                        },
                        error: function () {
                            gd.endEdit();
                        }
                    })
                }
                break;
            case 'datetime':
            case 'date':
            case 'time':
            case 'calc':
            case 'tip':
                gd.beginEdit(index, field, column.f_type);
                break;
            case 'text':
            case 'modal':
                gd.beginEdit(index, field, { type: column.f_type, options: { column: column } });
                break;
            default:
                gd.endEdit();
        }
    }

    //【.formui 单据】请求返回数据源&回调
    z.FormAttrDataUrl = function () {
        $('.formui').find('*').each(function () {
            var that = this,
                type = that.getAttribute("data-type"),
                url = that.getAttribute('data-url');
            if (type && url && type.indexOf('combo') >= 0) {
                url = url.toLowerCase();
                if (url in z.DC) {
                    z.FormAttrDataBind(this);
                } else {
                    $.ajax({
                        url: url,
                        type: "post",
                        dataType: "json",
                        success: function (data) {
                            z.DC[url] = data;
                            z.FormAttrDataBind(that);
                        },
                        error: function (e) {
                            z.DC[url] = [];
                            z.FormAttrDataBind(that);
                        }
                    })
                }
            }
        })
    }

    /*
     *  【.formui 单据】
     *  表单类型源绑定
     *  data-type：扩展类型：combotree、combobox、calc、datetime、date、time、modal
     *  data-url：请求url 【combox类型时：且为本地数据 z.DC对象的键，绑定源为 z.DC[data-url] 不区分大小写】
     *  data-state：状态，绑定后值为 bind
     */
    z.FormAttrDataBind = function (target) {
        var id = target.id,
            type = target.getAttribute("data-type"),
            url = target.getAttribute('data-url'),//url
            state = target.getAttribute('data-state');//是否已绑定
        if (!state) {
            switch (type) {
                case 'm_combotree':
                case 'm_combobox':
                case 'combotree':
                case 'combobox':
                    if (!url) { return false }
                    url = url.toLowerCase();
                    if (url in z.DC) {
                        var cb = z.Combo();
                        cb.checked = false;
                        cb.id = target;
                        cb.data = z.DC[url];
                        cb.editable = false;
                        cb.beforeSelectE = function (node) {
                            try { return z.DC[id + "_beforeSelectE"](node) } catch (e) { }
                        },
                        cb.selectE = function (node) {
                            try { return z.DC[id + "_selectE"](node) } catch (e) { }
                        },
                        cb.completeE(function () {
                            try { return z.DC[id + "_completeE"](this) } catch (e) { }
                        })
                        cb.multiple = type.indexOf('m_') >= 0 ? true : false;
                        cb.model = type.replace('m_', '');
                        cb.bind();
                        z.DC["combo_" + id] = cb;
                        target.setAttribute('data-state', 'bind');
                    }
                    break;
                case 'datetime':
                case 'date':
                case 'time':
                    var ops = { autoclose: true };
                    if (type == 'date') {
                        ops.format = 'yyyy-mm-dd';
                        ops.minView = 2;
                        ops.maxView = 2;
                    } else if (type == 'time') {
                        ops.format = 'hh:ii:ss';
                        ops.minView = 0;
                        ops.maxView = 1;
                        ops.startView = 1;
                        ops.showMeridian = true;
                    }

                    var divinput = $(target).parent();
                    divinput.addClass('input-append date form_datetime').append('<span class="add-on"><i class="icon-th fa fa-calendar form-control-feedback"></i></span>');
                    divinput.datetimepicker(ops).on('show', function () {
                        if (divinput.find('input').attr('data-type') == "time") {
                            var dtp = $(this).data('datetimepicker');
                            dtp.picker.find('th.switch').html('请选择时间');
                            dtp.picker.find('th.prev').removeClass('prev');
                            dtp.picker.find('th.next').removeClass('next');
                            dtp.picker.click(function () {
                                dtp.picker.find('th.switch').html('请选择时间');
                                dtp.picker.find('th.prev').removeClass('prev');
                                dtp.picker.find('th.next').removeClass('next');
                            });
                        }
                    });
                    target.setAttribute('data-state', 'bind');
                    break;
                case 'modal':
                    var input = $(target);
                    input.parent().append('<i class="fa fa-search form-control-feedback"></i>').parent().addClass('has-feedback');
                    input.next().click(function () {
                        var lowerUrl = input.attr('data-url').toLowerCase(), mo;
                        if (lowerUrl in z.DC) {
                            mo = z.DC[lowerUrl];
                            mo.show();
                        } else {
                            mo = z.Modal();
                            mo.content = "加载中...";
                            mo.url = lowerUrl;
                            mo.size = 3;
                            mo.append().show();
                            mo.complete = function () {
                                this.autosize();
                            }
                            var field = input.attr('id');
                            if (field && (field = field.toLowerCase())) {
                                (field + "_ModalOkClick" in z.DC) && (mo.okClick = z.DC[field + "_ModalOkClick"]);
                                (field + "_ModalCancelClick" in z.DC) && (mo.cancelClick = z.DC[field + "_ModalCancelClick"]);
                            }
                            z.DC[lowerUrl] = mo;
                        }
                    });
                    target.setAttribute('data-state', 'bind');
                    break;
            }
        }
    }

    //把编辑表单组装成JSON对象（用于DataGrid更新行） 默认 #Grid1Form
    z.FormToJson = function (FormId) {
        var jform = $(arguments.length ? arguments[0] : '#Grid1From'), arrData = jform.serializeArray(), row = {};
        jform.find('input[type="checkbox"]').each(function () {
            arrData.push({ name: this.name, value: this.checked ? 1 : 0 });
        });

        $(arrData).each(function () {
            var input = $('#' + this.name), ptype = input.attr('ptype') || input.attr('type');
            switch (ptype) {
                case 'combobox':
                case 'combotree':
                    if (input.attr('multiple')) {
                        var arr = row[this.name] || [];
                        arr.push(this.value);
                        row[this.name] = arr;
                    } else {
                        row[this.name] = this.value;
                    }
                    break;
                case 'checkbox':
                    row[this.name] = input[0].checked ? 1 : 0;
                    break;
                case 'file':
                    break;
                default:
                    ptype && (row[this.name] = this.value);
                    break;
            }
        });
        return row;
    }

    //必填验证 边框颜色(reset 重置边框) 表单ID 默认 #Grid1From
    z.RequiredValidation = function (css, formId) {
        var result = true;
        var form = $(arguments[1] || "#Grid1From");
        form.find('label').each(function () {
            if (this.className.indexOf('requiredTag') > -1) {
                var input = $(this).parent().find('input,select,textarea'), val = '', colorTarget;
                switch (input.attr('ptype')) {
                    case 'combobox':
                        val = input.combobox(input.attr('multiple') ? 'getValues' : 'getValue');
                        colorTarget = input.next();
                        break;
                    case 'combotree':
                        val = input.combobox(input.attr('multiple') ? 'getValues' : 'getValue');
                        colorTarget = input.next();
                        break;
                    default:
                        val = input.val();
                        colorTarget = input;
                        break;
                }
                if (css == "reset") {
                    colorTarget.css('border-color', '');
                } else {
                    if (val && val.length) {
                        colorTarget.css('border-color', '');
                    } else {
                        colorTarget.css('border-color', css || "red");
                        result = false;
                    }
                }
            }
        });
        return result;
    }

    //清空Form #ID 默认#Grid1From
    z.FormClear = function (formid) {
        var lbls = $(formid || "#Grid1From").find('label');
        lbls.next().children('input,textarea').val('');
    }

    //表单标题设置 icon,title,id,required
    z.FormTitle = function (ops) {
        var icon = ops.icon,
            title = ops.title,
            required = ops.required == false ? false : true,
            id = ops.id;
        if (icon == 0) {
            icon = "fa-plus blue";
        } else if (icon == 1) {
            icon = "fa-edit orange";
        }
        var htm = '<i class="fa ' + icon + ' fa-2x" style="margin-right:10px;vertical-align:middle"></i> ' + title;
        required && (htm += ' <label style="margin-left:20px;font-size:13px;font-weight:200;color:red;">蓝色标题项为必填</label>');
        $(id).html(htm);
    }

    //Combo
    z.Combo = function () { return new z.Combo.prototype.init() };
    z.Combo.fn = z.Combo.prototype = {
        init: function () { return this },
        //容器#ID 默认#Combo1
        id: "#Combo1",
        //模式 默认Combobox 可选Combotree
        model: "combobox",
        //url
        url: "",
        //数据源
        data: null,
        //数据源缓存
        dataCatch: null,
        //Combobox value键 默认id
        boxValue: 'id',
        //Combobox text值 默认text
        boxText: 'text',
        //多选 默认单选
        multiple: false,
        //编辑 默认启用
        editable: true,
        //选中一行 默认false
        checked: false,
        //是否显示复选框 默认false
        checkbox: false,
        //Combotree级联选择 默认是
        checkCascade: true,
        //绑定前事件
        bindBeforeE: null,
        //绑定数据
        bind: function () { z.ComboBind(this) },
        //格式化
        format: function (node) { return node.text },
        //勾选复选框前事件
        beforeCheckE: function (node) { },
        //Combo选中前事件调两次（before→select→before）的Bug做处理
        //处理方式：before事件在500毫秒内只执行一次，即500毫秒内忽略多次触发
        bug_beforCheckDefer: 500,
        //选择前事件
        beforeSelectE: function (node) { },
        //选择事件
        selectE: function (node) { },
        //完成事件索引
        completeIndex: 0,
        //完成事件 (注意死循环，在完成事件重绑数据需加标记跳出循环绑定)
        completeE: function (fn) {
            this.completeIndex += 1;
            this["completeF" + this.completeIndex] = fn;
        },
        //设置下拉框值
        setVal: function (value) { $(this.id)[this.model](this.multiple ? 'setValues' : 'setValue', value); return this; },
        //设置Combobox选中项
        setSelect: function (value) { $(this.id)[this.model]('select', value); return this; },
        //获取选中值
        getVal: function () { return $(this.id)[this.model](this.multiple ? 'getValues' : 'getValue') },
        //获取选中显示文本
        getTxt: function () { return $(this.id)[this.model]('getText') },
        //选中第一个值
        chkOne: function () {
            if (this.dataCatch.length) {
                this.model == "combobox" ? this.setSelect(this.dataCatch[0].id) : this.setVal(this.dataCatch[0].id);
            } return this;
        },
        //获取Combotree所有节点为数组
        getTreeAll: function () { return $(this.id).combotree('tree').tree('getChildren') }
    };
    z.Combo.prototype.init.prototype = z.Combo.prototype;

    //绑定Combo
    z.ComboBind = function (cb) {
        var that = $(cb.id), url, data;
        try {
            var eurl = eval(cb.url.replace(/\\"/g, ''));
            eurl.length >= 0 && (cb.data = eurl);
        } catch (e) { }
        if (cb.data != null && typeof cb.data == "object") {
            url = null;
        } else { url = cb.url; }
        if (typeof cb.data == "object") {
            data = cb.data;
        } else {
            url = null;
        }
        that.removeAttr("readOnly");

        var _obj = {
            url: url,
            data: data,
            method: 'post',
            multiple: cb.multiple,
            editable: cb.editable,
            checkbox: cb.checkbox,
            valueField: cb.boxValue,
            textField: cb.boxText,
            selectOnNavigation: false,
            cascadeCheck: cb.checkCascade,
            formatter: function (node) { return cb.format(node) },
            onBeforeCheck: function (node) { return cb.beforeCheckE(node) },
            onBeforeSelect: function (node) {
                var bugDefer = cb.bug_beforCheckDefer;
                if (bugDefer != -1) {
                    cb.bug_beforCheckDefer = -1;
                    setTimeout(function () { cb.bug_beforCheckDefer = bugDefer }, bugDefer)
                    return cb.beforeSelectE(node)
                }
            },
            onSelect: function (node) { that.defaultValue = node.id; return cb.selectE(node) },
            onLoadSuccess: function () {
                cb.dataCatch = arguments[0];
                z.ComboCssRepair(cb);
                cb.checked && !cb.multiple && cb.chkOne();
                that.next().find('input').first().focus(function () {
                    that[cb.model.toLowerCase()]('showPanel');
                });
                for (var i = 1; i < cb.completeIndex + 1; i++) {
                    typeof cb["completeF" + i] == "function" &&
                    cb["completeF" + i](cb);
                }
            },
            filter: function (q, row) {
                return String(row[cb.boxText]).indexOf(q) == 0;
            },
            delay: 200,
            onShowPanel: function () {
                that.defaultValue = that[cb.model.toLowerCase()]("getValue");
            },
            onHidePanel: function () {
                that[cb.model.toLowerCase()]("setValue", that.defaultValue);
            }
        };

        that[cb.model.toLowerCase()](_obj).attr('ptype', cb.model.toLowerCase());
        that.attr('multiple', cb.multiple);
    }

    //清空Combo的值 ids 多个id空格间隔
    z.ComboResetValue = function (ids) {
        $(ids.split(' ')).each(function () {
            try { $('#' + this).combobox('clear'); $('#' + this).combotree('clear'); } catch (e) { }
        });
    }

    //修正Combo的样式问题
    z.ComboCssRepair = function (cb) {
        var input = $(cb.id), span = input.next('span');
        span.css('width', '100%').css('height', 33);
        span.find('a').first().attr("style", "width:25px;height:33px");
        span.find('input').first().attr("style", "width:100%;line-height:2;padding-right:25px;padding-left:12px;height:33px");
    }

    //修复Tree多选模式赋值时半选中状态下的子节点全选问题
    //#id,赋值（逗号分割）
    z.TreeSetValueRepair = function (id, ids) {
        var mil = ids.split(','), jmil = $(id).combotree('tree'), mil_m = jmil.tree('getChildren');
        if (mil.length != mil_m.length) {
            var ignore = [];
            for (var i in mil_m) {
                var ihas = false;
                for (var u in mil) {
                    if (mil_m[i].id == mil[u]) {
                        ihas = true;
                        break;
                    }
                }
                !ihas && ignore.push(mil_m[i].id);
            }
            ignore.length && $(ignore).each(function () {
                jmil.tree('uncheck', jmil.tree('find', this).target);
            });
        }
    }

    //对象：url、async、dataForm、dataType、percent、success、error、complete
    z.ajax5 = function (ops) {
        var xhr = (window.XMLHttpRequest) ? (new XMLHttpRequest()) : (new ActiveXObject("Microsoft.XMLHTTP")),
            async = ops.async == undefined ? true : ops.async, //默认异步
            dataType = ops.dataType == undefined ? "" : ops.dataType.toLowerCase();

        //上传进度监听
        xhr.upload.onprogress = function (event) {
            if (event.lengthComputable) {
                var per = (event.loaded / event.total) * 100;
                typeof ops.percent == "function" && ops.percent(per.toFixed(0));
            }
        };

        //状态改变事件
        xhr.onreadystatechange = function () {
            if (xhr.readyState == 4) {
                if (xhr.status == 200) {
                    var val = xhr.responseText;
                    if (ops.success != undefined) {
                        if (dataType == "json") {
                            try {
                                val = $.parseJSON(val);
                                ops.success(val);
                            }
                            catch (e) {
                                ops.error != undefined && ops.error(xhr)
                            };
                        }
                        else if (dataType == "xml") { ops.success(xhr.responseXML) }
                        else { ops.success(val); }
                    }
                }
                else { if (ops.error != undefined) { ops.error(xhr) } }
                if (ops.complete != undefined) { ops.complete() }
            }
        };
        xhr.open("post", ops.url, async);
        xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
        xhr.send(ops.dataForm)
    }

    //验证为有效的正数 ids 多个id空格间隔
    z.DecimalValidation = function (ids) {
        $(ids.split(' ')).each(function () {
            $('#' + this).blur(function () {
                this.value = isNaN(parseFloat(this.value)) ? this.defaultValue : parseFloat(this.value);
            })
        })
    }

    //只能为0-1之间的小数 (百分比)
    z.PercentValidation = function (ids) {
        $(ids.split(' ')).each(function () {
            $('#' + this).blur(function () {
                var val = isNaN(parseFloat(this.value)) ? this.defaultValue : parseFloat(this.value);
                val > 1 && (val = 1);
                val < 0 && (val = 0);
                this.value = val;
            })
        })
    }

    //当前时间 自定义格式 yyyy-MM-dd HH:mm:ss
    z.time = function (format) {
        function t(n) { return n < 10 ? "0" + n : n }
        var d = new Date(),
            y = d.getFullYear(),
            m = t(d.getMonth() + 1),
            day = t(d.getDate()),
            h = t(d.getHours()),
            min = t(d.getMinutes()),
            s = t(d.getSeconds()),
            f = d.getMilliseconds();
        return !arguments.length ? (y + "-" + m + "-" + day + " " + h + ":" + min + ":" + s) :
                format.replace("yyyy", y).replace("MM", m).replace("dd", day).replace("HH", h).replace("mm", min).replace("ss", s).replace("ff", f)
    };

    //时间计算 (根据calcType值 做case判断写时间计算方式，默认相减 返回天数)
    z.timeCalc = function (date1, date2, calcType) {
        var result;
        try {
            date1 = new Date(date1.replace(/-/g, '/'));
            date2 = new Date(date2.replace(/-/g, '/'));
            result = date1.getTime() - date2.getTime();
            result = Math.floor(result / (1000 * 3600 * 24));
        } catch (e) { }
        return result;
    }

    //Modal
    z.Modal = function () { return new z.Modal.prototype.init() };
    z.Modal.fn = z.Modal.prototype = {
        init: function () {
            var random = Math.random().toString().substring(2, 6);
            this.id_Modal += random;
            this.id_ModalHeader += random;
            this.id_ModalLabel += random;
            this.id_ModalBody += random;
            this.id_ModalFooter += random;
            this.id_ButtonCancel += random;
            this.id_ButtonOk += random;
            return this;
        },
        //ID
        id_Modal: "_myModal",
        //头部ID
        id_ModalHeader: "_myModalHeader",
        //头部标题ID
        id_ModalLabel: "_myModalLabel",
        //内容ID
        id_ModalBody: "_myModalBody",
        //页脚ID
        id_ModalFooter: "_myModalFooter",
        //取消按钮ID
        id_ButtonCancel: "_btnCancel",
        //确定按钮ID
        id_ButtonOk: "_btnOk",
        //ok按钮文本
        okText: "确定",
        //cancel按钮文本
        cancelText: "取消",
        //Ok点击回调
        okClick: null,
        //Cancel点击回调
        cancelClick: null,
        //标题内容
        title: "消息",
        //主体内容
        content: "",
        //主体高度 默认不指定
        height: null,
        //iframe地址
        url: "",
        //iframe加载完成回调
        complete: null,
        //模态大小 默认2 可选：1|2|3
        size: 2,
        //显示页脚
        showFooter: true,
        //显示Cancel按钮
        showCancel: true,
        //追加到Body
        append: function () {
            var that = this, size = 'modal-dialog', style = '';
            switch (this.size) {
                case 1: size += " modal-sm"; break;
                case 3: size += " modal-lg";
            }
            if (this.height) { style = 'style="height:' + this.height + 'px"'; }

            var partHtml = '<div class="' + size + '"><div class="modal-content"><div id="' + this.id_ModalHeader
                         + '" class="modal-header"><button class="close" data-dismiss="modal" aria-label="Close">'
                         + '<span>&times;</span></button><h4 id="' + this.id_ModalLabel + '" class="modal-title">'
                         + this.title + '</h4></div><div id="' + this.id_ModalBody + '" class="modal-body" ' + style + '>'
                         + this.content + '</div><div id="' + this.id_ModalFooter + '" class="modal-footer" style="margin-top:0;">'
                         + '<button class="btn btn-white" style="margin:0 5px" id="' + this.id_ButtonCancel + '">' + this.cancelText + '</button>'
                         + '<button class="btn btn-primary" id="' + this.id_ButtonOk + '">' + this.okText + '</button></div></div></div>',
                mo = document.createElement('div');
            mo.id = this.id_Modal;
            mo.className = "modal";
            mo.setAttribute('role', 'dialog');
            mo.innerHTML = partHtml;
            document.body.appendChild(mo);

            if (this.url.length) {
                var ifr = document.createElement('iframe');
                ifr.src = this.url;
                ifr.frameBorder = 0;
                ifr.scrolling = "auto";
                ifr.style.cssText = "border:none;width:100%;height:100%;";
                $(ifr).appendTo($('#' + this.id_ModalBody).html('')).on('load', function () {
                    typeof that.complete == "function" && that.complete.call(that);
                });
            }

            !this.showFooter && (document.getElementById(this.id_ModalFooter).style.display = "none");
            !this.showCancel && (document.getElementById(this.id_ButtonCancel).style.display = "none");

            document.getElementById(this.id_ButtonCancel).onclick = function () {
                typeof that.cancelClick == "function" && that.cancelClick.call(that)
            }
            document.getElementById(this.id_ButtonOk).onclick = function () {
                typeof that.okClick == "function" && that.okClick.call(that)
            }
            /* 重定义按钮回调函数指向z.Modal对象&&执行函数 */
            return this;
        },
        //iframe 高度调整
        autosize: function () {
            var iframe = $('#' + this.id_ModalBody).find('iframe'), idoc = iframe[0].contentWindow.document, h = 100;
            h = idoc.documentElement.scrollHeight || idoc.body.scrollHeight;
            idoc.documentElement.style.overflow = "auto";
            iframe.css('height', h + 20);
        },
        //显示模态
        show: function () { $('#' + this.id_Modal).modal('show'); return this; },
        //隐藏模态
        hide: function () { $('#' + this.id_Modal).modal('hide'); return this; },
        //移除模态
        remove: function () { $('#' + this.id_Modal).modal('hide').remove(); return this; }
    };
    z.Modal.prototype.init.prototype = z.Modal.prototype;

    //模态内容更高度自适应
    z.ModalAutoHeight = function (selector) {
        var bd = $(selector).find('div.modal-body');
        var maxH = $(window).height() - 60 - 130;
        maxH = Math.max(maxH, 100);
        bd.css('max-height', maxH).css('overflow-y', 'auto');
    }

    //解析URL参数
    z.HrefQuery = function (name) {
        name = String(name).toLowerCase();
        var pars = location.search.toLowerCase().substring(1), prr = pars.split('&');
        for (var i = 0; i < prr.length; i++) {
            var pr = prr[i], pn = pr.split('=');
            if (pn[0] == name) {
                return pr.replace(pn[0] + "=", '');
            }
        }
    }

    //编辑类型扩展
    $.fn.datagrid && $.extend($.fn.datagrid.defaults.editors, {
        //日期时间
        datetime: {
            init: function (container, options) {
                var input = $('<input class="form-control" style="height:33px" />').appendTo(container);
                input.datetimepicker({ autoclose: true });
                input[0].focus(); return input;
            },
            destroy: function (target) { },
            resize: function (target, width) { },
            getValue: function (target) { return target.val() },
            setValue: function (target, value) { target.datetimepicker('setDate', new Date(value)); target.val(value) }
        },
        //日期
        date: {
            init: function (container, options) {
                var input = $('<input class="form-control" style="height:33px" />').appendTo(container);
                input.datetimepicker({ format: 'yyyy-mm-dd', minView: 2, maxView: 2, autoclose: true });
                input[0].focus();
                return input;
            },
            destroy: function (target) { },
            resize: function (target, width) { },
            getValue: function (target) { return target.val() },
            setValue: function (target, value) { target.datetimepicker('setDate', new Date(value)); target.val(value); }
        },
        //时间
        time: {
            init: function (container, options) {
                var input = $('<input class="form-control" style="height:33px" />').appendTo(container);
                input.datetimepicker({ format: "hh:ii:ss", maxView: 1, minView: 0, startView: 1, showMeridian: true, autoclose: true }).on('show', function () {
                    var dtp = $(this).data('datetimepicker');
                    dtp.picker.find('th.switch').html('请选择时间');
                    dtp.picker.find('th.prev').removeClass('prev');
                    dtp.picker.find('th.next').removeClass('next');
                    dtp.picker.click(function () {
                        dtp.picker.find('th.switch').html('请选择时间');
                        dtp.picker.find('th.prev').removeClass('prev');
                        dtp.picker.find('th.next').removeClass('next');
                    });
                });
                input[0].focus(); return input;
            },
            destroy: function (target) { },
            resize: function (target, width) { },
            getValue: function (target) { return target.val() },
            setValue: function (target, value) { target.val(value) }
        },
        //文本
        text: {
            init: function (container, options) {
                var input = $('<input class="form-control" style="height:33px" />').appendTo(container);
                input[0].focus(); return input;
            },
            destroy: function (target) { },
            resize: function (target, width) { },
            getValue: function (target) { return target.val() },
            setValue: function (target, value) { target.val(value) }
        },
        //模态
        modal: {
            init: function (container, options) {
                var input = $('<div class="form-group has-feedback" style="padding:0;margin:0;">'
                             + '<div><input class="form-control" data-type="modal" style="padding-right:27px;height:33px">'
                             + '<i class="fa fa-search form-control-feedback" style="text-align: right; right: 10px;"></i></div></div>').appendTo(container);
                //弹出模态
                function cmodals() {
                    var lowerUrl = options.column.url.toLowerCase(), mo;
                    if (lowerUrl in z.DC) {
                        mo = z.DC[lowerUrl];
                        mo.show();
                        try {
                            var _ifr = $('#' + mo.id_ModalBody).find('iframe')[0].contentWindow;
                            _ifr.$("#txtSearch").val(input.find('input').val());
                            _ifr.gd1.F5();
                        } catch (e) { }
                    } else {
                        mo = z.Modal();
                        mo.title = '<i class="fa fa-plus blue fa-2x" style="margin-right:10px;vertical-align:middle"></i>请选择';
                        mo.content = "加载中...";
                        mo.url = lowerUrl + "#" + unescape(input.find('input').val());
                        mo.size = 3;
                        mo.height = 385;
                        mo.okText = '<i class="fa fa-check"></i>&nbsp;选入并关闭';
                        mo.showCancel = false;
                        mo.append().show();
                        var field = options.column.field.toLowerCase();
                        (field + "_ModalOkClick" in z.DC) && (mo.okClick = z.DC[field + "_ModalOkClick"]);
                        (field + "_ModalCancelClick" in z.DC) && (mo.cancelClick = z.DC[field + "_ModalCancelClick"]);
                        z.DC[lowerUrl] = mo;
                    }
                }
                input.find('i').click(cmodals);
                input.find('input')[0].focus();
                return input;
            },
            destroy: function (target) { },
            resize: function (target, width) { },
            getValue: function (target) {
                var defaultvale = $(target).find("input").attr("defaultvalue");
                return defaultvale;
            },
            setValue: function (target, value) {
                var input = $(target).find("input");
                input.attr("defaultvalue", value);
                input.attr("searchvalue", value);
                input.val(value);
            }
        }
    });

    //数据缓存，以 url 为键 url小写
    z.DC = {};

    window.z = z;
})();




//提示信息、确认回调、取消回调
function art(message, fnOk, fnCancel) {
    var msg = '';
    switch (message) {
        case "fail": msg = '<div class="alert alert-danger" role="alert" style="font-size:1.6em;margin:0;font-weight:600;">操作失败</div>'; break;
        case "error": msg = '<div class="alert alert-danger" role="alert" style="font-size:1.6em;margin:0;font-weight:600;">网络错误</div>'; break;
        case "success": msg = '<div class="alert alert-success" role="alert" style="font-size:1.6em;margin:0;font-weight:600;">操作成功</div>'; break;
        case "select": msg = '<div class="alert alert-warning" role="alert" style="font-size:1.6em;margin:0;font-weight:600;">请选择一行再操作</div>'; break;
        default: msg = '<div class="alert alert-' + (arguments.length == 1 ? "info" : "warning") + '" role="alert" style="font-size:18px;margin:0;font-weight:600;">' + message + '</div>'; break;
    };
    var mo = z.Modal();
    mo.size = 1;
    mo.content = msg;
    if (arguments.length == 1) {
        mo.showCancel = false;
        mo.okClick = function () { this.remove() };
    } else {
        mo.okClick = function () { this.remove(); fnOk.call(mo); };
        mo.cancelClick = arguments.length == 3 ? function () { fnCancel.call(mo) } : function () { this.remove(); };
    }
    mo.append().show();
    $('#' + mo.id_Modal).on('hidden.bs.modal', function (e) { mo.remove() })[0].focus();
    $('#' + (arguments.length == 1 ? mo.id_ButtonOk : mo.id_ButtonCancel))[0].focus();
}

//页面操作按钮 事件
z.button = function (type, fn) { $('#myBtnMenu').on(type, fn) }
$("#myBtnMenu").click(function (e) {
    e = e || window.event;
    var target = e.target || e.srcElement;
    if (target.nodeName == "BUTTON" || target.nodeName == "I") {
        var btnId, btnState;
        btnId = target.nodeName == "BUTTON" ? target.id : target.parentElement.id;
        btnState = target.nodeName == "BUTTON" ? (target.className.indexOf('disabled') > -1 ? false : true) : (target.parentElement.className.indexOf('disabled') > -1 ? false : true);
        if (btnState && !$('#' + btnId).data('stop')) {
            $(this).trigger(btnId.toLowerCase().replace('m_', ''));
        }
    }
});

//样式加载、功能按钮风格
(function () {
    try {
        var fs = localStorage["_Style_FontSize"],
            ff = localStorage["_Style_FontFamily"],
            bs = localStorage["_Style_Button"];
        fs && (document.body.style["font-size"] = fs),
        ff && (document.body.style["font-family"] = ff);
        bs == 1 && ($('#myBtnMenu').removeClass('bgwhite'));
    } catch (e) { }
})();
