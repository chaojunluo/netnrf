# netnrf 响应式框架
- GitHub：<https://github.com/netnr/netnrf>
- Gitee：<https://gitee.com/netnr/netnrf>
- [联系（打赏）](https://ss.netnr.com/contact)
- [加入QQ群](http://qm.qq.com/cgi-bin/qm/qr?k=oLmAflGAIODgeYw9tImSvBVX1SK_warh)

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

> 如果该项目对你有帮助，请你为项目Star，谢谢，这是对我精神上的支持，也是能一直坚持下去的动力。

> 在线演示：<https://rf2.netnr.com>
----------
## v2.1　2018-10
- 跨平台跨数据库支持，<https://github.com/aspnet/EntityFrameworkCore> ，已测试`SQLite`、`MySql`、`SQLServer`
- 生成实体依赖于`Scaffold-DbContext`命令 <https://www.netnr.com/gist/code/5283651389582691979>
- 公共查询从SQL语句改为Linq
- 修复`z.js`若干问题
- 表管理工具，生成表配置、表字典
- 演示项目调整，从`SQLServer`改为`MySql`，服务器迁移国外搬瓦工（Centos7、Nginx、MySql、CN2线路）
---
- 顺便说一下这段时间的体会，一直用`Windows`服务器，接触`Linux`后，认为`Linux`做服务器是挺好的，已打算全部迁移至`Linux`服务器。
  -  `Linux`服务器便宜，相对而言
  - 国外的`VPS`大多限流量不限速度（比如1G的带宽，每月1T的流量），当然线路也重要，不然延迟掉包严重
  - 不用备案、可以搭梯子翻墙
  - `Linux`开机占用`100`MB左右，跑个`dotnet`进程也才`300`MB左右
  - `.net framework`的项目也能跑哦，`mono` 、[jexus](https://www.jexus.org/)
  - `SQLServer`数据库已经有`Linux`版本，当然也可以改为`MySql`
  - 上手`Linux`服务器，一脸懵逼，什么都不知道不知从何下手，怎么办，你说怎么办，凉拌，网上有大把的教程，弄懂一个算一个；这里提供一些我用到的东西：
    - ssh命令连接服务器
    - 安装环境：`dotnet`、`nginx`、`ftp`、`mysql`、`frp`（微信开发）、`shadowsocks`（翻墙）
    - 学习`vi`编辑器，学习`dotnet`命令，学习`nginx`配置
    - 存的一些干货 <https://www.netnr.com/gist/user/1>
## v2　2018-07
- 前端采用 jQuery + Bootstrap + EasyUI + AceAdmin
- 后端采用 Asp.Net Core + EF + SQLServer
- 全新重写 `z.js` 脚本包，与EasyUI提供的API高度保持一致（最大调整）
- 重写iframe选项卡
- 精简ace导航
- 若干调整...

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