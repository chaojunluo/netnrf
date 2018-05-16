# netnrf 响应式框架

Site：<https://rf.netnr.com>

Gitee：<https://gitee.com/netnr/netnrf>

GitHub：<https://github.com/netnr/netnrf>

----------
PoweredBy [netnr.com](https://www.netnr.com)　　[`联系（打赏）`](https://www.netnr.com/mix/contact)　　[`加入QQ群`](http://qm.qq.com/cgi-bin/qm/qr?k=oLmAflGAIODgeYw9tImSvBVX1SK_warh)

----------
### 菜单、按钮、权控

用户（User）、角色（Role）、菜单（Menu）、权限（Auth）

创建用户，设置角色，再设置角色权限（能操作的菜单、按钮）

### 列表、表单、查询

列表：动态配置标题、宽度、排序、对齐方式、格式化、冻结、点击排序等

表单：动态生成表单，自定义标题、排序、跨列、类型、必填等

查询：动态生成查询面板，自定义哪些字段支持查询

----------
### 版本

#### v2.0　　Demo：<https://rf2.netnr.com>　　（2018-05） 
> - Gitee：<https://gitee.com/netnr/rf2>
> - GitHub：<https://github.com/netnr/rf2>

> - 页面组成：`全局（_Layout）`、`按钮（Button组件）`、`主体View视图`、`表配置（TableConfig组件）`

> - Asp.Net Core 2.0 + EF + SQLServer
> - 优化ace导航
> - 重写iframe选项卡
> - 调整API，与官方API高度保持一致（最大调整）
> - 加入公共查询，以JSON结构表达查询条件
> - 若干调整...
> - 数据库设计用PD15.2，SQLServer版本，数据库服务器是虚拟主机送的20M，请不要摧毁数据库来成就自己，谢谢！
> - Iframe地址用indexOf判断是否打开，那么，/rf/modal 与 /rf/modallg 这两个链接会出问题，要么改indexOf或改url

------

#### v1.0　　Demo：<https://rf1.netnr.com>　　（2017-03）
> - Gitee：<https://gitee.com/netnr/rf1>
> - GitHub：<https://github.com/netnr/rf1>

> - asp.net mvc4 + sqlite
> - `jquery` `bootstrap` `easyui` `ace-admin` `font-awesome`

----------
### 第三方文档API
[EasyUI文档](https://ad.netnr.com/#EasyUI-1.5.2)、[jQuery文档](https://ad.netnr.com/#jQuery-1.11.3)