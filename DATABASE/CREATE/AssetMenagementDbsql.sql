Use AssetMenagementDb 
GO

INSERT INTO Area (NomeArea)
VALUES
('Bloco A - Terreo'),
('Bloco A - Primeiro andar')
GO

INSERT INTO TipoUsuario (Nome) 
VALUES
('Responsavel'),
('Coordenador')
GO

INSERT INTO Cargo (NomeCargo) 
VALUES
('Diretor'),
('Instrutor de formação profissiona')
GO

INSERT INTO TipoPatrimonio (NomeTipo)
VALUES
('Moveis'),
('Notebook')
GO

INSERT INTO StatusPatrimonio (NomeStatus)
VALUES
('Ativo'),
('Inativo'),
('Transferido'),
('Em manutenção')
GO

INSERT INTO StatusTranferencia (NomeStatus)
VALUES 
('Aprovada'),
('Pendente'),
('Negada')
GO

INSERT INTO TipoAlteracao (NomeTipoAlteracao)
VALUES 
('Atualização de dados'),
('Tranferencia')
GO

INSERT INTO Cidade(NomeCidade, NomeEstado)
VALUES
('São Caetano do Sul', 'São Paulo'),
('Ferraz de vasconcelos', 'São Pualo')
GO

INSERT INTO Localizacao (LocalSAP, DescricaoSAP, NomeLocal, AreaId)
VALUES
(NULL, NULL, 'Manutenção', (SELECT AreaId FROM Area WHERE NomeArea = 'Bloco A - Terreo')),
GO

INSERT INTO Bairro (NomeBairro, CidadeId)
VALUES
('Centro', (SELECT CidadeId FROM Cidade WHERE NomeCidade = 'São Caetano do Sul'))
GO