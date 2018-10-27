# 更新日志

## [v2.1.2] - 2018-10-27
- 添加`按钮管理`功能
- 添加`菜单管理`功能
- 添加`授权关联`功能（支持第三方登录）
- 添加`z.Combo`方法添加清除值按钮支持
- 添加`PostgreSQL`数据库支持
- 调整`Linq`查询，可不传排序列，即默认排序
- 调整选项卡右侧仅为刷新按钮

## [v2.1.1] - 2018-10-13
- 修复方法`z.FindTreeNode`存在的问题
- 修复方法`z.FormEdit`存在的问题
- 修复`Linq`查询先分页后排序的问题
- 修复系统操作日志，`IP`获取始终为`127.0.0.1`的问题，原因是`nginx`代理，需要判断代理环境

## [v2.1]　2018-10
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

## [v2] - 2018-07
- 前端采用 jQuery + Bootstrap + EasyUI + AceAdmin
- 后端采用 Asp.Net Core + EF + SQLServer
- 全新重写 `z.js` 脚本包，与EasyUI提供的API高度保持一致（最大调整）
- 重写iframe选项卡
- 精简ace导航
- 若干调整...