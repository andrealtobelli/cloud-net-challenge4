using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace challenge_3_net.Migrations
{
    /// <inheritdoc />
    public partial class OracleInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Motos_Usuarios_UsuarioId",
                table: "Motos");

            migrationBuilder.DropForeignKey(
                name: "FK_Operacoes_Motos_MotoId",
                table: "Operacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Operacoes_Usuarios_UsuarioId",
                table: "Operacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusMotos_Motos_MotoId",
                table: "StatusMotos");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusMotos_Usuarios_UsuarioId",
                table: "StatusMotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Operacoes",
                table: "Operacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Motos",
                table: "Motos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusMotos",
                table: "StatusMotos");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "USUARIOS");

            migrationBuilder.RenameTable(
                name: "Operacoes",
                newName: "OPERACOES");

            migrationBuilder.RenameTable(
                name: "Motos",
                newName: "MOTOS");

            migrationBuilder.RenameTable(
                name: "StatusMotos",
                newName: "STATUS_MOTOS");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "USUARIOS",
                newName: "TELEFONE");

            migrationBuilder.RenameColumn(
                name: "Perfil",
                table: "USUARIOS",
                newName: "PERFIL");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "USUARIOS",
                newName: "ENDERECO");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "USUARIOS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "Cnpj",
                table: "USUARIOS",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "USUARIOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "USUARIOS",
                newName: "SENHA_HASH");

            migrationBuilder.RenameColumn(
                name: "NomeFilial",
                table: "USUARIOS",
                newName: "NOME_FILIAL");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "USUARIOS",
                newName: "DATA_CRIACAO");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "USUARIOS",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Email",
                table: "USUARIOS",
                newName: "IX_USUARIOS_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_Cnpj",
                table: "USUARIOS",
                newName: "IX_USUARIOS_CNPJ");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "OPERACOES",
                newName: "DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OPERACOES",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "OPERACOES",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "TipoOperacao",
                table: "OPERACOES",
                newName: "TIPO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "MotoId",
                table: "OPERACOES",
                newName: "MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "DataOperacao",
                table: "OPERACOES",
                newName: "DATA_OPERACAO");

            migrationBuilder.RenameIndex(
                name: "IX_Operacoes_UsuarioId",
                table: "OPERACOES",
                newName: "IX_OPERACOES_USUARIO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Operacoes_MotoId",
                table: "OPERACOES",
                newName: "IX_OPERACOES_MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "Placa",
                table: "MOTOS",
                newName: "PLACA");

            migrationBuilder.RenameColumn(
                name: "Motor",
                table: "MOTOS",
                newName: "MOTOR");

            migrationBuilder.RenameColumn(
                name: "Chassi",
                table: "MOTOS",
                newName: "CHASSI");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MOTOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "MOTOS",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "MOTOS",
                newName: "DATA_CRIACAO");

            migrationBuilder.RenameColumn(
                name: "DataAtualizacao",
                table: "MOTOS",
                newName: "DATA_ATUALIZACAO");

            migrationBuilder.RenameIndex(
                name: "IX_Motos_Placa",
                table: "MOTOS",
                newName: "IX_MOTOS_PLACA");

            migrationBuilder.RenameIndex(
                name: "IX_Motos_Chassi",
                table: "MOTOS",
                newName: "IX_MOTOS_CHASSI");

            migrationBuilder.RenameIndex(
                name: "IX_Motos_UsuarioId",
                table: "MOTOS",
                newName: "IX_MOTOS_USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "STATUS_MOTOS",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "STATUS_MOTOS",
                newName: "DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "STATUS_MOTOS",
                newName: "AREA");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "STATUS_MOTOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "STATUS_MOTOS",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "MotoId",
                table: "STATUS_MOTOS",
                newName: "MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "DataStatus",
                table: "STATUS_MOTOS",
                newName: "DATA_STATUS");

            migrationBuilder.RenameIndex(
                name: "IX_StatusMotos_UsuarioId",
                table: "STATUS_MOTOS",
                newName: "IX_STATUS_MOTOS_USUARIO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_StatusMotos_MotoId",
                table: "STATUS_MOTOS",
                newName: "IX_STATUS_MOTOS_MOTO_ID");

            migrationBuilder.AlterColumn<string>(
                name: "TELEFONE",
                table: "USUARIOS",
                type: "NVARCHAR2(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PERFIL",
                table: "USUARIOS",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ENDERECO",
                table: "USUARIOS",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "USUARIOS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "USUARIOS",
                type: "NVARCHAR2(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(18)",
                oldMaxLength: 18);

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "USUARIOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "SENHA_HASH",
                table: "USUARIOS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NOME_FILIAL",
                table: "USUARIOS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_CRIACAO",
                table: "USUARIOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "USUARIOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRICAO",
                table: "OPERACOES",
                type: "NVARCHAR2(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "OPERACOES",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "USUARIO_ID",
                table: "OPERACOES",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "TIPO_OPERACAO",
                table: "OPERACOES",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "MOTO_ID",
                table: "OPERACOES",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_OPERACAO",
                table: "OPERACOES",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "PLACA",
                table: "MOTOS",
                type: "NVARCHAR2(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "MOTOR",
                table: "MOTOS",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CHASSI",
                table: "MOTOS",
                type: "NVARCHAR2(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(17)",
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "MOTOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "USUARIO_ID",
                table: "MOTOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_CRIACAO",
                table: "MOTOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "MOTOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "STATUS",
                table: "STATUS_MOTOS",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRICAO",
                table: "STATUS_MOTOS",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AREA",
                table: "STATUS_MOTOS",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "STATUS_MOTOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "USUARIO_ID",
                table: "STATUS_MOTOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "MOTO_ID",
                table: "STATUS_MOTOS",
                type: "NUMBER(19)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DATA_STATUS",
                table: "STATUS_MOTOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OPERACOES",
                table: "OPERACOES",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STATUS_MOTOS",
                table: "STATUS_MOTOS",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MOTOS_USUARIOS_USUARIO_ID",
                table: "MOTOS",
                column: "USUARIO_ID",
                principalTable: "USUARIOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OPERACOES_MOTOS_MOTO_ID",
                table: "OPERACOES",
                column: "MOTO_ID",
                principalTable: "MOTOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OPERACOES_USUARIOS_USUARIO_ID",
                table: "OPERACOES",
                column: "USUARIO_ID",
                principalTable: "USUARIOS",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_STATUS_MOTOS_MOTOS_MOTO_ID",
                table: "STATUS_MOTOS",
                column: "MOTO_ID",
                principalTable: "MOTOS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STATUS_MOTOS_USUARIOS_USUARIO_ID",
                table: "STATUS_MOTOS",
                column: "USUARIO_ID",
                principalTable: "USUARIOS",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MOTOS_USUARIOS_USUARIO_ID",
                table: "MOTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_OPERACOES_MOTOS_MOTO_ID",
                table: "OPERACOES");

            migrationBuilder.DropForeignKey(
                name: "FK_OPERACOES_USUARIOS_USUARIO_ID",
                table: "OPERACOES");

            migrationBuilder.DropForeignKey(
                name: "FK_STATUS_MOTOS_MOTOS_MOTO_ID",
                table: "STATUS_MOTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_STATUS_MOTOS_USUARIOS_USUARIO_ID",
                table: "STATUS_MOTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OPERACOES",
                table: "OPERACOES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STATUS_MOTOS",
                table: "STATUS_MOTOS");

            migrationBuilder.RenameTable(
                name: "USUARIOS",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "OPERACOES",
                newName: "Operacoes");

            migrationBuilder.RenameTable(
                name: "MOTOS",
                newName: "Motos");

            migrationBuilder.RenameTable(
                name: "STATUS_MOTOS",
                newName: "StatusMotos");

            migrationBuilder.RenameColumn(
                name: "TELEFONE",
                table: "Usuarios",
                newName: "Telefone");

            migrationBuilder.RenameColumn(
                name: "PERFIL",
                table: "Usuarios",
                newName: "Perfil");

            migrationBuilder.RenameColumn(
                name: "ENDERECO",
                table: "Usuarios",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "Usuarios",
                newName: "Cnpj");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SENHA_HASH",
                table: "Usuarios",
                newName: "SenhaHash");

            migrationBuilder.RenameColumn(
                name: "NOME_FILIAL",
                table: "Usuarios",
                newName: "NomeFilial");

            migrationBuilder.RenameColumn(
                name: "DATA_CRIACAO",
                table: "Usuarios",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "Usuarios",
                newName: "DataAtualizacao");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_EMAIL",
                table: "Usuarios",
                newName: "IX_Usuarios_Email");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_CNPJ",
                table: "Usuarios",
                newName: "IX_Usuarios_Cnpj");

            migrationBuilder.RenameColumn(
                name: "DESCRICAO",
                table: "Operacoes",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Operacoes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "Operacoes",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "TIPO_OPERACAO",
                table: "Operacoes",
                newName: "TipoOperacao");

            migrationBuilder.RenameColumn(
                name: "MOTO_ID",
                table: "Operacoes",
                newName: "MotoId");

            migrationBuilder.RenameColumn(
                name: "DATA_OPERACAO",
                table: "Operacoes",
                newName: "DataOperacao");

            migrationBuilder.RenameIndex(
                name: "IX_OPERACOES_USUARIO_ID",
                table: "Operacoes",
                newName: "IX_Operacoes_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_OPERACOES_MOTO_ID",
                table: "Operacoes",
                newName: "IX_Operacoes_MotoId");

            migrationBuilder.RenameColumn(
                name: "PLACA",
                table: "Motos",
                newName: "Placa");

            migrationBuilder.RenameColumn(
                name: "MOTOR",
                table: "Motos",
                newName: "Motor");

            migrationBuilder.RenameColumn(
                name: "CHASSI",
                table: "Motos",
                newName: "Chassi");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Motos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "Motos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "DATA_CRIACAO",
                table: "Motos",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "DATA_ATUALIZACAO",
                table: "Motos",
                newName: "DataAtualizacao");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_PLACA",
                table: "Motos",
                newName: "IX_Motos_Placa");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_CHASSI",
                table: "Motos",
                newName: "IX_Motos_Chassi");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_USUARIO_ID",
                table: "Motos",
                newName: "IX_Motos_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "StatusMotos",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DESCRICAO",
                table: "StatusMotos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "AREA",
                table: "StatusMotos",
                newName: "Area");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "StatusMotos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "StatusMotos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "MOTO_ID",
                table: "StatusMotos",
                newName: "MotoId");

            migrationBuilder.RenameColumn(
                name: "DATA_STATUS",
                table: "StatusMotos",
                newName: "DataStatus");

            migrationBuilder.RenameIndex(
                name: "IX_STATUS_MOTOS_USUARIO_ID",
                table: "StatusMotos",
                newName: "IX_StatusMotos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_STATUS_MOTOS_MOTO_ID",
                table: "StatusMotos",
                newName: "IX_StatusMotos_MotoId");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Perfil",
                table: "Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Endereco",
                table: "Usuarios",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Usuarios",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(18)",
                oldMaxLength: 18);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<string>(
                name: "SenhaHash",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NomeFilial",
                table: "Usuarios",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Operacoes",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Operacoes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Operacoes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<int>(
                name: "TipoOperacao",
                table: "Operacoes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<long>(
                name: "MotoId",
                table: "Operacoes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataOperacao",
                table: "Operacoes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AlterColumn<string>(
                name: "Placa",
                table: "Motos",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Motor",
                table: "Motos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Chassi",
                table: "Motos",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(17)",
                oldMaxLength: 17);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Motos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Motos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Motos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAtualizacao",
                table: "Motos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "StatusMotos",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "StatusMotos",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Area",
                table: "StatusMotos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "StatusMotos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)")
                .OldAnnotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "StatusMotos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<long>(
                name: "MotoId",
                table: "StatusMotos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "NUMBER(19)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataStatus",
                table: "StatusMotos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operacoes",
                table: "Operacoes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Motos",
                table: "Motos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusMotos",
                table: "StatusMotos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Motos_Usuarios_UsuarioId",
                table: "Motos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacoes_Motos_MotoId",
                table: "Operacoes",
                column: "MotoId",
                principalTable: "Motos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Operacoes_Usuarios_UsuarioId",
                table: "Operacoes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusMotos_Motos_MotoId",
                table: "StatusMotos",
                column: "MotoId",
                principalTable: "Motos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusMotos_Usuarios_UsuarioId",
                table: "StatusMotos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
