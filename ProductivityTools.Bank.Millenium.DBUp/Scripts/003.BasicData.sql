CREATE TABLE [bm].[BasicData]
(
	BasicDataId INT IDENTITY(1,1),
	Date DATE,
	Saldo DECIMAL(7,2),
    AvailiableFunds DECIMAL(7,2),
    BlockedFunds DECIMAL(7,2),
    IncomingTransfers DECIMAL(7,2),

	CONSTRAINT PK_BasicData PRIMARY KEY(BasicDataId)
	
)