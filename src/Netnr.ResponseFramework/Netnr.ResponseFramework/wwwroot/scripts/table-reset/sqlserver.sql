-- 删除所有表
DROP TABLE dbo.SysButton;
DROP TABLE dbo.SysDictionary;
-- TRUNCATE TABLE dbo.SysLog;
DROP TABLE dbo.SysMenu;
DROP TABLE dbo.SysRole;
DROP TABLE dbo.SysTableConfig;
DROP TABLE dbo.SysUser;
DROP TABLE dbo.TempExample;
DROP TABLE dbo.TempInvoiceMain;
DROP TABLE dbo.TempInvoiceDetail;

/****** Object:  Table [dbo].[SysButton]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysButton](
	[SbId] [varchar](50) NOT NULL,
	[SbPid] [varchar](50) NULL,
	[SbBtnText] [nvarchar](20) NULL,
	[SbBtnId] [varchar](50) NULL,
	[SbBtnClass] [varchar](50) NULL,
	[SbBtnIcon] [varchar](50) NULL,
	[SbBtnOrder] [int] NULL,
	[SbStatus] [int] NULL,
	[SbDescribe] [nvarchar](200) NULL,
	[SbBtnGroup] [int] NULL,
	[SbBtnHide] [int] NULL,
 CONSTRAINT [SysButton_SbId_PK] PRIMARY KEY CLUSTERED 
(
	[SbId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[SysDictionary]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysDictionary](
	[SdId] [varchar](50) NOT NULL,
	[SdPid] [varchar](50) NULL,
	[SdType] [varchar](200) NULL,
	[SdKey] [varchar](200) NULL,
	[SdValue] [nvarchar](200) NULL,
	[SdOrder] [int] NULL,
	[SdStatus] [int] NULL,
	[SdRemark] [nvarchar](200) NULL,
	[SdAttribute1] [varchar](50) NULL,
	[SdAttribute2] [varchar](50) NULL,
	[SdAttribute3] [varchar](50) NULL
) ON [PRIMARY]
;
SET ANSI_PADDING ON
;
/****** Object:  Index [SysDictionary_SdType]    Script Date: 2019-9-4 11:38:21 ******/
CREATE CLUSTERED INDEX [SysDictionary_SdType] ON [dbo].[SysDictionary]
(
	[SdType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Table [dbo].[SysMenu]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysMenu](
	[SmId] [varchar](50) NOT NULL,
	[SmPid] [varchar](50) NULL,
	[SmName] [nvarchar](50) NULL,
	[SmUrl] [varchar](200) NULL,
	[SmOrder] [int] NULL,
	[SmIcon] [varchar](50) NULL,
	[SmStatus] [int] NULL,
	[SmGroup] [int] NULL,
 CONSTRAINT [SysMenu_SmId_PK] PRIMARY KEY CLUSTERED 
(
	[SmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[SysRole]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysRole](
	[SrId] [varchar](50) NOT NULL,
	[SrName] [nvarchar](200) NULL,
	[SrStatus] [int] NULL,
	[SrDescribe] [nvarchar](200) NULL,
	[SrGroup] [int] NULL,
	[SrMenus] [nvarchar](max) NULL,
	[SrButtons] [nvarchar](max) NULL,
	[SrCreateTime] [datetime] NULL,
 CONSTRAINT [SysRole_SrId_PK] PRIMARY KEY CLUSTERED 
(
	[SrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [dbo].[SysTableConfig]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysTableConfig](
	[Id] [varchar](50) NOT NULL,
	[TableName] [varchar](200) NULL,
	[ColField] [varchar](200) NULL,
	[DvTitle] [nvarchar](200) NULL,
	[ColTitle] [nvarchar](200) NULL,
	[ColWidth] [int] NULL,
	[ColAlign] [int] NULL,
	[ColHide] [int] NULL,
	[ColOrder] [int] NULL,
	[ColFrozen] [int] NULL,
	[ColFormat] [nvarchar](200) NULL,
	[ColSort] [int] NULL,
	[ColExport] [int] NULL,
	[ColQuery] [int] NULL,
	[ColRelation] [varchar](200) NULL,
	[FormArea] [int] NULL,
	[FormUrl] [nvarchar](max) NULL,
	[FormType] [varchar](200) NULL,
	[FormSpan] [int] NULL,
	[FormHide] [int] NULL,
	[FormOrder] [int] NULL,
	[FormRequired] [int] NULL,
	[FormPlaceholder] [nvarchar](200) NULL,
	[FormValue] [nvarchar](max) NULL,
	[FormText] [nvarchar](max) NULL,
	[FormMaxlength] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
SET ANSI_PADDING ON
;
/****** Object:  Index [SysTableConfig_TableName]    Script Date: 2019-9-4 11:38:21 ******/
CREATE CLUSTERED INDEX [SysTableConfig_TableName] ON [dbo].[SysTableConfig]
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
/****** Object:  Table [dbo].[SysUser]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[SysUser](
	[SuId] [varchar](50) NOT NULL,
	[SrId] [varchar](50) NULL,
	[SuName] [nvarchar](50) NULL,
	[SuPwd] [nvarchar](50) NULL,
	[SuNickname] [nvarchar](50) NULL,
	[SuCreateTime] [datetime] NULL,
	[SuStatus] [int] NULL,
	[SuSign] [varchar](50) NULL,
	[SuGroup] [int] NULL,
 CONSTRAINT [SysUser_SuId_PK] PRIMARY KEY CLUSTERED 
(
	[SuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[TempExample]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[TempExample](
	[Id] [varchar](50) NOT NULL,
	[TableName] [varchar](200) NULL,
	[ColField] [varchar](200) NULL,
	[DvTitle] [nvarchar](200) NULL,
	[ColTitle] [nvarchar](200) NULL,
	[ColWidth] [int] NULL,
	[ColAlign] [int] NULL,
	[ColHide] [int] NULL,
	[ColOrder] [int] NULL,
	[ColFrozen] [int] NULL,
	[ColFormat] [nvarchar](200) NULL,
	[ColSort] [int] NULL,
	[ColExport] [int] NULL,
	[ColQuery] [int] NULL,
	[FormUrl] [nvarchar](max) NULL,
	[FormType] [varchar](200) NULL,
	[FormArea] [int] NULL,
	[FormSpan] [int] NULL,
	[FormHide] [int] NULL,
	[FormOrder] [int] NULL,
	[FormRequired] [int] NULL,
	[FormPlaceholder] [nvarchar](200) NULL,
	[FormValue] [nvarchar](max) NULL,
	[FormText] [nvarchar](max) NULL,
 CONSTRAINT [TempExample_Id_PK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;
/****** Object:  Table [dbo].[TempInvoiceDetail]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[TempInvoiceDetail](
	[TidId] [varchar](50) NOT NULL,
	[TimId] [varchar](50) NULL,
	[TimNo] [varchar](50) NULL,
	[TidOrder] [int] NULL,
	[GoodsId] [varchar](50) NULL,
	[GoodsCount] [int] NULL,
	[GoodsCost] [decimal](8, 2) NULL,
	[GoodsPrice] [decimal](8, 2) NULL,
	[Spare1] [varchar](50) NULL,
	[Spare2] [varchar](50) NULL,
	[Spare3] [varchar](50) NULL,
 CONSTRAINT [TempInvoiceDetail_TidId_PK] PRIMARY KEY CLUSTERED 
(
	[TidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[TempInvoiceMain]    Script Date: 2019-9-4 11:38:21 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE [dbo].[TempInvoiceMain](
	[TimId] [varchar](50) NOT NULL,
	[TimNo] [varchar](50) NULL,
	[TimDate] [datetime] NULL,
	[TimStore] [varchar](50) NULL,
	[TimType] [int] NULL,
	[TimSupplier] [varchar](50) NULL,
	[TimUser] [varchar](50) NULL,
	[TimRemark] [nvarchar](200) NULL,
	[TimOwnerId] [varchar](50) NULL,
	[TimOwnerName] [nvarchar](20) NULL,
	[TimCreateTime] [datetime] NULL,
	[TimStatus] [int] NULL,
	[Spare1] [varchar](50) NULL,
	[Spare2] [varchar](50) NULL,
	[Spare3] [varchar](50) NULL,
 CONSTRAINT [TempInvoiceMain_TimId_PK] PRIMARY KEY CLUSTERED 
(
	[TimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
;
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'26008EAA-4ED7-46E0-8BBF-DCF1472397E0', N'00000000-0000-0000-0000-000000000000', N'批处理', N'm_Batch', N'btn btn-sm btn-danger', N'fa fa-paint-brush', 40, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'3636B071-CE52-4551-BA67-4F982D14CD7C', N'00000000-0000-0000-0000-000000000000', N'导入', N'm_Import', N'btn btn-primary', N'fa fa-level-up', 74, 1, NULL, 1, -1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131', N'00000000-0000-0000-0000-000000000000', N'删除', N'm_Del', N'btn btn-sm  btn-danger', N'fa fa-remove', 4, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'3C6F626F-8D8E-46EE-B02A-0C90CFA90E02', N'00000000-0000-0000-0000-000000000000', N'启用', N'm_Start', N'btn btn-sm  btn-success', N'fa fa-play', 9, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'4674735D-B762-4876-8DE1-31AD7CD023F4', N'00000000-0000-0000-0000-000000000000', N'作废', N'm_Void', N'btn btn-sm  btn-default', N'fa fa-trash', 12, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'4FC96135-26B5-46D7-B809-747AD71F2A21', N'D2B8534F-D435-4E39-AC9D-4294AFC492DB', N'表单配置', N'list_Config_Form', N'', N'fa fa-table orange', 92, 1, N'', 4, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'58F7DA5E-37F8-4648-80F3-998E702A4D91', N'00000000-0000-0000-0000-000000000000', N'批量启用', N'm_Batch_Start', N'btn btn-success', N'fa fa-play', 41, 1, N'批处理功能按钮，以 m_Batch_ 开头', 1, 1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E', N'00000000-0000-0000-0000-000000000000', N'查看', N'm_See', N'btn btn-sm  btn-primary', N'fa fa-credit-card', 5, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'609287B6-4B59-4E59-A822-B8C1087BB603', N'00000000-0000-0000-0000-000000000000', N'导出', N'm_Export', N'btn btn-success', N'fa fa-level-down', 75, 1, NULL, 1, -1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'73CF6246-4429-4EF2-A0DA-F86F96B9BEBB', N'00000000-0000-0000-0000-000000000000', N'批量停用', N'm_Batch_Stop', N'btn btn-danger', N'fa fa-stop', 42, 1, NULL, 1, 1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a', N'00000000-0000-0000-0000-000000000000', N'全屏', N'm_Full_Screen', N'btn btn-default pull-right', N'fa fa-arrows-alt', 0, 1, N'居右时，排序最前面展示效果更佳', 1, -1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'807FF920-37AA-40F7-92BC-3FC894D4D7A3', N'00000000-0000-0000-0000-000000000000', N'批量删除', N'm_Batch_Del', N'btn btn-danger', N'fa fa-remove', 45, 1, NULL, 1, 1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'85C51241-19D1-4BD0-A35B-DB570ACD0E85', N'00000000-0000-0000-0000-000000000000', N'打印', N'm_Print', N'btn btn-danger', N'fa fa-print', 76, 1, NULL, 3, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'90ED8666-0961-426D-B582-E08C43EEE9E1', N'00000000-0000-0000-0000-000000000000', N'增加', N'm_Add', N'btn btn-sm btn-primary', N'fa fa-plus', 2, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'936D642A-CD7B-4A0E-837F-B4630A1BE894', N'00000000-0000-0000-0000-000000000000', N'参数设置', N'm_ParaSet', N'btn btn-sm  btn-success', N'fa fa-gear', 15, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'99A7F6EB-69BD-4167-B647-B10D3E12A3F3', N'00000000-0000-0000-0000-000000000000', N'修改', N'm_Edit', N'btn btn-sm btn-warning', N'fa fa-edit', 3, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'9B2265A4-A01F-48E8-9373-A6A294FCC1B7', N'00000000-0000-0000-0000-000000000000', N'关闭批处理', N'm_Batch_Close', N'btn btn-info', N'fa fa-mail-reply', 47, 1, NULL, 1, 1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'9BD9FE69-430B-4754-BF00-1DE1D117C384', N'00000000-0000-0000-0000-000000000000', N'停用', N'm_Stop', N'btn btn-sm  btn-warning', N'fa fa-stop', 10, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'9F128382-9A3E-42FB-89E7-D12E5D581584', N'00000000-0000-0000-0000-000000000000', N'弃废', N'm_UnVoid', N'btn btn-sm  btn-danger', N'fa fa-reply', 13, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'A04A57A2-014C-4D47-A6A2-B5018ED286CB', N'00000000-0000-0000-0000-000000000000', N'刷新', N'm_Reload', N'btn btn-sm  btn-info', N'fa fa-refresh', 39, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'a26aa1f9-1a8d-4a0f-a222-83c8470d86aa', N'00000000-0000-0000-0000-000000000000', N'复制权限', N'm_CAuth', N'btn btn-sm btn-warning', N'fa fa-copy', 16, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'ACD7FC7F-DE75-4502-B619-CF6BDA16CB9A', N'00000000-0000-0000-0000-000000000000', N'权限控制', N'm_Auth', N'btn btn-sm  btn-success', N'fa fa-gear', 14, 1, NULL, 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'AE0D0298-FE28-405F-82AB-00E310FFE9C2', N'D2B8534F-D435-4E39-AC9D-4294AFC492DB', N'表格配置', N'list_Config_Table', N'', N'fa fa-table orange', 91, 1, N'', 4, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'B97248C7-F53A-4289-BF06-A05E8009199B', N'00000000-0000-0000-0000-000000000000', N'切换', N'm_Switch', N'btn btn-sm  btn-primary', N'fa fa-exchange', 20, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'C42B2ECC-3A18-495B-9BC6-B315FEA5A951', N'00000000-0000-0000-0000-000000000000', N'批量修改', N'm_Batch_Edit', N'btn btn-warning', N'fa fa-edit', 44, 1, NULL, 1, 1)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'D2B8534F-D435-4E39-AC9D-4294AFC492DB', N'00000000-0000-0000-0000-000000000000', N'更多', N'list_More', N'btn btn-sm  btn-primary', N'fa fa-ellipsis-h', 80, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'D3A31A0C-C842-4709-82DD-A33B0253A462', N'00000000-0000-0000-0000-000000000000', N'保存', N'm_Save', N'btn btn-sm  btn-success', N'fa fa-save', 6, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'EAED8E4A-E6DA-4075-883F-8B5559B7A9AD', N'00000000-0000-0000-0000-000000000000', N'上传附件', N'm_Upload', N'btn btn-primary', N'fa fa-cloud-upload', 78, 1, NULL, 5, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'ED6830FD-DFD5-4B48-A155-76C8D7D6FEA4', N'00000000-0000-0000-0000-000000000000', N'取消', N'm_Cancel', N'btn btn-sm  btn-danger', N'fa fa-reply', 7, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'EFE021E2-30FE-4500-9BF6-52611F1AAA4A', N'00000000-0000-0000-0000-000000000000', N'查询', N'm_Query', N'btn btn-sm  btn-success', N'fa fa-search', 1, 1, N'', 1, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'F60C1C50-EBDC-430A-BE3A-30C4AB23C3FD', N'00000000-0000-0000-0000-000000000000', N'复制并新增', N'm_Copy', N'btn btn-info', N'fa fa-copy', 77, 1, NULL, 2, NULL)
INSERT [dbo].[SysButton] ([SbId], [SbPid], [SbBtnText], [SbBtnId], [SbBtnClass], [SbBtnIcon], [SbBtnOrder], [SbStatus], [SbDescribe], [SbBtnGroup], [SbBtnHide]) VALUES (N'FA51A36A-69DD-4838-AD03-EFA8F038F23F', N'00000000-0000-0000-0000-000000000000', N'审核', N'm_Check', N'btn btn-sm  btn-info', N'fa fa-check-square-o', 11, 1, N'', 1, NULL)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'00000000-0000-0000-0000-000000000000', N'系统设置', NULL, 9, N'fa-cog', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'2AE7FAF0-B627-4012-8A94-C5337579C8F5', N'00000000-0000-0000-0000-000000000000', N'RF框架示例', NULL, 3, N'fa-tag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'2d5567ed-80d6-491c-b5e2-ef17babb3246', N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'多表单生成', N'/rf/buildforms', 2, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'56ad6bac-d773-4460-b88f-164c08df4808', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'按钮管理', N'/setting/sysbutton', 1, N'fa-save', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'601C6500-808A-426B-9658-6BA830396AE3', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'角色管理', N'/setting/sysrole', 3, N'fa-mortar-board (alias)', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'60C478C8-B4B7-471F-AE7F-62DF7A6C44D4', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'日志管理', N'/setting/syslog', 6, N'fa-file-text-o', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'688BE98C-3D78-4B4D-A160-91476407599F', N'2AE7FAF0-B627-4012-8A94-C5337579C8F5', N'表格', NULL, 3, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'6C9E2090-B115-4D3E-948B-E5829A1886CF', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'表配置', N'/setting/systableconfig', 8, N'fa-cog', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'748d9667-bd7a-4745-be34-1057160ef444', N'688BE98C-3D78-4B4D-A160-91476407599F', N'Grid表格联动', N'/rf/gridchange', 3, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'800f8620-2b7b-4ea9-8e17-7c8d2b90561b', N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'上传', N'/rf/upload', 4, N'fa-cloud-upload', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'8120ACDF-0642-4EA0-8BEC-83306D744319', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'用户管理', N'/setting/sysuser', 4, N'fa-user', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'813984B9-06CA-4D85-AD82-3C4AD2CB834E', N'CAAFB396-C5F2-406A-9808-6B089E20F265', N'表管理', N'/tool/tablemanage', 1, N'fa-database', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'89EB0D3E-5BAA-494E-AD49-7FE247405CDA', N'688BE98C-3D78-4B4D-A160-91476407599F', N'TreeGrid', N'/rf/treegrid', 2, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'9494d996-bdf3-4cf9-9784-c40964ace3fb', N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'单据', N'/rf/invoice', 3, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'A40C1D01-C682-483F-AF87-CD843AA457C7', N'2AE7FAF0-B627-4012-8A94-C5337579C8F5', N'表配置示例', N'/rf/tce', 1, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'a5fc0578-72c4-4be1-9ad8-71ef0cc9f746', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'菜单管理', N'/setting/sysmenu', 2, N'fa-navicon', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'2AE7FAF0-B627-4012-8A94-C5337579C8F5', N'表单', NULL, 4, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'B27D2434-DC15-4EFA-A586-E11DF23D5344', N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'静态表单示例', N'/rf/form', 1, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'bb1516c7-834a-4ca4-a701-ed9e0f212c1d', N'1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', N'字典管理', N'/setting/sysdictionary', 7, N'fa-database', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'CAAFB396-C5F2-406A-9808-6B089E20F265', N'00000000-0000-0000-0000-000000000000', N'工具箱', NULL, 8, N'fa-wrench', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'DF9CFA05-847A-43B6-8119-E8FC7AE04734', N'688BE98C-3D78-4B4D-A160-91476407599F', N'DataGrid', N'/rf/datagrid', 1, N'fa-flag', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'f45615da-a915-47d2-a623-4c956c785a95', N'a9ae039c-2155-4ca8-b23a-5d793a71210e', N'富文本', N'/rf/richtext', 5, N'fa-file-text-o', 1, 1)
INSERT [dbo].[SysMenu] ([SmId], [SmPid], [SmName], [SmUrl], [SmOrder], [SmIcon], [SmStatus], [SmGroup]) VALUES (N'F8C1C161-F1FC-4729-A00C-4A9893BF8209', N'00000000-0000-0000-0000-000000000000', N'系统桌面', N'/home/desk', 1, N'fa-home', 1, 1)
INSERT [dbo].[SysRole] ([SrId], [SrName], [SrStatus], [SrDescribe], [SrGroup], [SrMenus], [SrButtons], [SrCreateTime]) VALUES (N'58307c67-76b8-4156-bde3-f307f4da25e9', N'测试', 1, NULL, NULL, N'F8C1C161-F1FC-4729-A00C-4A9893BF8209,2AE7FAF0-B627-4012-8A94-C5337579C8F5,A40C1D01-C682-483F-AF87-CD843AA457C7,B27D2434-DC15-4EFA-A586-E11DF23D5344,688BE98C-3D78-4B4D-A160-91476407599F,DF9CFA05-847A-43B6-8119-E8FC7AE04734,89EB0D3E-5BAA-494E-AD49-7FE247405CDA', N'{"F8C1C161-F1FC-4729-A00C-4A9893BF8209":"","A40C1D01-C682-483F-AF87-CD843AA457C7":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1"}', CAST(N'2018-05-20T10:23:19.000' AS DateTime))
INSERT [dbo].[SysRole] ([SrId], [SrName], [SrStatus], [SrDescribe], [SrGroup], [SrMenus], [SrButtons], [SrCreateTime]) VALUES (N'E663CE67-E9CA-4441-AB77-DC267C22C683', N'管理员', 1, NULL, 1, N'F8C1C161-F1FC-4729-A00C-4A9893BF8209,2AE7FAF0-B627-4012-8A94-C5337579C8F5,A40C1D01-C682-483F-AF87-CD843AA457C7,688BE98C-3D78-4B4D-A160-91476407599F,DF9CFA05-847A-43B6-8119-E8FC7AE04734,89EB0D3E-5BAA-494E-AD49-7FE247405CDA,748d9667-bd7a-4745-be34-1057160ef444,a9ae039c-2155-4ca8-b23a-5d793a71210e,B27D2434-DC15-4EFA-A586-E11DF23D5344,2d5567ed-80d6-491c-b5e2-ef17babb3246,9494d996-bdf3-4cf9-9784-c40964ace3fb,800f8620-2b7b-4ea9-8e17-7c8d2b90561b,f45615da-a915-47d2-a623-4c956c785a95,CAAFB396-C5F2-406A-9808-6B089E20F265,813984B9-06CA-4D85-AD82-3C4AD2CB834E,1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC,56ad6bac-d773-4460-b88f-164c08df4808,a5fc0578-72c4-4be1-9ad8-71ef0cc9f746,601C6500-808A-426B-9658-6BA830396AE3,8120ACDF-0642-4EA0-8BEC-83306D744319,60C478C8-B4B7-471F-AE7F-62DF7A6C44D4,bb1516c7-834a-4ca4-a701-ed9e0f212c1d,6C9E2090-B115-4D3E-948B-E5829A1886CF', N'{"601C6500-808A-426B-9658-6BA830396AE3":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,ACD7FC7F-DE75-4502-B619-CF6BDA16CB9A,a26aa1f9-1a8d-4a0f-a222-83c8470d86aa,A04A57A2-014C-4D47-A6A2-B5018ED286CB,609287B6-4B59-4E59-A822-B8C1087BB603,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","60C478C8-B4B7-471F-AE7F-62DF7A6C44D4":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,609287B6-4B59-4E59-A822-B8C1087BB603,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","6C9E2090-B115-4D3E-948B-E5829A1886CF":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","8120ACDF-0642-4EA0-8BEC-83306D744319":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,609287B6-4B59-4E59-A822-B8C1087BB603,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","F8C1C161-F1FC-4729-A00C-4A9893BF8209":"","9C145834-8336-4E63-A34C-6DF8E5854C96":"","B27D2434-DC15-4EFA-A586-E11DF23D5344":"90ED8666-0961-426D-B582-E08C43EEE9E1,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,D3A31A0C-C842-4709-82DD-A33B0253A462,A04A57A2-014C-4D47-A6A2-B5018ED286CB,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","1CDBF142-7F5C-42E8-A426-370BF4542224":"A04A57A2-014C-4D47-A6A2-B5018ED286CB","A40C1D01-C682-483F-AF87-CD843AA457C7":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,D3A31A0C-C842-4709-82DD-A33B0253A462,A04A57A2-014C-4D47-A6A2-B5018ED286CB,26008EAA-4ED7-46E0-8BBF-DCF1472397E0,58F7DA5E-37F8-4648-80F3-998E702A4D91,73CF6246-4429-4EF2-A0DA-F86F96B9BEBB,C42B2ECC-3A18-495B-9BC6-B315FEA5A951,807FF920-37AA-40F7-92BC-3FC894D4D7A3,9B2265A4-A01F-48E8-9373-A6A294FCC1B7,3636B071-CE52-4551-BA67-4F982D14CD7C,609287B6-4B59-4E59-A822-B8C1087BB603,85C51241-19D1-4BD0-A35B-DB570ACD0E85,F60C1C50-EBDC-430A-BE3A-30C4AB23C3FD,EAED8E4A-E6DA-4075-883F-8B5559B7A9AD,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","DF9CFA05-847A-43B6-8119-E8FC7AE04734":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,A04A57A2-014C-4D47-A6A2-B5018ED286CB,26008EAA-4ED7-46E0-8BBF-DCF1472397E0,58F7DA5E-37F8-4648-80F3-998E702A4D91,73CF6246-4429-4EF2-A0DA-F86F96B9BEBB,C42B2ECC-3A18-495B-9BC6-B315FEA5A951,807FF920-37AA-40F7-92BC-3FC894D4D7A3,9B2265A4-A01F-48E8-9373-A6A294FCC1B7,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","89EB0D3E-5BAA-494E-AD49-7FE247405CDA":"A04A57A2-014C-4D47-A6A2-B5018ED286CB,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","F5415EAE-694F-4332-B259-E86BDC54AA09":"A04A57A2-014C-4D47-A6A2-B5018ED286CB","56ad6bac-d773-4460-b88f-164c08df4808":"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","a5fc0578-72c4-4be1-9ad8-71ef0cc9f746":"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","faa428d6-6d57-4fe2-84cf-48ab7beaa950":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB","748d9667-bd7a-4745-be34-1057160ef444":"A04A57A2-014C-4D47-A6A2-B5018ED286CB,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","9494d996-bdf3-4cf9-9784-c40964ace3fb":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,D3A31A0C-C842-4709-82DD-A33B0253A462,ED6830FD-DFD5-4B48-A155-76C8D7D6FEA4,FA51A36A-69DD-4838-AD03-EFA8F038F23F,4674735D-B762-4876-8DE1-31AD7CD023F4,9F128382-9A3E-42FB-89E7-D12E5D581584,B97248C7-F53A-4289-BF06-A05E8009199B,A04A57A2-014C-4D47-A6A2-B5018ED286CB,85C51241-19D1-4BD0-A35B-DB570ACD0E85,F60C1C50-EBDC-430A-BE3A-30C4AB23C3FD,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","2d5567ed-80d6-491c-b5e2-ef17babb3246":"","800f8620-2b7b-4ea9-8e17-7c8d2b90561b":"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","bb1516c7-834a-4ca4-a701-ed9e0f212c1d":"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,609287B6-4B59-4E59-A822-B8C1087BB603,D2B8534F-D435-4E39-AC9D-4294AFC492DB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a","f45615da-a915-47d2-a623-4c956c785a95":"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,7ca5f9e0-9435-42d9-afe6-f388bd6c7b3a"}', CAST(N'2018-05-20T07:34:18.000' AS DateTime))
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2988c87c-ec7a-47ac-9d73-d96790c335d7', N'SysButton', N'SbId', N'SbId', N'SbId', 100, 1, 2, 0, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6976d441-159f-4dd4-8040-0bd383eb8875', N'SysButton', N'SbBtnText', N'按钮文本', N'按钮文本', 200, 1, 0, 0, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 0, 1, NULL, NULL, NULL, 20)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6f7f9deb-b756-4b0f-b3a9-8742cea11ca1', N'SysButton', N'SbPid', N'上级按钮', N'上级按钮', 100, 1, 2, 1, NULL, N'0', NULL, NULL, 0, N'', 1, N'/Common/QueryButtonTree', N'combotree', 1, 0, 1, NULL, N'无上级按钮不填', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'73fb80dc-7c0a-4a97-ad33-7b814b7b9271', N'SysButton', N'SbBtnHide', N'隐藏，1隐藏', N'隐藏', 80, 2, 0, 7, 0, N'17', NULL, 1, 0, N'', 1, NULL, N'checkbox', 1, 0, 8, NULL, NULL, NULL, N'隐藏按钮', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'86879d40-3167-45d2-b310-9723efd899e9', N'SysButton', N'SbBtnOrder', N'按钮排序', N'按钮排序', 100, 2, 0, 4, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 5, NULL, N'升序', NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'878b3861-9a7c-449d-b075-9c4064c070e3', N'SysButton', N'SbBtnIcon', N'按钮图标', N'按钮图标', 240, 1, 0, 3, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 4, 1, N'fa fa-*', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'8d616d71-4a50-4c0e-b77b-788c044e4551', N'SysButton', N'SbBtnId', N'按钮ID', N'按钮ID', 180, 1, 0, 1, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 2, 1, N'm_ 开头', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ae5cc893-6524-41ba-baef-ee9f9aeaf801', N'SysButton', N'SbStatus', N'按钮状态', N'按钮状态', 100, 2, 0, 5, 0, N'17', NULL, 1, 0, N'', 1, NULL, N'checkbox', 1, 0, 6, NULL, NULL, N'1', N'启用按钮', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'c6812054-d3a5-43b6-9920-0194f0ac7fda', N'SysButton', N'SbDescribe', N'按钮描述', N'按钮描述', 260, 1, 0, 8, 0, N'0', 0, 1, 0, N'', 1, N'', N'text', 1, 0, 9, 0, N'', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'df8bac04-ed8f-4b73-95d7-c0b851a827ea', N'SysButton', N'SbBtnClass', N'按钮类', N'按钮类', 240, 1, 0, 2, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 3, NULL, N'btn btn-*', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ef4bce92-48be-4e9e-a994-c4e6f3826d0c', N'SysButton', N'SbBtnGroup', N'按钮分组', N'按钮分组', 100, 2, 0, 6, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 7, 1, N'默认1，不相同有分割线，仅弹出的二级按钮', NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'8e685458-3234-4190-9540-501e690bf041', N'SysDictionary', N'SdOrder', N'排序', N'排序', 100, 2, 0, 3, 0, N'0', 1, 1, NULL, N'', 1, NULL, N'text', 1, 0, 7, NULL, NULL, N'50', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'aa258f28-cceb-451c-bbe1-61ed9298a93d', N'SysDictionary', N'SdRemark', N'备注', N'备注', 120, 1, 0, 4, 0, N'0', 0, 1, 0, N'', 1, N'', N'text', 1, 0, 9, 0, N'', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'abae8f20-4205-4505-96be-bf09d20f2990', N'SysDictionary', N'SdStatus', N'状态：1正常，-1删除，2停用', N'状态', 100, 2, 0, 2, NULL, N'col_custom_', NULL, 1, NULL, N'Equal', 1, N'dataurl_sdstatus', N'combobox', 1, 0, 8, 1, NULL, N'1', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'8349251a-493c-4ae4-9472-61c4e4c42f9a', N'SysDictionary', N'SdPid', N'上级ID', N'上级ID', 120, 1, 2, 2, NULL, N'0', NULL, NULL, NULL, N'', 1, NULL, N'text', 1, 2, 2, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'7eb8fd0f-5fef-4a40-9521-5cdf6ebab950', N'SysDictionary', N'SdType', N'字典类别', N'字典类别', 120, 1, 2, 3, NULL, N'0', NULL, NULL, NULL, N'', 1, N'dataurl_sdtype', N'combobox', 2, 0, 3, 1, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'54d5e20b-7af4-4528-8e7e-2ea828b2e9a1', N'SysDictionary', N'SdAttribute1', N'特性', N'特性1', 120, 1, 0, 5, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 10, NULL, N'值的属性延伸', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'dc43c1f5-cdb6-44d6-8af6-ffadf277b51b', N'SysDictionary', N'SdAttribute2', N'特性', N'特性2', 120, 1, 0, 6, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 11, NULL, N'值的属性延伸', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'a9828350-5626-40c5-bc9e-6ccadf4735d2', N'SysDictionary', N'SdAttribute3', N'特性', N'特性3', 120, 1, 0, 7, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 12, NULL, N'值的属性延伸', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ce05d7bd-4ce2-4ea4-bb62-3fb48b2ab1cf', N'SysDictionary', N'SdKey', N'键', N'键', 150, 1, 0, 1, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 5, NULL, N'需要时，填写自定义Key，常规取主键ID', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd10cff5e-b409-4f00-8a5f-efe69fd1f571', N'SysDictionary', N'SdValue', N'值', N'值', 200, 1, 0, 2, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 6, 1, N'显示的文本内容', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6b6715e3-f259-43d9-831f-838f67dcdef8', N'SysDictionary', N'SdId', N'SdId', N'ID', 120, 1, 2, 0, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'02f9fea8-9fd1-4d1c-9717-a4bdc7cebbbe', N'SysLog', N'LogId', N'LogId', N'LogId', 100, 1, 2, 0, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'0e03400c-57fe-45d1-90e0-ba1da7dc6893', N'SysLog', N'SuNickname', N'姓名', N'姓名', 160, 1, 0, 1, 0, N'0', 0, 1, 0, N'', 1, N'', N'text', 1, 0, 1, 0, N'', N'', N'', 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'16bf4fe9-f627-415b-a68e-aae4bb867af3', N'SysLog', N'LogContent', N'内容', N'内容', 250, 1, 0, 3, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 3, NULL, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'4f78165f-7ace-48a0-89d3-966295877931', N'SysLog', N'LogIp', N'IP', N'IP', 160, 2, 0, 5, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 5, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5a25fd32-ec78-4162-9a8d-ad55fa281ea8', N'SysLog', N'LogGroup', N'分组', N'分组', 100, 1, 1, 10, 0, N'0', NULL, 0, 0, N'', 1, NULL, N'text', 1, 1, 11, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6fa7ee1d-d075-4d0d-8e85-7bc54dc2e218', N'SysLog', N'LogAction', N'动作', N'动作', 250, 1, 0, 2, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 2, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'73c8b232-5cd6-4b75-9af5-6fc41fb92f7e', N'SysLog', N'LogCity', N'IP城市', N'IP城市', 200, 1, 0, 6, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 6, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'9175e691-74cc-4d93-bd30-844c3c926bfe', N'SysLog', N'SuName', N'账号', N'账号', 160, 1, 0, 0, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 0, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'a8ff3842-77bd-41f3-acfc-c25a51637c58', N'SysLog', N'LogUrl', N'链接', N'链接', 350, 1, 0, 4, 0, N'0', 0, 1, 0, N'', 1, N'', N'text', 1, 0, 4, 0, N'', N'', N'', 500)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'aefedccf-0478-4bc9-ab01-248bf218428c', N'SysLog', N'LogRemark', N'备注', N'备注', 100, 1, 1, 11, 0, N'0', NULL, 0, 0, N'', 1, NULL, N'text', 1, 1, 12, NULL, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd6bc738f-c141-4050-91ac-09430ce91c85', N'SysLog', N'LogSystemName', N'操作系统', N'操作系统', 150, 2, 0, 9, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 9, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'fa28e3c8-fe45-488f-978b-53604ac42037', N'SysLog', N'LogBrowserName', N'浏览器名称', N'浏览器名称', 180, 2, 0, 8, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 8, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'319e2c65-7054-409e-a790-7a9af1af8fda', N'SysLog', N'LogCreateTime', N'时间', N'时间', 200, 2, 0, 7, NULL, N'0', NULL, 1, 1, N'GreaterThanOrEqual,LessThanOrEqual,BetweenAnd', 1, NULL, N'datetime', 1, 0, 7, NULL, NULL, NULL, NULL, 23)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'190f7729-a3ef-4dc2-8a5d-5f3d188e2508', N'SysMenu', N'SmGroup', N'菜单分组', N'菜单分组', 100, 2, 0, 5, NULL, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 6, NULL, N'默认1，如PC、Mobile不同的菜单', N'1', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'1b0232e1-30e0-4c52-ba0a-a6c82e19eff8', N'SysMenu', N'SmId', N'SmId', N'SmId', 100, 1, 2, 0, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'31972d95-e35f-4acc-966b-d73675e876e3', N'SysMenu', N'SmUrl', N'菜单链接', N'菜单链接', 300, 1, 0, 1, NULL, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 2, NULL, N'统一小写', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'35b0e34c-64cc-4c29-98ca-41c0823bb21a', N'SysMenu', N'SmName', N'菜单名称', N'菜单名称', 300, 1, 0, 0, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 0, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'492605d9-19d0-41d7-bd97-d9bbdee4062b', N'SysMenu', N'SmOrder', N'菜单排序', N'菜单排序', 100, 2, 0, 3, NULL, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 4, NULL, N'升序', NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6472db34-e4fa-450b-be04-c680d942ed66', N'SysMenu', N'SmPid', N'上级菜单', N'上级菜单', 100, 1, 2, 1, NULL, N'0', NULL, NULL, 0, N'', 1, N'/Common/QueryMenu?type=all', N'combotree', 1, 0, 1, NULL, N'无上级菜单不填', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'81008fba-45d0-4e8f-b335-c97c9c7e3773', N'SysMenu', N'SmStatus', N'菜单状态', N'菜单状态', 100, 2, 0, 4, 0, N'17', NULL, 1, 0, N'', 1, NULL, N'checkbox', 1, 0, 5, NULL, NULL, N'1', N'启用菜单', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'a1c90aea-4e29-4591-a4fa-e0ef2dea83bd', N'SysMenu', N'SmIcon', N'菜单图标', N'菜单图标', 250, 1, 0, 2, NULL, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 3, 1, N'fa-*', NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'18a045ed-5831-4e00-8635-6d6d4622d71b', N'SysRole', N'SrButtons', N'按钮', N'按钮', 100, 1, 2, 6, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 6, NULL, NULL, NULL, NULL, -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'48622617-6eaf-4c51-9d21-65831c4833b1', N'SysRole', N'SrCreateTime', N'创建时间', N'创建时间', 100, 1, 2, 4, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 7, NULL, NULL, NULL, NULL, 23)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'60041c0f-97b3-428b-94cf-9efd8fe2dc77', N'SysRole', N'SrName', N'角色名称', N'角色名称', 200, 1, 0, 0, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 2, 0, 0, 1, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'601652f5-4e90-4003-95ec-7ecd28218284', N'SysRole', N'SrMenus', N'菜单', N'菜单', 100, 1, 2, 5, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 5, NULL, NULL, NULL, NULL, -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'c6d5e56a-2560-4a33-bead-c017a7e09e83', N'SysRole', N'SrId', N'SrId', N'SrId', 100, 1, 2, 0, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 0, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'fff71a49-8aa3-4621-91a1-b3c2d5e5ae15', N'SysRole', N'SrGroup', N'角色分组', N'角色分组', 100, 1, 2, 3, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 4, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'00791e34-a362-4ca1-a6db-d41c56f51e11', N'SysRole', N'SrStatus', N'角色状态', N'角色状态', 120, 2, 0, 2, NULL, N'17', 1, 1, 1, N'Equal', 1, NULL, N'checkbox', 2, 0, 1, NULL, NULL, N'1', N'启用角色', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'285ac686-156f-4b02-9323-5c14cadfcf06', N'SysRole', N'SrDescribe', N'角色描述', N'角色描述', 500, 1, 0, 1, NULL, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 2, 0, 2, NULL, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'0652069B-C977-41F6-9BCE-537A1A21C909', N'SysTableConfig', N'FormMaxlength', N'最大长度', N'最大长度', 100, 2, 0, 24, 0, N'0', NULL, 1, 0, N'', 2, NULL, N'text', 1, 0, 24, NULL, N'设置文本框 maxlength 属性，-1 MAX', NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'150475f0-2ada-4a82-ad6e-1e6a0f139c77', N'SysTableConfig', N'FormText', N'显示文本', N'显示文本', 200, 1, 0, 23, 0, N'0', NULL, 1, 0, N'', 2, NULL, N'text', 1, 0, 23, NULL, N'checkbox类型后面带的文字说明', NULL, NULL, -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2f0bad42-c44f-4912-82bd-e7e6009b501f', N'SysTableConfig', N'DvTitle', N'默认列标题', N'默认列标题', 200, 1, 0, 5, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 2, 1, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'3ab0d2cb-459f-4523-859e-eff22b8e46b1', N'SysTableConfig', N'Id', N'Id', N'Id', 100, 1, 2, 2, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'3b037cb4-7181-428c-9ae9-3b1ff2e3aed1', N'SysTableConfig', N'ColWidth', N'表格列宽', N'表格列宽', 100, 2, 0, 4, 0, N'0', NULL, 1, 0, N'', 1, NULL, N'text', 1, 0, 4, NULL, NULL, N'100', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'9bd08641-1e04-4f0f-a7e9-b89384879f3a', N'SysTableConfig', N'FormOrder', N'表单排序', N'表单排序', 100, 2, 0, 19, 0, N'0', NULL, 1, 0, N'', 2, NULL, N'text', 1, 0, 19, NULL, NULL, N'100', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'a2e69048-825b-49d7-8641-211320302b06', N'SysTableConfig', N'ColAlign', N'对齐方式', N'对齐方式', 100, 2, 0, 3, 0, N'col_custom_', NULL, 1, 0, N'', 1, N'dataurl_colalign', N'combobox', 1, 0, 5, NULL, NULL, N'1', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'bbea4c72-cec1-4cfa-b5f8-7b040432546b', N'SysTableConfig', N'ColOrder', N'表格排序', N'表格排序', 100, 2, 0, 7, 0, N'0', 1, 1, 0, N'', 1, NULL, N'text', 1, 0, 7, NULL, N'列排序，升序', N'100', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ccc32410-656a-4bf0-a037-49b809756c38', N'SysTableConfig', N'TableName', N'（虚）表名', N'（虚）表名', 200, 1, 0, 0, 1, N'0', 1, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 0, 1, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd223cdbf-e0c4-4d00-b0a5-0e2ce73239d7', N'SysTableConfig', N'FormPlaceholder', N'输入框提示', N'输入框提示', 200, 1, 0, 21, 0, N'0', NULL, 1, 0, N'', 2, NULL, N'text', 1, 0, 21, NULL, N'设置 placeholder 提示信息 或 file类型追加说明', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd6d96911-d329-47bc-afe7-ad3c26b6ccc2', N'SysTableConfig', N'FormValue', N'初始值', N'初始值', 150, 1, 0, 22, 0, N'0', NULL, 1, 0, N'', 2, NULL, N'text', 1, 0, 22, NULL, N'默认值', NULL, NULL, -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd9791202-bfba-4658-9558-d18e3216c74e', N'SysTableConfig', N'ColField', N'列键', N'列键', 200, 1, 0, 2, 1, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 1, 1, N'表字段', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ea59681d-af83-4419-a236-2435fbd6d8a6', N'SysTableConfig', N'FormSpan', N'表单跨列', N'表单跨列', 100, 2, 0, 17, 0, N'col_custom_', NULL, 1, 0, N'', 2, N'dataurl_formspan', N'combobox', 1, 0, 17, NULL, NULL, N'1', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'f1d4d3d1-770a-4813-bb1d-648718112c86', N'SysTableConfig', N'ColHide', N'表格隐藏', N'表格隐藏', 150, 2, 0, 6, 0, N'col_custom_', NULL, 1, 0, N'', 1, N'dataurl_colhide', N'combobox', 1, 0, 6, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'70bddf4a-2ffb-4728-9ac8-a23dbe101043', N'SysTableConfig', N'ColFrozen', N'表格列冻结', N'表格列冻结', 120, 2, 0, 8, NULL, N'17', NULL, 1, 1, N'Equal', 1, NULL, N'checkbox', 1, 0, 8, NULL, NULL, N'0', N'是冻结列', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'40477de2-1e16-47bd-ad34-d9510ee68296', N'SysTableConfig', N'ColFormat', N'表格格式化', N'表格格式化', 280, 1, 0, 9, NULL, N'col_custom_', NULL, 1, 1, N'Equal', 1, N'dataurl_colformat', N'combobox', 1, 0, 13, NULL, NULL, N'0', NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'c94de34f-5021-49c1-94fc-2107028654b7', N'SysTableConfig', N'ColExport', N'导出', N'导出', 80, 2, 0, 11, NULL, N'17', NULL, 1, 1, N'Equal', 1, NULL, N'checkbox', 1, 0, 12, NULL, NULL, N'0', N'是导出列', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'376257e4-993c-4c37-926f-5c59a80282ed', N'SysTableConfig', N'ColQuery', N'查询', N'查询', 80, 2, 0, 12, NULL, N'17', NULL, 1, 1, N'Equal', 1, NULL, N'checkbox', 1, 0, 10, NULL, NULL, N'0', N'启用查询', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'da7bb1c5-0f76-4037-a761-0154538746ad', N'SysTableConfig', N'FormArea', N'区域', N'区域', 120, 2, 0, 14, NULL, N'col_custom_', NULL, 1, 1, N'Equal', 2, N'dataurl_formarea', N'combobox', 1, 0, 14, NULL, NULL, N'1', NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5e271751-1bbe-4393-b366-0052178c11ea', N'SysTableConfig', N'FormType', N'输入类型', N'输入类型', 200, 1, 0, 15, NULL, N'0', NULL, 1, 1, N'Equal', 2, N'dataurl_formtype', N'combobox', 1, 0, 16, NULL, NULL, N'text', NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'e0141204-ba34-48e1-809d-7875c472adaa', N'SysTableConfig', N'FormUrl', N'表单来源（URL）', N'表单来源（URL）', 300, 1, 0, 16, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 2, NULL, N'text', 1, 0, 15, NULL, N'请求的地址 或 z.DC配置的key', NULL, NULL, -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'acaeabec-f4b2-490d-8814-3bac5f933956', N'SysTableConfig', N'FormHide', N'表单隐藏', N'表单隐藏', 150, 2, 0, 18, NULL, N'col_custom_', NULL, 1, 1, N'Equal', 2, N'dataurl_formhide', N'combobox', 1, 0, 18, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'916f1c76-9b68-4ce1-ab4e-c23a3bdef25e', N'SysTableConfig', N'FormRequired', N'表单必填', N'表单必填', 100, 2, 0, 20, NULL, N'17', NULL, 1, 1, N'Equal', 2, NULL, N'checkbox', 1, 0, 20, NULL, NULL, N'0', N'是必填项', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ac56b715-d42f-4c10-9457-12d424611216', N'SysTableConfig', N'ColSort', N'启用排序', N'启用排序', 100, 2, 0, 10, NULL, N'17', NULL, 1, 1, N'Equal', 1, NULL, N'checkbox', 1, 0, 9, NULL, NULL, N'0', N'点击标题排序', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'c6eae40f-258e-47e3-8309-59dd09ac37da', N'SysTableConfig', N'ColTitle', N'列标题', N'列标题', 250, 1, 0, 1, 1, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 1, 0, 3, 1, NULL, NULL, N'显示列标题', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'487F7E1E-E5F6-4EBB-A4EC-25BEE882B11E', N'SysTableConfig', N'ColRelation', N'查询关系符', N'查询关系符', 120, 2, 0, 13, NULL, N'0', NULL, 1, NULL, N'', 1, N'dataurl_colrelation', N'combobox', 1, 0, 11, NULL, N'支持的查询关系符，有先后顺序', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'1335ea24-e1a4-4c9b-bf9c-0e19e8647549', N'SysUser', N'OldUserPwd', N'OldUserPwd', N'OldUserPwd', 100, 1, 2, 8, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 100, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2b6ecc3a-d844-4b30-a70a-4f3b54987755', N'SysUser', N'SuName', N'账号', N'账号', 180, 1, 0, 0, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 2, 0, 0, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2e89474d-b82e-4eb3-9dd9-b27de8d34796', N'SysUser', N'SuGroup', N'分组', N'分组', 100, 1, 2, 7, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 9, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'37eb99c4-2e6f-4264-ac4a-04099c6066c0', N'SysUser', N'SuId', N'SuId', N'SuId', 100, 1, 2, 0, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'76a03893-47c2-49f7-aa7b-a5ab44e9b0f4', N'SysUser', N'SuSign', N'登录标识', N'登录标识', 100, 1, 2, 6, NULL, N'0', NULL, -1, 0, N'', 1, NULL, N'text', 1, 2, 8, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd52a415c-11f2-4dc0-934f-8512252b3347', N'SysUser', N'SuNickname', N'姓名', N'姓名', 180, 1, 0, 1, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 2, 0, 1, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'f66dbefe-b3e7-4de7-9871-1b635aee0590', N'SysUser', N'SrId', N'角色', N'角色', 180, 1, 0, 2, NULL, N'col_custom_', NULL, 1, 1, N'Equal', 1, N'/common/queryrole', N'combobox', 2, 0, 3, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'b0221d71-dc48-4ae0-adb9-24f32333b8d1', N'SysUser', N'SuPwd', N'密码', N'密码', 100, 1, 1, 4, NULL, N'0', NULL, NULL, 0, N'', 1, NULL, N'password', 2, 0, 2, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'4a982e49-18a5-4fe7-801b-2e0aff3cb56b', N'SysUser', N'SuStatus', N'状态', N'状态', 80, 2, 0, 5, NULL, N'17', NULL, 1, 1, N'Equal', 1, NULL, N'checkbox', 2, 0, 4, NULL, NULL, NULL, N'启用账号', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'058d78a0-d244-45bc-a8be-fbc31efee9cf', N'SysUser', N'SuCreateTime', N'创建时间', N'创建时间', 180, 2, 0, 3, NULL, N'0', NULL, 1, 1, N'GreaterThanOrEqual,LessThanOrEqual,BetweenAnd', 1, NULL, N'date', 1, 1, 6, NULL, NULL, NULL, NULL, 23)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'068cb3ea-c0a3-4839-8bd7-4deb35fa6ddb', N'TempExample', N'FormPlaceholder', N'人员姓名', N'人员姓名', 130, 1, 0, 10, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 1, N'/setting/sysuser', N'modal', 1, 0, 10, 1, NULL, NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'0cf595e4-d38d-4670-8d8c-24bc8e7a94b3', N'TempExample', N'ColQuery', N'1查询', N'1查询', 130, 1, 0, 19, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'checkbox', 1, 0, 19, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'112d21f8-102c-4c26-9776-1a5401d079d8', N'TempExample', N'ColField', N'必填项', N'必填项', 130, 1, 0, 13, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'text', 1, 0, 13, 1, N'请输入内容，必填', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'16f668be-74c4-4686-b4f0-fcb7d2fac43c', N'TempExample', N'FormType', N'下拉树', N'下拉树', 130, 1, 0, 0, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'/common/querymenu', N'combotree', 1, 0, 0, 0, N'', N'F8C1C161-F1FC-4729-A00C-4A9893BF8209', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'19c81b1a-3355-4a2e-b555-92b8a1928235', N'TempExample', N'FormText', N'显示文本', N'显示文本', 130, 1, 0, 22, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'text', 1, 0, 22, 0, N'', N'', N'', -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'1f35bdf5-1c06-4271-b2b8-cfbf6673718c', N'TempExample', N'TableName', N'文本域', N'文本域', 130, 1, 0, 16, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'textarea', 2, 0, 16, 1, N'请输入表名', NULL, NULL, 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'225547f4-b9ff-4d08-81fe-a8d4ce1f8616', N'TempExample', N'ColOrder', N'密码框', N'密码框', 130, 1, 0, 14, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'password', 1, 0, 14, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2681839b-59b0-4c6c-87a4-281ecb2c0b0c', N'TempExample', N'FormArea', N'默认多选', N'默认多选', 130, 1, 0, 5, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'dataurl_formarea', N'combobox', 1, 0, 5, 0, N'', N'1,2', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'2b69d797-f16d-426e-a027-e54252762278', N'TempExample', N'FormValue', N'选择角色', N'选择角色', 130, 1, 0, 12, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'/setting/sysrole', N'modal', 1, 0, 12, 1, N'', N'', N'', -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5357bb9f-3e6c-4d92-a881-1b578db0fb53', N'TempExample', N'DvTitle', N'时分秒', N'时分秒', 130, 1, 0, 9, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'time', 1, 0, 9, 0, N'', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'57d8737e-7882-4f8a-b2ec-3c81aae78ce5', N'TempExample', N'ColFormat', N'combobox', N'combobox', 130, 1, 0, 4, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'dataurl_colformat', N'combobox', 1, 0, 4, 0, N'', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'583d5a87-334c-4959-ae51-471f4347f639', N'TempExample', N'ColSort', N'默认勾选', N'默认勾选', 130, 1, 0, 3, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'checkbox', 1, 0, 3, 0, N'', N'1', N'默认选中', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5b956efd-d0f7-4cc4-a183-0b9b862164be', N'TempExample', N'ColWidth', N'人员账号', N'人员账号', 130, 1, 0, 11, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 1, N'/setting/sysuser', N'modal', 1, 0, 11, NULL, NULL, NULL, NULL, 10)
;
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6c95b7b9-cbfa-415f-8f89-6461b2f62f95', N'TempExample', N'ColAlign', N'默认值', N'默认值', 130, 1, 0, 6, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'dataurl_colalign', N'combobox', 1, 0, 6, 0, N'', N'2', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6cf8f27a-0af1-4cda-a220-f0aa7f30dd71', N'TempExample', N'ColExport', N'1导出', N'1导出', 130, 1, 0, 17, 0, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'checkbox', 1, 0, 17, NULL, NULL, NULL, N'导出', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'6f7a8a99-0c22-499f-a838-f6960a2194a6', N'TempExample', N'FormRequired', N'1必填', N'1必填', 130, 1, 0, 15, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'checkbox', 1, 0, 15, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'868a1e6c-e8e1-44ec-b792-ccacad1d78e4', N'TempExample', N'Id', N'Id', N'Id', 130, 1, 1, 1, NULL, N'0', NULL, 0, 0, N'', 1, NULL, N'text', 1, 1, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'871c1787-c5b1-48d3-89df-a4871a9f4c7e', N'TempExample', N'ColHide', N'勾选1或0', N'勾选1或0', 130, 1, 0, 2, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'checkbox', 1, 0, 2, 0, N'', N'', N'勾选得到1不勾选为空', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'8c3de45b-42ad-4336-9201-38261cc8be81', N'TempExample', N'FormOrder', N'排序', N'排序', 130, 1, 0, 21, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'text', 1, 0, 21, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'8ce5bbe0-730b-4115-a702-7fa6110dcc13', N'TempExample', N'FormSpan', N'combotree', N'combotree', 130, 1, 0, 1, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'/common/querymenu?custom=m', N'combotree', 1, 0, 1, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'b21e1cb6-1d06-4b65-9ef2-0e0d8ad6b1b5', N'TempExample', N'FormUrl', N'年月日时分秒', N'年月日时分秒', 130, 1, 0, 7, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'datetime', 1, 0, 7, 0, N'', N'', N'', -1)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'c1670769-2d9e-4497-9eea-c158c45f39c7', N'TempExample', N'FormHide', N'1隐藏', N'1隐藏', 130, 1, 0, 18, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'checkbox', 1, 0, 18, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'd832f9ed-594c-4b45-9fad-9e957c6ed778', N'TempExample', N'ColFrozen', N'1冻结', N'1冻结', 130, 1, 0, 20, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'text', 1, 0, 20, 0, N'', N'', N'', 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'e9d16c7e-94c9-4e2d-9496-bc9bf77c264f', N'TempExample', N'ColTitle', N'年月日', N'年月日', 130, 1, 0, 8, 0, N'0', 0, 1, 1, N'Contains,Equal', 1, N'', N'date', 1, 0, 8, 0, N'', N'', N'', 200)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'0055e38d-951a-4d9f-8028-375373455fe1', N'TempInvoiceDetail', N'TidId', N'TidId', N'TidId', 120, 1, 2, 1, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 1, 1, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'919f96ef-e9e2-4c81-8e28-f6d7e6c0b997', N'TempInvoiceDetail', N'TimId', N'单据主表ID', N'单据主表ID', 120, 1, 2, 2, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 2, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'3435753f-219e-4f48-b17e-c7f4872eafcd', N'TempInvoiceDetail', N'TimNo', N'单据号', N'单据号', 120, 1, 2, 3, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 3, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'75cf04e6-b63b-40ef-a39f-c00bdd87f973', N'TempInvoiceDetail', N'TidOrder', N'排序', N'排序', 120, 1, 2, 4, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 4, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'dfbff4f1-e20a-4f9f-9ca1-cd0e17e894a0', N'TempInvoiceDetail', N'GoodsCode', N'商品编码', N'商品编码', 120, 1, 0, 6, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 9, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'879fee52-f775-4999-b484-8217f6d052d9', N'TempInvoiceDetail', N'GoodsType', N'商品类别', N'商品类别', 120, 1, 0, 7, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 10, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'30d0531d-2e82-495f-ac7d-cdd5227a3e9e', N'TempInvoiceDetail', N'GoodsModel', N'规格型号', N'规格型号', 120, 1, 0, 8, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 11, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'95145dae-d6a6-4186-a5db-ac4fa75897c2', N'TempInvoiceDetail', N'GoodsCount', N'商品数量', N'商品数量', 120, 1, 0, 9, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 6, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'aa43839c-52dd-42a0-a93c-140e2f0fbf24', N'TempInvoiceDetail', N'GoodsCost', N'商品成本', N'商品成本', 120, 1, 0, 10, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 7, NULL, NULL, NULL, NULL, 8)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ce9f5c59-ef13-42d4-abf3-0b76513c5fe1', N'TempInvoiceDetail', N'GoodsPrice', N'商品售价', N'商品售价', 120, 1, 0, 11, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 8, NULL, NULL, NULL, NULL, 8)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'e9b2ea7a-57fd-41ed-b5d0-f145c426de9c', N'TempInvoiceDetail', N'GoodsId', N'商品ID', N'商品名称', 150, 1, 0, 5, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 0, 5, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'4c482a0b-e9a2-47e1-9680-de266b632fd4', N'TempInvoiceMain', N'TimId', N'TimId', N'TimId', 120, 1, 1, 1, 0, N'0', 0, 1, 0, N'', 1, N'', N'text', 1, 1, 1, 0, N'', N'', N'', 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'69b34a24-10ff-4cfa-839b-f821fd447f35', N'TempInvoiceMain', N'Spare1', N'备用', N'备用', 120, 1, 0, 13, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 2, 13, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ef879206-94c7-49b0-8aa1-aefcdb9d3c9f', N'TempInvoiceMain', N'Spare2', N'备用', N'备用', 120, 1, 0, 14, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 2, 14, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5a2480db-f828-4b62-8885-39c673b1e02a', N'TempInvoiceMain', N'Spare3', N'备用', N'备用', 120, 1, 0, 15, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 2, 15, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'a9866d1b-5193-44ae-ab33-8bf461dbabbe', N'TempInvoiceMain', N'TimNo', N'单据号', N'单据号', 120, 1, 0, 2, NULL, N'0', NULL, 1, 1, N'Contains,Equal', 1, NULL, N'text', 3, 0, 2, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'698d6423-3ee2-4a8c-a163-37973d273f42', N'TempInvoiceMain', N'TimStore', N'门店', N'门店', 120, 1, 0, 4, NULL, N'0', NULL, 1, 1, N'Equal', 1, N'/Common/QueryMenu', N'combotree', 4, 0, 5, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ff944de4-0177-4cf5-b373-e28f536fad39', N'TempInvoiceMain', N'TimType', N'采购类型', N'采购类型', 120, 1, 0, 5, NULL, N'0', NULL, 1, 1, N'Equal', 1, N'dataurl_timtype', N'combobox', 3, 0, 4, 1, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'3b1482a9-2096-4c76-99c8-cce89a41bf1c', N'TempInvoiceMain', N'TimSupplier', N'供应商', N'供应商', 120, 1, 0, 6, NULL, N'0', NULL, 1, 1, N'Equal', 1, N'dataurl_timsupplier', N'combobox', 3, 0, 6, 1, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'83fa6614-19c5-4935-9b5d-177e33fd88f5', N'TempInvoiceMain', N'TimDate', N'单据日期', N'单据日期', 120, 1, 0, 3, NULL, N'0', NULL, 1, 1, N'GreaterThanOrEqual,LessThanOrEqual,BetweenAnd', 1, NULL, N'date', 3, 0, 3, 1, N'输入日期', NULL, NULL, 23)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'b502a460-c575-4f76-8514-f041b66f795f', N'TempInvoiceMain', N'TimStatus', N'状态，1默认，2已审核，3未通过，4作废', N'状态，1默认，2已审核，3未通过，4作废', 120, 1, 0, 12, NULL, N'0', NULL, 1, NULL, N'', 1, NULL, N'text', 1, 2, 12, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'051fe1e9-2f01-444f-a08c-2504c538fc71', N'TempInvoiceMain', N'TimOwnerId', N'制单人', N'制单人', 120, 1, 0, 9, NULL, N'0', NULL, 1, NULL, N'', 2, NULL, N'text', 3, 2, 9, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'44cd81d4-4fc9-4964-92c5-ea509fbcee34', N'TempInvoiceMain', N'TimUser', N'采购员', N'采购员', 120, 1, 0, 7, NULL, N'0', NULL, 1, NULL, N'', 2, NULL, N'text', 3, 0, 8, NULL, NULL, NULL, NULL, 50)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'e4bf6fbb-86ab-44d5-986b-8e0c67388c58', N'TempInvoiceMain', N'TimCreateTime', N'创建时间', N'创建时间', 120, 1, 0, 11, NULL, N'0', NULL, 1, NULL, N'', 2, NULL, N'text', 3, 0, 11, NULL, N'默认系统时间', NULL, NULL, 23)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'5f3c858e-00bb-4235-b3de-413938169347', N'TempInvoiceMain', N'TimOwnerName', N'制单人', N'制单人', 120, 1, 0, 10, NULL, N'0', NULL, 1, NULL, N'', 2, NULL, N'text', 3, 0, 10, NULL, N'默认登录用户', NULL, NULL, 20)
INSERT [dbo].[SysTableConfig] ([Id], [TableName], [ColField], [DvTitle], [ColTitle], [ColWidth], [ColAlign], [ColHide], [ColOrder], [ColFrozen], [ColFormat], [ColSort], [ColExport], [ColQuery], [ColRelation], [FormArea], [FormUrl], [FormType], [FormSpan], [FormHide], [FormOrder], [FormRequired], [FormPlaceholder], [FormValue], [FormText], [FormMaxlength]) VALUES (N'ed06a3fc-a3ce-4519-8699-4441b13f2913', N'TempInvoiceMain', N'TimRemark', N'备注', N'备注', 120, 1, 0, 8, NULL, N'0', NULL, 1, NULL, N'', 2, NULL, N'text', 2, 0, 7, NULL, N'请输入订单备注', NULL, NULL, 200)
INSERT [dbo].[SysUser] ([SuId], [SrId], [SuName], [SuPwd], [SuNickname], [SuCreateTime], [SuStatus], [SuSign], [SuGroup]) VALUES (N'0ad60901-33d9-4bda-99c3-e720dd0685d7', N'58307c67-76b8-4156-bde3-f307f4da25e9', N'test', N'098f6bcd4621d373cade4e832627b4f6', N'test', CAST(N'2018-04-21T19:49:51.000' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SysUser] ([SuId], [SrId], [SuName], [SuPwd], [SuNickname], [SuCreateTime], [SuStatus], [SuSign], [SuGroup]) VALUES (N'F9A19BAB-49C3-4131-AEFC-FB80FAAE579A', N'E663CE67-E9CA-4441-AB77-DC267C22C683', N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'管理员', CAST(N'2018-02-14T09:33:00.000' AS DateTime), 1, NULL, 1)
INSERT [dbo].[TempInvoiceDetail] ([TidId], [TimId], [TimNo], [TidOrder], [GoodsId], [GoodsCount], [GoodsCost], [GoodsPrice], [Spare1], [Spare2], [Spare3]) VALUES (N'897dfbb3-c975-4e93-ab0b-d302769b0509', N'3b750ae8-0b7b-47f3-84ea-4eea75a3939c', NULL, NULL, N'明细3', 3, CAST(4.00 AS Decimal(8, 2)), CAST(5.00 AS Decimal(8, 2)), NULL, NULL, NULL)
INSERT [dbo].[TempInvoiceDetail] ([TidId], [TimId], [TimNo], [TidOrder], [GoodsId], [GoodsCount], [GoodsCost], [GoodsPrice], [Spare1], [Spare2], [Spare3]) VALUES (N'e78bee72-94d1-4ff8-a450-f609890ccb84', N'3b750ae8-0b7b-47f3-84ea-4eea75a3939c', NULL, NULL, N'明细2', 1, CAST(2.00 AS Decimal(8, 2)), CAST(3.00 AS Decimal(8, 2)), NULL, NULL, NULL)
INSERT [dbo].[TempInvoiceDetail] ([TidId], [TimId], [TimNo], [TidOrder], [GoodsId], [GoodsCount], [GoodsCost], [GoodsPrice], [Spare1], [Spare2], [Spare3]) VALUES (N'ee0d3d87-12d8-4f36-ac92-c14d60083328', N'3b750ae8-0b7b-47f3-84ea-4eea75a3939c', NULL, NULL, N'商品名称（浏览商品表，带出商品属性）', 5, CAST(10.00 AS Decimal(8, 2)), CAST(12.00 AS Decimal(8, 2)), NULL, NULL, NULL)
INSERT [dbo].[TempInvoiceMain] ([TimId], [TimNo], [TimDate], [TimStore], [TimType], [TimSupplier], [TimUser], [TimRemark], [TimOwnerId], [TimOwnerName], [TimCreateTime], [TimStatus], [Spare1], [Spare2], [Spare3]) VALUES (N'3b750ae8-0b7b-47f3-84ea-4eea75a3939c', N'1001', CAST(N'2019-08-29T00:00:00.000' AS DateTime), N'813984B9-06CA-4D85-AD82-3C4AD2CB834E', 1, N'2', N'netnr', N'测试信息，单据明细表，浏览模态框、下拉列表等输入模式支持需逐步完善', NULL, NULL, CAST(N'2019-08-29T15:54:14.000' AS DateTime), NULL, NULL, NULL, NULL)
SET ANSI_PADDING ON
;
/****** Object:  Index [SysDictionary_SdId_PK]    Script Date: 2019-9-4 11:38:22 ******/
ALTER TABLE [dbo].[SysDictionary] ADD  CONSTRAINT [SysDictionary_SdId_PK] PRIMARY KEY NONCLUSTERED 
(
	[SdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
SET ANSI_PADDING ON
;
/****** Object:  Index [SysTableConfig_Id_PK]    Script Date: 2019-9-4 11:38:22 ******/
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [SysTableConfig_Id_PK] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
;
ALTER TABLE [dbo].[SysRole] ADD  CONSTRAINT [DF_SysRole_Status]  DEFAULT ((0)) FOR [SrStatus]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_ColOrder]  DEFAULT ((0)) FOR [ColOrder]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_ColFrozen]  DEFAULT ((0)) FOR [ColFrozen]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_ColSort]  DEFAULT ((0)) FOR [ColSort]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_ColExport]  DEFAULT ((0)) FOR [ColExport]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_ColQuery]  DEFAULT ((0)) FOR [ColQuery]
;
ALTER TABLE [dbo].[SysTableConfig] ADD  CONSTRAINT [DF_SysTableConfig_FormRequired]  DEFAULT ((0)) FOR [FormRequired]
;
ALTER TABLE [dbo].[SysUser] ADD  CONSTRAINT [DF_SysUser_Status]  DEFAULT ((0)) FOR [SuStatus]
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnText'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnId'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnClass'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnIcon'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，1启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbDescribe'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnGroup'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'隐藏，1隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton', @level2type=N'COLUMN',@level2name=N'SbBtnHide'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统按钮表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysButton'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdPid'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdType'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdKey'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdValue'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：1正常，-1删除，2停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdRemark'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdAttribute1'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdAttribute2'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'SdAttribute3'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统字典表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmUrl'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmIcon'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，1启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组，默认1，比如移动端为2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu', @level2type=N'COLUMN',@level2name=N'SmGroup'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统菜单表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysMenu'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，1启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrDescribe'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrGroup'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrMenus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrButtons'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole', @level2type=N'COLUMN',@level2name=N'SrCreateTime'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysRole'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'（虚）表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'TableName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColField'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认列标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'DvTitle'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColTitle'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列宽' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColWidth'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对齐方式 1左，2中，3右' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColAlign'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColHide'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1冻结' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColFrozen'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'格式化' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColFormat'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用点击排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColSort'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1导出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColExport'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1查询' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColQuery'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询关系符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'ColRelation'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormArea'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormUrl'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'输入类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormType'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跨列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormSpan'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormHide'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormRequired'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'输入框提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormPlaceholder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'初始值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormValue'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormText'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大长度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig', @level2type=N'COLUMN',@level2name=N'FormMaxlength'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysTableConfig'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SrId'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuPwd'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuNickname'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuCreateTime'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，1正常' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录标识' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuSign'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'SuGroup'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'（虚）表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'TableName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColField'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'默认列标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'DvTitle'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColTitle'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列宽' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColWidth'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对齐方式 1左，2中，3右' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColAlign'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColHide'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1冻结' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColFrozen'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'格式化' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColFormat'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用点击排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColSort'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1导出' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColExport'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1查询' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'ColQuery'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormUrl'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'输入类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormType'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区域' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormArea'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跨列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormSpan'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormHide'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1必填' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormRequired'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'输入框提示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormPlaceholder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'初始值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormValue'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample', @level2type=N'COLUMN',@level2name=N'FormText'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'示例表，请删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempExample'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据主表ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'TimId'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'TimNo'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'TidOrder'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'GoodsId'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'GoodsCount'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品成本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'GoodsCost'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商品售价' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'GoodsPrice'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'Spare1'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'Spare2'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail', @level2type=N'COLUMN',@level2name=N'Spare3'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据明细' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceDetail'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimNo'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimDate'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'门店' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimStore'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimType'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimSupplier'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimUser'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimRemark'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimOwnerId'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'制单人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimOwnerName'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimCreateTime'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态，1默认，2已审核，3未通过，4作废' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'TimStatus'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'Spare1'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'Spare2'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain', @level2type=N'COLUMN',@level2name=N'Spare3'
;
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据主表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TempInvoiceMain'
;