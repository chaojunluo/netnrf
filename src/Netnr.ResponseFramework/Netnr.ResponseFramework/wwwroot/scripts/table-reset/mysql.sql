/*
 Navicat Premium Data Transfer

 Source Server Type    : MySQL
 Source Server Version : 50723
 Source Schema         : netnrf

 Target Server Type    : MySQL
 Target Server Version : 50723
 File Encoding         : 65001

 Date: 07/01/2019 17:01:34
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for SysAuthorize
-- ----------------------------
DROP TABLE IF EXISTS `SysAuthorize`;
CREATE TABLE `SysAuthorize`  (
  `SaId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SuId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '关联SysUser表ID',
  `OpenId1` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId2` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId4` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId3` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId5` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId6` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId7` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId8` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  `OpenId9` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '第三方授权标识',
  PRIMARY KEY (`SaId`) USING BTREE,
  INDEX `SysAuthorize_SuId_PK`(`SuId`) USING BTREE,
  INDEX `SysAuthorize_SaId`(`SaId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysAuthorize
-- ----------------------------
INSERT INTO `SysAuthorize` VALUES ('1750309e-ae6e-4337-b508-bedd274c2cae', 'F9A19BAB-49C3-4131-AEFC-FB80FAAE579A', NULL, NULL, NULL, '', NULL, NULL, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for SysButton
-- ----------------------------
DROP TABLE IF EXISTS `SysButton`;
CREATE TABLE `SysButton`  (
  `SbId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SbPid` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `SbBtnText` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '按钮文本',
  `SbBtnId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '按钮ID',
  `SbBtnClass` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '按钮类',
  `SbBtnIcon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '按钮图标',
  `SbBtnOrder` int(11) DEFAULT 0 COMMENT '排序',
  `SbStatus` int(11) DEFAULT 0 COMMENT '状态，1启用',
  `SbDescribe` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '描述',
  `SbBtnGroup` int(11) DEFAULT 0 COMMENT '分组',
  PRIMARY KEY (`SbId`) USING BTREE,
  INDEX `SysButton_SbId_PK`(`SbId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysButton
-- ----------------------------
INSERT INTO `SysButton` VALUES ('26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '00000000-0000-0000-0000-000000000000', '批处理', 'list_Batch', 'btn btn-sm btn-danger', 'fa fa-paint-brush', 40, 1, '', 1);
INSERT INTO `SysButton` VALUES ('3636B071-CE52-4551-BA67-4F982D14CD7C', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '导入', 'm_Import', '', 'fa fa-level-down blue', 82, 1, '', 5);
INSERT INTO `SysButton` VALUES ('3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131', '00000000-0000-0000-0000-000000000000', '删除', 'm_Del', 'btn btn-sm  btn-danger', 'fa fa-remove', 4, 1, NULL, 1);
INSERT INTO `SysButton` VALUES ('3C6F626F-8D8E-46EE-B02A-0C90CFA90E02', '00000000-0000-0000-0000-000000000000', '启用', 'm_Start', 'btn btn-sm  btn-success', 'fa fa-play', 9, 1, '', 1);
INSERT INTO `SysButton` VALUES ('4674735D-B762-4876-8DE1-31AD7CD023F4', '00000000-0000-0000-0000-000000000000', '作废', 'm_Void', 'btn btn-sm  btn-default', 'fa fa-trash', 12, 1, '', 1);
INSERT INTO `SysButton` VALUES ('4FC96135-26B5-46D7-B809-747AD71F2A21', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '表单配置', 'list_Config_Form', '', 'fa fa-table orange', 92, 1, '', 4);
INSERT INTO `SysButton` VALUES ('58F7DA5E-37F8-4648-80F3-998E702A4D91', '26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '批量启用', 'list_Batch_Start', '', 'fa fa-play green', 41, 1, '', 2);
INSERT INTO `SysButton` VALUES ('5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E', '00000000-0000-0000-0000-000000000000', '查看', 'm_See', 'btn btn-sm  btn-primary', 'fa fa-credit-card', 5, 1, NULL, 1);
INSERT INTO `SysButton` VALUES ('609287B6-4B59-4E59-A822-B8C1087BB603', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '导出', 'm_Export', '', 'fa fa-file-excel-o green', 85, 1, '', 3);
INSERT INTO `SysButton` VALUES ('73CF6246-4429-4EF2-A0DA-F86F96B9BEBB', '26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '批量停用', 'list_Batch_Stop', '', 'fa fa-stop red', 42, 1, '', 2);
INSERT INTO `SysButton` VALUES ('807FF920-37AA-40F7-92BC-3FC894D4D7A3', '26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '批量删除', 'list_Batch_Del', '', 'fa fa-remove red', 45, 1, '', 3);
INSERT INTO `SysButton` VALUES ('85C51241-19D1-4BD0-A35B-DB570ACD0E85', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '打印', 'm_Print', '', 'fa fa-print green', 86, 1, '', 3);
INSERT INTO `SysButton` VALUES ('90ED8666-0961-426D-B582-E08C43EEE9E1', '00000000-0000-0000-0000-000000000000', '增加', 'm_Add', 'btn btn-sm btn-primary', 'fa fa-plus', 2, 1, NULL, 1);
INSERT INTO `SysButton` VALUES ('936D642A-CD7B-4A0E-837F-B4630A1BE894', '00000000-0000-0000-0000-000000000000', '参数设置', 'm_ParaSet', 'btn btn-sm  btn-success', 'fa fa-gear', 15, 1, '', 1);
INSERT INTO `SysButton` VALUES ('99A7F6EB-69BD-4167-B647-B10D3E12A3F3', '00000000-0000-0000-0000-000000000000', '修改', 'm_Edit', 'btn btn-sm btn-warning', 'fa fa-edit', 3, 1, NULL, 1);
INSERT INTO `SysButton` VALUES ('9B2265A4-A01F-48E8-9373-A6A294FCC1B7', '26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '关闭批处理', 'list_Batch_Close', '', 'fa fa-mail-reply orange', 47, 1, '', 4);
INSERT INTO `SysButton` VALUES ('9BD9FE69-430B-4754-BF00-1DE1D117C384', '00000000-0000-0000-0000-000000000000', '停用', 'm_Stop', 'btn btn-sm  btn-warning', 'fa fa-stop', 10, 1, '', 1);
INSERT INTO `SysButton` VALUES ('9F128382-9A3E-42FB-89E7-D12E5D581584', '00000000-0000-0000-0000-000000000000', '弃废', 'm_UnVoid', 'btn btn-sm  btn-danger', 'fa fa-reply', 13, 1, '', 1);
INSERT INTO `SysButton` VALUES ('A04A57A2-014C-4D47-A6A2-B5018ED286CB', '00000000-0000-0000-0000-000000000000', '刷新', 'm_Reload', 'btn btn-sm  btn-info', 'fa fa-refresh', 39, 1, '', 1);
INSERT INTO `SysButton` VALUES ('ACD7FC7F-DE75-4502-B619-CF6BDA16CB9A', '00000000-0000-0000-0000-000000000000', '权限控制', 'm_Auth', 'btn btn-sm  btn-success', 'fa fa-gear', 14, 1, NULL, 1);
INSERT INTO `SysButton` VALUES ('AE0D0298-FE28-405F-82AB-00E310FFE9C2', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '表格配置', 'list_Config_Table', '', 'fa fa-table orange', 91, 1, '', 4);
INSERT INTO `SysButton` VALUES ('B97248C7-F53A-4289-BF06-A05E8009199B', '00000000-0000-0000-0000-000000000000', '切换', 'm_Switch', 'btn btn-sm  btn-primary', 'fa fa-exchange', 20, 1, '', 1);
INSERT INTO `SysButton` VALUES ('C42B2ECC-3A18-495B-9BC6-B315FEA5A951', '26008EAA-4ED7-46E0-8BBF-DCF1472397E0', '批量修改', 'list_Batch_Edit', '', 'fa fa-edit orange', 44, 1, '', 3);
INSERT INTO `SysButton` VALUES ('D2B8534F-D435-4E39-AC9D-4294AFC492DB', '00000000-0000-0000-0000-000000000000', '更多', 'list_More', 'btn btn-sm  btn-primary', 'fa fa-ellipsis-h', 80, 1, '', 1);
INSERT INTO `SysButton` VALUES ('D3A31A0C-C842-4709-82DD-A33B0253A462', '00000000-0000-0000-0000-000000000000', '保存', 'm_Save', 'btn btn-sm  btn-success', 'fa fa-save', 6, 1, '', 1);
INSERT INTO `SysButton` VALUES ('EAED8E4A-E6DA-4075-883F-8B5559B7A9AD', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '上传附件', 'm_Upload', NULL, 'fa fa-cloud-upload blue', 83, 1, NULL, 5);
INSERT INTO `SysButton` VALUES ('ED6830FD-DFD5-4B48-A155-76C8D7D6FEA4', '00000000-0000-0000-0000-000000000000', '取消', 'm_Cancel', 'btn btn-sm  btn-danger', 'fa fa-reply', 7, 1, '', 1);
INSERT INTO `SysButton` VALUES ('EFE021E2-30FE-4500-9BF6-52611F1AAA4A', '00000000-0000-0000-0000-000000000000', '查询', 'm_Query', 'btn btn-sm  btn-success', 'fa fa-search', 1, 1, '', 1);
INSERT INTO `SysButton` VALUES ('F60C1C50-EBDC-430A-BE3A-30C4AB23C3FD', 'D2B8534F-D435-4E39-AC9D-4294AFC492DB', '复制并新增', 'm_Copy', '', 'fa fa-copy blue', 81, 1, '', 2);
INSERT INTO `SysButton` VALUES ('FA51A36A-69DD-4838-AD03-EFA8F038F23F', '00000000-0000-0000-0000-000000000000', '审核', 'm_Check', 'btn btn-sm  btn-info', 'fa fa-check-square-o', 11, 1, '', 1);

-- ----------------------------
-- Table structure for SysMenu
-- ----------------------------
DROP TABLE IF EXISTS `SysMenu`;
CREATE TABLE `SysMenu`  (
  `SmId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SmPid` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `SmName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '名称',
  `SmUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '链接',
  `SmOrder` int(11) DEFAULT 0 COMMENT '排序',
  `SmIcon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '图标',
  `SmStatus` int(11) DEFAULT 0 COMMENT '状态，1启用',
  `SmGroup` int(11) DEFAULT 0 COMMENT '分组，默认1，比如移动端为2',
  PRIMARY KEY (`SmId`) USING BTREE,
  INDEX `SysMenu_SmId_PK`(`SmId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysMenu
-- ----------------------------
INSERT INTO `SysMenu` VALUES ('1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '00000000-0000-0000-0000-000000000000', '系统设置', NULL, 9, 'fa-cog', 1, 1);
INSERT INTO `SysMenu` VALUES ('2AE7FAF0-B627-4012-8A94-C5337579C8F5', '00000000-0000-0000-0000-000000000000', 'RF框架', '', 8, 'fa-tag', 1, 1);
INSERT INTO `SysMenu` VALUES ('56ad6bac-d773-4460-b88f-164c08df4808', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '按钮管理', '/setting/sysbutton', 1, 'fa-save', 1, 1);
INSERT INTO `SysMenu` VALUES ('601C6500-808A-426B-9658-6BA830396AE3', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '角色管理', '/setting/sysrole', 3, 'fa-mortar-board (alias)', 1, 1);
INSERT INTO `SysMenu` VALUES ('60C478C8-B4B7-471F-AE7F-62DF7A6C44D4', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '操作日志', '/setting/syslog', 6, 'fa-file-text-o', 1, 1);
INSERT INTO `SysMenu` VALUES ('688BE98C-3D78-4B4D-A160-91476407599F', '2AE7FAF0-B627-4012-8A94-C5337579C8F5', '表格示例', NULL, 3, 'fa-flag', 1, 1);
INSERT INTO `SysMenu` VALUES ('6C9E2090-B115-4D3E-948B-E5829A1886CF', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '表配置', '/setting/systableconfig', 7, 'fa-cog', 1, 1);
INSERT INTO `SysMenu` VALUES ('8120ACDF-0642-4EA0-8BEC-83306D744319', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '用户管理', '/setting/sysuser', 4, 'fa-user', 1, 1);
INSERT INTO `SysMenu` VALUES ('813984B9-06CA-4D85-AD82-3C4AD2CB834E', 'CAAFB396-C5F2-406A-9808-6B089E20F265', '表管理', '/tool/tablemanage', 1, 'fa-database', 1, 1);
INSERT INTO `SysMenu` VALUES ('89EB0D3E-5BAA-494E-AD49-7FE247405CDA', '688BE98C-3D78-4B4D-A160-91476407599F', 'TreeGrid', '/rf/treegrid', 2, 'fa-flag', 1, 1);
INSERT INTO `SysMenu` VALUES ('A40C1D01-C682-483F-AF87-CD843AA457C7', '2AE7FAF0-B627-4012-8A94-C5337579C8F5', '表配置示例', '/rf/tce', 2, 'fa-flag', 1, 1);
INSERT INTO `SysMenu` VALUES ('a5fc0578-72c4-4be1-9ad8-71ef0cc9f746', '1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC', '菜单管理', '/setting/sysmenu', 2, 'fa-navicon', 1, 1);
INSERT INTO `SysMenu` VALUES ('B27D2434-DC15-4EFA-A586-E11DF23D5344', '2AE7FAF0-B627-4012-8A94-C5337579C8F5', '静态表单示例', '/rf/form', 2, 'fa-flag', 1, 1);
INSERT INTO `SysMenu` VALUES ('CAAFB396-C5F2-406A-9808-6B089E20F265', '00000000-0000-0000-0000-000000000000', '工具箱', NULL, 8, 'fa-wrench', 1, 1);
INSERT INTO `SysMenu` VALUES ('DF9CFA05-847A-43B6-8119-E8FC7AE04734', '688BE98C-3D78-4B4D-A160-91476407599F', 'DataGrid', '/rf/datagrid', 1, 'fa-flag', 1, 1);
INSERT INTO `SysMenu` VALUES ('F8C1C161-F1FC-4729-A00C-4A9893BF8209', '00000000-0000-0000-0000-000000000000', '系统桌面', '/home/desk', 1, 'fa-home', 1, 1);

-- ----------------------------
-- Table structure for SysRole
-- ----------------------------
DROP TABLE IF EXISTS `SysRole`;
CREATE TABLE `SysRole`  (
  `SrId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SrName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '名称',
  `SrStatus` int(11) DEFAULT 0 COMMENT '状态，1启用',
  `SrDescribe` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '描述',
  `SrGroup` int(11) DEFAULT 0 COMMENT '分组',
  `SrMenus` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '菜单',
  `SrButtons` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '按钮',
  `SrCreateTime` datetime(3) DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`SrId`) USING BTREE,
  INDEX `SysRole_SrId_PK`(`SrId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysRole
-- ----------------------------
INSERT INTO `SysRole` VALUES ('58307c67-76b8-4156-bde3-f307f4da25e9', '测试', 1, NULL, NULL, 'F8C1C161-F1FC-4729-A00C-4A9893BF8209,2AE7FAF0-B627-4012-8A94-C5337579C8F5,A40C1D01-C682-483F-AF87-CD843AA457C7,B27D2434-DC15-4EFA-A586-E11DF23D5344,688BE98C-3D78-4B4D-A160-91476407599F,DF9CFA05-847A-43B6-8119-E8FC7AE04734,89EB0D3E-5BAA-494E-AD49-7FE247405CDA', '{\"F8C1C161-F1FC-4729-A00C-4A9893BF8209\":\"\",\"A40C1D01-C682-483F-AF87-CD843AA457C7\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1\"}', '2018-05-20 10:23:19.000');
INSERT INTO `SysRole` VALUES ('E663CE67-E9CA-4441-AB77-DC267C22C683', '管理员', 1, NULL, 1, 'F8C1C161-F1FC-4729-A00C-4A9893BF8209,2AE7FAF0-B627-4012-8A94-C5337579C8F5,A40C1D01-C682-483F-AF87-CD843AA457C7,B27D2434-DC15-4EFA-A586-E11DF23D5344,688BE98C-3D78-4B4D-A160-91476407599F,DF9CFA05-847A-43B6-8119-E8FC7AE04734,89EB0D3E-5BAA-494E-AD49-7FE247405CDA,CAAFB396-C5F2-406A-9808-6B089E20F265,813984B9-06CA-4D85-AD82-3C4AD2CB834E,1DAB94C9-EF51-4BA2-A604-7DC7B78D56BC,56ad6bac-d773-4460-b88f-164c08df4808,a5fc0578-72c4-4be1-9ad8-71ef0cc9f746,601C6500-808A-426B-9658-6BA830396AE3,8120ACDF-0642-4EA0-8BEC-83306D744319,60C478C8-B4B7-471F-AE7F-62DF7A6C44D4,6C9E2090-B115-4D3E-948B-E5829A1886CF', '{\"601C6500-808A-426B-9658-6BA830396AE3\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,ACD7FC7F-DE75-4502-B619-CF6BDA16CB9A,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"60C478C8-B4B7-471F-AE7F-62DF7A6C44D4\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"6C9E2090-B115-4D3E-948B-E5829A1886CF\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"8120ACDF-0642-4EA0-8BEC-83306D744319\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"F8C1C161-F1FC-4729-A00C-4A9893BF8209\":\"\",\"9C145834-8336-4E63-A34C-6DF8E5854C96\":\"\",\"B27D2434-DC15-4EFA-A586-E11DF23D5344\":\"90ED8666-0961-426D-B582-E08C43EEE9E1,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,D3A31A0C-C842-4709-82DD-A33B0253A462,A04A57A2-014C-4D47-A6A2-B5018ED286CB\",\"1CDBF142-7F5C-42E8-A426-370BF4542224\":\"A04A57A2-014C-4D47-A6A2-B5018ED286CB\",\"A40C1D01-C682-483F-AF87-CD843AA457C7\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,D3A31A0C-C842-4709-82DD-A33B0253A462,A04A57A2-014C-4D47-A6A2-B5018ED286CB,26008EAA-4ED7-46E0-8BBF-DCF1472397E0,58F7DA5E-37F8-4648-80F3-998E702A4D91,73CF6246-4429-4EF2-A0DA-F86F96B9BEBB,C42B2ECC-3A18-495B-9BC6-B315FEA5A951,807FF920-37AA-40F7-92BC-3FC894D4D7A3,9B2265A4-A01F-48E8-9373-A6A294FCC1B7,D2B8534F-D435-4E39-AC9D-4294AFC492DB,F60C1C50-EBDC-430A-BE3A-30C4AB23C3FD,3636B071-CE52-4551-BA67-4F982D14CD7C,EAED8E4A-E6DA-4075-883F-8B5559B7A9AD,609287B6-4B59-4E59-A822-B8C1087BB603,85C51241-19D1-4BD0-A35B-DB570ACD0E85,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21\",\"DF9CFA05-847A-43B6-8119-E8FC7AE04734\":\"A04A57A2-014C-4D47-A6A2-B5018ED286CB\",\"89EB0D3E-5BAA-494E-AD49-7FE247405CDA\":\"A04A57A2-014C-4D47-A6A2-B5018ED286CB\",\"F5415EAE-694F-4332-B259-E86BDC54AA09\":\"A04A57A2-014C-4D47-A6A2-B5018ED286CB\",\"56ad6bac-d773-4460-b88f-164c08df4808\":\"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"a5fc0578-72c4-4be1-9ad8-71ef0cc9f746\":\"90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\",\"faa428d6-6d57-4fe2-84cf-48ab7beaa950\":\"EFE021E2-30FE-4500-9BF6-52611F1AAA4A,90ED8666-0961-426D-B582-E08C43EEE9E1,99A7F6EB-69BD-4167-B647-B10D3E12A3F3,3B4EBAB9-F2A5-43C8-8BCB-94B9A3AA5131,5C1A8DE4-F0EC-4795-AF48-6F0C97EB940E,A04A57A2-014C-4D47-A6A2-B5018ED286CB,AE0D0298-FE28-405F-82AB-00E310FFE9C2,4FC96135-26B5-46D7-B809-747AD71F2A21,D2B8534F-D435-4E39-AC9D-4294AFC492DB\"}', '2018-05-20 07:34:18.000');

-- ----------------------------
-- Table structure for SysTableConfig
-- ----------------------------
DROP TABLE IF EXISTS `SysTableConfig`;
CREATE TABLE `SysTableConfig`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `TableName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '（虚）表名',
  `ColField` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '列键',
  `DvTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '默认列标题',
  `ColTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '列标题',
  `ColWidth` int(11) DEFAULT 0 COMMENT '列宽',
  `ColAlign` int(11) DEFAULT 0 COMMENT '对齐方式 1左，2中，3右',
  `ColHide` int(11) DEFAULT 0 COMMENT '1隐藏',
  `ColOrder` int(11) DEFAULT 0 COMMENT '排序',
  `ColFrozen` int(11) DEFAULT 0 COMMENT '1冻结',
  `ColFormat` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '格式化',
  `ColSort` int(11) DEFAULT 0 COMMENT '1启用点击排序',
  `ColExport` int(11) DEFAULT 0 COMMENT '1导出',
  `ColQuery` int(11) DEFAULT 0 COMMENT '1查询',
  `FormUrl` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '来源',
  `FormType` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '输入类型',
  `FormArea` int(11) DEFAULT 0 COMMENT '区域',
  `FormSpan` int(11) DEFAULT 0 COMMENT '跨列',
  `FormHide` int(11) DEFAULT 0 COMMENT '1隐藏',
  `FormOrder` int(11) DEFAULT 0 COMMENT '排序',
  `FormRequired` int(11) DEFAULT 0 COMMENT '1必填',
  `FormPlaceholder` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '输入框提示',
  `FormValue` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '初始值',
  `FormText` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '显示文本',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `SysTableConfig_TableName`(`TableName`) USING BTREE,
  INDEX `SysTableConfig_Id_PK`(`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysTableConfig
-- ----------------------------
INSERT INTO `SysTableConfig` VALUES ('02A215E4-D1E7-428B-B159-72439B7C3BDB', 'sysuser', 'SuGroup', '分组', '分组', 100, 1, 1, 7, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 9, NULL, '', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('079E1037-EC30-4DEB-8E4E-877FDF23562B', 'sysuser', 'SuPwd', '密码', '密码', 100, 1, 1, 4, 0, '0', NULL, 1, NULL, NULL, 'password', 1, 2, 0, 2, 1, '', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('0ACF9D2C-92B8-44DB-801B-B2E11B2B5A95', 'tempexample', 'ColAlign', '默认值', '默认值', 110, 1, 0, 6, 0, '0', 0, 1, 1, 'dataurl_colalign', 'combobox', 1, 1, 0, 6, 0, '', '2', '');
INSERT INTO `SysTableConfig` VALUES ('0DDD0335-6A80-4927-B7EC-4BC9334606B2', 'tempexample', 'ColHide', '勾选1或0', '勾选1或0', 110, 1, 0, 2, 0, '0', 0, 1, 1, '', 'checkbox', 1, 1, 0, 2, 0, '', '', '勾选得到1不勾选为空');
INSERT INTO `SysTableConfig` VALUES ('10460273-7EDE-4897-B16E-3A3038D59CD6', 'systableconfig', 'ColHide', '隐藏', '隐藏', 150, 2, 0, 6, 0, 'col_custom_', NULL, 1, NULL, 'dataurl_colhide', 'combobox', 1, 1, 0, 6, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('10817844-1899-44E7-B41B-CD9EF4BC5465', 'tempexample', 'FormRequired', '1必填', '1必填', 110, 1, 0, 15, 0, '0', 0, 1, 1, '', 'checkbox', 1, 1, 0, 15, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('15B65F50-E47B-4A3A-9042-0F3B08487EAA', 'tempexample', 'FormValue', '选择角色', '选择角色', 110, 1, 0, 12, 0, '0', 0, 1, 1, '/setting/sysrole', 'modal', 1, 1, 0, 12, 1, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('1638625D-601A-4FFD-8D7E-9904503C7543', 'tempexample', 'DvTitle', '时分秒', '时分秒', 110, 1, 0, 9, 0, '0', 0, 1, 1, '', 'time', 1, 1, 0, 9, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('17EF7B21-8332-48DE-A13D-81875A2CA85B', 'systableconfig', 'FormOrder', '排序', '排序', 80, 2, 0, 18, 0, '0', 0, 1, 0, '', 'text', 2, 1, 0, 18, 0, '', '100', '');
INSERT INTO `SysTableConfig` VALUES ('1A9A7B9A-B6E6-4869-BF64-C0F90019B1F0', 'tempexample', 'ColQuery', '1查询', '1查询', 110, 1, 0, 19, 0, '0', 0, 1, 1, '', 'checkbox', 1, 1, 0, 19, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('1B239E70-4E80-4A51-95B4-3F1A347924B2', 'systableconfig', 'FormArea', '区域', '区域', 120, 2, 0, 15, 0, 'col_custom_', NULL, 1, 1, 'dataurl_formarea', 'combobox', 2, 1, 0, 15, NULL, NULL, '1', NULL);
INSERT INTO `SysTableConfig` VALUES ('1CA16D3D-250F-4EA9-ADB4-A3D1F202EA3B', 'systableconfig', 'ColAlign', '对齐方式', '对齐方式', 100, 2, 0, 3, 0, 'col_custom_', NULL, 1, NULL, 'dataurl_colalign', 'combobox', 1, 1, 0, 5, NULL, NULL, '1', NULL);
INSERT INTO `SysTableConfig` VALUES ('1DBCFF81-4399-4FE3-8B34-68ABC051747A', 'tempexample', 'FormOrder', '排序', '排序', 110, 1, 0, 21, 0, '0', 0, 1, 1, '', 'text', 1, 1, 0, 21, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2501521D-BD4C-40C1-A1F4-5715BAC9E6CB', 'sysuser', 'SuNickname', '姓名', '姓名', 180, 1, 0, 1, 0, '0', NULL, 1, 1, NULL, 'text', 1, 2, 0, 1, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('26DCC3C7-F5D8-4F4F-A525-61C7D97CB6F3', 'sysmenu', 'SmPid', '上级菜单', '上级菜单', 100, 1, 2, 1, NULL, '0', NULL, 1, NULL, '/Common/QueryMenu?type=all', 'combotree', 1, 1, 0, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2810CB41-3366-475F-B7D2-D3588F7ACE23', 'tempexample', 'FormHide', '1隐藏', '1隐藏', 110, 1, 0, 18, 0, '0', 0, 1, 1, '', 'checkbox', 1, 1, 0, 18, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2B8BD8CC-2376-4E58-A6B5-B64FA07F1F20', 'sysmenu', 'SmStatus', '菜单状态', '菜单状态', 100, 2, 0, 4, 0, '17', NULL, 1, NULL, NULL, 'checkbox', 1, 1, 0, 5, NULL, NULL, '1', '启用菜单');
INSERT INTO `SysTableConfig` VALUES ('2efd71ce-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbId', 'SbId', 'SbId', 100, 1, 2, 0, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd72a6-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbPid', '上级按钮', '上级按钮', 100, 1, 2, 1, NULL, '0', NULL, 1, NULL, '/Common/QueryButtonTree', 'combotree', 1, 1, 0, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd72f0-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnText', '按钮文本', '按钮文本', 250, 1, 0, 0, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 0, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd7327-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnId', '按钮ID', '按钮ID', 150, 1, 0, 1, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 2, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd735a-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnClass', '按钮类', '按钮类', 200, 1, 0, 2, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 3, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd738d-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnIcon', '按钮图标', '按钮图标', 200, 1, 0, 3, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 4, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2efd73bf-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnOrder', '按钮排序', '按钮排序', 100, 2, 0, 4, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 5, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2efd73f0-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbStatus', '按钮状态', '按钮状态', 100, 2, 0, 5, 0, '17', NULL, 1, NULL, NULL, 'checkbox', 1, 1, 0, 6, NULL, NULL, '1', '启用按钮');
INSERT INTO `SysTableConfig` VALUES ('2efd7423-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbDescribe', '按钮描述', '按钮描述', 100, 1, 0, 7, 0, '0', 0, 1, 0, '', 'text', 1, 2, 0, 8, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2efd7452-c6e1-11e8-98fc-aaaa0010af81', 'SysButton', 'SbBtnGroup', '按钮分组', '按钮分组', 100, 2, 0, 6, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 7, 1, '默认1，不相同有分割线', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa7ce-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogId', 'LogId', 'LogId', 100, 1, 2, 0, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa86d-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'SuName', '账号', '账号', 160, 1, 0, 0, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 2, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa8af-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'SuNickname', '姓名', '姓名', 160, 1, 0, 1, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 3, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2f0aa8e1-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogAction', '动作', '动作', 250, 1, 0, 2, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 4, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa913-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogContent', '内容', '内容', 250, 1, 0, 3, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 5, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa944-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogUrl', '链接', '链接', 350, 1, 0, 4, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 6, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2f0aa975-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogIp', 'IP', 'IP', 160, 2, 0, 5, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 7, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa9a5-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogCreateTime', '时间', '时间', 200, 2, 0, 6, 0, '0', NULL, 1, 1, NULL, 'datetime', 1, 1, 0, 8, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aa9d7-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogBrowserName', '浏览器名称', '浏览器名称', 180, 1, 0, 7, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 9, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aaa0a-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogSystemName', '操作系统', '操作系统', 150, 1, 0, 8, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 10, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aaa3b-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogGroup', '分组', '分组', 100, 1, 1, 9, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 11, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f0aaa6b-c6e1-11e8-98fc-aaaa0010af81', 'SysLog', 'LogRemark', '备注', '备注', 100, 1, 1, 10, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 12, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180d9c-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrId', 'SrId', 'SrId', 100, 1, 2, 0, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 0, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180e18-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrName', '角色名称', '角色名称', 150, 1, 0, 0, 0, '0', NULL, 1, 1, NULL, 'text', 1, 2, 0, 0, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180ec0-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrStatus', '角色状态', '角色状态', 120, 2, 0, 2, 0, '17', 1, 1, NULL, NULL, 'checkbox', 1, 2, 0, 1, NULL, NULL, '1', '启用角色');
INSERT INTO `SysTableConfig` VALUES ('2f180f11-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrDescribe', '角色描述', '角色描述', 400, 1, 0, 1, 0, '0', 0, 1, 0, '', 'text', 1, 2, 0, 2, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('2f180f46-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrGroup', '角色分组', '角色分组', 100, 1, 1, 3, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 4, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180f7a-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrMenus', '菜单', '菜单', 100, 1, 2, 5, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 5, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180fad-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrButtons', '按钮', '按钮', 100, 1, 2, 6, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 6, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2f180fdf-c6e1-11e8-98fc-aaaa0010af81', 'SysRole', 'SrCreateTime', '创建时间', '创建时间', 100, 1, 1, 4, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 7, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('2FA5047A-81B0-442A-BB64-E3653A1F2978', 'systableconfig', 'FormRequired', '必填', '必填', 80, 2, 0, 19, 0, '17', NULL, 1, 1, NULL, 'checkbox', 2, 1, 0, 19, NULL, NULL, '0', '是必填项');
INSERT INTO `SysTableConfig` VALUES ('363b3f83-0caf-408b-8307-ae4cc6f298f5', 'sysuser', 'OldUserPwd', 'OldUserPwd', 'OldUserPwd', 100, 1, 1, 8, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 100, NULL, '', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('37DCF032-D8AB-41E7-B391-11937AA51332', 'systableconfig', 'ColSort', '启用排序', '启用排序', 100, 2, 0, 10, 0, '17', NULL, 1, 1, NULL, 'checkbox', 1, 1, 0, 9, NULL, NULL, '0', '点击标题排序');
INSERT INTO `SysTableConfig` VALUES ('3B8F9087-86A5-4777-A56E-5D9C98F972A4', 'sysmenu', 'SmUrl', '菜单链接', '菜单链接', 300, 1, 0, 1, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 2, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('3F70197D-A228-4E08-98F8-79FE22E718E3', 'tempexample', 'FormType', '下拉树', '下拉树', 110, 1, 0, 0, 0, '0', 0, 1, 1, '/common/querymenu', 'combotree', 1, 1, 0, 0, 0, '', 'F8C1C161-F1FC-4729-A00C-4A9893BF8209', '');
INSERT INTO `SysTableConfig` VALUES ('459F9EC8-BD11-43E7-BEBB-A640714B5565', 'tempexample', 'FormArea', '默认多选', '默认多选', 110, 1, 0, 5, 0, '0', 0, 1, 1, 'dataurl_formarea', 'combobox', 1, 1, 0, 5, 0, '', '1,2', '');
INSERT INTO `SysTableConfig` VALUES ('469A90B4-413C-43D9-BD76-6DE270071CD4', 'systableconfig', 'ColOrder', '排序', '排序', 80, 2, 0, 7, 0, '0', 1, 1, NULL, NULL, 'text', 1, 1, 0, 7, NULL, NULL, '100', NULL);
INSERT INTO `SysTableConfig` VALUES ('4A6531D0-AEDA-4A43-B189-2504825252FE', 'systableconfig', 'FormText', '显示文本', '显示文本', 200, 1, 0, 22, 0, '0', 0, 1, 0, '', 'text', 2, 1, 0, 22, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('4B25A348-7380-485D-8682-BF2116D37BAF', 'systableconfig', 'ColFormat', '格式化', '格式化', 280, 1, 0, 9, 0, 'col_custom_', NULL, 1, 1, 'dataurl_colformat', 'combobox', 1, 2, 0, 12, NULL, NULL, '0', NULL);
INSERT INTO `SysTableConfig` VALUES ('4D0376A3-0149-49B7-A330-A492F1DE5169', 'systableconfig', 'FormSpan', '跨列', '跨列', 80, 2, 0, 16, 0, 'col_custom_', NULL, 1, NULL, 'dataurl_formspan', 'combobox', 2, 1, 0, 16, NULL, NULL, '1', NULL);
INSERT INTO `SysTableConfig` VALUES ('52D3C3CE-0C21-483F-9A61-04D133F3BF6A', 'systableconfig', 'ColQuery', '查询', '查询', 80, 2, 0, 12, 0, '17', NULL, 1, 1, NULL, 'checkbox', 1, 1, 0, 10, NULL, NULL, '0', '启用查询');
INSERT INTO `SysTableConfig` VALUES ('536A235B-21A3-4821-B0B0-0F9A6A94F598', 'systableconfig', 'ColWidth', '列宽', '列宽', 80, 2, 0, 4, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 4, 0, '', '100', '');
INSERT INTO `SysTableConfig` VALUES ('5A933E73-6FBE-486A-93D3-C53DD02F7161', 'sysmenu', 'SmName', '菜单名称', '菜单名称', 300, 1, 0, 0, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 0, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('6B2D1765-993D-4B31-B089-58675F98C09D', 'tempexample', 'ColTitle', '年月日', '年月日', 110, 1, 0, 8, 0, '0', 0, 1, 1, '', 'date', 1, 1, 0, 8, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('6B73EBAD-5326-4E6F-95BA-A9684642668C', 'systableconfig', 'ColExport', '导出', '导出', 80, 2, 0, 11, 0, '17', NULL, 1, 1, NULL, 'checkbox', 1, 1, 0, 11, NULL, NULL, '0', '是导出列');
INSERT INTO `SysTableConfig` VALUES ('706D6A3F-7D2E-4327-82CD-6C49ACA44199', 'tempexample', 'ColSort', '默认勾选', '默认勾选', 110, 1, 0, 3, 0, '0', 0, 1, 1, '', 'checkbox', 1, 1, 0, 3, 0, '', '1', '默认选中');
INSERT INTO `SysTableConfig` VALUES ('7A40E5DE-4A25-40BD-BD8B-524630A87B01', 'sysuser', 'SuId', 'SuId', 'SuId', 100, 1, 2, 0, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('7D63D1F1-884C-439D-A973-F68D8D3C8FC4', 'tempexample', 'FormUrl', '年月日时分秒', '年月日时分秒', 110, 1, 0, 7, 0, '0', 0, 1, 1, '', 'datetime', 1, 1, 0, 7, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('8E339D70-995D-4933-8056-C490CC5775A3', 'sysuser', 'SuSign', '登录标识', '登录标识', 100, 1, 1, 6, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 8, NULL, '', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('8F812FCE-E03D-4C68-BDD7-A3DE64AE38B9', 'sysmenu', 'SmOrder', '菜单排序', '菜单排序', 100, 2, 0, 3, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 4, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('9FA037EA-7125-4D19-A98E-FAA97B22477B', 'systableconfig', 'ColFrozen', '冻结', '冻结', 80, 2, 0, 8, 0, '17', NULL, 1, 1, NULL, 'checkbox', 1, 1, 0, 8, NULL, NULL, '0', '是冻结列');
INSERT INTO `SysTableConfig` VALUES ('A961D9F3-67D3-44C6-9FDE-1E0CC31051CF', 'systableconfig', 'ColTitle', '列标题', '列标题', 250, 1, 0, 1, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 3, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('AA01AE44-5945-40FA-8C19-0F85D0347605', 'systableconfig', 'FormHide', '隐藏', '隐藏', 150, 2, 0, 17, 0, 'col_custom_', NULL, 1, 1, 'dataurl_formhide', 'combobox', 2, 1, 0, 17, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('AF4EC19D-14D6-4046-BAED-7116BA4A0FA2', 'sysuser', 'SuStatus', '状态', '状态', 60, 2, 0, 5, 0, '17', NULL, 1, 1, NULL, 'checkbox', 1, 2, 0, 4, NULL, NULL, NULL, '启用账号');
INSERT INTO `SysTableConfig` VALUES ('B4AE0D0D-5F5D-4DAB-8591-672C3EB2A526', 'tempexample', 'ColOrder', '密码框', '密码框', 110, 1, 0, 14, 0, '0', 0, 1, 1, '', 'password', 1, 1, 0, 14, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('B7CB1D39-401F-4C38-9771-A649819A79F5', 'systableconfig', 'Id', 'Id', 'Id', 100, 1, 2, 2, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('B8161370-A3B1-46BF-A3A1-986E8F7B70FD', 'systableconfig', 'FormPlaceholder', '输入框提示', '输入框提示', 200, 1, 0, 20, 0, '0', 0, 1, 0, '', 'text', 2, 1, 0, 20, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('BA169D09-3A4E-4480-A50E-A6B65F31D5E8', 'sysmenu', 'SmGroup', '菜单分组', '菜单分组', 100, 2, 0, 5, 0, '0', 0, 1, 0, '', 'text', 1, 1, 0, 6, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('BCA1BBE4-44CA-4793-B214-99A6BF137F3E', 'tempexample', 'TableName', '文本域', '文本域', 110, 1, 0, 16, 0, '0', NULL, 1, 1, NULL, 'textarea', 1, 2, 0, 16, 1, '请输入表名', NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('C06F0970-9EEA-462F-9B1C-37782E450F05', 'tempexample', 'FormPlaceholder', '人员姓名', '人员姓名', 110, 1, 0, 10, NULL, '0', NULL, 1, 1, '/setting/sysuser', 'modal', 1, 1, 0, 10, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('C2A8EE35-E805-46BF-BBF7-B09EA7101679', 'sysmenu', 'SmId', 'SmId', 'SmId', 100, 1, 2, 0, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 2, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('CAE94CDD-1D87-461B-A833-22396E73568C', 'systableconfig', 'FormType', '输入类型', '输入类型', 200, 1, 0, 14, 0, '0', NULL, 1, 1, 'dataurl_formtype', 'combobox', 2, 2, 0, 14, NULL, NULL, 'text', NULL);
INSERT INTO `SysTableConfig` VALUES ('D0527E79-6482-4D6D-90E3-03CD9764F3CC', 'sysuser', 'SuName', '账号', '账号', 180, 1, 0, 0, 0, '0', NULL, 1, 1, NULL, 'text', 1, 2, 0, 0, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('D1F078C7-C076-4B56-A399-D86CB02D04FA', 'systableconfig', 'FormValue', '初始值', '初始值', 150, 1, 0, 21, 0, '0', 0, 1, 0, '', 'text', 2, 1, 0, 21, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('D25C93CD-53F0-4BAE-B01C-26CA55512BAB', 'systableconfig', 'ColField', '列键', '列键', 200, 1, 0, 2, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 0, 1, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('D3FB79B9-22C8-4DC1-BC0F-85C681BF7656', 'tempexample', 'FormSpan', 'combotree', 'combotree', 110, 1, 0, 1, 0, '0', 0, 1, 1, '/common/querymenu?custom=m', 'combotree', 1, 1, 0, 1, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('D52AE07E-7E03-4DCD-A7C3-9AE2BD614D64', 'tempexample', 'ColField', '必填项', '必填项', 110, 1, 0, 13, 0, '0', 0, 1, 1, '', 'text', 1, 1, 0, 13, 1, '请输入内容，必填', '', '');
INSERT INTO `SysTableConfig` VALUES ('DAE7AE2A-D6F1-487B-9863-F3A29920E023', 'tempexample', 'ColFrozen', '1冻结', '1冻结', 110, 1, 0, 20, 0, '0', 0, 1, 1, '', 'text', 1, 1, 0, 20, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('DAF45196-E710-4100-B517-01D0AA7454FB', 'sysuser', 'SuCreateTime', '创建时间', '创建时间', 180, 2, 0, 3, 0, '0', NULL, 1, 1, NULL, 'text', 1, 1, 1, 6, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('E07E0463-D6A8-4760-9611-E656D870464C', 'sysuser', 'SrId', '角色', '角色', 180, 1, 0, 2, 0, 'col_custom_', NULL, 1, 1, '/common/queryrole', 'combobox', 1, 2, 0, 3, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('E158FE3F-9C66-48D3-BD76-DFA8C51EFE68', 'tempexample', 'FormText', '显示文本', '显示文本', 110, 1, 0, 22, 0, '0', 0, 1, 1, '', 'text', 1, 1, 0, 22, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('E8082943-A931-40D0-BAC0-8403BED3B06D', 'systableconfig', 'DvTitle', '默认列标题', '默认列标题', 200, 1, 0, 5, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 2, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('E8D0114E-63B3-41C4-99D0-5F9AF9EF79CC', 'tempexample', 'Id', 'Id', 'Id', 110, 1, 1, 1, NULL, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 1, 1, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('E9E6345A-EE33-4B25-834D-8748D252B2F1', 'systableconfig', 'FormUrl', '来源（URL）', '来源（URL）', 300, 1, 0, 13, 0, '0', NULL, 1, 1, '/common/querymenu', 'text', 2, 2, 0, 13, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('F08489C7-6A0F-4C5D-A81E-9EE6621994AC', 'systableconfig', 'TableName', '（虚）表名', '（虚）表名', 200, 1, 0, 0, 1, '0', 1, 1, 1, NULL, 'text', 1, 1, 0, 0, 1, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('F7A57E11-3B98-43F8-993A-47E6733C9F3C', 'tempexample', 'ColExport', '1导出', '1导出', 110, 1, 0, 17, 0, '0', NULL, 1, 1, NULL, 'checkbox', 1, 1, 0, 17, NULL, NULL, NULL, '导出');
INSERT INTO `SysTableConfig` VALUES ('F821E861-BB7B-4D20-915F-1B35EFA68ECC', 'tempexample', 'ColWidth', '人员账号', '人员账号', 110, 1, 0, 11, NULL, '0', NULL, 1, 1, '/setting/sysuser', 'modal', 1, 1, 0, 11, NULL, NULL, NULL, NULL);
INSERT INTO `SysTableConfig` VALUES ('FBEA8B72-B88A-4A24-B3DC-A9A5813BD9B2', 'tempexample', 'ColFormat', 'combobox', 'combobox', 110, 1, 0, 4, 0, '0', 0, 1, 1, 'dataurl_colformat', 'combobox', 1, 1, 0, 4, 0, '', '', '');
INSERT INTO `SysTableConfig` VALUES ('FEAE4BB7-4A5F-4F53-AE6B-996F08BE4471', 'sysmenu', 'SmIcon', '菜单图标', '菜单图标', 250, 1, 0, 2, 0, '0', NULL, 1, NULL, NULL, 'text', 1, 1, 0, 3, 1, NULL, NULL, NULL);

-- ----------------------------
-- Table structure for SysUser
-- ----------------------------
DROP TABLE IF EXISTS `SysUser`;
CREATE TABLE `SysUser`  (
  `SuId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SrId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '角色',
  `SuName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '账号',
  `SuPwd` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '密码',
  `SuNickname` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '昵称',
  `SuCreateTime` datetime(3) DEFAULT NULL COMMENT '创建时间',
  `SuStatus` int(11) DEFAULT 0 COMMENT '状态，1正常',
  `SuSign` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '登录标识',
  `SuGroup` int(11) DEFAULT 0 COMMENT '分组',
  PRIMARY KEY (`SuId`) USING BTREE,
  INDEX `SysUser_SuId_PK`(`SuId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SysUser
-- ----------------------------
INSERT INTO `SysUser` VALUES ('0ad60901-33d9-4bda-99c3-e720dd0685d7', '58307c67-76b8-4156-bde3-f307f4da25e9', 'test', '098f6bcd4621d373cade4e832627b4f6', 'test', '2018-04-21 19:49:51.000', 1, NULL, NULL);
INSERT INTO `SysUser` VALUES ('F9A19BAB-49C3-4131-AEFC-FB80FAAE579A', 'E663CE67-E9CA-4441-AB77-DC267C22C683', 'admin', '21232f297a57a5a743894a0e4a801fc3', '管理员', '2018-02-14 09:33:00.000', 1, NULL, 1);

-- ----------------------------
-- Table structure for TempExample
-- ----------------------------
DROP TABLE IF EXISTS `TempExample`;
CREATE TABLE `TempExample`  (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `TableName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '（虚）表名',
  `ColField` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '列键',
  `DvTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '默认列标题',
  `ColTitle` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '列标题',
  `ColWidth` int(11) DEFAULT 0 COMMENT '列宽',
  `ColAlign` int(11) DEFAULT 0 COMMENT '对齐方式 1左，2中，3右',
  `ColHide` int(11) DEFAULT 0 COMMENT '1隐藏',
  `ColOrder` int(11) DEFAULT 0 COMMENT '排序',
  `ColFrozen` int(11) DEFAULT 0 COMMENT '1冻结',
  `ColFormat` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '格式化',
  `ColSort` int(11) DEFAULT 0 COMMENT '1启用点击排序',
  `ColExport` int(11) DEFAULT 0 COMMENT '1导出',
  `ColQuery` int(11) DEFAULT 0 COMMENT '1查询',
  `FormUrl` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '来源',
  `FormType` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '输入类型',
  `FormArea` int(11) DEFAULT 0 COMMENT '区域',
  `FormSpan` int(11) DEFAULT 0 COMMENT '跨列',
  `FormHide` int(11) DEFAULT 0 COMMENT '1隐藏',
  `FormOrder` int(11) DEFAULT 0 COMMENT '排序',
  `FormRequired` int(11) DEFAULT 0 COMMENT '1必填',
  `FormPlaceholder` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '输入框提示',
  `FormValue` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '初始值',
  `FormText` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '显示文本',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `TempExample_Id_PK`(`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
