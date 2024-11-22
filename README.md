# EcoEnergy_GS

## Introdução a Nossa solução
Com o avanço da tecnologia e sua integração crescente em nossas vidas, o mundo tornou-se mais conectado, e a circulação de informações atinge uma velocidade sem precedentes. Esse progresso trouxe não apenas uma proximidade global maior, mas também um aumento significativo no consumo de energia elétrica, impulsionado pelo uso constante de dispositivos conectados em nossas rotinas diárias.

Com isso em mente, desenvolvemos o projeto EcoEnergy, que oferece uma solução prática e acessível para monitorar o consumo de energia, aplicável tanto a residências quanto a propriedades comerciais. Utilizando um dispositivo conectado à internet, o usuário pode acompanhar seu consumo em tempo real. Além disso, o projeto inclui um sistema de pontos, que acumula recompensas de acordo com a economia gerada, permitindo a troca de pontos por cupons e outras vantagens.

## Objetivo
Nosso objetivo é desenvolver um programa que permite monitorar, em tempo real, o consumo de energia de propriedades residenciais e comerciais, incentivando a economia com um sistema de pontos. O programa gerará relatórios de consumo e recompensará usuários que economizarem energia com pontos, que poderão ser trocados por cupons de desconto e outras vantagens oferecidas por empresas parceiras.

## Descrição do Sistema
O sistema EcoEnergy foi projetado para monitorar o consumo de energia de propriedades residenciais e comerciais em tempo real, oferecendo aos usuários ferramentas para acompanhar seu consumo, identificar oportunidades de economia e ser recompensado por seus esforços. A plataforma é composta por um dispositivo de monitoramento de energia, uma base de dados para armazenar informações de consumo, um aplicativo e um website que permite ao usuário visualizar os dados e participar do sistema de pontos.

### 1. Pricipais Funcionalidades
 ●	Monitoramento em Tempo Real
O EcoEnergy captura dados de consumo de energia em tempo real e os transmite para o sistema central, onde são processados e armazenados. O usuário pode visualizar seu consumo instantaneamente, seja pelo aplicativo ou pela interface web, permitindo acompanhamento contínuo e controle sobre seus gastos de energia.

●	Geração de Relatórios
Com base nos dados coletados, o EcoEnergy gera relatórios de consumo com indicadores diários, mensais e anuais. Esses relatórios incluem médias de consumo, e comparações históricas, ajudando o usuário a entender melhor seu padrão de consumo e a tomar decisões informadas sobre a redução de gastos.

●	Sistema de Pontos e Recompensas
Para incentivar a economia de energia, o sistema utiliza um programa de pontos. Os usuários que atingem metas de economia ganham pontos, que podem ser trocados por cupons de desconto, produtos e outros benefícios oferecidos por empresas parceiras.

### 2. Componentes do Sistema
●	Dispositivo de Monitoramento
O sistema conta com um dispositivo de monitoramento de energia que mede o consumo elétrico e envia esses dados para o servidor central. Esse dispositivo é conectado à rede elétrica e transmite os dados via internet para a base de dados.

●	Base de Dados	
Todos os dados de consumo coletados pelo dispositivo são armazenados em uma base de dados segura, permitindo consulta e processamento para geração de relatórios e cálculo de pontos.

●	Aplicativo/Interface do Usuário
A plataforma oferece uma interface amigável, onde o usuário pode acessar seu histórico de consumo, gerar relatórios e verificar seu saldo de pontos. A interface é acessível via aplicativo mobile e interface web.

### 3. Fluxo de Funcionamento
1.	Coleta de Dados
O dispositivo de monitoramento coleta dados de consumo de energia e os transmite para o servidor central do EcoEnergy em tempo real.
2.	Armazenamento e Processamento
Os dados recebidos são armazenados na base de dados, onde são processados para criar métricas como consumo médio, picos de uso e quantidades economizadas.
3.	Geração de Relatórios e Acúmulo de Pontos
Com base nos dados processados, o sistema gera relatórios para o usuário. Além disso, calcula a quantidade economizada e, se o usuário atingir metas de economia, pontos são acumulados na conta.
4.	Resgate de Pontos 
O usuário pode visualizar seu saldo de pontos e trocá-los por recompensas diretamente na interface.

