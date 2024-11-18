using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoEnergy_GS.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ENDERECO",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CEP = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RUA = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    NUMERO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID_ENDERECO);
                });

            migrationBuilder.CreateTable(
                name: "RECOMPENSAS",
                columns: table => new
                {
                    ID_RECOMPENSAS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PONTOS_NECESSARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RECOMPENSAS", x => x.ID_RECOMPENSAS);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_ELETRODOMESTICO",
                columns: table => new
                {
                    ID_ELETRODOMESTICO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_ELETRODOMESTICO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QUANTIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_ELETRODOMESTICO", x => x.ID_ELETRODOMESTICO);
                });

            migrationBuilder.CreateTable(
                name: "USUARIOS",
                columns: table => new
                {
                    ID_USUARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PONTOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIOS", x => x.ID_USUARIOS);
                });

            migrationBuilder.CreateTable(
                name: "HISTORICO_PONTOS",
                columns: table => new
                {
                    ID_HISTORICO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_HISTORICO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    QUANTIDADE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USUARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HISTORICO_PONTOS", x => x.ID_HISTORICO);
                    table.ForeignKey(
                        name: "FK_HISTORICO_PONTOS_USUARIOS_ID_USUARIOS",
                        column: x => x.ID_USUARIOS,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIOS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RESIDENCIA",
                columns: table => new
                {
                    ID_RESIDENCIA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DISPOSITIVO_MONITORAMENTO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    QUANTIDADE_PESSOAS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MEDIA_CONSUMO = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    ID_USUARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_ELETRODOMESTICO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESIDENCIA", x => x.ID_RESIDENCIA);
                    table.ForeignKey(
                        name: "FK_RESIDENCIA_ENDERECO_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "ENDERECO",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESIDENCIA_TIPO_ELETRODOMESTICO_ID_ELETRODOMESTICO",
                        column: x => x.ID_ELETRODOMESTICO,
                        principalTable: "TIPO_ELETRODOMESTICO",
                        principalColumn: "ID_ELETRODOMESTICO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RESIDENCIA_USUARIOS_ID_USUARIOS",
                        column: x => x.ID_USUARIOS,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIOS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TROCAS_RECOMPENSAS",
                columns: table => new
                {
                    ID_TROCAS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_TROCA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PONTOS_UTILIZADOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USUARIOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_RECOMPENSAS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TROCAS_RECOMPENSAS", x => x.ID_TROCAS);
                    table.ForeignKey(
                        name: "FK_TROCAS_RECOMPENSAS_RECOMPENSAS_ID_RECOMPENSAS",
                        column: x => x.ID_RECOMPENSAS,
                        principalTable: "RECOMPENSAS",
                        principalColumn: "ID_RECOMPENSAS",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TROCAS_RECOMPENSAS_USUARIOS_ID_USUARIOS",
                        column: x => x.ID_USUARIOS,
                        principalTable: "USUARIOS",
                        principalColumn: "ID_USUARIOS",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CONSUMO_ENERGIA",
                columns: table => new
                {
                    ID_CONSUMO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DATA_CONSUMO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    CONSUMO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_RESIDENCIA = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONSUMO_ENERGIA", x => x.ID_CONSUMO);
                    table.ForeignKey(
                        name: "FK_CONSUMO_ENERGIA_RESIDENCIA_ID_RESIDENCIA",
                        column: x => x.ID_RESIDENCIA,
                        principalTable: "RESIDENCIA",
                        principalColumn: "ID_RESIDENCIA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONSUMO_ENERGIA_ID_RESIDENCIA",
                table: "CONSUMO_ENERGIA",
                column: "ID_RESIDENCIA");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_PONTOS_ID_USUARIOS",
                table: "HISTORICO_PONTOS",
                column: "ID_USUARIOS");

            migrationBuilder.CreateIndex(
                name: "IX_RESIDENCIA_ID_ELETRODOMESTICO",
                table: "RESIDENCIA",
                column: "ID_ELETRODOMESTICO");

            migrationBuilder.CreateIndex(
                name: "IX_RESIDENCIA_ID_ENDERECO",
                table: "RESIDENCIA",
                column: "ID_ENDERECO");

            migrationBuilder.CreateIndex(
                name: "IX_RESIDENCIA_ID_USUARIOS",
                table: "RESIDENCIA",
                column: "ID_USUARIOS");

            migrationBuilder.CreateIndex(
                name: "IX_TROCAS_RECOMPENSAS_ID_RECOMPENSAS",
                table: "TROCAS_RECOMPENSAS",
                column: "ID_RECOMPENSAS");

            migrationBuilder.CreateIndex(
                name: "IX_TROCAS_RECOMPENSAS_ID_USUARIOS",
                table: "TROCAS_RECOMPENSAS",
                column: "ID_USUARIOS");

            //Procedure para inserir usuários
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_usuario(
                    p_id_usuarios IN usuarios.id_usuarios%TYPE,
                    p_nome IN usuarios.nome%TYPE,
                    p_senha IN usuarios.senha%TYPE,
                    p_telefone IN usuarios.telefone%TYPE,
                    p_pontos IN usuarios.pontos%TYPE
                ) AS
                BEGIN
                    INSERT INTO usuarios (id_usuarios, nome, senha, telefone, pontos)
                    VALUES (p_id_usuarios, p_nome, p_senha, p_telefone, p_pontos);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Já existe um usuário com este ID.');
                    WHEN VALUE_ERROR THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Valor inválido foi fornecido.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir usuário: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir tipos de eletrodomestico
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_tipo_eletrodomestico(
                    p_id_eletrodomestico IN tipo_eletrodomestico.id_eletrodomestico%TYPE,
                    p_nome_eletrodomestico IN tipo_eletrodomestico.nome_eletrodomestico%TYPE,
                    p_quantidade IN tipo_eletrodomestico.quantidade%TYPE
                ) AS
                BEGIN
                    INSERT INTO tipo_eletrodomestico (id_eletrodomestico, nome_eletrodomestico, quantidade)
                    VALUES (p_id_eletrodomestico, p_nome_eletrodomestico, p_quantidade);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Eletrodoméstico já cadastrado com este ID.');
                    WHEN ZERO_DIVIDE THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Tentativa de divisão por zero.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir eletrodoméstico: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir endereço
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_endereco(
                    p_id_endereco IN endereco.id_endereco%TYPE,
                    p_cep IN endereco.cep%TYPE,
                    p_rua IN endereco.rua%TYPE,
                    p_numero IN endereco.numero%TYPE,
                    p_complemento IN endereco.complemento%TYPE
                ) AS
                BEGIN
                    INSERT INTO endereco (id_endereco, cep, rua, numero, complemento)
                    VALUES (p_id_endereco, p_cep, p_rua, p_numero, p_complemento);
                    COMMIT;
                EXCEPTION
                    WHEN NO_DATA_FOUND THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Não foram encontrados dados para a inserção.');
                    WHEN TOO_MANY_ROWS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Mais de um resultado encontrado ao tentar inserir.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir endereço: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir recompensa
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_recompensa(
                    p_id_recompensas IN recompensas.id_recompensas%TYPE,
                    p_descricao IN recompensas.descricao%TYPE,
                    p_pontos_necessarios IN recompensas.pontos_necessarios%TYPE
                ) AS
                BEGIN
                    INSERT INTO recompensas (id_recompensas, descricao, pontos_necessarios)
                    VALUES (p_id_recompensas, p_descricao, p_pontos_necessarios);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Já existe uma recompensa com este ID.');
                    WHEN VALUE_ERROR THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Valor inválido para pontos necessários.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir recompensa: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir residência
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_residencia(
                    p_id_residencia IN residencia.id_residencia%TYPE,
                    p_dispositivo_monitoramento IN residencia.dispositivo_monitoramento%TYPE,
                    p_quantidade_pessoas IN residencia.quantidade_pessoas%TYPE,
                    p_media_consumo IN residencia.media_consumo%TYPE,
                    p_id_usuarios IN residencia.id_usuarios%TYPE,
                    p_id_eletrodomestico IN residencia.id_eletrodomestico%TYPE,
                    p_id_endereco IN residencia.id_endereco%TYPE
                ) AS
                BEGIN
                    INSERT INTO residencia (id_residencia, dispositivo_monitoramento, quantidade_pessoas, media_consumo, id_usuarios, id_eletrodomestico, id_endereco)
                    VALUES (p_id_residencia, p_dispositivo_monitoramento, p_quantidade_pessoas, p_media_consumo, p_id_usuarios, p_id_eletrodomestico, p_id_endereco);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Residência já cadastrada com este ID.');
                    WHEN NO_DATA_FOUND THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Usuário ou endereço associado não encontrado.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir residência: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir consumo de energia
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_consumo_energia(
                    p_id_consumo IN consumo_energia.id_consumo%TYPE,
                    p_data_consumo IN consumo_energia.data_consumo%TYPE,
                    p_consumo IN consumo_energia.consumo%TYPE,
                    p_id_residencia IN consumo_energia.id_residencia%TYPE
                ) AS
                BEGIN
                    INSERT INTO consumo_energia (id_consumo, data_consumo, consumo, id_residencia)
                    VALUES (p_id_consumo, p_data_consumo, p_consumo, p_id_residencia);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Já existe um registro de consumo com este ID.');
                    WHEN NO_DATA_FOUND THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Residência não encontrada.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir consumo de energia: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir historico de pontos
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_historico_pontos(
                    p_id_historico IN historico_pontos.id_historico%TYPE,
                    p_data_historico IN historico_pontos.data_historico%TYPE,
                    p_quantidade IN historico_pontos.quantidade%TYPE,
                    p_id_usuarios IN historico_pontos.id_usuarios%TYPE
                ) AS
                BEGIN
                    INSERT INTO historico_pontos (id_historico, data_historico, quantidade, id_usuarios)
                    VALUES (p_id_historico, p_data_historico, p_quantidade, p_id_usuarios);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Já existe um histórico com este ID.');
                    WHEN NO_DATA_FOUND THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Usuário associado não encontrado.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir histórico de pontos: ' || SQLERRM);
                END;
                ");

            //Procedure para inserir troca de recompensas
            migrationBuilder.Sql(@"
                CREATE OR REPLACE PROCEDURE inserir_troca_recompensa(
                    p_id_troca IN trocas_recompensas.id_trocas%TYPE,
                    p_data_troca IN trocas_recompensas.data_troca%TYPE,
                    p_pontos_utilizados IN trocas_recompensas.pontos_utilizados%TYPE,
                    p_id_recompensas IN trocas_recompensas.id_recompensas%TYPE,
                    p_id_usuarios IN trocas_recompensas.id_usuarios%TYPE
                ) AS
                BEGIN
                    INSERT INTO trocas_recompensas (id_trocas, data_troca, pontos_utilizados, id_recompensas, id_usuarios)
                    VALUES (p_id_troca, p_data_troca, p_pontos_utilizados, p_id_recompensas, p_id_usuarios);
                    COMMIT;
                EXCEPTION
                    WHEN DUP_VAL_ON_INDEX THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Já existe uma troca com este ID.');
                    WHEN NO_DATA_FOUND THEN
                        DBMS_OUTPUT.PUT_LINE('Erro: Usuário ou recompensa associada não encontrado.');
                    WHEN OTHERS THEN
                        DBMS_OUTPUT.PUT_LINE('Erro ao inserir troca de recompensa: ' || SQLERRM);
                END;
                ");

            //Gatilho para atualizar os pontos de usuários
            migrationBuilder.Sql(@"
                CREATE OR REPLACE TRIGGER trg_atualizar_pontos_usuarios
                AFTER INSERT ON trocas_recompensas
                FOR EACH ROW
                DECLARE
                    v_pontos_usuario NUMBER(10, 2);
                BEGIN
                    SELECT pontos INTO v_pontos_usuario
                    FROM usuarios
                    WHERE id_usuarios = :NEW.id_usuarios;

                    IF v_pontos_usuario >= :NEW.pontos_utilizados THEN
                        UPDATE usuarios
                        SET pontos = pontos - :NEW.pontos_utilizados
                        WHERE id_usuarios = :NEW.id_usuarios;

                        UPDATE historico_pontos
                        SET quantidade = v_pontos_usuario - :NEW.pontos_utilizados, 
                            data_historico = SYSDATE  
                        WHERE id_usuarios = :NEW.id_usuarios;
                    ELSE
                        RAISE_APPLICATION_ERROR(-20001, 'Pontos insuficientes para realizar a troca.');
                    END IF;
                END;
                ");

            //Bloco de comando para inserir usuários
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_usuario(1, 'João Silva', '12345678900', '1987654', 300);
                    inserir_usuario(2, 'Maria Oliveira', '23456789001', '116543', 550);
                    inserir_usuario(3, 'Carlos Pereira', '34567890102', '115432', 400);
                    inserir_usuario(4, 'Ana Souza', '45678901203', '119821', 500);
                    inserir_usuario(5, 'Roberto Lima', '56789012304', '1192109', 650);
                    inserir_usuario(6, 'Cláudia Santos', '67890123405', '1821098', 800);
                    inserir_usuario(7, 'Pedro Costa', '78901234506', '1198985', 800);
                    inserir_usuario(8, 'Patrícia Alves', '89012345607', '1107654', 180);
                    inserir_usuario(9, 'Rafael Lima', '90123456708', '119543', 200);
                    inserir_usuario(10, 'Juliana Ribeiro', '01234567809', '1165432', 900);
                     COMMIT;
                END;
                ");

            //Bloco de comando para inserir tipo de eletrodoméstico
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_tipo_eletrodomestico(1, 'Geladeira', 2);
                    inserir_tipo_eletrodomestico(2, 'Máquina de Lavar', 1);
                    inserir_tipo_eletrodomestico(3, 'Microondas', 1);
                    inserir_tipo_eletrodomestico(4, 'Ar Condicionado', 4);
                    inserir_tipo_eletrodomestico(5, 'Televisão', 4);
                    inserir_tipo_eletrodomestico(6, 'Fogão', 1);
                    inserir_tipo_eletrodomestico(7, 'Secadora de Roupas', 1);
                    inserir_tipo_eletrodomestico(8, 'Aspirador de Pó', 2);
                    inserir_tipo_eletrodomestico(9, 'Máquina de Café', 2);
                    inserir_tipo_eletrodomestico(10, 'Forno Elétrico', 1);
                    COMMIT;
                END;
                ");

            //Bloco de comando para inserir endereço
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_endereco(1, '12345678', 'Av. Paulista', '1000', 'Apt. 101');
                    inserir_endereco(2, '23456789', 'Rua dos Três Irmãos', '250', 'Casa 12');
                    inserir_endereco(3, '34567890', 'Rua Sete de Setembro', '350', 'Apt. 202');
                    inserir_endereco(4, '45678901', 'Rua dos Três Rios', '410', 'Casa 8');
                    inserir_endereco(5, '56789012', 'Rua Amazonas', '500', 'Apt. 303');
                    inserir_endereco(6, '67890123', 'Rua da Consolação', '550', 'Casa 14');
                    inserir_endereco(7, '78901234', 'Av. Rio Branco', '600', 'Apt. 405');
                    inserir_endereco(8, '89012345', 'Rua Marquês de São Vicente', '700', 'Casa 15');
                    inserir_endereco(9, '90123456', 'Rua João Pessoa', '800', 'Apt. 506');
                    inserir_endereco(10, '01234567', 'Rua das Flores', '900', 'Casa 25');
                END;
                ");

            //Bloco de comando para inserir residência
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_residencia (1, 'Monitoramento 12323', 3, 450, 1, 1, 1); 
                    inserir_residencia(2, 'Monitoramento 248', 4, 500, 2, 2, 2);
                    inserir_residencia(3, 'Monitoramento 345', 2, 600, 3, 3, 3);
                    inserir_residencia(4, 'Monitoramento 465', 5, 350, 4, 4, 4); 
                    inserir_residencia(5, 'Monitoramento 775', 3, 400, 5, 5, 5);
                    inserir_residencia(6, 'Monitoramento 645', 6, 500, 6, 6, 6);  
                    inserir_residencia(7, 'Monitoramento 744', 4, 250, 7, 7, 7); 
                    inserir_residencia(8, 'Monitoramento 888', 3, 550, 8, 8, 8); 
                    inserir_residencia(9, 'Monitoramento 90', 2, 300, 9, 9, 9); 
                    inserir_residencia(10, 'Monitoramento 100', 5, 700, 10, 10, 10); 
                END;
                ");

            //Bloco de comando para inserir consumo de energia
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_consumo_energia(1, TO_DATE('2024-10-01', 'YYYY-MM-DD'), 450, 1);
                    inserir_consumo_energia(2, TO_DATE('2024-10-02', 'YYYY-MM-DD'), 300, 2);
                    inserir_consumo_energia(3, TO_DATE('2024-10-03', 'YYYY-MM-DD'), 600, 3);
                    inserir_consumo_energia(4, TO_DATE('2024-10-04', 'YYYY-MM-DD'), 350, 4);
                    inserir_consumo_energia(5, TO_DATE('2024-10-05', 'YYYY-MM-DD'), 400, 5);
                    inserir_consumo_energia(6, TO_DATE('2024-10-06', 'YYYY-MM-DD'), 500, 6);
                    inserir_consumo_energia(7, TO_DATE('2024-10-07', 'YYYY-MM-DD'), 250, 7);
                    inserir_consumo_energia(8, TO_DATE('2024-10-08', 'YYYY-MM-DD'), 550, 8);
                    inserir_consumo_energia(9, TO_DATE('2024-10-09', 'YYYY-MM-DD'), 300, 9);
                    inserir_consumo_energia(10, TO_DATE('2024-10-10', 'YYYY-MM-DD'), 700, 10);
                END;
                ");

            //Bloco de comando para inserir recompensas
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_recompensa(1, 'Desconto em Gás de Cozinha 10% - Ultragaz', -300);
                    inserir_recompensa(2, 'Frete Grátis - Ultracargo', -500);
                    inserir_recompensa(3, '15% em Cursos e Eventos - FIA', -200);
                    inserir_recompensa(4, '1 Consultoria - SAP', -250);
                    inserir_recompensa(5, ' 5% de desconto em Veículos e Equipamentos - Mahindra', -350);
                    inserir_recompensa(6, 'Gás de Cozinha Gratuito - Ultragaz', -650);
                    inserir_recompensa(7, 'Desconto em Transporte - Ultracargo', -500);
                    inserir_recompensa(8, 'Voucher para Recarga de Gás - Ultragaz', -120);
                    inserir_recompensa(9, 'Crédito para Upgrade de Equipamentos - Mahindra', -180);
                    inserir_recompensa(10, 'Desconto em Transporte Internacional - Ultracargo', -700);
                END;
                ");

            //Bloco de comando para inserir histórico de pontos
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_historico_pontos(1, SYSDATE, 300, 1);
                    inserir_historico_pontos(2, SYSDATE, 550, 2);
                    inserir_historico_pontos(3, SYSDATE, 400, 3);
                    inserir_historico_pontos(4, SYSDATE, 500, 4);
                    inserir_historico_pontos(5, SYSDATE, 650, 5);
                    inserir_historico_pontos(6, SYSDATE, 800, 6);
                    inserir_historico_pontos(7, SYSDATE, 800, 7);
                    inserir_historico_pontos(8, SYSDATE, 180, 8);
                    inserir_historico_pontos(9, SYSDATE, 200, 9);
                    inserir_historico_pontos(10, SYSDATE, 900, 10);
                END;
                ");

            //Bloco de comando para inserir troca de recompensas
            migrationBuilder.Sql(@"
                BEGIN
                    inserir_troca_recompensa(1, TO_DATE('2024-11-01', 'YYYY-MM-DD'), 300, 1, 1);
                    inserir_troca_recompensa(2, TO_DATE('2024-11-02', 'YYYY-MM-DD'), 500, 2, 2);
                    inserir_troca_recompensa(3, TO_DATE('2024-11-03', 'YYYY-MM-DD'), 200, 3, 3);
                    inserir_troca_recompensa(4, TO_DATE('2024-11-04', 'YYYY-MM-DD'), 250, 4, 4);
                    inserir_troca_recompensa(5, TO_DATE('2024-11-05', 'YYYY-MM-DD'), 350, 5, 5);
                    inserir_troca_recompensa(6, TO_DATE('2024-11-06', 'YYYY-MM-DD'), 650, 6, 6);
                    inserir_troca_recompensa(7, TO_DATE('2024-11-07', 'YYYY-MM-DD'), 500, 7, 7);
                    inserir_troca_recompensa(8, TO_DATE('2024-11-08', 'YYYY-MM-DD'), 120, 8, 8);
                    inserir_troca_recompensa(9, TO_DATE('2024-11-09', 'YYYY-MM-DD'), 180, 9, 9);
                    inserir_troca_recompensa(10, TO_DATE('2024-11-10', 'YYYY-MM-DD'), 700, 10, 10); 
                END;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CONSUMO_ENERGIA");

            migrationBuilder.DropTable(
                name: "HISTORICO_PONTOS");

            migrationBuilder.DropTable(
                name: "TROCAS_RECOMPENSAS");

            migrationBuilder.DropTable(
                name: "RESIDENCIA");

            migrationBuilder.DropTable(
                name: "RECOMPENSAS");

            migrationBuilder.DropTable(
                name: "ENDERECO");

            migrationBuilder.DropTable(
                name: "TIPO_ELETRODOMESTICO");

            migrationBuilder.DropTable(
                name: "USUARIOS");
        }
    }
}
