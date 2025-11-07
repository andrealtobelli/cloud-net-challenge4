-- =====================================================
-- SCRIPT DDL - TRACKZONE SISTEMA DE GESTÃO DE MOTOS
-- =====================================================
-- Descrição: Script de criação das tabelas do sistema TrackZone
-- Autor: [SEU NOME]
-- Data: 29/09/2025
-- Versão: 1.0
-- =====================================================

-- Criação da tabela de Usuários
-- Responsável por armazenar informações dos usuários do sistema
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,                    -- Chave primária auto-incremento
    NomeFilial NVARCHAR(255) NOT NULL,                   -- Nome da filial/empresa
    Email NVARCHAR(255) NOT NULL UNIQUE,                 -- Email único para login
    SenhaHash NVARCHAR(500) NOT NULL,                    -- Senha criptografada com BCrypt
    Cnpj NVARCHAR(18) NOT NULL UNIQUE,                   -- CNPJ único da empresa
    Endereco NVARCHAR(500) NULL,                         -- Endereço da empresa (opcional)
    Telefone NVARCHAR(20) NULL,                          -- Telefone de contato (opcional)
    Perfil INT NOT NULL CHECK (Perfil IN (0, 1, 2)),     -- Perfil: 0=ADMIN, 1=GERENTE, 2=OPERADOR
    DataCriacao DATETIME2 NOT NULL DEFAULT GETDATE(),    -- Data de criação do registro
    DataAtualizacao DATETIME2 NOT NULL DEFAULT GETDATE() -- Data da última atualização
);

-- Índices para otimização de consultas na tabela Usuarios
CREATE INDEX IX_Usuarios_Email ON Usuarios(Email);
CREATE INDEX IX_Usuarios_Cnpj ON Usuarios(Cnpj);
CREATE INDEX IX_Usuarios_Perfil ON Usuarios(Perfil);

-- =====================================================

-- Criação da tabela de Motos
-- Responsável por armazenar informações das motos gerenciadas
CREATE TABLE Motos (
    Id INT IDENTITY(1,1) PRIMARY KEY,                    -- Chave primária auto-incremento
    Placa NVARCHAR(10) NOT NULL UNIQUE,                  -- Placa única da moto
    Chassi NVARCHAR(50) NOT NULL UNIQUE,                 -- Chassi único da moto
    Motor NVARCHAR(100) NOT NULL,                        -- Especificação do motor
    Status INT NOT NULL CHECK (Status IN (0, 1, 2, 3)), -- Status: 0=DISPONIVEL, 1=ALUGADA, 2=MANUTENCAO, 3=VENDIDA
    UsuarioId INT NOT NULL,                              -- Referência ao usuário responsável
    DataCriacao DATETIME2 NOT NULL DEFAULT GETDATE(),    -- Data de criação do registro
    DataAtualizacao DATETIME2 NOT NULL DEFAULT GETDATE(), -- Data da última atualização
    
    -- Chave estrangeira para Usuarios
    CONSTRAINT FK_Motos_Usuario FOREIGN KEY (UsuarioId) 
        REFERENCES Usuarios(Id) ON DELETE CASCADE
);

-- Índices para otimização de consultas na tabela Motos
CREATE INDEX IX_Motos_Placa ON Motos(Placa);
CREATE INDEX IX_Motos_Chassi ON Motos(Chassi);
CREATE INDEX IX_Motos_Status ON Motos(Status);
CREATE INDEX IX_Motos_UsuarioId ON Motos(UsuarioId);

-- =====================================================

-- Criação da tabela de Operações
-- Responsável por registrar todas as operações realizadas com as motos
CREATE TABLE Operacoes (
    Id INT IDENTITY(1,1) PRIMARY KEY,                         -- Chave primária auto-incremento
    TipoOperacao INT NOT NULL CHECK (TipoOperacao IN (0, 1, 2, 3)), -- Tipo: 0=VENDA, 1=ALUGUEL, 2=MANUTENCAO, 3=DEVOLUCAO
    Descricao NVARCHAR(500) NOT NULL,                         -- Descrição detalhada da operação
    DataOperacao DATETIME2 NOT NULL DEFAULT GETDATE(),        -- Data e hora da operação
    MotoId INT NOT NULL,                                       -- Referência à moto envolvida
    UsuarioId INT NOT NULL,                                    -- Referência ao usuário que realizou a operação
    
    -- Chaves estrangeiras
    CONSTRAINT FK_Operacoes_Moto FOREIGN KEY (MotoId) 
        REFERENCES Motos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Operacoes_Usuario FOREIGN KEY (UsuarioId) 
        REFERENCES Usuarios(Id) ON DELETE NO ACTION
);

