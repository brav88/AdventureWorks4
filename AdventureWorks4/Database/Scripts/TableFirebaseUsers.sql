USE [Booking]
GO

/****** Object:  Table [dbo].[FirebaseUsers]    Script Date: 26/10/2023 19:51:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FirebaseUsers](
	[documentId] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[id] [varchar](100) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[photoPath] [varchar](max) NOT NULL,
 CONSTRAINT [PK_FirebaseUsers] PRIMARY KEY CLUSTERED 
(
	[documentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


