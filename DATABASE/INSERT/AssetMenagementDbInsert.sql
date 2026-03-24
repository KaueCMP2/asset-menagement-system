USE AssetMenagementDb
GO

-- Area
INSERT INTO Area (NomeArea) VALUES
('Bloco A - Térreo'),
('Bloco A - 1° Andar')
GO

-- TipoUsuario
INSERT INTO TipoUsuario (Nome) VALUES
('Responsável'),
('Coordenador')
GO

-- Cargo
INSERT INTO Cargo (NomeCargo) VALUES
('Diretor'),
('Instrutor de Formação Profissional II')
GO

-- TipoPatrimonio
INSERT INTO TipoPatrimonio (NomeTipo) VALUES
('Mesa'),
('Notebook')
GO

-- StatusPatrimonio
-- Inativo, Ativo, Transferido, Assis. Tecnica
INSERT INTO StatusPatrimonio (NomeStatus) VALUES
('Inativo'),
('Ativo'),
('Transferido'),
('Em manutenção')
GO

-- StatusTransferencia
-- Pendente de aprovação, Aprovado e Recusado
INSERT INTO StatusTranferencia (NomeStatus) VALUES
('Pendente de aprovação'),
('Aprovado'),
('Recusado')
GO

-- TipoAlteracao
-- Modificação e transferência
INSERT INTO TipoAlteracao (NomeTipoAlteracao) VALUES
('Atualização de dados'),
('Transferência')
GO

-- Cidade
INSERT INTO Cidade (NomeCidade, NomeEstado) VALUES
('São Caetano do Sul', 'São Paulo'),
('Diadema', 'São Paulo')
GO

-- Local
INSERT INTO Localizacao (LocalSAP, DescricaoSAP, NomeLocal, AreaID) VALUES
(NULL, NULL, 'Manutenção', (SELECT AreaID FROM Area WHERE NomeArea = 'Bloco A - Térreo'))
GO

-- Bairro
INSERT INTO Bairro (NomeBairro, CidadeID) VALUES
('Centro', (SELECT CidadeID FROM Cidade WHERE NomeCidade = 'São Caetano do Sul'))
GO