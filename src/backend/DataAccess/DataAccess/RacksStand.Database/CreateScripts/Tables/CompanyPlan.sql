CREATE TABLE [dbo].[CompanyPlan]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [PlanId] TINYINT NOT NULL, 
    [RiskFreeEndDate] DATETIME NULL, 
    [ActivationDate] DATETIME NOT NULL, 
    [NextBillDate] DATETIME NOT NULL, 
    [ExpireDate] DATETIME NOT NULL, 
    [LastPaymentId] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [PlanCancelOn] DATETIME NULL, 
    [BillingCycle] TINYINT NOT NULL
)