# Arquitetura da API

A escolha pela arquitetura monolítica para o desenvolvimento da nossa API foi baseada em fatores estratégicos relacionados ao time, ao prazo do projeto e à familiaridade com a tecnologia. Como estamos lidando com uma equipe reduzida e dispomos de um tempo limitado para a entrega, optar por uma solução monolítica nos permite concentrar esforços em uma única aplicação, facilitando o desenvolvimento e a manutenção.

Além disso, o conhecimento prévio da equipe com a tecnologia envolvida favoreceu essa abordagem, proporcionando mais agilidade no desenvolvimento. A arquitetura monolítica também simplifica a integração entre os componentes do sistema, o que é crucial em projetos com prazos apertados. Essa decisão nos permite entregar um produto funcional de maneira eficiente, sem a complexidade adicional que outras arquiteturas, como microsserviços, poderiam introduzir neste momento.

## IA
A **IA do EcoEnergy** foi projetada para calcular pontos de economia energética e fornecer recomendações com base no consumo de energia previsto. Utilizando dados de entrada fornecidos pelo usuário, ela processa as informações para estimar a economia potencial e incentivar a adoção de hábitos sustentáveis.  

---

## **Como a IA Funciona?**  

### 1. Entrada de Dados  
O sistema recebe os seguintes dados como entrada:  
- **Hora:** Representando o momento do dia em que o consumo foi registrado.  
- **Temperatura:** Dados ambientais que impactam o uso de aparelhos como ar-condicionado ou aquecedores.  
- **Consumo Atual:** O consumo registrado em tempo real pelo dispositivo de monitoramento.  
- **Consumo Previsto:** Uma estimativa do consumo ideal calculada com base em padrões históricos e condições ambientais.  

---

### 2. Cálculo de Pontos  
A economia é determinada comparando o **consumo atual** com o **consumo previsto**. O número de pontos gerados é proporcional à economia obtida:  

#### Fórmulas:  
- Economia:  
    (ConsumoAtual - ConsumoPrevisto) * 0.1f
- Pontuação:  
    porcentagem = (Economia / ConsumoAtual) * 100

    Round(porcentagem * 10)

Quanto maior a economia, mais pontos são atribuídos, incentivando o usuário a reduzir o consumo de forma sustentável.  

---

### 3. Saída de Dados  
Após processar os dados, a IA retorna:  
- A pontuação baseada na economia calculada.  
- Uma recomendação personalizada para o usuário.  

---

## **Exemplo de Funcionamento**  

### **Entrada:**  
```json
{
  "Hora": "2024-11-01T10:00:00",
  "Temperatura": 25.0,
  "ConsumoAtual": 700,
  "ConsumoPrevisto": 680
}
```

### **Saída:**  
```json
{
  "recommendation": "Você economizou energia! Continue assim para reduzir ainda mais o seu consumo.",
  "points": 33
}
```

### Benefícios e Valor Agregado

**1. Otimização do Consumo de Energia:**
A IA integrada ao sistema permite a análise preditiva do consumo de energia com base em variáveis como temperatura, hora do dia, e consumo atual. Isso proporciona uma recomendação precisa para otimizar o uso de energia, ajudando tanto consumidores residenciais quanto empresas a economizar e melhorar a eficiência energética.

**2. Redução de Custos:**
Ao fornecer insights sobre o consumo de energia e sugerir ajustes para reduzir desperdícios, o sistema contribui diretamente para a redução de custos operacionais, principalmente em empresas ou indústrias que dependem de grandes quantidades de energia.

