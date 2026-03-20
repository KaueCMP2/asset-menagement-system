CREATE DATABASE	AssetMenagementDb
GO

USE AssetMenagementDb
GO

CREATE TABLE Cargo
(
	CargoId					UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeCargo				VARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE Cidade
(
	CidadeId				UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeEstado				VARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE Bairro
(
	BairroId				UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY, 
	NomeBairro				VARCHAR(50) NOT NULL,
	CidadeId				UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_Bairro_bairroCidade_cidadeId	
		FOREIGN KEY (CidadeId) 
			REFERENCES Cidade(CidadeId) ON DELETE CASCADE
)
GO

CREATE TABLE Endereco
(
	EnderecoId				UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Logradouro				VARCHAR(100) NOT NULL,
	Numero					INT NOT NULL,
	Complemento				VARCHAR(50) NOT NULL,
	CEP						VARCHAR(10) NOT NULL,
	BairroId				UNIQUEIDENTIFIER NOT NULL,

		CONSTRAINT FK_Endereco_EnderecoBairro_BairroId
			FOREIGN KEY (BairroId) 
				REFERENCES Bairro(BairroId) ON DELETE CASCADE
)
GO

CREATE TABLE TipoUsuario
(
	TipoUsuarioId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Nome					VARCHAR(100) NOT NULL
)
GO

CREATE TABLE Usuario
(
	UsuarioID				UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NIF						VARCHAR(7)  NOT NULL UNIQUE,
	NomeUsuario				VARCHAR(50)  NOT NULL,
	RG						VARCHAR(15)  NOT NULL UNIQUE,
	CPF						VARCHAR(11)  NOT NULL UNIQUE,
	CarteiraDeTabalho		VARCHAR(14),
	Senha					VARBINARY(32) NULL,
	Email					VARCHAR(150)  NOT NULL UNIQUE,
	EnderecoId				UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_Usuario_UsuaroEndereco_EnderecoId	
		FOREIGN KEY (EnderecoId) 
			REFERENCES Endereco(EnderecoId) ON DELETE CASCADE
)
GO

CREATE TABLE Area
(
	AreaId					UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeArea				VARCHAR(50)  NOT NULL UNIQUE
)
GO

CREATE TABLE Localizacao
(
	LocalizacaoId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeLocal				VARCHAR(100) NOT NULL,
	LocalSAP				INT NOT NULL,
	DescricaoSAP			VARCHAR(100),
	AreaId					UNIQUEIDENTIFIER,

	CONSTRAINT FK_Localizacao_Area_AreaId 
		FOREIGN KEY (AreaId)	
			REFERENCES Area(AreaId) ON DELETE CASCADE
)
GO

CREATE TABLE TipoPatrimonio
(
	TipoAlteracaoId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeTipo				NVARCHAR(100) NOT NULL UNIQUE
)
GO

CREATE TABLE StatusPatrimonio
(
	StatusPatrimonioId		UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeStatus				NVARCHAR(50) NOT NULL UNIQUE 
)
GO

CREATE TABLE Patrimonio
(
	PatrimonioId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	Denominacao				VARCHAR(MAX),
	NumeroSerie				INT NOT NULL,
	Valor					DECIMAL(10,2) NOT NULL,
	Imagem					VARCHAR(MAX) NULL,
	LocalizacaoId			UNIQUEIDENTIFIER NOT NULL,
	TipoPatrimonioId		UNIQUEIDENTIFIER NOT NULL,
	StatusPatrimonioId		UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_Patrimonio_Localizacao_LocalizacaoId
		FOREIGN KEY (LocalizacaoId) 
			REFERENCES Localizacao(LocalizacaoId) ON DELETE CASCADE,

	CONSTRAINT FK_Patrimonio_TipoPatrimonioId
		FOREIGN KEY (TipoPatrimonioId) 
			REFERENCES TipoPatrimonio(TipoAlteracaoId),

	CONSTRAINT FK_Patrimonio_StatusPatrimonioId 
		FOREIGN KEY (StatusPatrimonioId) 
			REFERENCES StatusPatrimonio(StatusPatrimonioId) 
)
GO

CREATE TABLE TipoAlteracao
(
	TipoAlteracaoId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeTipoAlteracao		NVARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE Log_Patrimonio
(
	LogPatrimonioId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	DataTranferencia		DATETIME2 NOT NULL,
	TipoAlteracaoId			UNIQUEIDENTIFIER NOT NULL,
	StatusPatrimonioId		UNIQUEIDENTIFIER NOT NULL,
	PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
	UsuarioId				UNIQUEIDENTIFIER NOT NULL,
	LocalizacaoId			UNIQUEIDENTIFIER NOT NULL

	CONSTRAINT FK_Patrimonio_TipoAlteracao_TipoAlteracaoId 
		FOREIGN KEY (TipoAlteracaoId) 
			REFERENCES TipoAlteracao(TipoAlteracaoId) ON DELETE CASCADE,

	CONSTRAINT FK_Patrimonio_StatusPatimonio_StatusPatrimonioId 
		FOREIGN KEY (StatusPatrimonioId) 
			REFERENCES StatusPatrimonio(StatusPatrimonioId) ON DELETE CASCADE,

	CONSTRAINT FK_Patrimonio_PatrimonioId 
		FOREIGN KEY (PatrimonioId)
			REFERENCES Patrimonio(PatrimonioId) ON DELETE CASCADE,

	CONSTRAINT FK_Patrimonio_UsuarioId 
		FOREIGN KEY (UsuarioId) 
			REFERENCES Usuario(UsuarioId) ON DELETE CASCADE,

	CONSTRAINT FK_Patrimonio_LocalizacaoId 
		FOREIGN KEY (LocalizacaoId) 
			REFERENCES  Localizacao(LocalizacaoId) ON DELETE CASCADE
)
GO

CREATE TABLE StatusTranferencia 
(
	StatusTransferenciaId	UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NomeStatus				VARCHAR(50) NOT NULL UNIQUE
)
GO

CREATE TABLE SolicitacaoTranferencia
(
	SolicitacaoId			UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY, 
	DataSolicitacao			DATETIME2 DEFAULT GETDATE(),
	DataResposta			DATETIME2 NOT NULL,
	Justificativa			NVARCHAR(MAX) NOT NULL,
	StatusTransferenciaId	UNIQUEIDENTIFIER NOT NULL,
	UsuarioSolicitacaoId	UNIQUEIDENTIFIER NOT NULL,
	UsuarioAprovacaoId		UNIQUEIDENTIFIER NOT NULL,
	PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
	LocalizacaoId			UNIQUEIDENTIFIER NOT NULL
	
	CONSTRAINT FK_SolicitacaoTranferencia_StatusTransferenciaId 
		FOREIGN KEY (StatusTransferenciaId) 
			REFERENCES StatusTranferencia(StatusTransferenciaId) ON DELETE CASCADE,
	
	CONSTRAINT FK_SolicitacaoTranferencia_UsuarioSolicitacaoId 
		FOREIGN KEY (UsuarioSolicitacaoId) 
			REFERENCES Usuario(UsuarioId) ON DELETE CASCADE,

	CONSTRAINT FK_SolicitacaoTranferencia_UsuarioAprovacaoId 
		FOREIGN KEY (UsuarioAprovacaoId) 
			REFERENCES Usuario(UsuarioId) ON DELETE CASCADE,

	CONSTRAINT FK_SolicitacaoTranferencia_PatrimonioId 
		FOREIGN KEY (PatrimonioId) 
			REFERENCES Patrimonio(PatrimonioId) ON DELETE CASCADE,

	CONSTRAINT FK_SolicitacaoTranferencia_LocalizacaoId 
			FOREIGN KEY (LocalizacaoId) 
				REFERENCES Localizacao(LocalizacaoId) ON DELETE CASCADE
)
GO