-- Índices para otimização de consultas na tabela Operacoes
CREATE INDEX IX_Operacoes_TipoOperacao ON Operacoes(TipoOperacao);
CREATE INDEX IX_Operacoes_DataOperacao ON Operacoes(DataOperacao);
CREATE INDEX IX_Operacoes_MotoId ON Operacoes(MotoId);
CREATE INDEX IX_Operacoes_UsuarioId ON Operacoes(UsuarioId);

-- =====================================================

-- Criação da tabela de Status das Motos
-- Responsável por manter histórico detalhado dos status das motos
CREATE TABLE StatusMotos (
    Id INT IDENTITY(1,1) PRIMARY KEY,                         -- Chave primária auto-incremento
    Status INT NOT NULL CHECK (Status IN (0, 1, 2)),          -- Status: 0=PENDENTE, 1=CONCLUIDA, 2=CANCELADA
    Descricao NVARCHAR(500) NOT NULL,                         -- Descrição detalhada do status
    Area NVARCHAR(100) NULL,                                  -- Área/local onde se encontra a moto (opcional)
    DataStatus DATETIME2 NOT NULL DEFAULT GETDATE(),          -- Data e hora da mudança de status
    MotoId INT NOT NULL,                                       -- Referência à moto
    UsuarioId INT NOT NULL,                                    -- Referência ao usuário que alterou o status
    
    -- Chaves estrangeiras
    CONSTRAINT FK_StatusMotos_Moto FOREIGN KEY (MotoId) 
        REFERENCES Motos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_StatusMotos_Usuario FOREIGN KEY (UsuarioId) 
        REFERENCES Usuarios(Id) ON DELETE NO ACTION
);

-- Índices para otimização de consultas na tabela StatusMotos
CREATE INDEX IX_StatusMotos_Status ON StatusMotos(Status);
CREATE INDEX IX_StatusMotos_DataStatus ON StatusMotos(DataStatus);
CREATE INDEX IX_StatusMotos_MotoId ON StatusMotos(MotoId);
CREATE INDEX IX_StatusMotos_UsuarioId ON StatusMotos(UsuarioId);
CREATE INDEX IX_StatusMotos_Area ON StatusMotos(Area);

-- =====================================================
-- INSERÇÃO DE DADOS DE EXEMPLO
-- =====================================================

-- Inserir usuários de exemplo
INSERT INTO Usuarios (NomeFilial, Email, SenhaHash, Cnpj, Endereco, Telefone, Perfil) VALUES
('Matriz São Paulo', 'admin@trackzone.com', '$2a$11$hashedpassword1', '12345678000195', 'Av. Paulista, 1000, São Paulo - SP', '(11) 99999-0001', 0),
('Filial Rio de Janeiro', 'gerente@trackzone.com', '$2a$11$hashedpassword2', '98765432000186', 'Av. Copacabana, 500, Rio de Janeiro - RJ', '(21) 99999-0002', 1),
('Filial Belo Horizonte', 'operador@trackzone.com', '$2a$11$hashedpassword3', '11122233000177', 'Av. Afonso Pena, 200, Belo Horizonte - MG', '(31) 99999-0003', 2);

-- Inserir motos de exemplo
INSERT INTO Motos (Placa, Chassi, Motor, Status, UsuarioId) VALUES
('ABC1234', '9BWHE21JX24060831', 'Honda CB600F Hornet', 0, 1),
('DEF5678', '9BWSU19FX24001234', 'Yamaha MT-07', 1, 2),
('GHI9012', '9BWDB11JX24005678', 'Kawasaki Ninja 650', 0, 3);

