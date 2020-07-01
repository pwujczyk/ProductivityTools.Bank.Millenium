CREATE TABLE [bm].[Transaction]
(
	    TransactionId INT IDENTITY(1,1),

	    AugmentTransactionId VARCHAR(20),

        Bank VARCHAR(100),
        AccountName VARCHAR(100),
        [Date] DATE,
        [Value] DECIMAL(8,2),

        SourceAccount VARCHAR(100),
        DestAccount VARCHAR(100),
        Type VARCHAR(100),
        Title VARCHAR(100),

        SourceBank VARCHAR(100),
        Sender VARCHAR(100),
        Receipment VARCHAR(100),

        TransactionPlace VARCHAR(100),

        
        CardNumber VARCHAR(100),
        CardOwner VARCHAR(100),
)