**3. Sustentabilidade e Conformidade Ambiental:**
O uso de tecnologias de IA para monitorar e otimizar o consumo de energia também está alinhado com as práticas de sustentabilidade. Reduzir o consumo de energia não só gera economia financeira, mas também contribui para a diminuição da pegada de carbono da organização ou indivíduo, ajudando a cumprir as metas ambientais e regulamentações de eficiência energética.

**4. Personalização das Recomendação:**
As recomendações geradas pela IA são personalizadas para cada contexto de uso, levando em consideração dados específicos como a temperatura e o histórico de consumo. Isso garante que as sugestões sejam altamente relevantes e aplicáveis, maximizando as chances de impacto positivo no consumo energético.

**5. Aumento da Eficiência Operacional:**
Ao oferecer insights claros e objetivos sobre como melhorar a eficiência do uso de energia, o sistema ajuda os usuários a tomar decisões mais informadas e práticas. Isso pode resultar em uma operação mais eficiente, com menos desperdício e maior controle sobre os custos de energia.

**6. Escalabilidade:**
Este sistema é altamente escalável e pode ser implementado em diversos contextos, desde residências até grandes indústrias, com a mesma eficácia. À medida que mais dados são coletados, a IA pode continuar a aprender e aprimorar as previsões, adaptando-se às necessidades específicas de diferentes ambientes de consumo.

# Testes xUnit
Os testes deste projeto foram desenvolvidos para garantir a funcionalidade e a estabilidade da API. Eles cobrem as operações essenciais de cada endpoint.

## Banco de dados
### Detalhe na migration
A migration configurada no projeto inclui procedimentos armazenados (stored procedures) e gatilhos (triggers) para garantir a integridade e o fluxo de dados. Especificamente:

- Procedures: Foram criados procedimentos para adicionar dados às tabelas. Cada procedimento insere 10 linhas em suas respectivas tabelas, facilitando a alimentação inicial do banco de dados.

- Triggers: A migração também configura gatilhos para garantir o correto gerenciamento dos pontos do usuário. A trigger verifica se o usuário possui pontos suficientes para resgatar uma recompensa. Após o resgate, o gatilho é acionado para atualizar os pontos do usuário, refletindo a quantidade restante após a utilização.

Essa abordagem permite uma gestão eficiente e automatizada dos dados no sistema, mantendo a integridade e o controle necessário para as interações dos usuários.

## Estrutura do Banco de Dados

### **1. ConsumoEnergia**

| Campo          | Tipo        | Restrição     |
|----------------|-------------|---------------|
| ID_CONSUMO     | INTEGER     | Primary Key   |
| DATA_CONSUMO   | DATE        | Not Null      |
| CONSUMO        | INTEGER     | Not Null      |
| ID_RESIDENCIA  | INTEGER     | Foreign Key   |

#### Constraints:
- Primary Key: `CONSUMO_ENERGIA_PK(ID_CONSUMO)`
- Foreign Key: `CONSUMO_ENERGIA_RESIDENCIA_FK(ID_RESIDENCIA)` references `RESIDENCIA`

---

### **2. Endereco**

| Campo       | Tipo          | Restrição     |
|-------------|---------------|---------------|
| ID_ENDERECO | INTEGER       | Primary Key   |
| CEP         | VARCHAR(20)   | Not Null      |
| RUA         | VARCHAR(50)   | Not Null      |
| NUMERO      | INTEGER       | Not Null      |
| COMPLEMENTO | VARCHAR(50)   |               |

#### Constraints:
- Primary Key: `ENDERECO_PK(ID_ENDERECO)`

---

### **3. HistoricoPontos**

| Campo         | Tipo        | Restrição     |
|---------------|-------------|---------------|
| ID_HISTORICO  | INTEGER     | Primary Key   |
| DATA_HISTORICO| DATE        | Not Null      |
| QUANTIDADE    | INTEGER     | Not Null      |
| ID_USUARIOS   | INTEGER     | Foreign Key   |

