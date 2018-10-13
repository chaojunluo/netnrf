# netnrf 响应式框架	
- GitHub：<https://github.com/netnr/netnrf>
- Gitee：<https://gitee.com/netnr/netnrf>
- [联系（打赏）](https://ss.netnr.com/contact)
- [加入QQ群](http://qm.qq.com/cgi-bin/qm/qr?k=oLmAflGAIODgeYw9tImSvBVX1SK_warh)

----------
## [更新日志](CHANGELOG.md)

----------
## 项目结构
- Netnr.Core 类库
- Netnr.Data 数据访问、仓储
- Netnr.Domain 实体
- Netnr.Func 应用
- Netnr.ResponseFramework Web站点

----------
## 数据表
- 系统用户（SysUser）
- 系统角色、角色权限（SysRole）
- 系统菜单（SysMenu）
- 系统按钮（SysButton）
- 系统日志（SysLog）
- 表配置（SysTableConfig）
----------

## 功能
- 表格：动态配置标题、宽度、排序、对齐方式、格式化、冻结、点击排序等
- 表单：动态生成表单，自定义标题、排序、跨列、类型、必填等
- 查询：动态生成查询面板，自定义哪些字段支持查询
- 等等
----------

## 使用
1. 创建表、写字段注释（方便生成表配置）
2. 生成表配置，可以用【工具箱】-【表管理】-【生成表配置】，也可以直接拷贝文件夹`wwwroot/scripts/table-config/`对应的`SQL`脚本运行
3. 修改表配置，表格，表单、查询，调整为需要展示的形式（标题、宽度、排序、输入类型、列格式化、必填、默认值等，根据业务拓展配置项）
4. 修改表配置，输入类型配置，需要配置下拉框、下拉树等，在`Common`控制器写方法，`url`源指向这个方法访问的地址
5. 修改表配置，列格式化配置，比如状态需要格式化为`启用`、`停用`，有常用公共的格式化方法，也可以配置自定义格式化方法`col_custom_字段小写`
6. 创建一个页面，菜单表添加此页面，配置操作按钮
7. 写表对应的查询、保存（新增/修改）、删除方法，参考【系统设置】里面的功能
8. 基于`z.js`封装的表格方法（API与EasyUI保持一致，看EasyUI文档即可），配置查询表的请求地址、表格类型、分页、复选等
----------

> 如果该项目对你有帮助，请你为项目Star，谢谢，这是对我精神上的支持，也是能一直坚持下去的动力。

> 在线演示：<https://rf2.netnr.com>
----------

## v2.X
- 前端采用 jQuery + Bootstrap + EasyUI + AceAdmin + fontAwesome
- 后端采用 Asp.Net Core + EF + SQL（SQLServer、MySQL、SQLite）
----------
## 截图

#### 列表 

![列表](https://netnr.gitee.io/gs/2018/05/18/403ce7d002.png)

#### 新增、编辑、查看

![表单](https://netnr.gitee.io/gs/2018/05/18/8d25d345b2.png)

#### 列表配置

![列表配置](https://netnr.gitee.io/gs/2018/05/18/13da6572a3.png)

#### 表单配置

![表单配置](https://netnr.gitee.io/gs/2018/05/18/0c98ee578c.png)

#### 角色权限配置（树）

![角色权限配置](https://netnr.gitee.io/gs/2018/08/16/31a55cac78.png)

----------
## 第三方文档API
- [EasyUI文档](https://ad.netnr.com/#EasyUI-1.5.2)
- [jQuery文档](https://ad.netnr.com/#jQuery-1.11.3)

----------
## v1　2017-03（已停更）
- 在线演示：<https://rf1.netnr.com>
- asp.net mvc4 + sqlite
- `jquery` `bootstrap` `easyui` `ace-admin` `font-awesome`

----------
PoweredBy [netnr.com](https://www.netnr.com)