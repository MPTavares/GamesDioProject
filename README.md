# GamesDioProject

API simples de para executar CRUD em uma base de Games. 
O projeto foi desenvolvido para o bootCamp Digital Innovartion One (Criando um catálogo de jogos usando boas práticas de arquitetura com .NET) 
ministrado pelo instrutor Thiago Campos de Oliveira.

O projeto utiliza SqlClient acessando diretamente uma base de dados Sql Server. 
Foi desenvolvido um middleware simples para captar erros genéricos e também esta configurado com Swagger.


Script de criação da tabela:

Crie um database chamado GamesAPI e inclua a tabela Games.

CREATE TABLE [dbo].[Games](
	[game_id] [uniqueidentifier] NOT NULL,
	[game_name] [varchar](100) NULL,
	[game_producer] [varchar](100) NULL,
	[game_price] [money] NULL,
 CONSTRAINT [PK_Games] PRIMARY KEY CLUSTERED 
(
	[game_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Games] ADD  CONSTRAINT [DF_Games_game_id]  DEFAULT (newid()) FOR [game_id]
GO
