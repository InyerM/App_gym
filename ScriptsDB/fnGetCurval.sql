USE [App_gym]
GO

/****** Object:  UserDefinedFunction [dbo].[get_curval]    Script Date: 20/02/2022 2:59:13 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE   FUNCTION [dbo].[get_curval]()
RETURNS BIGINT as
    BEGIN
		DECLARE @N BIGINT;

		SET @N = (SELECT CONVERT(Bigint,Current_value) FROM sys.sequences WHERE name = 'SEQ_CODIGO_COMPRA');
		RETURN @N;
    END
;
GO


