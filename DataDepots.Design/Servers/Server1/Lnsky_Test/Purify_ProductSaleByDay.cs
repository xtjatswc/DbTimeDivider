using DataDepots;
using DataDepots.Core;
using DataDepots.IFace;
using System;

namespace DataDepotsDemo
{
    public class Purify_ProductSaleByDay : AbsTableDefine<Lnsky_Test>
    {
        protected override void Define()
        {
            Table.Name = "Purify_ProductSaleByDay_{0}";
            Table.DepotsFlag = DepotsFlag.MM;
        }

        public override void Create(string tableName)
        {
            string sql = @"
/*
 Navicat Premium Data Transfer

 Source Server         : sql server
 Source Server Type    : SQL Server
 Source Server Version : 11002100
 Source Host           : .:1433
 Source Catalog        : Lnsky_Test_19
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 11002100
 File Encoding         : 65001

 Date: 08/07/2019 14:23:03
*/


-- ----------------------------
-- Table structure for {0}
-- ----------------------------

CREATE TABLE [dbo].[{0}] (
  [SysNo] uniqueidentifier DEFAULT (newid()) NOT NULL,
  [DataSource] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [OutProductID] nvarchar(100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL,
  [BrandID] uniqueidentifier DEFAULT (CONVERT([binary],(0))) NOT NULL,
  [CategoryID] uniqueidentifier DEFAULT (CONVERT([binary],(0))) NOT NULL,
  [ProductID] uniqueidentifier DEFAULT (CONVERT([binary],(0))) NOT NULL,
  [ProductName] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ShopID] uniqueidentifier DEFAULT (CONVERT([binary],(0))) NOT NULL,
  [ShopName] nvarchar(100) COLLATE Chinese_PRC_CI_AS DEFAULT '' NULL,
  [StatisticalDate] date  NOT NULL,
  [Sales] decimal(18,2) DEFAULT ((0)) NOT NULL,
  [NumberOfSales] int DEFAULT ((0)) NOT NULL,
  [AveragePrice] decimal(18,2) DEFAULT ((0)) NOT NULL,
  [OrderQuantity] int DEFAULT ((0)) NOT NULL,
  [CreateDate] datetime DEFAULT (getdate()) NOT NULL,
  [CreateUserID] uniqueidentifier DEFAULT (CONVERT([binary],(0))) NOT NULL,
  [UpdateDate] datetime  NULL,
  [UpdateUserID] uniqueidentifier  NULL,
  [ImportGroupId] uniqueidentifier  NOT NULL,
  [IsExclude] bit DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[{0}] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据来源',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'DataSource'
GO

EXEC sp_addextendedproperty
'MS_Description', N'外部商品ID',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'OutProductID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'分类id',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'CategoryID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品id',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'ProductID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品名称',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'ProductName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺ID',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'ShopID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'店铺名称',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'ShopName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'统计日期',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'StatisticalDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'销售额',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'Sales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'销量',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'NumberOfSales'
GO

EXEC sp_addextendedproperty
'MS_Description', N'商品均价',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'AveragePrice'
GO

EXEC sp_addextendedproperty
'MS_Description', N'订单量',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'OrderQuantity'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'CreateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建人',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'CreateUserID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'UpdateDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新人',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'UpdateUserID'
GO

EXEC sp_addextendedproperty
'MS_Description', N'导入组',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'ImportGroupId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'黑名单',
'SCHEMA', N'dbo',
'TABLE', N'{0}',
'COLUMN', N'IsExclude'
GO


-- ----------------------------
-- Indexes structure for table {0}
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_{0}]
ON [dbo].[{0}] (
  [UpdateDate] ASC
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_{0}_DOS]
ON [dbo].[{0}] (
  [DataSource] ASC,
  [OutProductID] ASC,
  [StatisticalDate] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_{0}_GenerateShopSaleByDay]
ON [dbo].[{0}] (
  [StatisticalDate] ASC,
  [IsExclude] ASC,
  [BrandID] ASC,
  [CategoryID] ASC
)
INCLUDE ([DataSource], [ShopID], [Sales], [NumberOfSales], [OrderQuantity])
GO

CREATE NONCLUSTERED INDEX [IX_{0}_OrderBy]
ON [dbo].[{0}] (
  [StatisticalDate] DESC,
  [UpdateDate] DESC,
  [CreateDate] DESC,
  [SysNo] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_{0}_ShopID]
ON [dbo].[{0}] (
  [ShopID] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_{0}_SI]
ON [dbo].[{0}] (
  [StatisticalDate] DESC,
  [IsExclude] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_{0}_StatisticalDate]
ON [dbo].[{0}] (
  [StatisticalDate] DESC
)
GO


-- ----------------------------
-- Primary Key structure for table {0}
-- ----------------------------
ALTER TABLE [dbo].[{0}] ADD CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ([SysNo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO
";

            sql = string.Format(sql, tableName);
            string[] arr = sql.Split(new string[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arr)
            {
                try
                {
                    Table.Database.DBProvider.DbContext.Sql(item).Execute();
                }
                catch
                {
                }
            }
        }

    }
}
