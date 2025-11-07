using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace challenge_3_net.Migrations
{
    /// <inheritdoc />
    public partial class OracleDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PK_STATUS_MOTOS",
                table: "STATUS_MOTOS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OPERACOES",
                table: "OPERACOES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "USUARIOS");

            migrationBuilder.DropColumn(
                name: "DATA_ATUALIZACAO",
                table: "MOTOS");

            migrationBuilder.RenameTable(
                name: "USUARIOS",
                newName: "usuarios");

            migrationBuilder.RenameTable(
                name: "STATUS_MOTOS",
                newName: "status_motos");

            migrationBuilder.RenameTable(
                name: "OPERACOES",
                newName: "operacoes");

            migrationBuilder.RenameTable(
                name: "MOTOS",
                newName: "motos");

            migrationBuilder.RenameColumn(
                name: "TELEFONE",
                table: "usuarios",
                newName: "telefone");

            migrationBuilder.RenameColumn(
                name: "SENHA_HASH",
                table: "usuarios",
                newName: "senha_hash");

            migrationBuilder.RenameColumn(
                name: "PERFIL",
                table: "usuarios",
                newName: "perfil");

            migrationBuilder.RenameColumn(
                name: "NOME_FILIAL",
                table: "usuarios",
                newName: "nome_filial");

            migrationBuilder.RenameColumn(
                name: "ENDERECO",
                table: "usuarios",
                newName: "endereco");

            migrationBuilder.RenameColumn(
                name: "EMAIL",
                table: "usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "DATA_CRIACAO",
                table: "usuarios",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "usuarios",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "usuarios",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_EMAIL",
                table: "usuarios",
                newName: "IX_usuarios_email");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIOS_CNPJ",
                table: "usuarios",
                newName: "IX_usuarios_cnpj");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "status_motos",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "STATUS",
                table: "status_motos",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "MOTO_ID",
                table: "status_motos",
                newName: "moto_id");

            migrationBuilder.RenameColumn(
                name: "DESCRICAO",
                table: "status_motos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "AREA",
                table: "status_motos",
                newName: "area");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "status_motos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DATA_STATUS",
                table: "status_motos",
                newName: "data_criacao");

            migrationBuilder.RenameIndex(
                name: "IX_STATUS_MOTOS_USUARIO_ID",
                table: "status_motos",
                newName: "IX_status_motos_usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_STATUS_MOTOS_MOTO_ID",
                table: "status_motos",
                newName: "IX_status_motos_moto_id");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "operacoes",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "TIPO_OPERACAO",
                table: "operacoes",
                newName: "tipo_operacao");

            migrationBuilder.RenameColumn(
                name: "MOTO_ID",
                table: "operacoes",
                newName: "moto_id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "operacoes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DESCRICAO",
                table: "operacoes",
                newName: "observacoes");

            migrationBuilder.RenameColumn(
                name: "DATA_OPERACAO",
                table: "operacoes",
                newName: "data_criacao");

            migrationBuilder.RenameIndex(
                name: "IX_OPERACOES_USUARIO_ID",
                table: "operacoes",
                newName: "IX_operacoes_usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_OPERACOES_MOTO_ID",
                table: "operacoes",
                newName: "IX_operacoes_moto_id");

            migrationBuilder.RenameColumn(
                name: "USUARIO_ID",
                table: "motos",
                newName: "usuario_id");

            migrationBuilder.RenameColumn(
                name: "PLACA",
                table: "motos",
                newName: "placa");

            migrationBuilder.RenameColumn(
                name: "MOTOR",
                table: "motos",
                newName: "motor");

            migrationBuilder.RenameColumn(
                name: "DATA_CRIACAO",
                table: "motos",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "CHASSI",
                table: "motos",
                newName: "chassi");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "motos",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_USUARIO_ID",
                table: "motos",
                newName: "IX_motos_usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_PLACA",
                table: "motos",
                newName: "IX_motos_placa");

            migrationBuilder.RenameIndex(
                name: "IX_MOTOS_CHASSI",
                table: "motos",
                newName: "IX_motos_chassi");

            migrationBuilder.AlterColumn<string>(
                name: "perfil",
                table: "usuarios",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "nome_filial",
                table: "usuarios",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "endereco",
                table: "usuarios",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "status_motos",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "tipo_operacao",
                table: "operacoes",
                type: "NVARCHAR2(2000)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "NUMBER(10)");

            migrationBuilder.AlterColumn<string>(
                name: "motor",
                table: "motos",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "chassi",
                table: "motos",
                type: "NVARCHAR2(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(17)",
                oldMaxLength: 17);

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_status_motos",
                table: "status_motos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_operacoes",
                table: "operacoes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_motos",
                table: "motos",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_motos_usuarios_usuario_id",
                table: "motos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_operacoes_motos_moto_id",
                table: "operacoes",
                column: "moto_id",
                principalTable: "motos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_operacoes_usuarios_usuario_id",
                table: "operacoes",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_status_motos_motos_moto_id",
                table: "status_motos",
                column: "moto_id",
                principalTable: "motos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_status_motos_usuarios_usuario_id",
                table: "status_motos",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_motos_usuarios_usuario_id",
                table: "motos");

            migrationBuilder.DropForeignKey(
                name: "FK_operacoes_motos_moto_id",
                table: "operacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_operacoes_usuarios_usuario_id",
                table: "operacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_status_motos_motos_moto_id",
                table: "status_motos");

            migrationBuilder.DropForeignKey(
                name: "FK_status_motos_usuarios_usuario_id",
                table: "status_motos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarios",
                table: "usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_status_motos",
                table: "status_motos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_operacoes",
                table: "operacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_motos",
                table: "motos");

            migrationBuilder.RenameTable(
                name: "usuarios",
                newName: "USUARIOS");

            migrationBuilder.RenameTable(
                name: "status_motos",
                newName: "STATUS_MOTOS");

            migrationBuilder.RenameTable(
                name: "operacoes",
                newName: "OPERACOES");

            migrationBuilder.RenameTable(
                name: "motos",
                newName: "MOTOS");

            migrationBuilder.RenameColumn(
                name: "telefone",
                table: "USUARIOS",
                newName: "TELEFONE");

            migrationBuilder.RenameColumn(
                name: "senha_hash",
                table: "USUARIOS",
                newName: "SENHA_HASH");

            migrationBuilder.RenameColumn(
                name: "perfil",
                table: "USUARIOS",
                newName: "PERFIL");

            migrationBuilder.RenameColumn(
                name: "nome_filial",
                table: "USUARIOS",
                newName: "NOME_FILIAL");

            migrationBuilder.RenameColumn(
                name: "endereco",
                table: "USUARIOS",
                newName: "ENDERECO");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "USUARIOS",
                newName: "EMAIL");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "USUARIOS",
                newName: "DATA_CRIACAO");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "USUARIOS",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "USUARIOS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_email",
                table: "USUARIOS",
                newName: "IX_USUARIOS_EMAIL");

            migrationBuilder.RenameIndex(
                name: "IX_usuarios_cnpj",
                table: "USUARIOS",
                newName: "IX_USUARIOS_CNPJ");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "STATUS_MOTOS",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "STATUS_MOTOS",
                newName: "STATUS");

            migrationBuilder.RenameColumn(
                name: "moto_id",
                table: "STATUS_MOTOS",
                newName: "MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "STATUS_MOTOS",
                newName: "DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "area",
                table: "STATUS_MOTOS",
                newName: "AREA");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "STATUS_MOTOS",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "STATUS_MOTOS",
                newName: "DATA_STATUS");

            migrationBuilder.RenameIndex(
                name: "IX_status_motos_usuario_id",
                table: "STATUS_MOTOS",
                newName: "IX_STATUS_MOTOS_USUARIO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_status_motos_moto_id",
                table: "STATUS_MOTOS",
                newName: "IX_STATUS_MOTOS_MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "OPERACOES",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "tipo_operacao",
                table: "OPERACOES",
                newName: "TIPO_OPERACAO");

            migrationBuilder.RenameColumn(
                name: "moto_id",
                table: "OPERACOES",
                newName: "MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "OPERACOES",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "observacoes",
                table: "OPERACOES",
                newName: "DESCRICAO");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "OPERACOES",
                newName: "DATA_OPERACAO");

            migrationBuilder.RenameIndex(
                name: "IX_operacoes_usuario_id",
                table: "OPERACOES",
                newName: "IX_OPERACOES_USUARIO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_operacoes_moto_id",
                table: "OPERACOES",
                newName: "IX_OPERACOES_MOTO_ID");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "MOTOS",
                newName: "USUARIO_ID");

            migrationBuilder.RenameColumn(
                name: "placa",
                table: "MOTOS",
                newName: "PLACA");

            migrationBuilder.RenameColumn(
                name: "motor",
                table: "MOTOS",
                newName: "MOTOR");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "MOTOS",
                newName: "DATA_CRIACAO");

            migrationBuilder.RenameColumn(
                name: "chassi",
                table: "MOTOS",
                newName: "CHASSI");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MOTOS",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_motos_usuario_id",
                table: "MOTOS",
                newName: "IX_MOTOS_USUARIO_ID");

            migrationBuilder.RenameIndex(
                name: "IX_motos_placa",
                table: "MOTOS",
                newName: "IX_MOTOS_PLACA");

            migrationBuilder.RenameIndex(
                name: "IX_motos_chassi",
                table: "MOTOS",
                newName: "IX_MOTOS_CHASSI");

            migrationBuilder.AlterColumn<int>(
                name: "PERFIL",
                table: "USUARIOS",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "NOME_FILIAL",
                table: "USUARIOS",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ENDERECO",
                table: "USUARIOS",
                type: "NVARCHAR2(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "USUARIOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "STATUS",
                table: "STATUS_MOTOS",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<int>(
                name: "TIPO_OPERACAO",
                table: "OPERACOES",
                type: "NUMBER(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");

            migrationBuilder.AlterColumn<string>(
                name: "MOTOR",
                table: "MOTOS",
                type: "NVARCHAR2(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CHASSI",
                table: "MOTOS",
                type: "NVARCHAR2(17)",
                maxLength: 17,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<DateTime>(
                name: "DATA_ATUALIZACAO",
                table: "MOTOS",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_USUARIOS",
                table: "USUARIOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STATUS_MOTOS",
                table: "STATUS_MOTOS",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OPERACOES",
                table: "OPERACOES",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MOTOS",
                table: "MOTOS",
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
    }
}