-- Inserir operações de exemplo
INSERT INTO Operacoes (TipoOperacao, Descricao, MotoId, UsuarioId) VALUES
(1, 'Aluguel para cliente João Silva - Período: 30 dias', 1, 1),
(2, 'Manutenção preventiva - Troca de óleo e filtros', 2, 2),
(1, 'Aluguel para cliente Maria Santos - Período: 15 dias', 3, 3);

-- Inserir status das motos de exemplo
INSERT INTO StatusMotos (Status, Descricao, Area, MotoId, UsuarioId) VALUES
(1, 'Moto disponível para aluguel na garagem principal', 'Garagem A - Setor 1', 1, 1),
(1, 'Moto em uso pelo cliente - Aluguel ativo', 'Cliente Externo', 2, 2),
(0, 'Aguardando manutenção preventiva - Agendada para próxima semana', 'Oficina B - Setor 2', 3, 3);

-- =====================================================
-- VIEWS PARA RELATÓRIOS
-- =====================================================

-- View para relatório completo de motos com usuário responsável
CREATE VIEW vw_MotosCompletas AS
SELECT 
    m.Id,
    m.Placa,
    m.Chassi,
    m.Motor,
    CASE m.Status 
        WHEN 0 THEN 'DISPONIVEL'
        WHEN 1 THEN 'ALUGADA'
        WHEN 2 THEN 'MANUTENCAO'
        WHEN 3 THEN 'VENDIDA'
    END AS StatusDescricao,
    u.NomeFilial,
    u.Email AS UsuarioResponsavel,
    m.DataCriacao,
    m.DataAtualizacao
FROM Motos m
INNER JOIN Usuarios u ON m.UsuarioId = u.Id;

-- View para histórico de operações com detalhes
CREATE VIEW vw_HistoricoOperacoes AS
SELECT 
    o.Id,
    CASE o.TipoOperacao 
        WHEN 0 THEN 'VENDA'
        WHEN 1 THEN 'ALUGUEL'
        WHEN 2 THEN 'MANUTENCAO'
        WHEN 3 THEN 'DEVOLUCAO'
    END AS TipoOperacaoDescricao,
    o.Descricao,
    o.DataOperacao,
    m.Placa AS MotoPlaca,
    m.Chassi AS MotoChassi,
    u.NomeFilial,
    u.Email AS UsuarioOperacao
FROM Operacoes o
INNER JOIN Motos m ON o.MotoId = m.Id
INNER JOIN Usuarios u ON o.UsuarioId = u.Id;

-- =====================================================
-- COMENTÁRIOS ADICIONAIS
-- =====================================================

/*
OBSERVAÇÕES IMPORTANTES:

1. PERFIS DE USUÁRIO:
   - 0 = ADMIN: Acesso completo ao sistema
   - 1 = GERENTE: Acesso a operações e relatórios
   - 2 = OPERADOR: Acesso básico para operações diárias

2. STATUS DAS MOTOS:
   - 0 = DISPONIVEL: Moto pronta para uso
   - 1 = ALUGADA: Moto em uso por cliente
   - 2 = MANUTENCAO: Moto em manutenção
   - 3 = VENDIDA: Moto vendida (histórico)

3. TIPOS DE OPERAÇÃO:
   - 0 = VENDA: Venda definitiva da moto
   - 1 = ALUGUEL: Aluguel temporário
   - 2 = MANUTENCAO: Operação de manutenção
   - 3 = DEVOLUCAO: Devolução de aluguel

4. STATUS DE OPERAÇÃO (StatusMotos):
   - 0 = PENDENTE: Operação aguardando execução
   - 1 = CONCLUIDA: Operação finalizada com sucesso
   - 2 = CANCELADA: Operação cancelada

5. ÍNDICES CRIADOS:
   - Otimizam consultas por email, CNPJ, status, placas e datas
   - Melhoram performance em joins e filtros
   - Suportam consultas de relatórios complexos

6. INTEGRIDADE REFERENCIAL:
   - Cascade em Motos: ao deletar usuário, deleta motos associadas
   - No Action em Operações: preserva histórico mesmo se usuário for removido
   - Garantem consistência dos dados

7. VIEWS CRIADAS:
   - vw_MotosCompletas: Visão consolidada de motos com usuários
   - vw_HistoricoOperacoes: Histórico completo de operações
   - Facilitam relatórios e consultas complexas
*/