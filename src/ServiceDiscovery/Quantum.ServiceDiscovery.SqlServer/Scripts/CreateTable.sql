USE [ServiceDiscovery]
GO

/****** Object:  Table [dbo].[Services]    Script Date: 12/5/2023 6:13:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Services](
	[Id] [int] NOT NULL,
	[ServiceId] [int] NULL,
	[Name] [nvarchar](4000) NOT NULL,
	[Tags] [nvarchar](4000) NULL,
	[Port] [int] NOT NULL,
	[Address] [varchar](4000) NOT NULL,
	[Check] [text] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