#### Constraints:
- Primary Key: `HISTORICO_PONTOS_PK(ID_HISTORICO)`
- Foreign Key: `HISTORICO_PONTOS_USUARIOS_FK(ID_USUARIOS)` references `USUARIOS`

---

### **4. Recompensas**

| Campo             | Tipo          | Restrição     |
|-------------------|---------------|---------------|
| ID_RECOMPENSAS    | INTEGER       | Primary Key   |
| DESCRICAO         | VARCHAR(100)  |               |
| PONTOS_NECESSARIOS| INTEGER       | Not Null      |

#### Constraints:
- Primary Key: `RECOMPENSAS_PK(ID_RECOMPENSAS)`

---

### **5. Residencia**

| Campo                    | Tipo        | Restrição     |
|--------------------------|-------------|---------------|
| ID_RESIDENCIA            | INTEGER     | Primary Key   |
| DISPOSITIVO_MONITORAMENTO| VARCHAR(50) | Not Null      |
| QUANTIDADE_PESSOAS       | INTEGER     | Not Null      |
| MEDIA_CONSUMO            | FLOAT       | Not Null      |
| ID_USUARIOS              | INTEGER     | Foreign Key   |
| ID_ELETRODOMESTICO       | INTEGER     | Foreign Key   |
| ID_ENDERECO              | INTEGER     | Foreign Key   |

#### Constraints:
- Primary Key: `RESIDENCIA_PK(ID_RESIDENCIA)`
- Foreign Keys:
  - `RESIDENCIA_USUARIOS_FK(ID_USUARIOS)` references `USUARIOS`
  - `RESIDENCIA_ELETRODOMESTICO_FK(ID_ELETRODOMESTICO)` references `TIPO_ELETRODOMESTICO`
  - `RESIDENCIA_ENDERECO_FK(ID_ENDERECO)` references `ENDERECO`

---

### **6. TipoEletrodomestico**

| Campo             | Tipo          | Restrição     |
|-------------------|---------------|---------------|
| ID_ELETRODOMESTICO| INTEGER       | Primary Key   |
| NOME_ELETRODOMESTICO| VARCHAR(50) | Not Null      |
| QUANTIDADE        | INTEGER       | Not Null      |

#### Constraints:
- Primary Key: `TIPO_ELETRODOMESTICO_PK(ID_ELETRODOMESTICO)`

---

### **7. TrocasRecompensas**

| Campo           | Tipo        | Restrição     |
|-----------------|-------------|---------------|
| ID_TROCAS       | INTEGER     | Primary Key   |
| DATA_TROCA      | DATE        | Not Null      |
| PONTOS_UTILIZADOS| INTEGER     | Not Null      |
| ID_USUARIOS     | INTEGER     | Foreign Key   |
| ID_RECOMPENSAS  | INTEGER     | Foreign Key   |

#### Constraints:
- Primary Key: `TROCAS_RECOMPENSAS_PK(ID_TROCAS)`
- Foreign Keys:
  - `TROCAS_RECOMPENSAS_USUARIOS_FK(ID_USUARIOS)` references `USUARIOS`
  - `TROCAS_RECOMPENSAS_RECOMPENSAS_FK(ID_RECOMPENSAS)` references `RECOMPENSAS`

---

### **8. Usuarios**

| Campo       | Tipo          | Restrição     |
|-------------|---------------|---------------|
| ID_USUARIOS | INTEGER       | Primary Key   |
| NOME        | VARCHAR(50)   | Not Null      |
| SENHA       | VARCHAR(20)   | Not Null      |
| TELEFONE    | VARCHAR(20)   |               |
| PONTOS      | INTEGER       | Not Null      |

#### Constraints:
- Primary Key: `USUARIOS_PK(ID_USUARIOS)`

---

# Estrutura de pastas

