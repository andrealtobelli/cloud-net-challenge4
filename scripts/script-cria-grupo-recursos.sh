#!/bin/bash

# Variáveis
RESOURCE_GROUP="rg-trackzone"
LOCATION="brazilsouth"

# Criar grupo de recursos
echo "Criando grupo de recursos: $RESOURCE_GROUP..."
az group create --name $RESOURCE_GROUP --location $LOCATION

echo "✅ Grupo de recursos criado com sucesso!"
