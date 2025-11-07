-- REMOVIDO: este script era específico para Oracle.
-- O projeto agora usa SQL Server. Mova/adicione scripts T-SQL para SQL Server se necessário.
-- ============================================

-- Sequences
CREATE SEQUENCE seq_usuarios START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_motos START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_status_motos START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_operacoes START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE seq_dashboard START WITH 1 INCREMENT BY 1;

-- Tabela de usuários
CREATE TABLE usuarios (
    id NUMBER DEFAULT seq_usuarios.NEXTVAL PRIMARY KEY,
    nome_filial VARCHAR2(100) NOT NULL,
    email VARCHAR2(255) NOT NULL,
    senha_hash VARCHAR2(255) NOT NULL,
    cnpj VARCHAR2(18) NOT NULL,
    endereco VARCHAR2(255),
    telefone VARCHAR2(20),
    perfil VARCHAR2(20) NOT NULL,
    data_criacao TIMESTAMP DEFAULT SYSTIMESTAMP,
    CONSTRAINT uk_usuario_email UNIQUE (email),
    CONSTRAINT uk_usuario_cnpj UNIQUE (cnpj),
    CONSTRAINT ck_perfil CHECK (perfil IN ('ADMIN', 'GERENTE', 'OPERADOR'))
);

-- Tabela de motos
CREATE TABLE motos (
    id NUMBER DEFAULT seq_motos.NEXTVAL PRIMARY KEY,
    placa VARCHAR2(10) NOT NULL,
    chassi VARCHAR2(50) NOT NULL,
    motor VARCHAR2(50),
    usuario_id NUMBER,
    data_criacao TIMESTAMP DEFAULT SYSTIMESTAMP,
    CONSTRAINT fk_moto_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    CONSTRAINT uk_moto_placa UNIQUE (placa),
    CONSTRAINT uk_moto_chassi UNIQUE (chassi)
);

-- Tabela de status das motos
CREATE TABLE status_motos (
    id NUMBER DEFAULT seq_status_motos.NEXTVAL PRIMARY KEY,
    moto_id NUMBER,
    status VARCHAR2(50) NOT NULL,
    area VARCHAR2(50) NOT NULL,
    usuario_id NUMBER,
    data_criacao TIMESTAMP DEFAULT SYSTIMESTAMP,
    CONSTRAINT fk_status_moto FOREIGN KEY (moto_id) REFERENCES motos(id),
    CONSTRAINT fk_status_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    CONSTRAINT ck_status CHECK (status IN (
        'PENDENTE', 'REPARO_SIMPLES', 'DANOS_ESTRUTURAIS',
        'MOTOR_DEFEITUOSO', 'MANUTENCAO_AGENDADA',
        'PRONTA', 'SEM_PLACA', 'ALUGADA', 'AGUARDANDO_ALUGUEL'
    ))
);

-- Tabela de operações
CREATE TABLE operacoes (
    id NUMBER DEFAULT seq_operacoes.NEXTVAL PRIMARY KEY,
    moto_id NUMBER,
    tipo_operacao VARCHAR2(20) NOT NULL,
    usuario_id NUMBER,
    observacoes CLOB,
    data_criacao TIMESTAMP DEFAULT SYSTIMESTAMP,
    CONSTRAINT fk_operacao_moto FOREIGN KEY (moto_id) REFERENCES motos(id),
    CONSTRAINT fk_operacao_usuario FOREIGN KEY (usuario_id) REFERENCES usuarios(id),
    CONSTRAINT ck_tipo_operacao CHECK (tipo_operacao IN ('CHECK_IN', 'CHECK_OUT'))
);

-- Tabela de dashboard
CREATE TABLE dashboard (
    id NUMBER DEFAULT seq_dashboard.NEXTVAL PRIMARY KEY,
    total_motos NUMBER,
    motos_disponiveis NUMBER,
    motos_em_manutencao NUMBER,
    motos_alugadas NUMBER,
    data_atualizacao TIMESTAMP DEFAULT SYSTIMESTAMP
);

-- ============================================
-- V2: Inserção de dados de exemplo
-- ============================================

-- Inserir usuários de exemplo
INSERT INTO usuarios (nome_filial, email, senha_hash, cnpj, endereco, telefone, perfil) VALUES
('MotoCenter SP', 'admin@motocenter.com', '$2a$11$rQZ8K9vL2nM3pO4qR5sT6uV7wX8yZ9aB0cD1eF2gH3iJ4kL5mN6oP7qR8sT9uV', '12.345.678/0001-90', 'Rua das Motos, 123', '(11) 99999-9999', 'ADMIN');

INSERT INTO usuarios (nome_filial, email, senha_hash, cnpj, endereco, telefone, perfil) VALUES
('BikeStore RJ', 'gerente@bikestore.com', '$2a$11$rQZ8K9vL2nM3pO4qR5sT6uV7wX8yZ9aB0cD1eF2gH3iJ4kL5mN6oP7qR8sT9uV', '98.765.432/0001-10', 'Av. das Bicicletas, 456', '(21) 88888-8888', 'GERENTE');

-- Inserir motos de exemplo
INSERT INTO motos (placa, chassi, motor, usuario_id) VALUES
('ABC-1234', 'CHASSI001', 'Motor 150cc', 1);

INSERT INTO motos (placa, chassi, motor, usuario_id) VALUES
('XYZ-5678', 'CHASSI002', 'Motor 200cc', 1);

-- Inserir status de exemplo
INSERT INTO status_motos (moto_id, status, area, usuario_id) VALUES
(1, 'PRONTA', 'GARAGEM_A', 1);

INSERT INTO status_motos (moto_id, status, area, usuario_id) VALUES
(2, 'MANUTENCAO_AGENDADA', 'OFICINA_B', 1);

-- Inserir operações de exemplo
INSERT INTO operacoes (moto_id, tipo_operacao, usuario_id, observacoes) VALUES
(1, 'CHECK_IN', 1, 'Moto chegou na garagem');

INSERT INTO operacoes (moto_id, tipo_operacao, usuario_id, observacoes) VALUES
(2, 'CHECK_OUT', 1, 'Moto saiu para manutenção');

-- Inserir dados do dashboard
INSERT INTO dashboard (total_motos, motos_disponiveis, motos_em_manutencao, motos_alugadas) VALUES
(2, 1, 1, 0);

COMMIT;