<pre>
.
└── EcoEnergy-GS/
    ├── EcoEnergy-GS/
    │   ├── Properties/
    │   │   └── lauchSettings.json
    │   ├── Controllers/
    │   │   ├── ConsumoEnergiaController.cs
    │   │   ├── EnderecoController.cs
    │   │   ├── HistoricoPontosController.cs
    │   │   ├── IAController.cs
    │   │   ├── RecompensasController.cs
    │   │   ├── ResidenciaController.cs
    │   │   ├── TipoEletrodomesticoController.cs
    │   │   ├── TrocasRecompensasController.cs
    │   │   └── UsuarioController.cs
    │   ├── Data/
    │   │   └── AppDbContext.cs
    │   ├── DTO/
    │   │   ├── ConsumoEnergia/
    │   │   │   ├── ConsumoEnergiaCreateDto.cs
    │   │   │   └── ConsumoEnergiaEditDto.cs
    │   │   ├── Endereco/
    │   │   │   ├── EnderecoCreateDto.cs
    │   │   │   └── EnderecoEditDto.cs
    │   │   ├── HistoricoPontos/
    │   │   │   ├── HistoricoPontosCreateDto.cs
    │   │   │   └── HistoricoPontosEditDto.cs
    │   │   ├── Recompensas/
    │   │   │   ├── RecompensasCreateDto.cs
    │   │   │   └── RecompensasEditDto.cs
    │   │   ├── Residencia/
    │   │   │   ├── ResidenciaCreateDto.cs
    │   │   │   └── ResidenciaEditDto.cs
    │   │   ├── TipoEletrodomestico/
    │   │   │   ├── TipoEletrodomesticoCreateDto.cs
    │   │   │   └── TipoEletrodomesticoEditDto.cs
    │   │   ├── TrocasRecompensas/
    │   │   │   ├── TrocasRecompensasCreateDto.cs
    │   │   │   └── TrocasRecompensasEditDto.cs
    │   │   └── Usuario/
    │   │       ├── UsuarioCreateDto.cs
    │   │       └── UsuarioEditDto.cs
    │   ├── Migrations/
    │   │   ├── 20241118004841_initial.cs
    │   │   └── AppDbContextModelSnapshot.cs
    │   ├── Models/
    │   │   ├── ConsumoEnergiaModel.cs
    │   │   ├── EnderecoModel.cs
    │   │   ├── HistoricoPontosModel.cs
    │   │   ├── RecompensasModel.cs
    │   │   ├── ResidenciaModel.cs
    │   │   ├── ResponseModel.cs
    │   │   ├── TipoEletrodomesticoModel.cs
    │   │   ├── TrocasRecompensasModel.cs
    │   │   └── UsuarioModel.cs
    │   ├── Services/
    │   │   ├── ConsumoEnergia/
    │   │   │   ├── ConsumoEnergiaService.cs
    │   │   │   └── IConsumoEnergiaInterface.cs
    │   │   ├── Endereco/
    │   │   │   ├── EnderecoService.cs
    │   │   │   └── IEnderecoInterface.cs
    │   │   ├── HistoricoPontos/
    │   │   │   ├── HistoricoPontosService.cs
    │   │   │   └── IHistoricoPontosInterface.cs
    │   │   ├── Recompensas/
    │   │   │   ├── IRecompensasInterface.cs
    │   │   │   └── RecompensasService.cs
    │   │   ├── Residencia/
    │   │   │   ├── IResidenciaInterface.cs
    │   │   │   └── ResidenciaService.cs
    │   │   ├── TipoEletrodomestico/
    │   │   │   ├── ITipoEletrodomesticoInterface.cs
    │   │   │   └── TipoEletrodomesticoService.cs
    │   │   ├── TrocasRecompensas/
    │   │   │   ├── ITrocasRecompensasInterface.cs
    │   │   │   └── TrocasRecompensasService.cs
    │   │   └── Usuario/
    │   │       ├── ITrocasRecompensasInterface.cs
    │   │       └── TrocasRecompensasService.cs
    │   ├── appsettings.json
    │   ├── EcoEnergy-GS.http
    │   ├── model.zip
    │   └── Program.cs
    ├── EcoEnergy-GS.IA/
    │   └── Data/
    │       └── DataTrain.json
    └── EcoEnergy-GS.Tests/
        ├── Data/
        │   ├── CustomWebApplicationFactory.cs
        │   ├── DbContextFactory.cs
        │   └── TestDbContextFixture.cs
        └── Tests/
            ├── ConsumoEnergiaApiTests.cs
            ├── EnderecoApiTests.cs
            ├── HistoricoPontosApiTests.cs
            ├── RecompensasApiTests.cs
            ├── ResidenciaApiTests.cs
            ├── TipoEletrodomesticoApiTests.cs
            ├── TrocasRecompensasApiTests.cs
            └── UsuarioApiTests.cs
