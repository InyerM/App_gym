USE [App_gym]
GO

/****** Object:  Table [dbo].[ARTICULO]    Script Date: 20/02/2022 2:55:17 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ARTICULO](
	[ID] [bigint] NOT NULL,
	[NOMBRE] [varchar](30) NULL,
	[PRECIO] [int] NULL,
	[TIPO] [varchar](20) NULL,
	[SUBTIPO] [varchar](20) NULL,
 CONSTRAINT [ARTICULO_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


