USE [App_gym]
GO

/****** Object:  Table [dbo].[CREDITOS]    Script Date: 20/02/2022 2:58:01 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CREDITOS](
	[ID] [bigint] NULL,
	[DEUDA] [bigint] NULL,
	[CODIGO_CREDITO] [bigint] NOT NULL,
 CONSTRAINT [CREDITOS_PK] PRIMARY KEY CLUSTERED 
(
	[CODIGO_CREDITO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CREDITOS]  WITH CHECK ADD  CONSTRAINT [CREDITOS_FK] FOREIGN KEY([ID])
REFERENCES [dbo].[COMPRAS] ([CODIGO_COMPRA])
GO

ALTER TABLE [dbo].[CREDITOS] CHECK CONSTRAINT [CREDITOS_FK]
GO