</pre>

# Design patterns
## Repository
O Repository Pattern é um padrão que isola a lógica de acesso a dados da lógica de negócios. Os serviços são responsáveis por interagir com o banco de dados através do AppDbContext e fornecer métodos de alto nível para manipulação de dados. Neste caso, embora não haja uma pasta explicitamente chamada "Repositories", os serviços estão desempenhando o papel de repositórios.

## Dependency Injection
A injeção de dependência é um padrão que facilita o gerenciamento de dependências na aplicação, tornando-as mais fáceis de testar e mantendo o código desacoplado. Aqui, estamos injetando as dependências dos serviços nas controllers.

## DTO
O DTO Pattern é utilizado para transferir dados entre camadas da aplicação. Os DTOs são usados para transferir dados entre o frontend e o backend, ajudando a controlar os dados que entram e saem da API.

## Service Layer
A Camada de Serviço abstrai a lógica de negócios da aplicação, centralizando essa lógica em classes específicas. Isso mantém as controllers mais enxutas, delegando tarefas complexas aos serviços. Cada serviço implementa sua respectiva interface (ICampaignInterface, ICompanyInterface), o que facilita o desacoplamento e os testes.

## Controller
Seguindo o padrão MVC (Model-View-Controller), as controllers atuam como intermediárias entre a camada de visualização (ou requisições HTTP) e as camadas de lógica de negócios. Elas recebem requisições, chamam os serviços necessários e retornam respostas ao cliente.

## Models
Seguindo o padrão MVC, os Models representam os dados e a lógica de negócios no sistema. Essas classes são as que interagem diretamente com o banco de dados via AppDbContext

# Instruções
Para executar a aplicação, siga os passos abaixo:  

1. Copie o link do repositório.
2. Cole o link: `https://github.com/EcoEnergy-GS/EcoEnergy-GS.git`.
3. Clone o repositório para sua IDE (ex: Visual Studio 2022, Rider).
4. No arquivo "appsettings.json" troque o campo "Id" e o "Password".
5. Instale as dependências necessárias com o comando `dotnet restore`.
6. Localize seu Package Manager Console digite `update-database` e pressione enter.
7. Na Program.cs na linha 43, altere o caminho correspondente ao da sua máquina do arquivo `ProductDescriptionData.json` dentro da solução EcoEnergy-GS.
8. Dê run na API.
9. De acordo com nossas regras de negócio, é necessário começar a operação de Create na seguinte ordem: Usuários, Historico_Pontos, Recompensas, Trocas_Recompensas, Enderecos, Tipo_Eletrodomestico, Residencia e Consumo_Energia.
10. Agora fique à vontade para testar todos endpoints.

# Integrantes

Gabriel Siqueira Rodrigues RM:98626

Gustavo de Oliveira Azevedo RM:550548

Isabella Jorge Ferreira RM:552329

Mateus Mantovani Araújo RM:98524

Juan de Godoy RM:551408

### Todos 2TDSPF
