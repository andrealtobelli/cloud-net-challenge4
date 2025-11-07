-- Script para corrigir tipos de dados no banco
-- Atualizar campo Perfil de string para int

-- Verificar estrutura atual
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Usuarios' AND COLUMN_NAME = 'Perfil';

-- Se Perfil for varchar, vamos atualizar
UPDATE Usuarios 
SET Perfil = CASE 
    WHEN Perfil = '1' THEN 1
    WHEN Perfil = '2' THEN 2
    WHEN Perfil = '3' THEN 3
    ELSE 1
END;

-- Verificar se há dados
SELECT Id, NomeFilial, Email, Perfil FROM Usuarios;
