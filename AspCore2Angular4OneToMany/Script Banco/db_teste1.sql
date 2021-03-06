USE [db_teste1]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 22/01/2018 11:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[DepartamentoID] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[DepartamentoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Empregado]    Script Date: 22/01/2018 11:17:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empregado](
	[EmpregadoID] [int] IDENTITY(1,1) NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[Nome] [nvarchar](50) NULL,
	[Sobrenome] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_Empregado] PRIMARY KEY CLUSTERED 
(
	[EmpregadoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Empregado]  WITH CHECK ADD  CONSTRAINT [FK_Empregado_Departamento] FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([DepartamentoID])
GO
ALTER TABLE [dbo].[Empregado] CHECK CONSTRAINT [FK_Empregado_Departamento]
GO
