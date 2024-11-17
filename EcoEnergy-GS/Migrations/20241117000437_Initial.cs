using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoEnergy_GS.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Numero = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id_endereco);
                });

            migrationBuilder.CreateTable(
                name: "Recompensas",
                columns: table => new
                {
                    Id_recompensas = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Descricao = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Pontos_necessarios = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recompensas", x => x.Id_recompensas);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Eletrodomestico",
                columns: table => new
                {
                    Id_eletrodomestico = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Quantidade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Eletrodomestico", x => x.Id_eletrodomestico);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Pontos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Historico_Pontos",
                columns: table => new
                {
                    Id_historico = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Quantidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico_Pontos", x => x.Id_historico);
                    table.ForeignKey(
                        name: "FK_Historico_Pontos_Usuario_Id_usuario",
                        column: x => x.Id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "Id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Residencia",
                columns: table => new
                {
                    Id_residencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Dispotivico_monitoramento = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Quantidade_pessoas = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Media_consumo = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_eletrodomestico = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residencia", x => x.Id_residencia);
                    table.ForeignKey(
                        name: "FK_Residencia_Endereco_Id_endereco",
                        column: x => x.Id_endereco,
                        principalTable: "Endereco",
                        principalColumn: "Id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Residencia_Tipo_Eletrodomestico_Id_eletrodomestico",
                        column: x => x.Id_eletrodomestico,
                        principalTable: "Tipo_Eletrodomestico",
                        principalColumn: "Id_eletrodomestico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Residencia_Usuario_Id_usuario",
                        column: x => x.Id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "Id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trocas_recompensas",
                columns: table => new
                {
                    Id_trocas = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DataTroca = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Pontos_utilizados = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_usuario = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_recompensas = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trocas_recompensas", x => x.Id_trocas);
                    table.ForeignKey(
                        name: "FK_Trocas_recompensas_Recompensas_Id_recompensas",
                        column: x => x.Id_recompensas,
                        principalTable: "Recompensas",
                        principalColumn: "Id_recompensas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trocas_recompensas_Usuario_Id_usuario",
                        column: x => x.Id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "Id_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumo_Energia",
                columns: table => new
                {
                    Id_consumo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Consumo = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Id_residencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumo_Energia", x => x.Id_consumo);
                    table.ForeignKey(
                        name: "FK_Consumo_Energia_Residencia_Id_residencia",
                        column: x => x.Id_residencia,
                        principalTable: "Residencia",
                        principalColumn: "Id_residencia",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumo_Energia_Id_residencia",
                table: "Consumo_Energia",
                column: "Id_residencia");

            migrationBuilder.CreateIndex(
                name: "IX_Historico_Pontos_Id_usuario",
                table: "Historico_Pontos",
                column: "Id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Residencia_Id_eletrodomestico",
                table: "Residencia",
                column: "Id_eletrodomestico");

            migrationBuilder.CreateIndex(
                name: "IX_Residencia_Id_endereco",
                table: "Residencia",
                column: "Id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_Residencia_Id_usuario",
                table: "Residencia",
                column: "Id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_recompensas_Id_recompensas",
                table: "Trocas_recompensas",
                column: "Id_recompensas");

            migrationBuilder.CreateIndex(
                name: "IX_Trocas_recompensas_Id_usuario",
                table: "Trocas_recompensas",
                column: "Id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumo_Energia");

            migrationBuilder.DropTable(
                name: "Historico_Pontos");

            migrationBuilder.DropTable(
                name: "Trocas_recompensas");

            migrationBuilder.DropTable(
                name: "Residencia");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Tipo_Eletrodomestico");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
