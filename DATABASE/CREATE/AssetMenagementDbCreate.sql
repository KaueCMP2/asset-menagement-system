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
	NomeCidade				VARCHAR(50) NOT NULL,
	NomeEstado				VARCHAR(50) NOT NULL
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
	UsuarioId				UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
	NIF						VARCHAR(7)  NOT NULL UNIQUE,
	NomeUsuario				VARCHAR(50)  NOT NULL,
	RG						VARCHAR(15)  NOT NULL UNIQUE,
	CPF						VARCHAR(11)  NOT NULL UNIQUE,
	CarteiraDeTabalho		VARCHAR(14),
	Senha					VARBINARY(32) NULL,
	Email					VARCHAR(150)  NOT NULL UNIQUE,
	StatusUsuario			BIT DEFAULT 1,
	EnderecoId				UNIQUEIDENTIFIER NOT NULL,

	CONSTRAINT FK_Usuario_UsuaroEndereco_EnderecoId	
		FOREIGN KEY (EnderecoId) REFERENCES Endereco(EnderecoId)
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
	NomeLocal				VARCHAR(100),
	LocalSAP				INT,
	DescricaoSAP			VARCHAR(100),
	StatusLocalizacao		BIT DEFAULT 1,
	AreaId					UNIQUEIDENTIFIER,

	CONSTRAINT FK_Localizacao_Area_AreaId 
		FOREIGN KEY (AreaId) REFERENCES Area(AreaId)
)
GO

ALTER TABLE Localizacao
ALTER COLUMN LocalSap INT NULL

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
		FOREIGN KEY (LocalizacaoId) REFERENCES Localizacao(LocalizacaoId),

	CONSTRAINT FK_Patrimonio_TipoPatrimonioId
		FOREIGN KEY (TipoPatrimonioId) REFERENCES TipoPatrimonio(TipoAlteracaoId),

	CONSTRAINT FK_Patrimonio_StatusPatrimonioId 
		FOREIGN KEY (StatusPatrimonioId) REFERENCES StatusPatrimonio(StatusPatrimonioId) 
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
	DataTranferencia		DATETIME2 (0) NOT NULL,
	TipoAlteracaoId			UNIQUEIDENTIFIER NOT NULL,
	StatusPatrimonioId		UNIQUEIDENTIFIER NOT NULL,
	PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
	UsuarioId				UNIQUEIDENTIFIER NOT NULL,
	LocalizacaoId			UNIQUEIDENTIFIER NOT NULL

	CONSTRAINT FK_Patrimonio_TipoAlteracao_TipoAlteracaoId 
		FOREIGN KEY (TipoAlteracaoId) REFERENCES TipoAlteracao(TipoAlteracaoId),

	CONSTRAINT FK_Patrimonio_StatusPatimonio_StatusPatrimonioId 
		FOREIGN KEY (StatusPatrimonioId) REFERENCES StatusPatrimonio(StatusPatrimonioId),

	CONSTRAINT FK_Patrimonio_PatrimonioId 
		FOREIGN KEY (PatrimonioId) REFERENCES Patrimonio(PatrimonioId),

	CONSTRAINT FK_Patrimonio_UsuarioId 
		FOREIGN KEY (UsuarioId) REFERENCES Usuario(UsuarioId),

	CONSTRAINT FK_Patrimonio_LocalizacaoId 
		FOREIGN KEY (LocalizacaoId) REFERENCES Localizacao(LocalizacaoId)
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
	DataSolicitacao			DATETIME2 (0) DEFAULT GETDATE(),
	DataResposta			DATETIME2 (0) NOT NULL,
	Justificativa			NVARCHAR(MAX) NOT NULL,
	StatusTransferenciaId	UNIQUEIDENTIFIER NOT NULL,
	UsuarioSolicitacaoId	UNIQUEIDENTIFIER NOT NULL,
	UsuarioAprovacaoId		UNIQUEIDENTIFIER NOT NULL,
	PatrimonioId			UNIQUEIDENTIFIER NOT NULL,
	LocalizacaoId			UNIQUEIDENTIFIER NOT NULL
	
	CONSTRAINT FK_SolicitacaoTranferencia_StatusTransferenciaId 
		FOREIGN KEY (StatusTransferenciaId) REFERENCES StatusTranferencia(StatusTransferenciaId),
	
	CONSTRAINT FK_SolicitacaoTranferencia_UsuarioSolicitacaoId 
		FOREIGN KEY (UsuarioSolicitacaoId) REFERENCES Usuario(UsuarioId),

	CONSTRAINT FK_SolicitacaoTranferencia_UsuarioAprovacaoId 
		FOREIGN KEY (UsuarioAprovacaoId) REFERENCES Usuario(UsuarioId),

	CONSTRAINT FK_SolicitacaoTranferencia_PatrimonioId 
		FOREIGN KEY (PatrimonioId) REFERENCES Patrimonio(PatrimonioId),

	CONSTRAINT FK_SolicitacaoTranferencia_LocalizacaoId 
			FOREIGN KEY (LocalizacaoId)	REFERENCES Localizacao(LocalizacaoId)
)
GO

-- TRIGGER PARA SOFT DELETE DO USUARIO
CREATE TRIGGER trg_Usuario_SoftDelete
ON Usuario
INSTEAD OF DELETE
AS
BEGIN
	UPDATE Usuario
		SET StatusUsuario = 0
		WHERE UsuarioId IN (SELECT UsuarioId FROM deleted);
END
GO

-- TRIGGER PARA SOFT DELETE DE LOCALIZAÇÃO
CREATE TRIGGER trg_Local_SoftDelete
ON Localizacao
INSTEAD OF DELETE
AS
BEGIN
	UPDATE Localizacao
		SET StatusLocalizacao = 0
		WHERE LocalizacaoId IN (SELECT LocalizacaoId FROM deleted);
END
GO

-- TRIGGER PARA SOFT DELETE DE PATRIMONIO
CREATE TRIGGER trg_Patrimonio_SoftDelete
ON Patrimonio
INSTEAD OF DELETE
AS
BEGIN
	UPDATE Patrimonio
		SET	 StatusPatrimonioId = (SELECT StatusPatrimonioId FROM StatusPatrimonio WHERE NomeStatus = 'Inativo')
		WHERE StatusPatrimonioId IN (SELECT PatrimonioId FROM deleted);
END
GO