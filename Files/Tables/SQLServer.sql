
/* GRUPOS USUARIOS */
create table grupos_usuarios
(
	id         int           not null,
    nome       varchar(100)  not null,

	primary key(id)
);

/*  USUARIOS  */
create table usuarios
(
	id		              int         not null,
	nome                  varchar(50) not null,
	senha                 varchar(50) not null,
	admin                 bit                   default 0,
	ativo                 bit                   default 1, /* 0 - NAO; 1 - SIM;  */
	grupo_usuarios_id     int         not null,
   
	primary key(id),
	foreign key(grupo_usuarios_id) references grupos_usuarios(id)
);

/* TELAS  */
create table telas
(
    /*
		EXEMPLO:
		alias: CONS_PRD_1401 
		descricao: Consulta de produtos
	*/
	id                varchar(30)  not null, /* cada tela terá o seu alias fixamente definido, na view ou no model */
	descricao         varchar(100) not null,

	primary key(id)
);

/*  PERMISSOES */
create table permissoes
(  
	grupo_usuarios_id    int         not null,
	telas_id             varchar(30) not null,  /* tabela de telas  */
	acesso               bit         not null,
	inserir              bit         not null,
	atualizar            bit         not null,
	excluir              bit         not null,
	 
	foreign key(telas_id)           references telas           (id),
	foreign key(grupo_usuarios_id)  references grupos_usuarios (id)
);

/*  FOTOS */
create table fotos
(
    /*
		esta tabela quardará fotos em geral, os arquivos serão armazenados
		fisicamente no banco e renderizados no disco em uma eventual consulta.
		a tabela que precisar de uma foto, bastará apontar uma chave
		para a id desta tabela.
	*/
	id    int            not null,
	foto  varbinary(max) not null,

	primary key(id)
);

/* ENDERECOS  */
create table enderecos
(
	id          int          not null,
	cep         varchar(20)  not null,
	logradouro  varchar(100) not null,
	bairro      varchar(100) not null,
	municipio   varchar(100) not null,
	uf          varchar(2)   not null,
	numero      int          not null,
	pais        varchar(100) not null,
	complemento varchar(300) not null,

	primary key(id)
)

/*  EMPRESA */
create table empresa
(
	id			     int           not null,
	nome_fantasia    varchar(100)  not null,
	razao_social     varchar(100)  not null,
	cnpj			 varchar(100)  not null,
	crt              int           not null  default 0, /* codigo do regime tributário da empresa */
	telefone1        varchar(100)  not null,
	telefone2        varchar(100),
	celular          varchar(100),
	email			 varchar(100),
	responsavel      varchar(100)  not null,
	ativo            bit           not null,  /* 0 - NAO; 1 SIM; */
	tipo             int		   not null,  /* 0 - Matriz; 1 - Filial */
	tipo_ie          int           not null  default 0, /* 0 - Contribuinte; 1 - Isento; 2 - Não contribuinte; */
	inscr_estadual   varchar(40),
	inscr_municipal  varchar(40),
	optante_simples  bit           not null  default 1, /* 1 - SIM; 0 - NÂO; */
	
	nfe_cert_serie   varchar(100)  not null,
	nfe_serie        int           not null  default 0,
	nfe_modelo       int           not null  default 0,
	nfe_ambiente     int           not null  default 0,
	
	nfce_serie       int           not null  default 0,
	nfce_modelo      int           not null  default 0,
	nfce_ambiente    int           not null  default 0,
	nfce_token       varchar(100),

	endereco_id      int           not null,
	foto_id          int,                    /* Logo da empresa, tabela de fotos */

	primary key(id),
	foreign key(endereco_id) references enderecos(id)
);

/*  DOCUMENTOS  */
create table documentos
(
	id		int           not null,
	rg		varchar(20),
	ie      varchar(20), /* Inscr. estadual */
	im      varchar(20), /* Inscr. municipal */
	cpf     varchar(20),
	cnpj	varchar(20),
	cnh		varchar(20),
	rntc    varchar(30),

	primary key(id)
);

/* BANCOS */
create table bancos
(
    /*
	    'dv' = digito verificador
	*/
    id              int          not null,
	nome            int          not null,
	numero          int          not null,  /* numero do banco, cada banco tem um proprio */
	
	primary key(id)
);

/* CONTAS BANCARIAS */
create table contas_bancarias
(
	id              int             not null,
	nome            varchar(50)     not null,
	agencia         varchar(5)      not null,
	conta		    varchar(10)     not null,
	numero_banco    varchar(15),
	telefone        varchar(20),
	correntista     varchar(100),
	saldo_atual     decimal(10,2),
	limite_credito  decimal(10,2), 
	cnpj            varchar(20),
	endereco_id     int,                       /* tabela de enderecos */
	banco_id        int             not null,
	
	primary key(id),
	foreign key(banco_id) references bancos(id)
);

/* FORNECEDORES */
create table fornecedores
(
	id        		int          not null,
	loja_fornecedor int,
	razao_social    varchar(100) not null,
	nome_fantasia   varchar(100) not null,
	telefone 		varchar(20),
	fax				varchar(20),
	email			varchar(100),
	observacoes		varchar(1000),
	website			varchar(100),
	ativo			bit,                               /* 0 - NAO; 1 - SIM; */
	credito			decimal(10, 2),
	ultima_compra	varchar(19),
	tipo_pessoa		int          not null   default 2, /*  0 - Fisico; 1 - Juridico; 2 - Outros; */
	endereco_id	    int          not null,             /*  tabela de enderecos */
	documento_id    int          not null,             /*  tabela de documentos */
	banco_id		int,                               /*  tabela de bancos */
	data_cadastro   varchar(19),

	primary key(id),
	foreign key(endereco_id ) references enderecos  (id),
	foreign key(documento_id) references documentos (id)
);

/*  CONTATOS FORNECEDORES  */
create table contatos_fornecedores
(
	filial          int          not null,
	pessoa_contato  varchar(100) not null,
	telefone		varchar(20)  not null,
	email			varchar(20),
	setor			varchar(100),
	fornecedor_id   int          not null,

	foreign key(fornecedor_id) references fornecedores(id)
);

/*  TRANSPORTADORAS  */
create table transportadoras
(
	id             int          not null,
	razao_social   varchar(100) not null,
	nome_fantasia  varchar(100),
	telefone1      varchar(15),
	telefone2      varchar(15),
	email          varchar(100),
	website        varchar(100),
	observacoes    varchar(1000),
	inativo        bit,
	endereco_id    int           not null,
	documento_id   int           not null,

	primary key(id),
	foreign key(endereco_id)    references enderecos  (id),
	foreign key(documento_id)   references documentos (id)
);

/*  CARGOS  */
create table cargos
(
	id	            	int            not null,
	nome                varchar(100)   not null,
	descricao_detalhada varchar(100),

	primary key(id)
);

/*  TURNOS  */
create table turnos
(
	id              int            not null,
	descricao       varchar(100)   not null,
	hora_entrada    varchar(8)     not null, /* 14:30:00 */
	hora_saida      varchar(8)     not null,

	primary key(id)
);

/*  INTERVALOS TURNOS  */
create table intervalos_turnos
(
	/*
		intervalos que o funcionario faz ao longo do turno, horario de lanche por exemplo
	*/
	descricao     varchar(100)    not null,
	hora_inicio   varchar(8)      not null,
	hora_termino  varchar(8)      not null,
	turno_id      int             not null  default 0 /* tabela de turnos, opcional */
);

/*  FUNCIONARIOS  */
create table funcionarios
(
	id			     int		   not null,
	matricula        int    	   not null,
	nome		     varchar(100)  not null,
	nome_mae         varchar(100),
	nome_pai         varchar(100),
	sexo	  	     int		   not null   default 0,    /* 0 - masculino; 1 - feminino */
	estado_civil     int                      default 0,    /* 0 - solteiro; 1 - casado; 2 - nao informado */ 
	data_nascimento  varchar(10)   not null,
	nacionalidade    varchar(100),
	naturalidade_uf  varchar(2),
	apelido          varchar(100),
	data_adm         varchar(10)		   not null,
	data_dem		 varchar(10)                     default null,
	horas_dia        int           not null,                 /* horas que o func. trabalha por dia */
	horas_semana     int		   not null,                 /* horas por semana */
	salario          decimal(10,2) not null,
	comissao_venda   decimal(10,2)            default 0,
	comissao_servico decimal(10,2)            default 0,
	data_pagamento   varchar(10),
	cargo_id         int,                                     /* tabela de cargos */
	documento_id	 int,                                     /* tabela de documentos  */
	turno_id         int,                                     /* tabela de turnos */
	usuario_id       int                      default 0,      /* tabela de usuarios, usuario no qual esse funcionario representa, se houver */
	endereco_id      int           not null,
	empresa_id       int           not null,

	primary key(id),
	foreign key(documento_id)  references documentos (id),
	foreign key(cargo_id)      references cargos     (id),
	foreign key(endereco_id)   references enderecos  (id),
	foreign key(empresa_id)    references empresa    (id)
	
);

/*   TABELAS DE PRECO   */
create table precos
(
	id                int            not null,
	nome              varchar(100)   not null,
	data_inativacao   varchar(19)    not null  default 0, /* data em que essa tabela de preço devará ser inativada automaticamente */
	inativo           bit            not null  default 0,

	primary key(id)
);

/* GRUPOS CLIENTES */
create table grupos_clientes
(
	id          int          not null,
	nome        varchar(100) not null,

	primary key(id)
);

/* CLIENTES  */
create table clientes
(
	id                  int           not null,
	nome                varchar(100)  not null,
	apelido             varchar(100),
	telefone1           varchar(15),
	telefone2           varchar(15),
	fax                 varchar(30),
	celular             varchar(15),
	email1              varchar(100),
	email2              varchar(100),
	website             varchar(100),
	obs                 varchar(300),
	bloqueado           bit                          default 0,
	inativo             bit                          default 0,
	data_nascimanto     varchar(10),
	data_cadastro       varchar(10),
	ultima_alteracao    varchar(10),
	credito             decimal(10,2) not null        default 0,
	debito              decimal(10,2) not null        default 0,
	tipo_pessoa         int           not null        default 0,    /* 0 - Fisica; 1 - Juridica; */
	sexo                int           not null        default 2,    /* 0 - Masculino; 1 - Feminino; 2 - Não informar */
	documento_id        int           not null,                     /* Tabela de documentos */
	endereco_id         int           not null,                     /* tabela de enderecos */
	grupo_id            int           not null        default 0,    /* Tabela de grupos, opcional */
	tabela_preco_id     int           not null        default 0,    /* caso o cliente tenha alguma tabela de preço especial */

	primary key(id),
	foreign key(documento_id)  references documentos (id),
	foreign key(endereco_id)   references enderecos  (id)  
);

/* BLOQUEIOS CLIENTES */
create table bloqueios_clientes
(
	id                  int          not null,
	data_bloqueio       varchar(19)  not null,
	data_desbloqueio    varchar(19)  not null,
	gatilho_desbloqueio int          not null  default 0,  /* 0 - NOTIFICAR; 1 - DESBLOQUEAR */
	cliente_id          int          not null,

    primary key(id),
	foreign key(cliente_id) references clientes(id)
);

/*  UNIDADES   */
create table unidades
(
	id           int          not null,
	sigla        varchar(30)  not null,
	descricao    varchar(100) not null,
	fracionado   bit          not null  default 0,  /* 0 - NAO; 1 - SIM */

	primary key(id)
);

/*   CARACTERISTICAS PRODUTOS   */
create table caracteristicas
(
     /*
	    Exemplo:

		Atributo: Cor
		Valor: Amarelo
	 */
	id          int          not null,
	atributo    varchar(100) not null,
	valor       varchar(100) not null,

	primary key(id)
);

/* GRUPOS DE PRODUTOS  */
create table grupos_produtos
(
	id          int           not null,
	descricao   varchar(100)  not null,
	foto_id     int,                                /* Tabela de fotos, opcional */
	inativo     bit           not null  default 0,  /*  0 - NÃO; 1 - SIM;  */

	primary key(id)
);

/*  FAMILIAS PRODUTOS  */
create table familias_produtos
(
	id         int          not null,
	nome       varchar(100) not null,
	
	primary key(id)
);

/* CAIXAS */
create table caixas
(
	id                   int          not null,
	nome                 varchar(100) not null,
	ativo                bit          not null,
	empresa_id           int          not null,
	
	primary key(id),
	foreign key(empresa_id)  references empresa(id) 
);

/* MOVIMENTAÇÕES CAIXAS */
create table movimentacoes_caixas
(
	id                  int            not null,
	data_abertura       varchar(19)    not null,
	usuario_abertura    int            not null,

	data_fechamento     varchar(19)    not null,
	usuario_fechamento  int            not null,

	/* TOTAIS BASEADOS NOS TIPOS DE PAGAMENTO DAS FORMAS DE PAGAMENTO */
	/* Dinheiro, Cartao, Cheque, Credito cliente, Boleto */

	dinheiro            decimal(18,2)  not null   default 0,
	cartao              decimal(18,2)  not null   default 0,
	cheque              decimal(18,2)  not null   default 0,
	credito_cliente     decimal(18,2)  not null   default 0,
	boleto              decimal(18,2)  not null   default 0,
	
	troco               decimal(18,2)  not null   default 0,
    caixa_id            int            not null,

	primary key(id),
	foreign key(usuario_abertura)  references usuarios  (id),
	foreign key(caixa_id)          references caixas    (id)
);

/*  ARMAZENS   */
create table armazens
(
	id               int             not null,
	nome             varchar(100)    not null,
	tipo_armazem     int             not null  default 0, /* 0 - Proprio; 1 - Alugado; 2 - De terceiros; */
	empresa_id       int             not null  default 0, /* Se for maior que 0, indica que é o armazem da empresa atual e no qual deve ser priorizado ao retirar um produto do estoque */
	
	primary key(id)
);

/* LOCAIS ESTOQUE */
create table locais_estoque
(
	id               int                not null,
	nome             varchar(20)        not null,

    largura          double precision   not null  default 0,
	unidade_largura  varchar(10)        not null,
	
	altura           double precision   not null  default 0,
	unidade_altura   varchar(10)        not null,
	
 	comprimento      double precision   not null  default 0,
	unidade_compr    varchar(10)        not null,

	armazem_id       int                not null,

	primary key(id),
	foreign key(armazem_id) references armazens(id)
);

/*  CEST  */
create table cest
(
	id          int           not null,
    codigo      varchar(50)   not null, /* Identificador do CEST */
	ncm         varchar(100)  not null,
	inativo     bit           not null   default 0,

	primary key(id)
);

/*   PRODUTOS  */
create table produtos
(
	id                 int           not null,
	ean                varchar(13),
	referencia         varchar(10),
	descricao          varchar(100)  not null,
    ncm                varchar(10),                           /*  Com a chegada do CEST, esse campo será inutilizado  */
	anp                varchar(15),                           /* PRODUTO ESPECIFICO, ANP */
	data_cadastro      varchar(19)   not null,
	ultima_alteracao   varchar(19)   not null,
	fabricado          bit           not null,
	insumo             bit           not null,
	fracionado         bit           not null,
	inativo            bit           not null,
	para_balanca       bit           not null, 
	marca              varchar(100),
	fabricante         varchar(100),
	cod_fabricante     varchar(50),
	unidade1           int          not null,                 /* TABELA DE UNIDADES */
    fator_conversao    int          not null   default 0,
	unidade2           int                     default 0,     /* CASO INFORMADO fator_conversao, UNIDADE_2 SERÁ OBRIGATORIA */
    custo_medio        decimal(18,2)           default 0,
	classes_imposto_id int          not null,                 /* TABELA DE CLASSE DE IMPOSTO */
	cest_id            int          not null   default 0,     /* TABELA DE CEST  */
	grupo_produtos_id  int          not null   default 0,
	validade           int          not null   default 0,

	primary key(id),
	foreign key(unidade1)  references unidades (id)
);

/*  PRODUTOS x PRECOS  */
create table produtos_precos
(
	produto_id     int           not null,
	preco_id       int           not null,
	valor          decimal(10,2) not null   default 0,
	preco_padrao   bit           not null   default 1,

	foreign key(produto_id) references produtos (id),
	foreign key(preco_id)   references precos   (id)
);

/*   ESTOQUE  */
create table estoque
(
	id                int            not null,
	produto_id        int            not null,            /* TABELA DE PRODUTOS */
	local_estoque_id  int            not null,            /* TABELA LOCAIS ESTOQUE */
	quant             decimal(10,2)  not null  default 0,
	local_padrao      bit            not null  default 0,

	primary key(id),
	foreign key(produto_id)       references produtos       (id),
	foreign key(local_estoque_id) references locais_estoque (id)
);

/*  PRODUTOS x FORNECEDORES  */
create table produtos_fornecedores
(
	produto_id      int not null,
	fornecedor_id   int not null,

	foreign key(produto_id)    references produtos     (id),
	foreign key(fornecedor_id) references fornecedores (id)
);

/* PRODUTOS x CLIENTES */
create table produtos_clientes
(
	produto_id    int not null,
	cliente_id    int not null,

	foreign key(produto_id) references produtos  (id),
	foreign key(cliente_id) references clientes  (id)
);

/* PRODUTOS x CARACTERIStICAS */
create table produtos_caractetisticas
(
	caracteristica_id      int not null,
	produto_id             int not null,

	foreign key(caracteristica_id) references caracteristicas (id),
	foreign key(produto_id)        references produtos        (id)
);

/*  LOTES  */
create table lotes
(
	id                 int          not null,
	lote               varchar(50)  not null,
	data_fabricacao    varchar(19)  not null,
	data_vencimento    varchar(19)  not null,

	primary key(id)
);

/*  LOTES x PRODUTOS */
create table lotes_produtos
(
	lote_id      int not null,
	produto_id   int not null,

	foreign key(lote_id)    references lotes    (id),
	foreign key(produto_id) references produtos (id)
);

/* OPERADORAS CARTAO */
create table operadoras_cartao
(
	id                          int             not null,
	nome                        varchar(100)    not null,
	credito_dias_recebimento    int             not null default 0,
	debito_horas_recebimento    int             not null default 0,
	taxa                        decimal(10,5)   not null default 0,

	primary key(id)
);

/* FORMAS DE PAGEMENTO */
create table formas_pagamento
(
	id                    int           not null,
	descricao             varchar(100)  not null,
	tipo_pagamento        int           not null  default 0, /* Dinheiro, Cartao, Cheque, Credito cliente, Boleto */
	banco_id              int           not null  default 0, /* Tabela de bancos, informado caso tipo_pag for: Cheque, ou  Boleto */
	operadora_cartao_id   int           not null  default 0, /* Tab. Operadoras_cartao, se o tipo_pag for Cartao */
	permite_entrada       bit           not null  default 0, /* 0 - NAO; 1 - SIM; Se o cliente pode dar uma entrada de R$300,00 e parcelar o resto */
	dia_base              int           not null  default 0, /* dia base do pagamento. o sistema deve notificar o usuario */
    maximo_parcelas       int           not null  default 0, /* se for alguma forma parcelada, deve informar esse campo */

	primary key(id)
);

/*   TIPOS MOVIMENTO   */
create table tipos_movimento
(
	id                        int           not null,
	descricao                 varchar(100)  not null,
	movimentacao_financeiro   int           not null    default 0, /* 0 - entrada; 1 - saida */
	movimentacao_estoque      int           not null    default 0, /* 0 - entrada; 1 - saida */
	gera_comissao             bit           not null    default 0, /* 0 - NAO; 1 - SIM */
	gera_nfe                  bit           not null    default 0, /* 0 - NAO; 1 - SIM*/
	gera_nfce                 bit           not null    default 0, /* 0 - NAO; 1 - SIM */
	inativo                   bit           not null    default 0, /* 0 - NAO; 1 - SIM */    

	primary key(id)
);

/* SERVICOS */
create table servicos
(
	id         int   not null,

	primary key(id)
);

/*   CFOP   */
create table cfop
(
	id              varchar(10)    not null,
	descricao       varchar(2000)  not null,
	aplicacao       varchar(2000)  not null,
);

/*  MOVIMENTOS  */
create table movimentos
(
	id                    int             not null,
	data                  varchar(19)     not null,
	parcelado             bit             not null   default 0,
	efetivado             bit             not null   default 0,
	obs                   varchar(1000),
	valor_frete           decimal(18,2)   not null   default 0, 
	base_icms             decimal(18,2)   not null   default 0, /* base de calculo ICMS */
	total_icms            decimal(18,2)   not null   default 0, /* valor liquido ICMS */
	base_icms_st          decimal(18,2)   not null   default 0, /* base de calculo ICMS ST */ 
	total_icms_st         decimal(18,2)   not null   default 0, /* valor liquido ICMS ST */
	base_pis              decimal(18,2)   not null   default 0, /* base de calculo PIS */
	total_pis             decimal(18,2)   not null   default 0, /* valor liquido PIS */
	base_cofins           decimal(18,2)   not null   default 0, /* base de calculo COFINS */
	total_cofins          decimal(18,2)   not null   default 0, /* valor liquido COFINS */
	base_ipi              decimal(18,2)   not null   default 0, /* base de calculo IPI */
	total_ipi             decimal(18,2)   not null   default 0, /* valor liquido IPI */
	valor_outro           decimal(18,2)   not null   default 0,
	fornecedor_id         int             not null   default 0, /* tabela de fornecedores */
	cliente_id            int             not null   default 0, /* tabela de clientes */
	transportadora_id     int             not null   default 0, /* Tabela de transportadoras, opcional */
    nf_id                 int             not null   default 0, /* tabela movimento_nfs */
	usuario_id            int             not null,
	caixa_id              int             not null,
	empresa_id            int             not null,
	tipo_movimento_id     int             not null,

	primary key(id),
	foreign key(usuario_id)         references usuarios        (id),
	foreign key(caixa_id)           references caixas          (id),
	foreign key(empresa_id)         references empresa         (id),
	foreign key(tipo_movimento_id)  references tipos_movimento (id)
);

/* MOVIMENTO - NFs */
create table movimento_nfs
(
	id                      int           not null,
	chave_acesso            varchar(50)   not null,
	tipo_documento          int           not null,
	tipo_emissao            int           not null,
	modelo                  int           not null,
	serie                   int           not null,
	lote                    int           not null,
	data_emissao            varchar(19)   not null,
	ambiente                int           not null,
	protocolo_autorizacao   varchar(20)   not null,
	protocolo_denegado      varchar(20)   not null,
	codigo_status           int           not null,
	descricao_status        varchar(300)  not null,
	data_autorizacao        varchar(19)   not null,
	cancelado               bit           not null,
	data_cancelamento       varchar(19)   not null,
	protocolo_cancelamento  varchar(20)   not null,
	inutilizado             bit           not null,
	data_inutilizacao       varchar(19)   not null,

	primary key(id)
);

/* ITENS PAGAMENTO */
create table itens_pagamento
(
    id                     int             not null,
	
	desconto_perc          decimal(18,2)   not null   default 0,
	desconto_valor         decimal(18,2)   not null   default 0,
	
	acrescimo_perc         decimal(18,2)   not null   default 0,
	acrescimo_valor        decimal(18,2)   not null   default 0,
	
	valor_item             decimal(18,2)   not null   default 0, /* valor original do item, sem desc/acr */
	total_item             decimal(18,2)   not null   default 0, /* valor total do item, com desc/acresc */

    forma_pagamento_id     int             not null,
	movimento_id           int             not null,

	primary key(id),
	foreign key(forma_pagamento_id)  references  formas_pagamento   (id),
	foreign key(movimento_id)        references  movimentos         (id)   
);

/*  ITENS MOVIMENTO */
create table itens_movimento
(
	id                     int            not null,
	tipo_item              int            not null     default 0,  /* 0 - Produto; 1 - Servico; */
	produto_id             int            not null     default 0,  /* se tipo_item for 0, campo obrigatorio */
	servico_id             int            not null     default 0,  /* se tipo_item for 1, campo obrigatorio */
	quant                  decimal(18,2)  not null     default 0,
	valor_unit             decimal(18,2)  not null     default 0, 
	base_icms              decimal(18,2)  not null     default 0, /* base de calculo ICMS */
	total_icms             decimal(18,2)  not null     default 0, /* valor liquido ICMS */
	base_icms_st           decimal(18,2)  not null     default 0, /* base de calculo ICMS ST */
	total_icms_st          decimal(18,2)  not null     default 0, /* valor liquido ICMS ST */
	icms_mva               decimal(18,2)  not null     default 0, /* margem de valor agregado para ICMS ST */
	base_ipi               decimal(18,2)  not null     default 0, /* base de calculo IPI */
	total_ipi              decimal(18,2)  not null     default 0, /* valor liquido IPI */
	base_pis               decimal(18,2)  not null     default 0, /* base de calculo PIS */
	total_pis              decimal(18,2)  not null     default 0, /* valor liquido PIS */
	base_cofins            decimal(18,2)  not null     default 0, /* base COFINS */
	total_cofins           decimal(18,2)  not null     default 0, /* valor liquido COFINS */
	icms_perc              int            not null     default 0, /* % ICMS */
	icms_perc_st           int            not null     default 0, /* % ICMS ST */
	cred_icms_perc         int            not null     default 0, /* % do credito do ICMS */
	total_cred_icms        int            not null     default 0, /* R$  credito do ICMS */
	desconto_perc          decimal(18,2)  not null     default 0, /* desconto em % */
	desconto_valor         decimal(18,2)  not null     default 0, /* desconto em R$ */
	comissao_perc          decimal(18,2)  not null     default 0, /* comissao em % */
	comissao_valor         decimal(18,2)  not null     default 0, /* comissao em R$ */
	valor_outros           decimal(18,8)  not null     default 0, 
	valor_total            decimal(18,2)  not null     default 0,

	funcionario_id         int            not null     default 0, /* TABELA DE FUNCIONARIOS, permite que varios funcionarios atendam o mesmo cliente em uma venda, (bar, rest.) */
	cfop_id                varchar(10)    not null,
	movimento_id           int            not null,

	primary key(id),
	foreign key(movimento_id) references movimentos  (id)
);

/*  ALIQUOTA ICMS UFs  */
create table icms_ufs
(
	id                      int           not null,
	uf_origem               varchar(2)    not null,
	uf_destino              varchar(2)    not null,
	aliquota                decimal(18,2) not null,
	fundo_combate_pobresa   decimal(18,2) not null,
	data_alteracao          varchar(19)   not null,

	primary key(id)

);

/*   CLASSES IMPOSTO   */
create table classes_imposto
(
	id             int          not null,
	nome           varchar(100) not null,
	data_alteracao varchar(19)     not null,

	primary key(id)
);

/*  OPERACOES CLASSE IMPOSTO  */
create table operacoes_classe_imposto
(
    /*
	    perc = percentual
	*/
	id                       int           not null,
	icms_cst                 varchar(5)    not null,
	icms_perc                decimal(18,2) not null,  /* icms % */
	icms_perc_st             decimal(18,2) not null,  /* icms % com ST */
	icms_mva                 decimal(18,2) not null,  /* icms MVA */
	impressora_fiscal_tipo   varchar(5),              /* ICMS - Tributado, NN - NAO INCIDENTE, II - Isento, FF - Retiro/Subistituição */
	impressora_fiscal_perc   decimal(18,2) not null,  /* % - caso impressora_fiscal_tipo seja ICMS, o percentual deve deve ser informado */
	pis_cofins_tipo          varchar(5)    not null,
	pis_perc                 decimal(18,2) not null,
	pis_perc_st              decimal(18,2) not null,
	cofins_perc              decimal(18,2) not null,
	cofins_perc_st           decimal(18,2) not null,
	perc_credito_icms        decimal(18,2) not null,
	classe_imposto_id        int           not null,  /* TABELA DE CLASSE IMPOSTO */
	tipos_movimento_id       int           not null,  /* TABELA DE TIPOS MOVIMENTO, QUANDO FOR INFORMADO O TIPO MOVIMENTO NO MOVIMENTO, ESSA OPERAÇÃO SERÁ ACIONADA */
	cfop_id                  varchar(10)   not null,  /* TABELA DE CFOP */

	primary key(id),
	foreign key(classe_imposto_id)  references classes_imposto  (id),
	foreign key(tipos_movimento_id) references tipos_movimento  (id)
);

/* PLANO DE CONTAS */
create table plano_contas
(
	id            int          not null,
	descricao     varchar(100) not null,

	primary key(id)
);

/* PARCELAS MOVIMENTOS */
create table parcelas
(
	id                    int            not null,
	tipo                  int            not null   default 0,  /* 0 - pagar; 1 - receber */
	parcela_numero        varchar(15)    not null,              /*  Ex: 1/6    */
	plano_conta_id        int            not null,
	forma_pagamento_id    int            not null,              /*  tabela de formas de pagamento */
	conta_bancaria_id     int            not null   default 0,  /* em caso de boleto, obrigatorio */
	operadora_cartao_id   int            not null   default 0,  /* em caso de cartão, obrigatório */
	cheque_numero         int            not null   default 0,  /* em caso de cheque, obriatorio */
	data_emissao          varchar(10)    not null,
	data_vencimento       varchar(10)    not null,
	valor                 decimal(18,2)  not null,
	pago                  bit            not null,
	movimento_id          int            not null   default 0,  /* se for gerado apartir de um movimento, informar aqui */
	
	primary key(id),
	foreign key(plano_conta_id)      references plano_contas     (id),
	foreign key(forma_pagamento_id)  references formas_pagamento (id)
);

/* REGRAS DE DESCONTO */
create table regras_desconto
(
	id                    int             not null,
	descricao             varchar(100)    not null,
	grupo_clientes_id     int             not null   default 0,
	cliente_id            int             not null   default 0,
	tabela_preco_id       int             not null   default 0,
	tipo_movimento_id     int             not null   default 0,
	forma_pagamento_id    int             not null   default 0,
	
	faixa_valores         decimal(18,2)   not null   default 0,
	tipo_faixa_vlr        int             not null   default 0, /* 0 - Até; 1 - Igual á; 2 - Acima de */

	desconto_perc         decimal(10,2)   not null   default 0, /* % de desconto */
	inativo               bit             not null   default 0,
	data_final            varchar(19),

	primary key(id)
);

/*  REGRAS ACRESCIMO */
create table regras_acrescimo
(
	id                    int             not null,
	descricao             varchar(100)    not null,
	grupo_clientes_id     int             not null   default 0,
	cliente_id            int             not null   default 0,
	tabela_preco_id       int             not null   default 0,
	tipo_movimento_id     int             not null   default 0,
	forma_pagamento_id    int             not null   default 0,
	
	faixa_valores         decimal(18,2)   not null   default 0,
	tipo_faixa_vlr        int             not null   default 0, /* 0 - Até; 1 - Igual á; 2 - Acima de */

	acrescimo_perc        decimal(10,2)   not null   default 0, /* % de acrescimo */
	inativo               bit             not null   default 0,
	data_final            varchar(19),

	primary key(id)
);

insert into grupos_usuarios values(1, 'Administradores');
insert into usuarios values (1, 'Admin', 'admin', 1, 1, 1);

insert into telas(id, descricao) values (1, 'Unidades');
insert into telas(id, descricao) values (2, 'Características');
insert into telas(id, descricao) values (3, 'Armazéns');
insert into telas(id, descricao) values (4, 'Locais de estoque');
insert into telas(id, descricao) values (5, 'Grupos de produtos');
insert into telas(id, descricao) values (6, 'Produtos');
insert into telas(id, descricao) values (7, 'Kit de produtos');
insert into telas(id, descricao) values (8, 'Produtos x Características');
insert into telas(id, descricao) values (9, 'Produtos x Locais de estoque');
insert into telas(id, descricao) values (10, 'Cadastro de unidades');
insert into telas(id, descricao) values (11, 'Devolução para fornecedor');
insert into telas(id, descricao) values (12, 'Saída para revendedor');
insert into telas(id, descricao) values (13, 'Solicitação de compras');
insert into telas(id, descricao) values (14, 'Pedidos de compra');
insert into telas(id, descricao) values (15, 'Fornecedores');
insert into telas(id, descricao) values (16, 'Produtos x Fornecedor');
insert into telas(id, descricao) values (17, 'Grupos de produto x Fornecedor');
insert into telas(id, descricao) values (18, 'Documento de entrada');
insert into telas(id, descricao) values (19, 'Tipos de movimento');
insert into telas(id, descricao) values (20, 'Classes de imposto');
insert into telas(id, descricao) values (21, 'Formas de pagamento');
insert into telas(id, descricao) values (22, 'Operadoras de cartão');
insert into telas(id, descricao) values (23, 'Bancos');
insert into telas(id, descricao) values (24, 'Contas bancárias');
insert into telas(id, descricao) values (25, 'Caixas');
insert into telas(id, descricao) values (26, 'Planos de conta');
insert into telas(id, descricao) values (27, 'Tabelas de preço');
insert into telas(id, descricao) values (28, 'Lançamento de movimentos (Genérico)');
insert into telas(id, descricao) values (29, 'Pedidos de venda');
insert into telas(id, descricao) values (30, 'Venda rápida');
insert into telas(id, descricao) values (31, 'Contas a pagar');
insert into telas(id, descricao) values (32, 'Contas a receber');
insert into telas(id, descricao) values (33, 'Documento de saída');
insert into telas(id, descricao) values (34, 'Funcionários');
insert into telas(id, descricao) values (35, 'Cargos');
insert into telas(id, descricao) values (36, 'Turnos');
insert into telas(id, descricao) values (37, 'Benefícios');
insert into telas(id, descricao) values (38, 'Funcionários x Cargos');
insert into telas(id, descricao) values (39, 'Funcionários x Turnos');
insert into telas(id, descricao) values (40, 'Funcionários x Benefícios');
insert into telas(id, descricao) values (41, 'Empresa');
insert into telas(id, descricao) values (42, 'Usuários');
insert into telas(id, descricao) values (43, 'Grupos de usuários');
insert into telas(id, descricao) values (44, 'Grupos x Permissões');


/*     CFOP     */
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1000',	'ENTRADAS OU AQUISIÇÕES DE SERVIÇOS DO ESTADO',	'Classificam-se, neste grupo, as operações ou prestações em que o estabelecimento remetente esteja localizado na mesma unidade da Federação do destinatário');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1100',	'COMPRAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU PRESTAÇÃO DE SERVIÇOS',	'(NR Ajuste SINIEF 05/2005) (DECRETO Nº 28.868, DE 31/01/2006)\r\n\r\n(Dec. 28.868/2006 – Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1101',	'Compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria a ser utilizada em processo de industrialização ou produção rural, bem como a entrada de mercadoria em estabelecimento industrial ou produtor rural de cooperativa recebida de seus cooperados ou de estabelecimento de outra cooperativa.\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1102',	'Compra para comercialização',	'Classificam-se neste código as compras de mercadorias a serem comercializadas. Também serão classificadas neste código as entradas de mercadorias em estabelecimento comercial de cooperativa recebidas de seus cooperados ou de estabelecimento de outra cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1111',	'Compra para industrialização de mercadoria recebida anteriormente em consignação industrial',	'Classificam-se neste código as compras efetivas de mercadorias a serem utilizadas em processo de industrialização, recebidas anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1113',	'Compra para comercialização, de mercadoria recebida anteriormente em consignação mercantil',	'Classificam-se neste código as compras efetivas de mercadorias recebidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1116',	'Compra para industrialização ou produção rural originada de encomenda para recebimento futuro (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria, a ser utilizada em processo de industrialização ou produção rural, quando da entrada real da mercadoria, cuja aquisição tenha sido classificada no código “1.922 – Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro”.\r\n\r\n (DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1117',	'Compra para comercialização originada de encomenda para recebimento futuro',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, quando da entrada real da mercadoria, cuja aquisição tenha sido classificada no código 1.922 - Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1118',	'Compra de mercadoria para comercialização pelo adquirente originário, entregue pelo vendedor remetente ao destinatário, em venda à ordem.',	'Classificam-se neste código as compras de mercadorias já comercializadas, que, sem transitar pelo estabelecimento do adquirente originário, sejam entregues pelo vendedor remetente diretamente ao destinatário, em operação de venda à ordem, cuja venda seja classificada, pelo adquirente originário, no código 5.120 - Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário pelo vendedor remetente, em venda à ordem.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1120',	'Compra para industrialização, em venda à ordem, já recebida do vendedor remetente',	'Classificam-se neste código as compras de mercadorias a serem utilizadas em processo de industrialização, em vendas à ordem, já recebidas do vendedor remetente, por ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1121',	'Compra para comercialização, em venda à ordem, já recebida do vendedor remetente',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, em vendas à ordem, já recebidas do vendedor remetente por ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1122',	'Compra para industrialização em que a mercadoria foi remetida pelo fornecedor ao industrializador sem transitar pelo estabelecimento adquirente',	'Classificam-se neste código as compras de mercadorias a serem utilizadas em processo de industrialização, remetidas pelo fornecedor para o industrializador sem que a mercadoria tenha transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1124',	'Industrialização efetuada por outra empresa',	'Classificam-se neste código as entradas de mercadorias industrializadas por terceiros, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial. Quando a industrialização efetuada se referir a bens do ativo imobilizado ou de mercadorias para uso ou consumo do estabelecimento encomendante, a entrada deverá ser classificada nos códigos 1.551 - Compra de bem para o ativo imobilizado ou 1.556 - Compra de material para uso ou consumo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1125',	'Industrialização efetuada por outra empresa quando a mercadoria remetida para utilização no processo de industrialização não transitou pelo estabelecimento adquirente da mercadoria',	'Classificam-se neste código as entradas de mercadorias industrializadas por outras empresas, em que as mercadorias remetidas para utilização no processo de industrialização não transitaram pelo estabelecimento do adquirente das mercadorias, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial. Quando a industrialização efetuada se referir a bens do ativo imobilizado ou de mercadorias para uso ou consumo do estabelecimento encomend');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1126',	'Compra para utilização na prestação de serviço',	'Classificam-se neste código as entradas de mercadorias a serem utilizadas nas prestações de serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1150',	'TRANSFERÊNCIAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU PRESTAÇÃO DE SERVIÇOS (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'(DECRETO Nº 28.868, DE 31/01/2006 -&#x2013; Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1151',	'Transferência para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Entrada de mercadoria recebida, em transferência de outro estabelecimento da mesma empresa, para ser utilizada em processo de industrialização ou produção rural.\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1152',	'Transferência para comercialização',	'Classificam-se neste código as entradas de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1153',	'Transferência de energia elétrica para distribuição',	'Classificam-se neste código as entradas de energia elétrica recebida em transferência de outro estabelecimento da mesma empresa, para distribuição.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1154',	'Transferência para utilização na prestação de serviço',	'Classificam-se neste código as entradas de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem utilizadas nas prestações de serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1200',	'DEVOLUÇÕES DE VENDAS DE PRODUÇÃO DO ESTABELECIMENTO, DE PRODUTOS DE TERCEIROS OU ANULAÇÕES DE VALORES',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1201',	'Devolução de venda de produção do estabelecimento',	'Devolução de venda de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada como \"Venda de produção do estabelecimento\". (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1202',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros',	'Classificam-se neste código as devoluções de vendas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de industrialização no estabelecimento, cujas saídas tenham sido classificadas como Venda de mercadoria adquirida ou recebida de terceiros.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1203',	'Devolução de venda de produção do estabelecimento, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Devolução de venda de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada no código \"5.109 – Venda de produção do estabelecimento destinada à Zona Franca de Manaus ou Áreas de Livre Comércio\". (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1204',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Classificam-se neste código as devoluções de vendas de mercadorias adquiridas ou recebidas de terceiros, cujas saídas foram classificadas no código 5.110 - Venda de mercadoria adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1205',	'Anulação de valor relativo à prestação de serviço de comunicação',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de prestações de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1206',	'Anulação de valor relativo à prestação de serviço de transporte',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de prestações de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1207',	'Anulação de valor relativo à venda de energia elétrica',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de venda de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1208',	'Devolução de produção do estabelecimento, remetida em transferência',	'Devolução de produto industrializado ou produzido pelo estabelecimento transferido para outro estabelecimento da mesma empresa. (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1209',	'Devolução de mercadoria adquirida ou recebida de terceiros, remetida em transferência',	'Classificam-se neste código as devoluções de mercadorias adquiridas ou recebidas de terceiros, transferidas para outros estabelecimentos da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1250',	'COMPRAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1251',	'Compra de energia elétrica para distribuição ou comercialização',	'Classificam-se neste código as compras de energia elétrica utilizada em sistema de distribuição ou comercialização. Também serão classificadas neste código as compras de energia elétrica por cooperativas para distribuição aos seus cooperados.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1252',	'Compra de energia elétrica por estabelecimento industrial',	'Classificam-se neste código as compras de energia elétrica utilizada no processo de industrialização. Também serão classificadas neste código as compras de energia elétrica utilizada por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1253',	'Compra de energia elétrica por estabelecimento comercial',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento comercial. Também serão classificadas neste código as compras de energia elétrica utilizada por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1254',	'Compra de energia elétrica por estabelecimento prestador de serviço de transporte',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento prestador de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1255',	'Compra de energia elétrica por estabelecimento prestador de serviço de comunicação',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1256',	'Compra de energia elétrica por estabelecimento de produtor rural',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1257',	'Compra de energia elétrica para consumo por demanda contratada',	'Classificam-se neste código as compras de energia elétrica para consumo por demanda contratada, que prevalecerá sobre os demais códigos deste subgrupo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1300',	'AQUISIÇÕES DE SERVIÇOS DE COMUNICAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1301',	'Aquisição de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1302',	'Aquisição de serviço de comunicação por estabelecimento industrial',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento industrial. Também serão classificadas neste código as aquisições de serviços de comunicação utilizados por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1303',	'Aquisição de serviço de comunicação por estabelecimento comercial',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento comercial. Também serão classificadas neste código as aquisições de serviços de comunicação utilizados por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1304',	'Aquisição de serviço de comunicação por estabelecimento de prestador de serviço de transporte',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento prestador de serviço de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1305',	'Aquisição de serviço de comunicação por estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1306',	'Aquisição de serviço de comunicação por estabelecimento de produtor rural',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1350',	'AQUISIÇÕES DE SERVIÇOS DE TRANSPORTE',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1351',	'Aquisição de serviço de transporte para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de transporte utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1352',	'Aquisição de serviço de transporte por estabelecimento industrial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1353',	'Aquisição de serviço de transporte por estabelecimento comercial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1354',	'Aquisição de serviço de transporte por estabelecimento de prestador de serviço de comunicação',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1355',	'Aquisição de serviço de transporte por estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1356',	'Aquisição de serviço de transporte por estabelecimento de produtor rural',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1360',	'Aquisição de serviço de transporte por contribuinte-substituto em relação ao serviço de transporte (ACR) (Ajuste SINIEF 06/2007- Decreto nº 30.861/2007) –– a partir de 01.01.2008',	'Aquisição de serviço de transporte quando o adquirente for contribuinte-substituto em relação ao imposto incidente na prestação dos serviços');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1400',	'ENTRADAS DE MERCADORIAS SUJEITAS AO REGIME DE SUBSTITUIÇÃO TRIBUTÁRIA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1401',	'Compra para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria sujeita ao regime de substituição tributária, a ser utilizada em processo de industrialização ou produção rural, bem como compra, por estabelecimento industrial ou produtor rural de cooperativa, de mercadoria sujeita ao mencionado regime.\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1403',	'Compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, decorrentes de operações com mercadorias sujeitas ao regime de substituição tributária. Também serão classificadas neste código as compras de mercadorias sujeitas ao regime de substituição tributária em estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1406',	'Compra de bem para o ativo imobilizado cuja mercadoria está sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de bens destinados ao ativo imobilizado do estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1407',	'Compra de mercadoria para uso ou consumo cuja mercadoria está sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de mercadorias destinadas ao uso ou consumo do estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1408',	'Transferência para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Mercadoria sujeita ao regime de substituição tributária, recebida em transferência de outro estabelecimento da mesma empresa, para ser industrializada ou consumida na produção rural no estabelecimento.\r\n\r\n(DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1409',	'Transferência para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas, decorrentes de operações sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1410',	'Devolução de venda de mercadoria, de produção do estabelecimento, sujeita ao regime de substituição tributária',	'Devolução de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada como \"Venda de mercadoria de produção do estabelecimento sujeita ao regime de substituição tributária\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1411',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de vendas de mercadorias adquiridas ou recebidas de terceiros, cujas saídas tenham sido classificadas como Venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1414',	'Retorno de mercadoria de produção do estabelecimento, remetida para venda fora do estabelecimento, sujeita ao regime de substituição tributária',	'Entrada, em retorno, de produto industrializado ou produzido pelo próprio estabelecimento, remetido para venda fora do estabelecimento, inclusive por meio de veículo, sujeito ao regime de substituição tributária e não comercializado.\r\n\r\n (NR Ajuste SINIEF 05/2005) (DECRETO Nº 28.868, DE 31/01/2006-– Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1415',	'Retorno de mercadoria adquirida ou recebida de terceiros, remetida para venda fora do estabelecimento em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as entradas, em retorno, de mercadorias adquiridas ou recebidas de terceiros remetidas para vendas fora do estabelecimento, inclusive por meio de veículos, em operações com mercadorias sujeitas ao regime de substituição tributária, e não comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1450',	'SISTEMAS DE INTEGRAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1451',	'Retorno de animal do estabelecimento produtor',	'Classificam-se neste código as entradas referentes ao retorno de animais criados pelo produtor no sistema integrado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1452',	'Retorno de insumo não utilizado na produção',	'Classificam-se neste código o retorno de insumos não utilizados pelo produtor na criação de animais pelo sistema integrado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1500',	'ENTRADAS DE MERCADORIAS REMETIDAS PARA FORMAÇÃO DE LOTE OU COM FIM ESPECÍFICO DE EXPORTAÇÃO E EVENTUAIS DEVOLUÇÕES (NR Ajuste SINIEF 09/2005)',	'(DECRETO Nº 28.868, DE 31/01/2006—(Dec.\r\n28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1501',	'Entrada de mercadoria recebida com fim específico de exportação',	'Classificam-se neste código as entradas de mercadorias em estabelecimento de trading company, empresa comercial exportadora ou outro estabelecimento do remetente, com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1503',	'Entrada decorrente de devolução de produto, de fabricação do estabelecimento, remetido com fim específico de exportação',	'Devolução de produto industrializado ou produzido pelo estabelecimento, remetido a \"trading company\", a empresa comercial exportadora ou a outro estabelecimento do remetente, com fim específico de exportação, cuja saída tenha sido classificada no código \"5.501 – Remessa de produção do estabelecimento com fim específico de exportação\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a s');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1504',	'Entrada decorrente de devolução de mercadoria remetida com fim específico de exportação, adquirida ou recebida de terceiros',	'Devolução de mercadoria, adquirida ou recebida de terceiro, remetida a trading company, a empresa comercial exportadora ou a outro estabelecimento do remetente, com fim específico de exportação, cuja saída tenha sido classificada no código “5.502 - Remessa de mercadoria adquirida ou recebida de terceiros, com fim específico de exportação”.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1505',	'Entrada decorrente de devolução simbólica de mercadoria remetida para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.',	'Devolução simbólica de mercadoria remetida para formação de lote de exportação, cuja saída tenha sido classificada no código \"5.504 – Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento\".\r\n\r\n (ACR Ajuste SINIEF 09/2005) (Dec.28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1506',	'Entrada decorrente de devolução simbólica de mercadoria, adquirida ou recebida de terceiros, remetida para formação de lote de exportação.',	'Devolução simbólica de mercadoria remetida para formação de lote de exportação em armazéns alfandegados, entrepostos aduaneiros ou outros estabelecimentos que venham a ser regulamentados pela legislação tributária de cada Unidade Federada, efetuada pelo estabelecimento depositário, cuja saída tenha sido classificada no código \"5.505 – Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação\".\r\n\r\n (ACR Ajuste SINIEF 09/2005) (NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - a sua aplicação se');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1551',	'Compra de bem para o ativo imobilizado',	'Classificam-se neste código as compras de bens destinados ao ativo imobilizado do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1552',	'Transferência de bem do ativo imobilizado',	'Classificam-se neste código as entradas de bens destinados ao ativo imobilizado recebidos em transferência de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1553',	'Devolução de venda de bem do ativo imobilizado',	'Classificam-se neste código as devoluções de vendas de bens do ativo imobilizado, cujas saídas tenham sido classificadas no código 5.551 - Venda de bem do ativo imobilizado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1554',	'Retorno de bem do ativo imobilizado remetido para uso fora do estabelecimento',	'Classificam-se neste código as entradas por retorno de bens do ativo imobilizado remetidos para uso fora do estabelecimento, cujas saídas tenham sido classificadas no código 5.554 - Remessa de bem do ativo imobilizado para uso fora do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1555',	'Entrada de bem do ativo imobilizado de terceiro, remetido para uso no estabelecimento',	'Classificam-se neste código as entradas de bens do ativo imobilizado de terceiros, remetidos para uso no estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1556',	'Compra de material para uso ou consumo',	'Classificam-se neste código as compras de mercadorias destinadas ao uso ou consumo do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1557',	'Transferência de material para uso ou consumo',	'Classificam-se neste código as entradas de materiais para uso ou consumo recebidos em transferência de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1600',	'CRÉDITOS E RESSARCIMENTOS DE ICMS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1601',	'Recebimento, por transferência, de crédito de ICMS',	'Classificam-se neste código os lançamentos destinados ao registro de créditos de ICMS, recebidos por transferência de outras empresas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1602',	'Recebimento, por transferência, de saldo credor do ICMS, de outro estabelecimento da mesma empresa, para compensação de saldo devedor do imposto.',	'Lançamento destinado ao registro da transferência de saldo credor do ICMS, recebido de outro estabelecimento da mesma empresa, destinado à compensação do saldo devedor do estabelecimento, inclusive no caso de apuração centralizada do imposto.\r\n\r\n(NR Ajuste SINIEF 9/2003 – a partir 01.01.2004) (Decreto nº 26.174/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1603',	'Ressarcimento de ICMS retido por substituição tributária',	'Lançamento destinado ao registro de ressarcimento de ICMS retido por substituição tributária à contribuinte substituído, efetuado pelo contribuinte substituto, ou, ainda, quando o ressarcimento for apropriado pelo próprio contribuinte substituído, nas hipóteses previstas na legislação aplicável.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1604',	'Lançamento do crédito relativo à compra de bem para o ativo imobilizado',	'Lançamento destinado ao registro da apropriação de crédito de bem do ativo imobilizado. (Dec.25.068/2003-EFEITOS A PARTIR DE 01.01.2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1605',	'Recebimento, por transferência, de saldo devedor do ICMS de outro estabelecimento da mesma empresa',	'Lançamento destinado ao registro da transferência de saldo devedor do ICMS, recebido de outro estabelecimento da mesma empresa, para efetivação da apuração centralizada do imposto. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1650',	'ENTRADAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTES (ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004)',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1651',	'Compra de combustível ou lubrificante para industrialização subseqüente',	'Compra de combustível ou lubrificante a ser utilizados em processo de industrialização do próprio produto. (ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1652',	'Compra de combustível ou lubrificante para comercialização',	'Compra de combustível ou lubrificante a ser comercializados. (ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1653',	'Compra de combustível ou lubrificante por consumidor ou usuário final',	'Compra de combustível ou lubrificante, a ser consumidos em processo de industrialização de outros produtos, na produção rural, na prestação de serviço ou por usuário final.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1658',	'Transferência de combustível ou lubrificante para industrialização',	'Entrada de combustível ou lubrificante, recebidos em transferência de outro estabelecimento da mesma empresa, para ser utilizados em processo de industrialização do próprio produto.(Decreto 26.174/2003)(efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1659',	'Transferência de combustível ou lubrificante para comercialização',	'Entrada de combustível ou lubrificante, recebidos em transferência de outro estabelecimento da mesma empresa, para ser comercializados. .(Decreto 26.174/2003) (efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1660',	'Devolução de venda de combustível ou lubrificante destinados à industrialização subseqüente',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante destinados à industrialização subseqüente\". (Decreto 26.174/2003)(efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1661',	'Devolução de venda de combustível ou lubrificante destinados à comercialização',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante para comercialização\".(Decreto 26.174/2003)(efeitos a partir 01.01.2004).');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1662',	'Devolução de venda de combustível ou lubrificante destinados a consumidor ou usuário final',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante por consumidor ou usuário final\"..(Decreto 26.174/2003)(efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1663',	'Entrada de combustível ou lubrificante para armazenagem',	'Entrada de combustível ou lubrificante para armazenagem. .(Decreto 26.174/2003)(efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1664',	'Retorno de combustível ou lubrificante remetidos para armazenagem',	'Entrada, ainda que simbólica, por retorno de combustível ou lubrificante, remetidos para armazenagem. .(Decreto 26.174/2003)(efeitos a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1900',	'OUTRAS ENTRADAS DE MERCADORIAS OU AQUISIÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1901',	'Entrada para industrialização por encomenda',	'Classificam-se neste código as entradas de insumos recebidos para industrialização por encomenda de outra empresa ou de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1902',	'Retorno de mercadoria remetida para industrialização por encomenda',	'Classificam-se neste código o retorno dos insumos remetidos para industrialização por encomenda, incorporados ao produto final pelo estabelecimento industrializador.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1903',	'Entrada de mercadoria remetida para industrialização e não aplicada no referido processo',	'Classificam-se neste código as entradas em devolução de insumos remetidos para industrialização e não aplicados no referido processo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1904',	'Retorno de remessa para venda fora do estabelecimento',	'Classificam-se neste código as entradas em retorno de mercadorias remetidas para venda fora do estabelecimento, inclusive por meio de veículos, e não comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1905',	'Entrada de mercadoria recebida para depósito em depósito fechado ou armazém geral',	'Classificam-se neste código as entradas de mercadorias recebidas para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1906',	'Retorno de mercadoria remetida para depósito fechado ou armazém geral',	'Classificam-se neste código as entradas em retorno de mercadorias remetidas para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1907',	'Retorno simbólico de mercadoria remetida para depósito fechado ou armazém geral',	'Classificam-se neste código as entradas em retorno simbólico de mercadorias remetidas para depósito em depósito fechado ou armazém geral, quando as mercadorias depositadas tenham sido objeto de saída a qualquer título e que não tenham retornado ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1908',	'Entrada de bem por conta de contrato de comodato',	'Classificam-se neste código as entradas de bens recebidos em cumprimento de contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1909',	'Retorno de bem remetido por conta de contrato de comodato',	'Classificam-se neste código as entradas de bens recebidos em devolução após cumprido o contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1910',	'Entrada de bonificação, doação ou brinde',	'Classificam-se neste código as entradas de mercadorias recebidas a título de bonificação, doação ou brinde.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1911',	'Entrada de amostra grátis',	'Classificam-se neste código as entradas de mercadorias recebidas a título de amostra grátis.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1912',	'Entrada de mercadoria ou bem recebido para demonstração',	'Classificam-se neste código as entradas de mercadorias ou bens recebidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1913',	'Retorno de mercadoria ou bem remetido para demonstração',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1914',	'Retorno de mercadoria ou bem remetido para exposição ou feira',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para exposição ou feira.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1915',	'Entrada de mercadoria ou bem recebido para conserto ou reparo',	'Classificam-se neste código as entradas de mercadorias ou bens recebidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1916',	'Retorno de mercadoria ou bem remetido para conserto ou reparo',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1917',	'Entrada de mercadoria recebida em consignação mercantil ou industrial',	'Classificam-se neste código as entradas de mercadorias recebidas a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1918',	'Devolução de mercadoria remetida em consignação mercantil ou industrial',	'Classificam-se neste código as entradas por devolução de mercadorias remetidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1919',	'Devolução simbólica de mercadoria vendida ou utilizada em processo industrial, remetida anteriormente em consignação mercantil ou industrial',	'Classificam-se neste código as entradas por devolução simbólica de mercadorias vendidas ou utilizadas em processo industrial, remetidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1920',	'Entrada de vasilhame ou sacaria',	'Classificam-se neste código as entradas de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1921',	'Retorno de vasilhame ou sacaria',	'Classificam-se neste código as entradas em retorno de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1922',	'Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro',	'Classificam-se neste código os registros efetuados a título de simples faturamento decorrente de compra para recebimento futuro.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1923',	'Entrada de mercadoria recebida do vendedor remetente, em venda à ordem',	'Classificam-se neste código as entradas de mercadorias recebidas do vendedor remetente, em vendas à ordem, cuja compra do adquirente originário, foi classificada nos códigos 1.120 - Compra para industrialização, em venda à ordem, já recebida do vendedor remetente ou 1.121 - Compra para comercialização, em venda à ordem, já recebida do vendedor remetente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1924',	'Entrada para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as entradas de insumos recebidos para serem industrializados por conta e ordem do adquirente, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente dos mesmos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1925',	'Retorno de mercadoria remetida para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código o retorno dos insumos remetidos por conta e ordem do adquirente, para industrialização e incorporados ao produto final pelo estabelecimento industrializador, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1926',	'Lançamento efetuado a título de reclassificação de mercadoria decorrente de formação de kit ou de sua desagregação',	'Classificam-se neste código os registros efetuados a título de reclassificação decorrente de formação de kit de mercadorias ou de sua desagregação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1931',	'Lançamento efetuado pelo tomador do serviço de transporte, quando a responsabilidade de retenção do imposto for atribuída ao remetente ou alienante da mercadoria, pelo serviço de transporte realizado ',	'Lançamento efetuado pelo tomador do serviço de transporte realizado por transportador autônomo ou por transportador não-inscrito na Unidade da Federação onde se tenha iniciado o serviço, quando a responsabilidade pela retenção do imposto for atribuída ao remetente ou alienante da mercadoria.\r\n\r\n (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004))(efeitos a partir 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1932',	'Aquisição de serviço de transporte iniciado em Unidade da Federação diversa daquela onde esteja inscrito o prestador',	'Aquisição de serviço de transporte que tenha sido iniciado em Unidade da Federação diversa daquela onde o prestador esteja inscrito como contribuinte. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (efeitos a partir 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1933',	'Aquisição de serviço tributado pelo Imposto sobre Serviços de Qualquer Natureza (Ajuste SINIEF 06/2005) (NR)',	'Aquisição de serviço, cujo imposto é de competência municipal, desde que informado em Nota Fiscal modelo 1 ou 1-A. (NR Ajuste SINIEF 06/2005) (DECRETO Nº 26.868/2006 - efeitos a partir 01.01.2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('1949',	'Outra entrada de mercadoria ou prestação de serviço não especificada',	'Classificam-se neste código as outras entradas de mercadorias ou prestações de serviços que não tenham sido especificadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2000',	'ENTRADAS OU AQUISIÇÕES DE SERVIÇOS DE OUTROS ESTADOS',	'Classificam-se, neste grupo, as operações ou prestações em que o estabelecimento remetente esteja localizado em unidade da Federação diversa daquela do destinatário');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2100',	'COMPRAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU PRESTAÇÃO DE SERVIÇOS (NR Ajuste SINIEF 05/2005  (Decreto 28.868/2006)',	'(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2101',	'Compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria a ser utilizada em processo de industrialização ou produção rural, bem como a entrada de mercadoria em estabelecimento industrial ou produtor rural de cooperativa, recebida de seus cooperados ou de estabelecimento de outra cooperativa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2102',	'Compra para comercialização',	'Classificam-se neste código as compras de mercadorias a serem comercializadas. Também serão classificadas neste código as entradas de mercadorias em estabelecimento comercial de cooperativa recebidas de seus cooperados ou de estabelecimento de outra cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2111',	'Compra para industrialização de mercadoria recebida anteriormente em consignação industrial',	'Classificam-se neste código as compras efetivas de mercadorias a serem utilizadas em processo de industrialização, recebidas anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2113',	'Compra para comercialização, de mercadoria recebida anteriormente em consignação mercantil',	'Classificam-se neste código as compras efetivas de mercadorias recebidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2116',	'Compra para industrialização ou produção rural originada de encomenda para recebimento futuro (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria a ser utilizada em processo de industrialização ou produção rural, quando da entrada real da mercadoria, cuja aquisição tenha sido classificada no código \"2.922 – Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2117',	'Compra para comercialização originada de encomenda para recebimento futuro',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, quando da entrada real da mercadoria, cuja aquisição tenha sido classificada no código 2.922 - Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2118',	'Compra de mercadoria para comercialização pelo adquirente originário, entregue pelo vendedor remetente ao destinatário, em venda à ordem',	'Classificam-se neste código as compras de mercadorias já comercializadas, que, sem transitar pelo estabelecimento do adquirente originário, sejam entregues pelo vendedor remetente diretamente ao destinatário, em operação de venda à ordem, cuja venda seja classificada, pelo adquirente originário, no código 6.120 - Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário pelo vendedor remetente, em venda à ordem.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2120',	'Compra para industrialização, em venda à ordem, já recebida do vendedor remetente',	'Classificam-se neste código as compras de mercadorias a serem utilizadas em processo de industrialização, em vendas à ordem, já recebidas do vendedor remetente, por ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2121',	'Compra para comercialização, em venda à ordem, já recebida do vendedor remetente',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, em vendas à ordem, já recebidas do vendedor remetente por ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2122',	'Compra para industrialização em que a mercadoria foi remetida pelo fornecedor ao industrializador sem transitar pelo estabelecimento adquirente',	'Classificam-se neste código as compras de mercadorias a serem utilizadas em processo de industrialização, remetidas pelo fornecedor para o industrializador sem que a mercadoria tenha transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2124',	'Industrialização efetuada por outra empresa',	'Classificam-se neste código as entradas de mercadorias industrializadas por terceiros, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial. Quando a industrialização efetuada se referir a bens do ativo imobilizado ou de mercadorias para uso ou consumo do estabelecimento encomendante, a entrada deverá ser classificada nos códigos 2.551 - Compra de bem para o ativo imobilizado ou 2.556 - Compra de material para uso ou consumo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2125',	'Industrialização efetuada por outra empresa quando a mercadoria remetida para utilização no processo de industrialização não transitou pelo estabelecimento adquirente da mercadoria',	'Classificam-se neste código as entradas de mercadorias industrializadas por outras empresas, em que as mercadorias remetidas para utilização no processo de industrialização não transitaram pelo estabelecimento do adquirente das mercadorias, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial. Quando a industrialização efetuada se referir a bens do ativo imobilizado ou de mercadorias para uso ou consumo do estabelecimento encomend');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2126',	'Compra para utilização na prestação de serviço',	'Classificam-se neste código as entradas de mercadorias a serem utilizadas nas prestações de serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2150',	'TRANSFERÊNCIAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU PRESTAÇÃO DE SERVIÇOS (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2151',	'Transferência para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Entrada de mercadoria, recebida em transferência de outro estabelecimento da mesma empresa, para ser utilizada em processo de industrialização ou produção rural.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2152',	'Transferência para comercialização',	'Classificam-se neste código as entradas de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2153',	'Transferência de energia elétrica para distribuição',	'Classificam-se neste código as entradas de energia elétrica recebida em transferência de outro estabelecimento da mesma empresa, para distribuição.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2154',	'Transferência para utilização na prestação de serviço',	'Classificam-se neste código as entradas de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem utilizadas nas prestações de serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2200',	'DEVOLUÇÕES DE VENDAS DE PRODUÇÃO DO ESTABELECIMENTO OU DE TERCEIROS OU ANULAÇÕES DE VALORES',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2201',	'Devolução de venda de produção do estabelecimento',	'Devolução de venda de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada no código \"6.101 - Venda de produção do estabelecimento\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2202',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros',	'Devolução de vendas de mercadoria, adquirida ou recebida de terceiro, que não tenham sido objeto de industrialização no estabelecimento, cuja saída tenha sido classificada como Venda de mercadoria adquirida ou recebida de terceiros.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2203',	'Devolução de venda de produção do estabelecimento destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Devolução de venda de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada no código \"6.109 – Venda de produção do estabelecimento destinada à Zona Franca de Manaus ou Áreas de Livre Comércio\".\r\n\r\n (NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2204',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Devolução de venda de mercadoria adquirida ou recebida de terceiro, cuja saída tenha sido classificada no código “6.110 - Venda de mercadoria adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio”.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2205',	'Anulação de valor relativo à prestação de serviço de comunicação',	'Anulação correspondente a valor faturado indevidamente, decorrente de prestação de serviço de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2206',	'Anulação de valor relativo à prestação de serviço de transporte',	'Anulação correspondente a valor faturado indevidamente, decorrente de prestação de serviço de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2207',	'Anulação de valor relativo à venda de energia elétrica',	'Anulação correspondente a valor faturado indevidamente, decorrente de venda de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2208',	'Devolução de produção do estabelecimento, remetida em transferência.',	'Devolução de produto industrializado ou produzido pelo estabelecimento e transferido para outro estabelecimento da mesma empresa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2209',	'Devolução de mercadoria adquirida ou recebida de terceiros e remetida em transferência',	'Devolução de mercadoria adquirida ou recebida de terceiros, transferidas para outros estabelecimentos da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2250',	'COMPRAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2251',	'Compra de energia elétrica para distribuição ou comercialização',	'Classificam-se neste código as compras de energia elétrica utilizada em sistema de distribuição ou comercialização. Também serão classificadas neste código as compras de energia elétrica por cooperativas para distribuição com seus cooperados.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2252',	'Compra de energia elétrica por estabelecimento industrial',	'Classificam-se neste código as compras de energia elétrica utilizada no processo de industrialização. Também serão classificadas neste código as compras de energia elétrica utilizada por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2253',	'Compra de energia elétrica por estabelecimento comercial',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento comercial. Também serão classificadas neste código as compras de energia elétrica utilizada por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2254',	'Compra de energia elétrica por estabelecimento prestador de serviço de transporte',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento prestador de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2255',	'Compra de energia elétrica por estabelecimento prestador de serviço de comunicação',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2256',	'Compra de energia elétrica por estabelecimento de produtor rural',	'Classificam-se neste código as compras de energia elétrica utilizada por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2257',	'Compra de energia elétrica para consumo por demanda contratada',	'Classificam-se neste código as compras de energia elétrica para consumo por demanda contratada, que prevalecerá sobre os demais códigos deste subgrupo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2300',	'AQUISIÇÕES DE SERVIÇOS DE COMUNICAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2301',	'Aquisição de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2302',	'Aquisição de serviço de comunicação por estabelecimento industrial',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento industrial. Também serão classificadas neste código as aquisições de serviços de comunicação utilizados por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2303',	'Aquisição de serviço de comunicação por estabelecimento comercial',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento comercial. Também serão classificadas neste código as aquisições de serviços de comunicação utilizados por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2304',	'Aquisição de serviço de comunicação por estabelecimento de prestador de serviço de transporte',	'Classificam-se neste código as aquisições de serviços de comunicação utilizado por estabelecimento prestador de serviço de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2305',	'Aquisição de serviço de comunicação por estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2306',	'Aquisição de serviço de comunicação por estabelecimento de produtor rural',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2350',	'AQUISIÇÕES DE SERVIÇOS DE TRANSPORTE',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2351',	'Aquisição de serviço de transporte para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de transporte utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2352',	'Aquisição de serviço de transporte por estabelecimento industrial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2353',	'Aquisição de serviço de transporte por estabelecimento comercial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2354',	'Aquisição de serviço de transporte por estabelecimento de prestador de serviço de comunicação',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2355',	'Aquisição de serviço de transporte por estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2356',	'Aquisição de serviço de transporte por estabelecimento de produtor rural',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2400',	'ENTRADAS DE MERCADORIAS SUJEITAS AO REGIME DE SUBSTITUIÇÃO TRIBUTÁRIA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2401',	'Compra para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria, sujeita ao regime de substituição tributária, a ser utilizada em processo de industrialização ou produção rural, bem como compra, por estabelecimento industrial ou produtor rural de cooperativa, de mercadoria sujeita ao mencionado regime.\n\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2403',	'Compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de mercadorias a serem comercializadas, decorrentes de operações com mercadorias sujeitas ao regime de substituição tributária. Também serão classificadas neste código as compras de mercadorias sujeitas ao regime de substituição tributária em estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2406',	'Compra de bem para o ativo imobilizado cuja mercadoria está sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de bens destinados ao ativo imobilizado do estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2407',	'Compra de mercadoria para uso ou consumo cuja mercadoria está sujeita ao regime de substituição tributária',	'Classificam-se neste código as compras de mercadorias destinadas ao uso ou consumo do estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2408',	'Transferência para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Entrada de mercadoria, sujeita ao regime de substituição tributária, recebida em transferência de outro estabelecimento da mesma empresa, para ser industrializada ou consumida na produção rural no estabelecimento destinatário.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2409',	'Transferência para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas, decorrentes de operações sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2410',	'Devolução de venda de produção do estabelecimento, quando o produto estiver sujeito ao regime de substituição tributária',	'Devolução de produto industrializado ou produzido pelo estabelecimento, cuja saída tenha sido classificada como \"Venda de produção do estabelecimento quando o produto estiver sujeito ao regime de substituição tributária\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2411',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária',	'Devolução de vendas de mercadoria adquirida ou recebida de terceiro, cuja saída tenha sido classificada como “Venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária”.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2414',	'Retorno de produção do estabelecimento, remetida para venda fora do estabelecimento, quando o produto estiver sujeito ao regime de substituição tributária',	'Entrada, em retorno, de produto industrializado ou produzido pelo estabelecimento sujeito ao regime de substituição tributária, remetido para venda fora do estabelecimento, inclusive por meio de veículo, e não comercializado.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2415',	'Retorno de mercadoria adquirida ou recebida de terceiros, remetida para venda fora do estabelecimento em operação com mercadoria sujeita ao regime de substituição tributária',	'Entrada, em retorno, de mercadoria sujeita ao regime de substituição tributária, adquirida ou recebida de terceiro remetida para venda fora do estabelecimento, inclusive por meio de veículo, e não comercializada.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2500',	'ENTRADAS DE MERCADORIAS REMETIDAS PARA FORMAÇÃO DE LOTE OU COM FIM ESPECÍFICO DE EXPORTAÇÃO E EVENTUAIS DEVOLUÇÕES (NR Ajuste SINIEF 09/2005)',	'(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2501',	'Entrada de mercadoria recebida com fim específico de exportação',	'Classificam-se neste código as entradas de mercadorias em estabelecimento de trading company, empresa comercial exportadora ou outro estabelecimento do remetente, com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2503',	'Entrada decorrente de devolução de produto industrializado pelo estabelecimento, remetido com fim específico de exportação',	'Devolução de produto industrializado ou produzido pelo estabelecimento, remetido a \"trading company\", a empresa comercial exportadora ou a outro estabelecimento do remetente, com fim específico de exportação, cuja saída tenha sido classificada no código \"6.501 – Remessa de produção do estabelecimento com fim específico de exportação\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2504',	'Entrada decorrente de devolução de mercadoria remetida com fim específico de exportação, adquirida ou recebida de terceiros',	'Classificam-se neste código as devoluções de mercadorias adquiridas ou recebidas de terceiros remetidas a trading company, a empresa comercial exportadora ou a outro estabelecimento do remetente, com fim específico de exportação, cujas saídas tenham sido classificadas no código 6.502 - Remessa de mercadoria adquirida ou recebida de terceiros, com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2505',	'Entrada decorrente de devolução simbólica de mercadoria remetida para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.',	'Devolução simbólica de mercadoria remetida para formação de lote de exportação, cuja saída tenha sido classificada no código \"6.504 – Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento\". (ACR Ajuste SINIEF 09/2005) (Decreto 28.868/2006)\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2506',	'Entrada decorrente de devolução simbólica de mercadoria, adquirida ou recebida de terceiros, remetida para formação de lote de exportação.',	'Devolução simbólica de mercadoria remetida para formação de lote de exportação em armazéns alfandegados, entrepostos aduaneiros ou outros estabelecimentos que venham a ser regulamentados pela legislação tributária de cada Unidade Federada, efetuada pelo estabelecimento depositário, cuja saída tenha sido classificada no código \"6.505 – Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação\".\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação a');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2551',	'Compra de bem para o ativo imobilizado',	'Classificam-se neste código as compras de bens destinados ao ativo imobilizado do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2552',	'Transferência de bem do ativo imobilizado',	'Classificam-se neste código as entradas de bens destinados ao ativo imobilizado recebidos em transferência de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2553',	'Devolução de venda de bem do ativo imobilizado',	'Classificam-se neste código as devoluções de vendas de bens do ativo imobilizado, cujas saídas tenham sido classificadas no código 6.551 - Venda de bem do ativo imobilizado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2554',	'Retorno de bem do ativo imobilizado remetido para uso fora do estabelecimento',	'Classificam-se neste código as entradas por retorno de bens do ativo imobilizado remetidos para uso fora do estabelecimento, cujas saídas tenham sido classificadas no código 6.554 - Remessa de bem do ativo imobilizado para uso fora do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2555',	'Entrada de bem do ativo imobilizado de terceiro, remetido para uso no estabelecimento',	'Classificam-se neste código as entradas de bens do ativo imobilizado de terceiros, remetidos para uso no estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2556',	'Compra de material para uso ou consumo',	'Classificam-se neste código as compras de mercadorias destinadas ao uso ou consumo do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2557',	'Transferência de material para uso ou consumo',	'Classificam-se neste código as entradas de materiais para uso ou consumo recebidos em transferência de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2600',	'CRÉDITOS E RESSARCIMENTOS DE ICMS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2603',	'Ressarcimento de ICMS retido por substituição tributária',	'Classificam-se neste código os lançamentos destinados ao registro de ressarcimento de ICMS retido por substituição tributária a contribuinte substituído, efetuado pelo contribuinte substituto, nas hipóteses previstas na legislação aplicável.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2650',	'ENTRADAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTES (ACR Ajuste SINIEF 9/2003)',	'(ACR Ajuste SINIEF 05/2005) (Dec.28.868/2006 )');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2651',	'Compra de combustível ou lubrificante para industrialização subseqüente',	'Compra de combustível ou lubrificante a ser utilizados em processo de industrialização do próprio produto. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2652',	'Compra de combustível ou lubrificante para comercialização',	'Compra de combustível ou lubrificante a ser comercializados. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2653',	'Compra de combustível ou lubrificante por consumidor ou usuário final',	'Compra de combustível ou lubrificante, a ser consumidos em processo de industrialização de outros produtos, na produção rural, na prestação de serviço ou por usuário final.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2658',	'Transferência de combustível ou lubrificante para industrialização',	'Entrada de combustível ou lubrificante, recebidos em transferência de outro estabelecimento da mesma empresa, para ser utilizados em processo de industrialização do próprio produto. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2659',	'Transferência de combustível ou lubrificante para comercialização',	'Entrada de combustível ou lubrificante, recebidos em transferência de outro estabelecimento da mesma empresa, para ser comercializados. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2660',	'Devolução de venda de combustível ou lubrificante destinados à industrialização subseqüente',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante destinados à industrialização subseqüente\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2661',	'Devolução de venda de combustível ou lubrificante destinados à comercialização',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante para comercialização\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2662',	'Devolução de venda de combustível ou lubrificante destinados a consumidor ou usuário final',	'Devolução de venda de combustível ou lubrificante, cuja saída tenha sido classificada como \"Venda de combustível ou lubrificante por consumidor ou usuário final\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2663',	'Entrada de combustível ou lubrificante para armazenagem',	'Entrada de combustível ou lubrificante para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2664',	'Retorno de combustível ou lubrificante remetidos para armazenagem',	'Entrada, ainda que simbólica, por retorno de combustível ou lubrificante, remetidos para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2900',	'OUTRAS ENTRADAS DE MERCADORIAS OU AQUISIÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2901',	'Entrada para industrialização por encomenda',	'Classificam-se neste código as entradas de insumos recebidos para industrialização por encomenda de outra empresa ou de outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2902',	'Retorno de mercadoria remetida para industrialização por encomenda',	'Classificam-se neste código o retorno dos insumos remetidos para industrialização por encomenda, incorporados ao produto final pelo estabelecimento industrializador.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2903',	'Entrada de mercadoria remetida para industrialização e não aplicada no referido processo',	'Classificam-se neste código as entradas em devolução de insumos remetidos para industrialização e não aplicados no referido processo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2904',	'Retorno de remessa para venda fora do estabelecimento',	'Classificam-se neste código as entradas em retorno de mercadorias remetidas para venda fora do estabelecimento, inclusive por meio de veículos, e não comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2905',	'Entrada de mercadoria recebida para depósito em depósito fechado ou armazém geral',	'Classificam-se neste código as entradas de mercadorias recebidas para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2906',	'Retorno de mercadoria remetida para depósito fechado ou armazém geral',	'Classificam-se neste código as entradas em retorno de mercadorias remetidas para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2907',	'Retorno simbólico de mercadoria remetida para depósito fechado ou armazém geral',	'Classificam-se neste código as entradas em retorno simbólico de mercadorias remetidas para depósito em depósito fechado ou armazém geral, quando as mercadorias depositadas tenham sido objeto de saída a qualquer título e que não tenham retornado ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2908',	'Entrada de bem por conta de contrato de comodato',	'Classificam-se neste código as entradas de bens recebidos em cumprimento de contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2909',	'Retorno de bem remetido por conta de contrato de comodato',	'Classificam-se neste código as entradas de bens recebidos em devolução após cumprido o contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2910',	'Entrada de bonificação, doação ou brinde',	'Classificam-se neste código as entradas de mercadorias recebidas a título de bonificação, doação ou brinde.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2911',	'Entrada de amostra grátis',	'Classificam-se neste código as entradas de mercadorias recebidas a título de amostra grátis.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2912',	'Entrada de mercadoria ou bem recebido para demonstração',	'Classificam-se neste código as entradas de mercadorias ou bens recebidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2913',	'Retorno de mercadoria ou bem remetido para demonstração',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2914',	'Retorno de mercadoria ou bem remetido para exposição ou feira',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para exposição ou feira.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2915',	'Entrada de mercadoria ou bem recebido para conserto ou reparo',	'Classificam-se neste código as entradas de mercadorias ou bens recebidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2916',	'Retorno de mercadoria ou bem remetido para conserto ou reparo',	'Classificam-se neste código as entradas em retorno de mercadorias ou bens remetidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2917',	'Entrada de mercadoria recebida em consignação mercantil ou industrial',	'Classificam-se neste código as entradas de mercadorias recebidas a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2918',	'Devolução de mercadoria remetida em consignação mercantil ou industrial',	'Classificam-se neste código as entradas por devolução de mercadorias remetidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2919',	'Devolução simbólica de mercadoria vendida ou utilizada em processo industrial, remetida anteriormente em consignação mercantil ou industrial',	'Classificam-se neste código as entradas por devolução simbólica de mercadorias vendidas ou utilizadas em processo industrial, remetidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2920',	'Entrada de vasilhame ou sacaria',	'Classificam-se neste código as entradas de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2921',	'Retorno de vasilhame ou sacaria',	'Classificam-se neste código as entradas em retorno de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2922',	'Lançamento efetuado a título de simples faturamento decorrente de compra para recebimento futuro',	'Classificam-se neste código os registros efetuados a título de simples faturamento decorrente de compra para recebimento futuro.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2923',	'Entrada de mercadoria recebida do vendedor remetente, em venda à ordem',	'Classificam-se neste código as entradas de mercadorias recebidas do vendedor remetente, em vendas à ordem, cuja compra do adquirente originário, foi classificada nos códigos 2.120 - Compra para industrialização, em venda à ordem, já recebida do vendedor remetente ou 2.121 - Compra para comercialização, em venda à ordem, já recebida do vendedor remetente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2924',	'Entrada para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as entradas de insumos recebidos para serem industrializados por conta e ordem do adquirente, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente dos mesmos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2925',	'Retorno de mercadoria remetida para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código o retorno dos insumos remetidos por conta e ordem do adquirente, para industrialização e incorporados ao produto final pelo estabelecimento industrializador, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2931',	'Lançamento efetuado pelo tomador do serviço de transporte, quando a responsabilidade de retenção do imposto for atribuída ao remetente ou alienante da mercadoria, pelo serviço de transporte realizado ',	'Lançamento efetuado pelo tomador do serviço de transporte realizado por transportador autônomo ou por transportador não-inscrito na Unidade da Federação onde se tenha iniciado o serviço, quando a responsabilidade pela retenção do imposto for atribuída ao remetente ou alienante da mercadoria.\r\n\r\n (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2932',	'Aquisição de serviço de transporte iniciado em Unidade da Federação diversa daquela onde esteja inscrito o prestador',	'Aquisição de serviço de transporte que tenha sido iniciado em Unidade da Federação diversa daquela onde o prestador esteja inscrito como contribuinte. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2933',	'Aquisição de serviço tributado pelo Imposto Sobre Serviços de Qualquer Natureza',	'Aquisição de serviço, cujo imposto é de competência municipal, desde que informado em Nota Fiscal modelo 1 e 1-A. (NR Ajuste SINIEF 06/2005) (a partir de 01.01.2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('2949',	'Outra entrada de mercadoria ou prestação de serviço não especificado',	'Classificam-se neste código as outras entradas de mercadorias ou prestações de serviços que não tenham sido especificados nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3000',	'ENTRADAS OU AQUISIÇÕES DE SERVIÇOS DO EXTERIOR',	'Classificam-se, neste grupo, as entradas de mercadorias oriundas de outro país, inclusive as decorrentes de aquisição por arrematação, concorrência ou qualquer outra forma de alienação promovida pelo poder público, e os serviços iniciados no exterior');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3100',	'COMPRAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU PRESTAÇÃO DE SERVIÇOS (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3101',	'Compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Compra de mercadoria a ser utilizada em processo de industrialização ou produção rural, bem como a entrada de mercadoria em estabelecimento industrial ou produtor rural de cooperativa.\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3102',	'Compra para comercialização',	'Classificam-se neste código as compras de mercadorias a serem comercializadas. Também serão classificadas neste código as entradas de mercadorias em estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3126',	'Compra para utilização na prestação de serviço',	'Classificam-se neste código as entradas de mercadorias a serem utilizadas nas prestações de serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3127',	'Compra para industrialização sob o regime de drawback',	'Classificam-se neste código as compras de mercadorias a serem utilizadas em processo de industrialização e posterior exportação do produto resultante, cujas vendas serão classificadas no código 7.127 - Venda de produção do estabelecimento sob o regime de drawback.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3200',	'DEVOLUÇÕES DE VENDAS DE PRODUÇÃO PRÓPRIA, DE TERCEIROS OU ANULAÇÕES DE VALORES',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3201',	'Devolução de venda de produção do estabelecimento',	'Devolução de venda de produto industrializado ou produzido pelo próprio estabelecimento, cuja saída tenha sido classificada como \"Venda de produção do estabelecimento\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3202',	'Devolução de venda de mercadoria adquirida ou recebida de terceiros',	'Classificam-se neste código as devoluções de vendas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de industrialização no estabelecimento, cujas saídas tenham sido classificadas como Venda de mercadoria adquirida ou recebida de terceiros.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3205',	'Anulação de valor relativo à prestação de serviço de comunicação',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de prestações de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3206',	'Anulação de valor relativo à prestação de serviço de transporte',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de prestações de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3207',	'Anulação de valor relativo à venda de energia elétrica',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes de venda de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3211',	'Devolução de venda de produção do estabelecimento sob o regime de drawback',	'Classificam-se neste código as devoluções de vendas de produtos industrializados pelo estabelecimento sob o regime de drawback.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3250',	'COMPRAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3251',	'Compra de energia elétrica para distribuição ou comercialização',	'Classificam-se neste código as compras de energia elétrica utilizada em sistema de distribuição ou comercialização. Também serão classificadas neste código as compras de energia elétrica por cooperativas para distribuição aos seus cooperados.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3301',	'Aquisição de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de comunicação utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3350',	'AQUISIÇÕES DE SERVIÇOS DE TRANSPORTE',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3351',	'Aquisição de serviço de transporte para execução de serviço da mesma natureza',	'Classificam-se neste código as aquisições de serviços de transporte utilizados nas prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3352',	'Aquisição de serviço de transporte por estabelecimento industrial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3353',	'Aquisição de serviço de transporte por estabelecimento comercial',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial. Também serão classificadas neste código as aquisições de serviços de transporte utilizados por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3354',	'Aquisição de serviço de transporte por estabelecimento de prestador de serviço de comunicação',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3355',	'Aquisição de serviço de transporte por estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3356',	'Aquisição de serviço de transporte por estabelecimento de produtor rural',	'Classificam-se neste código as aquisições de serviços de transporte utilizados por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3500',	'ENTRADAS DE MERCADORIAS REMETIDAS COM FIM ESPECÍFICO DE EXPORTAÇÃO E EVENTUAIS DEVOLUÇÕES',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3503',	'Devolução de mercadoria exportada que tenha sido recebida com fim específico de exportação',	'Classificam-se neste código as devoluções de mercadorias exportadas por trading company, empresa comercial exportadora ou outro estabelecimento do remetente, recebidas com fim específico de exportação, cujas saídas tenham sido classificadas no código 7.501 - Exportação de mercadorias recebidas com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3551',	'Compra de bem para o ativo imobilizado',	'Classificam-se neste código as compras de bens destinados ao ativo imobilizado do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3553',	'Devolução de venda de bem do ativo imobilizado',	'Classificam-se neste código as devoluções de vendas de bens do ativo imobilizado, cujas saídas tenham sido classificadas no código 7.551 - Venda de bem do ativo imobilizado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3556',	'Compra de material para uso ou consumo',	'Classificam-se neste código as compras de mercadorias destinadas ao uso ou consumo do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3650',	'ENTRADAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTES',	'(ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004) (Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3651',	'Compra de combustível ou lubrificante para industrialização subseqüente',	'Compra de combustível ou lubrificante a ser utilizados em processo de industrialização do próprio produto. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3652',	'Compra de combustível ou lubrificante para comercialização',	'Compra de combustível ou lubrificante a ser comercializados. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3653',	'Compra de combustível ou lubrificante por consumidor ou usuário final',	'Compra de combustível ou lubrificante, a ser consumidos em processo de industrialização de outros produtos, na produção rural, na prestação de serviço ou por usuário final.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3900',	'OUTRAS ENTRADAS DE MERCADORIAS OU AQUISIÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3930',	'Lançamento efetuado a título de entrada de bem sob amparo de regime especial aduaneiro de admissão temporária',	'Lançamento efetuado a título de entrada de bem amparada por regime especial aduaneiro de admissão temporária. – (Decreto Nº 26.174 de 26/11/2003). a partir 01.01.2004   ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('3949',	'Outra entrada de mercadoria ou prestação de serviço não especificado',	'Outra entrada de mercadoria ou prestação de serviço que não tenham sido especificada nos códigos anteriores. – (Decreto Nº 26.174 de 26/11/2003). a partir 01.01.2004  ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5000',	'SAÍDAS OU PRESTAÇÕES DE SERVIÇOS PARA O ESTADO',	'Classificam-se, neste grupo, as operações ou prestações em que o estabelecimento remetente esteja localizado na mesma unidade da Federação do destinatário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5100',	'VENDAS DE PRODUÇÃO PRÓPRIA OU DE TERCEIROS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5101',	'Venda de produção do estabelecimento',	'Venda de produto industrializado ou produzido pelo estabelecimento, bem como a de mercadoria por estabelecimento industrial ou produtor rural de cooperativa destinada a seus cooperados ou a estabelecimento de outra cooperativa.\n\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5102',	'Venda de mercadoria adquirida ou recebida de terceiros',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial no estabelecimento. Também serão classificadas neste código as vendas de mercadorias por estabelecimento comercial de cooperativa destinadas a seus cooperados ou estabelecimento de outra cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5103',	'Venda de produção do estabelecimento efetuada fora do estabelecimento',	'Venda efetuada fora do estabelecimento, inclusive por meio de veículo, de produto industrializado ou produzido no estabelecimento.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5104',	'Venda de mercadoria adquirida ou recebida de terceiros, efetuada fora do estabelecimento',	'Classificam-se neste código as vendas efetuadas fora do estabelecimento, inclusive por meio de veículo, de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial no estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5105',	'Venda de produção do estabelecimento que não deva por ele transitar',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento, armazenados em depósito fechado, armazém geral ou outro sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5106',	'Venda de mercadoria adquirida ou recebida de terceiros, que não deva por ele transitar',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, armazenadas em depósito fechado, armazém geral ou outro, que não tenham sido objeto de qualquer processo industrial no estabelecimento sem que haja retorno ao estabelecimento depositante. Também serão classificadas neste código as vendas de mercadorias importadas, cuja saída ocorra do recinto alfandegado ou da repartição alfandegária onde se processou o desembaraço aduaneiro, com destino ao esta');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5109',	'Venda de produção do estabelecimento destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Venda de produto industrializado ou produzido pelo estabelecimento destinado à Zona Franca de Manaus ou Áreas de Livre Comércio.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5110',	'Venda de mercadoria, adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comercio, de que trata o Anexo do Convênio SINIEF s/n, de 15 de dezembro de 1970, que dispõ',	'Venda de mercadoria, adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio, desde que alcançada pelos benefícios fiscais de que tratam o Decreto-Lei nº 288, de 28 de fevereiro de 1967, o Convênio ICM 65/88, de 06 de dezembro de 1988, o Convênio ICMS 36/97, de 23 de maio de 1997, e o Convênio ICMS 37/97, de 23 de maio de 1997. (NR Ajuste SINIEF 09/2004) (Decreto nº 26.955/2004) RETROAGINDO SEUS EFEITOS A 24.06.2004.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5111',	'Venda de produção do estabelecimento remetida anteriormente em consignação industrial',	'Classificam-se neste código as vendas efetivas de produtos industrializados no estabelecimento remetidos anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5112',	'Venda de mercadoria adquirida ou recebida de terceiros remetida anteriormente em consignação industrial',	'Classificam-se neste código as vendas efetivas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5113',	'Venda de produção do estabelecimento remetida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas efetivas de produtos industrializados no estabelecimento remetidos anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5114',	'Venda de mercadoria adquirida ou recebida de terceiros remetida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas efetivas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5115',	'Venda de mercadoria adquirida ou recebida de terceiros, recebida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, recebidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5116',	'Venda de produção do estabelecimento originada de encomenda para entrega futura',	'Venda de produto industrializado ou produzido pelo estabelecimento, quando da saída real do produto, cujo faturamento tenha sido classificado no código \"5.922 – Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5118',	'Venda de produção do estabelecimento entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem',	'Classificam-se neste código as vendas à ordem de produtos industrializados pelo estabelecimento, entregues ao destinatário por conta e ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5119',	'Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem',	'Classificam-se neste código as vendas à ordem de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, entregues ao destinatário por conta e ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5120',	'Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário pelo vendedor remetente, em venda à ordem',	'Classificam-se neste código as vendas à ordem de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, entregues pelo vendedor remetente ao destinatário, cuja compra seja classificada, pelo adquirente originário, no código 1.118 - Compra de mercadoria pelo adquirente originário, entregue pelo vendedor remetente ao destinatário, em venda à ordem.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5122',	'Venda de produção do estabelecimento remetida para industrialização, por conta e ordem do adquirente, sem transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento, remetidos para serem industrializados em outro estabelecimento, por conta e ordem do adquirente, sem que os produtos tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5123',	'Venda de mercadoria adquirida ou recebida de terceiros remetida para industrialização, por conta e ordem do adquirente, sem transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas para serem industrializadas em outro estabelecimento, por conta e ordem do adquirente, sem que as mercadorias tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5124',	'Industrialização efetuada para outra empresa',	'Classificam-se neste código as saídas de mercadorias industrializadas para terceiros, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5125',	'Industrialização efetuada para outra empresa quando a mercadoria recebida para utilização no processo de industrialização não transitar pelo estabelecimento adquirente da mercadoria',	'Classificam-se neste código as saídas de mercadorias industrializadas para outras empresas, em que as mercadorias recebidas para utilização no processo de industrialização não tenham transitado pelo estabelecimento do adquirente das mercadorias, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5150',	'TRANSFERÊNCIAS DE PRODUÇÃO PRÓPRIA OU DE TERCEIROS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5151',	'Transferência de produção do estabelecimento',	'Transferência de produto industrializado ou produzido no estabelecimento para outro estabelecimento da mesma empresa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5152',	'Transferência de mercadoria adquirida ou recebida de terceiros',	'Mercadoria adquirida ou recebida de terceiros para industrialização, comercialização ou utilização na prestação de serviço e que não tenha sido objeto de qualquer processo industrial no estabelecimento, transferida para outro estabelecimento da mesma empresa. A partir  10 de julho de 2003. (Decreto nº 26.020/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5153',	'Transferência de energia elétrica',	'Classificam-se neste código as transferências de energia elétrica para outro estabelecimento da mesma empresa, para distribuição.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5155',	'Transferência de produção do estabelecimento, que não deva por ele transitar',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de produtos industrializados no estabelecimento que tenham sido remetidos para armazém geral, depósito fechado ou outro, sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5156',	'Transferência de mercadoria adquirida ou recebida de terceiros, que não deva por ele transitar',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial, remetidas para armazém geral, depósito fechado ou outro, sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5200',	'DEVOLUÇÕES DE COMPRAS PARA INDUSTRIALIZAÇÃO, PRODUÇÃO RURAL, COMERCIALIZAÇÃO OU ANULAÇÕES DE VALORES (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5201',	'Devolução de compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Devolução de mercadoria adquirida para ser utilizada em processo de industrialização ou produção rural, cuja entrada tenha sido classificada como \"1.101 - Compra para industrialização ou produção rural\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5202',	'Devolução de compra para comercialização',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem comercializadas, cujas entradas tenham sido classificadas como Compra para comercialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5205',	'Anulação de valor relativo a aquisição de serviço de comunicação',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5206',	'Anulação de valor relativo a aquisição de serviço de transporte',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5207',	'Anulação de valor relativo à compra de energia elétrica',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes da compra de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5208',	'Devolução de mercadoria recebida em transferência para industrialização ou produção rural',	'Devolução de mercadoria recebida em transferência de outro estabelecimento da mesma empresa, para ser utilizada em processo de industrialização ou produção rural.\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5209',	'Devolução de mercadoria recebida em transferência para comercialização',	'Classificam-se neste código as devoluções de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5210',	'Devolução de compra para utilização na prestação de serviço',	'Classificam-se neste código as devoluções de mercadorias adquiridas para utilização na prestação de serviços, cujas entradas tenham sido classificadas no código 1.126 - Compra para utilização na prestação de serviço.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5250',	'VENDAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5251',	'Venda de energia elétrica para distribuição ou comercialização',	'Classificam-se neste código as vendas de energia elétrica destinada à distribuição ou comercialização. Também serão classificadas neste código as vendas de energia elétrica destinada a cooperativas para distribuição aos seus cooperados.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5252',	'Venda de energia elétrica para estabelecimento industrial',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento industrial. Também serão classificadas neste código as vendas de energia elétrica destinada a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5253',	'Venda de energia elétrica para estabelecimento comercial',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento comercial. Também serão classificadas neste código as vendas de energia elétrica destinada a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5254',	'Venda de energia elétrica para estabelecimento prestador de serviço de transporte',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de prestador de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5255',	'Venda de energia elétrica para estabelecimento prestador de serviço de comunicação',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5256',	'Venda de energia elétrica para estabelecimento de produtor rural',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5257',	'Venda de energia elétrica para consumo por demanda contratada',	'Classificam-se neste código as vendas de energia elétrica para consumo por demanda contratada, que prevalecerá sobre os demais códigos deste subgrupo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5258',	'Venda de energia elétrica a não contribuinte',	'Classificam-se neste código as vendas de energia elétrica a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5300',	'PRESTAÇÕES DE SERVIÇOS DE COMUNICAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5301',	'Prestação de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as prestações de serviços de comunicação destinados às prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5302',	'Prestação de serviço de comunicação a estabelecimento industrial',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento industrial. Também serão classificados neste código os serviços de comunicação prestados a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5303',	'Prestação de serviço de comunicação a estabelecimento comercial',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento comercial. Também serão classificados neste código os serviços de comunicação prestados a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5304',	'Prestação de serviço de comunicação a estabelecimento de prestador de serviço de transporte',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento prestador de serviço de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5305',	'Prestação de serviço de comunicação a estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5306',	'Prestação de serviço de comunicação a estabelecimento de produtor rural',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5307',	'Prestação de serviço de comunicação a não contribuinte',	'Classificam-se neste código as prestações de serviços de comunicação a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5350',	'PRESTAÇÕES DE SERVIÇOS DE TRANSPORTE',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5351',	'Prestação de serviço de transporte para execução de serviço da mesma natureza',	'Classificam-se neste código as prestações de serviços de transporte destinados às prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5352',	'Prestação de serviço de transporte a estabelecimento industrial',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento industrial. Também serão classificados neste código os serviços de transporte prestados a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5353',	'Prestação de serviço de transporte a estabelecimento comercial',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento comercial. Também serão classificados neste código os serviços de transporte prestados a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5354',	'Prestação de serviço de transporte a estabelecimento de prestador de serviço de comunicação',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5355',	'Prestação de serviço de transporte a estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5356',	'Prestação de serviço de transporte a estabelecimento de produtor rural',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5357',	'Prestação de serviço de transporte a não contribuinte',	'Classificam-se neste código as prestações de serviços de transporte a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5359',	'Prestação de serviço de transporte a contribuinte ou a não-contribuinte, quando a mercadoria transportada esteja dispensada de emissão de Nota Fiscal ',	'Prestação de serviço de transporte a contribuinte ou a não-contribuinte, quando não existir a obrigação legal de emissão de Nota Fiscal para a mercadoria transportada. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810, DE 10 DE JUNHO DE 2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5360',	'Prestação de serviço de transporte a contribuinte-substituto em relação ao serviço de transporte (ACR) (Ajuste SINIEF 06/2007- Decreto nº 30.861/2007) – a partir de 01.01.2008',	'Prestação de serviço de transporte a contribuinte a quem tenha sido atribuída a condição de contribuinte-substituto em relação ao imposto incidente na prestação dos serviços.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5400',	'SAÍDAS DE MERCADORIAS SUJEITAS AO REGIME DE SUBSTITUIÇÃO TRIBUTÁRIA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5401',	'Venda de produção do estabelecimento quando o produto esteja sujeito ao regime de substituição tributária',	'Venda de produto industrializado ou produzido pelo estabelecimento, quando o referido produto estiver sujeito ao regime de substituição tributária, bem como a de produto industrializado, por estabelecimento industrial ou produtor rural de cooperativa, sujeito ao regime de substituição tributária.\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5402',	'Venda de produção do estabelecimento de produto sujeito ao regime de substituição tributária, em operação entre contribuintes substitutos do mesmo produto',	'Classificam-se neste código as vendas de produtos sujeitos ao regime de substituição tributária industrializados no estabelecimento, em operações entre contribuintes substitutos do mesmo produto');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5403',	'Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de contribuinte-substituto',	'Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de contribuinte-substituto.\r\n\r\n– (Decreto Nº 25.068/2003). a partir 01.01.2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5405',	'Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de contribuinte-substituído',	'Venda de mercadoria, adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária, na condição de contribuinte-substituído.\r\n\r\n– (Decreto Nº 25.068/2003). a partir 01.01.2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5408',	'Transferência de produção do estabelecimento quando o produto estiver sujeito ao regime de substituição tributária',	'Transferência de produto industrializado ou produzido no estabelecimento, para outro estabelecimento da mesma empresa, quando o produto estiver sujeito ao regime de substituição tributária.\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5409',	'Transferência de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de mercadorias adquiridas ou recebidas de terceiros que não tenham sido objeto de qualquer processo industrial no estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5410',	'Devolução de compra para industrialização de mercadoria sujeita ao regime de substituição tributária',	'Devolução de mercadoria adquirida para ser utilizada em processo de industrialização ou produção rural, cuja entrada tenha sido classificada como \"Compra para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária\".\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5411',	'Devolução de compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem comercializadas, cujas entradas tenham sido classificadas como Compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5412',	'Devolução de bem do ativo imobilizado, em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de bens adquiridos para integrar o ativo imobilizado do estabelecimento, cuja entrada tenha sido classificada no código 1.406 - Compra de bem para o ativo imobilizado cuja mercadoria está sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5413',	'Devolução de mercadoria destinada ao uso ou consumo, em operação com mercadoria sujeita ao regime de substituição tributária.',	'Classificam-se neste código as devoluções de mercadorias adquiridas para uso ou consumo do estabelecimento, cuja entrada tenha sido classificada no código 1.407 - Compra de mercadoria para uso ou consumo cuja mercadoria está sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5414',	'Remessa de produção do estabelecimento para venda fora do estabelecimento, quando o produto estiver sujeito ao regime de substituição tributária',	'Remessa de produto industrializado ou produzido pelo estabelecimento para ser vendido fora do estabelecimento, inclusive por meio de veículo, quando o mencionado produto estiver sujeito ao regime de substituição tributária.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5415',	'Remessa de mercadoria adquirida ou recebida de terceiros para venda fora do estabelecimento, em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as remessas de mercadorias adquiridas ou recebidas de terceiros para serem vendidas fora do estabelecimento, inclusive por meio de veículos, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5450',	'SISTEMAS DE INTEGRAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5451',	'Remessa de animal e de insumo para estabelecimento produtor',	'Classificam-se neste código as saídas referentes à remessa de animais e de insumos para criação de animais no sistema integrado, tais como: pintos, leitões, rações e medicamentos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5500',	'REMESSAS PARA FORMAÇÃO DE LOTE E COM FIM ESPECÍFICO DE EXPORTAÇÃO E EVENTUAIS DEVOLUÇÕES (NR Ajuste SINIEF 09/2005)',	'(NR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5501',	'Remessa de produção do estabelecimento, com fim específico de exportação',	'Saída de produto industrializado ou produzido pelo estabelecimento, remetido com fim específico de exportação a \"trading company\", empresa comercial exportadora ou outro estabelecimento do remetente\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5502',	'Remessa de mercadoria adquirida ou recebida de terceiros, com fim específico de exportação',	'Classificam-se neste código as saídas de mercadorias adquiridas ou recebidas de terceiros, remetidas com fim específico de exportação a trading company, empresa comercial exportadora ou outro estabelecimento do remetente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5503',	'Devolução de mercadoria recebida com fim específico de exportação',	'Classificam-se neste código as devoluções efetuadas por trading company, empresa comercial exportadora ou outro estabelecimento do destinatário, de mercadorias recebidas com fim específico de exportação, cujas entradas tenham sido classificadas no código 1.501 - Entrada de mercadoria recebida com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5504',	'Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.',	'Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5505',	'Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação.',	'Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação.\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5551',	'Venda de bem do ativo imobilizado',	'Classificam-se neste código as vendas de bens integrantes do ativo imobilizado do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5552',	'Transferência de bem do ativo imobilizado',	'Classificam-se neste código os bens do ativo imobilizado transferidos para outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5553',	'Devolução de compra de bem para o ativo imobilizado',	'Classificam-se neste código as devoluções de bens adquiridos para integrar o ativo imobilizado do estabelecimento, cuja entrada foi classificada no código 1.551 - Compra de bem para o ativo imobilizado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5554',	'Remessa de bem do ativo imobilizado para uso fora do estabelecimento',	'Classificam-se neste código as remessas de bens do ativo imobilizado para uso fora do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5555',	'Devolução de bem do ativo imobilizado de terceiro, recebido para uso no estabelecimento',	'Classificam-se neste código as saídas em devolução, de bens do ativo imobilizado de terceiros, recebidos para uso no estabelecimento, cuja entrada tenha sido classificada no código 1.555 - Entrada de bem do ativo imobilizado de terceiro, remetido para uso no estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5556',	'Devolução de compra de material de uso ou consumo',	'Classificam-se neste código as devoluções de mercadorias destinadas ao uso ou consumo do estabelecimento, cuja entrada tenha sido classificada no código 1.556 - Compra de material para uso ou consumo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5557',	'Transferência de material de uso ou consumo',	'Classificam-se neste código os materiais para uso ou consumo transferidos para outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5600',	'CRÉDITOS E RESSARCIMENTOS DE ICMS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5601',	'Transferência de crédito de ICMS acumulado',	'Classificam-se neste código os lançamentos destinados ao registro da transferência de créditos de ICMS para outras empresas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5602',	'Transferência de saldo credor do ICMS, para outro estabelecimento da mesma empresa, destinado à compensação de saldo devedor do ICMS',	'Lançamento destinado ao registro da transferência de saldo credor do ICMS, para outro estabelecimento da mesma empresa, destinado à compensação do saldo devedor desse estabelecimento, inclusive no caso de apuração centralizada do imposto. (NR Ajuste SINIEF 09/2003 – a partir 01.01.2004)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5603',	'Ressarcimento de ICMS retido por substituição tributária',	'Classificam-se neste código os lançamentos destinados ao registro de ressarcimento de ICMS retido por substituição tributária a contribuinte substituído, efetuado pelo contribuinte substituto, nas hipóteses previstas na legislação aplicável.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5605',	'Transferência de saldo devedor do ICMS de outro estabelecimento da mesma empresa ',	'Lançamento destinado ao registro da transferência de saldo devedor do ICMS para outro estabelecimento da mesma empresa, para efetivação da apuração centralizada do imposto. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5606',	'Utilização de saldo credor do ICMS para extinção por compensação de débitos fiscais',	'Lançamento destinado ao registro de utilização de saldo credor do ICMS em conta gráfica para extinção por compensação de débitos fiscais desvinculados de conta gráfica. (ACR Ajuste SINIEF 02/2005 – a partir de 01.01.2006). (DECRETO Nº 27.995 de 06.06.2005) a partir de 01.01.2006');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5650',	'SAÍDAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTES',	' (ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004) ( Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5651',	'Venda de combustível ou lubrificante de produção do estabelecimento destinados à industrialização subseqüente',	'Venda de combustível ou lubrificante, industrializados no estabelecimento e destinados à industrialização do próprio produto, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5652',	'Venda de combustível ou lubrificante, de produção do estabelecimento, destinados à comercialização',	'Venda de combustível ou lubrificante, industrializados no estabelecimento, destinados à comercialização, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5653',	'Venda de combustível ou lubrificante, de produção do estabelecimento, destinados a consumidor ou usuário final',	'Venda de combustível ou lubrificante, industrializados no estabelecimento, destinados a consumo em processo de industrialização de outro produto, à prestação de serviço ou a usuário final, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5654',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à industrialização subseqüente',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à industrialização do próprio produto, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5655',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à comercialização',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à comercialização, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5656',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados a consumidor ou usuário final',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados a consumo em processo de industrialização de outro produto, à prestação de serviço ou a usuário final, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5657',	'Remessa de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para venda fora do estabelecimento',	'Remessa de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para ser vendidos fora do estabelecimento, inclusive por meio de veículos. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5658',	'Transferência de combustível ou lubrificante de produção do estabelecimento',	'Transferência de combustível ou lubrificante, industrializados no estabelecimento, para outro estabelecimento da mesma empresa. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5659',	'Transferência de combustível ou lubrificante adquiridos ou recebidos de terceiros',	'Transferência de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para outro estabelecimento da mesma empresa. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5660',	'Devolução de compra de combustível ou lubrificante adquiridos para industrialização subseqüente',	'Devolução de compra de combustível ou lubrificante, adquiridos para industrialização do próprio produto, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante para industrialização subseqüente\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5661',	'Devolução de compra de combustível ou lubrificante adquiridos para comercialização',	'Devolução de compra de combustível ou lubrificante, adquiridos para comercialização, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante para comercialização\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5662',	'Devolução de compra de combustível ou lubrificante adquiridos por consumidor ou usuário final',	'Devolução de compra de combustível ou lubrificante, adquiridos para consumo em processo de industrialização de outro produto, na prestação de serviço ou por usuário final, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante por consumidor ou usuário final\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5663',	'Remessa para armazenagem de combustível ou lubrificante',	'Remessa para armazenagem de combustível ou lubrificante. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5664',	'Retorno de combustível ou lubrificante recebidos para armazenagem',	'Remessa, em devolução, de combustível ou lubrificante, recebidos para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5665',	'Retorno simbólico de combustível ou lubrificante recebidos para armazenagem',	'Retorno simbólico de combustível ou lubrificante, recebidos para armazenagem, quando a mercadoria armazenada tenha sido objeto de saída, a qualquer título, e não deva retornar ao estabelecimento depositante. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5666',	'Remessa, por conta e ordem de terceiros, de combustível ou lubrificante recebidos para armazenagem',	'Saída, por conta e ordem de terceiros, de combustível ou lubrificante, recebidos anteriormente para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5667',	'Venda de combustível ou lubrificante a consumidor ou usuário final estabelecido em outra Unidade da Federação',	'Venda de combustível ou lubrificante a consumidor ou a usuário final estabelecido em outra Unidade da Federação, cujo abastecimento tenha sido efetuado na unidade da Federação do remetente. ACR Ajuste SINIEF 05/2009 – a partir de 01.07.2009)(Decreto nº 34.490/2009)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5900',	'OUTRAS SAÍDAS DE MERCADORIAS OU PRESTAÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5901',	'Remessa para industrialização por encomenda',	'Classificam-se neste código as remessas de insumos remetidos para industrialização por encomenda, a ser realizada em outra empresa ou em outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5902',	'Retorno de mercadoria utilizada na industrialização por encomenda',	'Classificam-se neste código as remessas, pelo estabelecimento industrializador, dos insumos recebidos para industrialização e incorporados ao produto final, por encomenda de outra empresa ou de outro estabelecimento da mesma empresa. O valor dos insumos nesta operação deverá ser igual ao valor dos insumos recebidos para industrialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5903',	'Retorno de mercadoria recebida para industrialização e não aplicada no referido processo',	'Classificam-se neste código as remessas em devolução de insumos recebidos para industrialização e não aplicados no referido processo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5904',	'Remessa para venda fora do estabelecimento',	'Classificam-se neste código as remessas de mercadorias para venda fora do estabelecimento, inclusive por meio de veículos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5905',	'Remessa para depósito fechado ou armazém geral',	'Classificam-se neste código as remessas de mercadorias para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5906',	'Retorno de mercadoria depositada em depósito fechado ou armazém geral',	'Classificam-se neste código os retornos de mercadorias depositadas em depósito fechado ou armazém geral ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5907',	'Retorno simbólico de mercadoria depositada em depósito fechado ou armazém geral',	'Classificam-se neste código os retornos simbólicos de mercadorias recebidas para depósito em depósito fechado ou armazém geral, quando as mercadorias depositadas tenham sido objeto de saída a qualquer título e que não devam retornar ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5908',	'Remessa de bem por conta de contrato de comodato',	'Classificam-se neste código as remessas de bens para o cumprimento de contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5909',	'Retorno de bem recebido por conta de contrato de comodato',	'Classificam-se neste código as remessas de bens em devolução após cumprido o contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5910',	'Remessa em bonificação, doação ou brinde',	'Classificam-se neste código as remessas de mercadorias a título de bonificação, doação ou brinde.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5911',	'Remessa de amostra grátis',	'Classificam-se neste código as remessas de mercadorias a título de amostra grátis.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5912',	'Remessa de mercadoria ou bem para demonstração',	'Classificam-se neste código as remessas de mercadorias ou bens para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5913',	'Retorno de mercadoria ou bem recebido para demonstração',	'Classificam-se neste código as remessas em devolução de mercadorias ou bens recebidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5914',	'Remessa de mercadoria ou bem para exposição ou feira',	'Classificam-se neste código as remessas de mercadorias ou bens para exposição ou feira.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5915',	'Remessa de mercadoria ou bem para conserto ou reparo',	'Classificam-se neste código as remessas de mercadorias ou bens para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5916',	'Retorno de mercadoria ou bem recebido para conserto ou reparo',	'Classificam-se neste código as remessas em devolução de mercadorias ou bens recebidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5917',	'Remessa de mercadoria em consignação mercantil ou industrial',	'Classificam-se neste código as remessas de mercadorias a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5918',	'Devolução de mercadoria recebida em consignação mercantil ou industrial',	'Classificam-se neste código as devoluções de mercadorias recebidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5919',	'Devolução simbólica de mercadoria vendida ou utilizada em processo industrial, recebida anteriormente em consignação mercantil ou industrial',	'Classificam-se neste código as devoluções simbólicas de mercadorias vendidas ou utilizadas em processo industrial, que tenham sido recebidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5920',	'Remessa de vasilhame ou sacaria',	'Classificam-se neste código as remessas de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5921',	'Devolução de vasilhame ou sacaria',	'Classificam-se neste código as saídas por devolução de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5922',	'Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura',	'Classificam-se neste código os registros efetuados a título de simples faturamento decorrente de venda para entrega futura.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5923',	'Remessa de mercadoria por conta e ordem de terceiros, em venda à ordem',	'Classificam-se neste código as saídas correspondentes à entrega de mercadorias por conta e ordem de terceiros, em vendas à ordem, cuja venda ao adquirente originário, foi classificada nos códigos 5.118 - Venda de produção do estabelecimento entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem ou 5.119');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5924',	'Remessa para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as saídas de insumos com destino a estabelecimento industrializador, para serem industrializados por conta e ordem do adquirente, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente dos mesmos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5925',	'Retorno de mercadoria recebida para industrialização por conta e ordem do adquirente da mercadoria, quando aquela não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as remessas, pelo estabelecimento industrializador, dos insumos recebidos, por conta e ordem do adquirente, para industrialização e incorporados ao produto final, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente. O valor dos insumos nesta operação deverá ser igual ao valor dos insumos recebidos para industrialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5926',	'Lançamento efetuado a título de reclassificação de mercadoria decorrente de formação de kit ou de sua desagregação',	'Classificam-se neste código os registros efetuados a título de reclassificação decorrente de formação de kit de mercadorias ou de sua desagregação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5927',	'Lançamento efetuado a título de baixa de estoque decorrente de perda, roubo ou deterioração',	'Classificam-se neste código os registros efetuados a título de baixa de estoque decorrente de perda, roubou ou deterioração das mercadorias.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5928',	'Lançamento efetuado a título de baixa de estoque decorrente de perda, roubo ou deterioração',	'Classificam-se neste código os registros efetuados a título de baixa de estoque decorrente de perda, roubou ou deterioração das mercadorias.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5929',	'Lançamento efetuado em decorrência de emissão de documento fiscal relativo a operação ou prestação também registrada em equipamento Emissor de Cupom Fiscal - ECF',	'Classificam-se neste código os registros relativos aos documentos fiscais emitidos em operações ou prestações que também tenham sido registradas em equipamento Emissor de Cupom Fiscal - ECF.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5931',	'Lançamento efetuado em decorrência da responsabilidade de retenção do imposto por substituição tributária, atribuída ao remetente ou alienante da mercadoria, pelo serviço de transporte realizado por t',	'Classificam-se neste código exclusivamente os lançamentos efetuados pelo remetente ou alienante da mercadoria quando lhe for atribuída a responsabilidade pelo recolhimento do imposto devido pelo serviço de transporte realizado por transportador autônomo ou por transportador não inscrito na unidade da Federação onde iniciado o serviço.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5932',	'Prestação de serviço de transporte iniciada em unidade da Federação diversa daquela onde inscrito o prestador',	'Classificam-se neste código as prestações de serviço de transporte que tenham sido iniciadas em unidade da Federação diversa daquela onde o prestador está inscrito como contribuinte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5933',	'Prestação de serviço tributado pelo Imposto Sobre Serviços de Qualquer Natureza',	'Prestação de serviço, cujo imposto é de competência municipal, desde que informado em Nota Fiscal modelo 1 ou 1-A. (NR Ajuste SINIEF 06/2005)a partir de 01/01/2006');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('5949',	'Outra saída de mercadoria ou prestação de serviço não especificado',	'Classificam-se neste código as outras saídas de mercadorias ou prestações de serviços que não tenham sido especificados nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6000',	'SAÍDAS OU PRESTAÇÕES DE SERVIÇOS PARA OUTROS ESTADOS',	'Classificam-se, neste grupo, as operações ou prestações em que o estabelecimento remetente esteja localizado em unidade da Federação diversa daquela do destinatário');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6101',	'Venda de produção do estabelecimento',	'Venda de produto industrializado ou produzido pelo estabelecimento, bem como a de mercadoria por estabelecimento industrial ou produtor rural de cooperativa destinada a seus cooperados ou a estabelecimento de outra cooperativa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6102',	'Venda de mercadoria adquirida ou recebida de terceiros',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial no estabelecimento. Também serão classificadas neste código as vendas de mercadorias por estabelecimento comercial de cooperativa destinadas a seus cooperados ou estabelecimento de outra cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6103',	'Venda de produção do estabelecimento, efetuada fora do estabelecimento',	'venda efetuada fora do estabelecimento, inclusive por meio de veículo, de produto industrializado no estabelecimento.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6104',	'Venda de mercadoria adquirida ou recebida de terceiros, efetuada fora do estabelecimento',	'venda efetuada fora do estabelecimento, inclusive por meio de veículo, de mercadoria adquirida ou recebida de terceiro para industrialização ou comercialização, que não tenha sido objeto de qualquer processo industrial no estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6105',	'Venda de produção do estabelecimento que não deva por ele transitar',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento, armazenados em depósito fechado, armazém geral ou outro sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6106',	'Venda de mercadoria adquirida ou recebida de terceiros, que não deva por ele transitar',	'Vendas de mercadoria adquirida ou recebida de terceiro para industrialização ou comercialização, armazenada em depósito fechado, armazém geral ou outro, que não tenha sido objeto de qualquer processo industrial no estabelecimento sem que haja retorno ao estabelecimento depositante. Bem como venda de mercadoria importada, cuja saída ocorra do recinto alfandegado ou da repartição alfandegária onde se processou o desembaraço aduaneiro, com destino ao estabelecimento do comprador, sem que tenha transitado pelo estabelecimento do');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6107',	'Venda de produção do estabelecimento, destinada a não contribuinte',	'Vendas de produto industrializado no estabelecimento, ou produzido no estabelecimento do produtor rural, destinada a não contribuinte, bem como qualquer operação de venda destinada a não contribuinte\r\n\r\n (NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6108',	'Venda de mercadoria adquirida ou recebida de terceiros, destinada a não contribuinte',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial no estabelecimento, destinadas a não contribuintes. Quaisquer operações de venda destinadas a não contribuintes deverão ser classificadas neste código.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6109',	'Venda de produção do estabelecimento destinada à Zona Franca de Manaus ou Áreas de Livre Comércio',	'Venda de produto industrializado ou produzido pelo estabelecimento destinado à Zona Franca de Manaus ou Áreas de Livre Comércio.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6110',	'Venda de mercadoria, adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio, de que trata o Anexo do Convênio SINIEF s/n, de 15 de dezembro de 1970, que dispõ',	'Venda de mercadoria, adquirida ou recebida de terceiros, destinada à Zona Franca de Manaus ou Áreas de Livre Comércio, desde que alcançada pelos benefícios fiscais de que tratam o Decreto-Lei nº 288, de 28 de fevereiro de 1967, o Convênio ICM 65/88, de 06 de dezembro de 1988, o Convênio ICMS 36/97, de 23 de maio de 1997, e o Convênio ICMS 37/97, de 23 de maio de 1997. (NR Ajuste SINIEF 09/2004) (Decreto nº 26.955/2004) RETROAGINDO SEUS EFEITOS A 24.06.2004');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6111',	'Venda de produção do estabelecimento remetida anteriormente em consignação industrial',	'Classificam-se neste código as vendas efetivas de produtos industrializados no estabelecimento remetidos anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6112',	'Venda de mercadoria adquirida ou recebida de Terceiros remetida anteriormente em consignação industrial',	'Classificam-se neste código as vendas efetivas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas anteriormente a título de consignação industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6113',	'Venda de produção do estabelecimento remetida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas efetivas de produtos industrializados no estabelecimento remetidos anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6114',	'Venda de mercadoria adquirida ou recebida de terceiros remetida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas efetivas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6115',	'Venda de mercadoria adquirida ou recebida de terceiros, recebida anteriormente em consignação mercantil',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, recebidas anteriormente a título de consignação mercantil.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6116',	'Venda de produção do estabelecimento originada de encomenda para entrega futura',	'Venda de produto industrializado ou produzido pelo estabelecimento, quando da saída real do produto, cujo faturamento tenha sido classificado no código \"5.922 – Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6117',	'Venda de mercadoria adquirida ou recebida de terceiros, originada de encomenda para entrega futura',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, quando da saída real da mercadoria, cujo faturamento tenha sido classificado no código 6.922 - Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6118',	'Venda de produção do estabelecimento entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem',	'Classificam-se neste código as vendas à ordem de produtos industrializados pelo estabelecimento, entregues ao destinatário por conta e ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6119',	'Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem',	'Classificam-se neste código as vendas à ordem de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, entregues ao destinatário por conta e ordem do adquirente originário.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6120',	'Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário pelo vendedor remetente, em venda à ordem',	'Classificam-se neste código as vendas à ordem de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, entregues pelo vendedor remetente ao destinatário, cuja compra seja classificada, pelo adquirente originário, no código 2.118 - Compra de mercadoria pelo adquirente originário, entregue pelo vendedor remetente ao destinatário, em venda à ordem.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6122',	'Venda de produção do estabelecimento remetida para industrialização, por conta e ordem do adquirente, sem transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento, remetidos para serem industrializados em outro estabelecimento, por conta e ordem do adquirente, sem que os produtos tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6123',	'Venda de mercadoria adquirida ou recebida de terceiros remetida para industrialização, por conta e ordem do adquirente, sem transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, que não tenham sido objeto de qualquer processo industrial no estabelecimento, remetidas para serem industrializadas em outro estabelecimento, por conta e ordem do adquirente, sem que as mercadorias tenham transitado pelo estabelecimento do adquirente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6124',	'Industrialização efetuada para outra empresa',	'Classificam-se neste código as saídas de mercadorias industrializadas para terceiros, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6125',	'Industrialização efetuada para outra empresa quando a mercadoria recebida para utilização no processo de industrialização não transitar pelo estabelecimento adquirente da mercadoria',	'Classificam-se neste código as saídas de mercadorias industrializadas para outras empresas, em que as mercadorias recebidas para utilização no processo de industrialização não tenham transitado pelo estabelecimento do adquirente das mercadorias, compreendendo os valores referentes aos serviços prestados e os das mercadorias de propriedade do industrializador empregadas no processo industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6150',	'TRANSFERÊNCIAS DE PRODUÇÃO PRÓPRIA OU DE TERCEIROS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6151',	'Transferência de produção do estabelecimento',	'Produtos industrializado ou produzido no estabelecimento e transferido para outro estabelecimento da mesma empresa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6152',	'Transferência de mercadoria adquirida ou recebida de terceiros',	'Mercadoria adquirida ou recebida de terceiros para industrialização, comercialização ou utilização na prestação de serviço e que não tenha sido objeto de qualquer processo industrial no estabelecimento, transferida para outro estabelecimento da mesma empresa. A partir  10 de julho de 2003. (Decreto nº 26.020/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6153',	'Transferência de energia elétrica',	'Classificam-se neste código as transferências de energia elétrica para outro estabelecimento da mesma empresa, para distribuição.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6155',	'Transferência de produção do estabelecimento, que não deva por ele transitar',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de produtos industrializados no estabelecimento que tenham sido remetidos para armazém geral, depósito fechado ou outro, sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6156',	'Transferência de mercadoria adquirida ou recebida de terceiros, que não deva por ele transitar',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial, remetidas para armazém geral, depósito fechado ou outro, sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6200',	'DEVOLUÇÕES DE COMPRAS PARA INDUSTRIALIZAÇÃO, COMERCIALIZAÇÃO OU ANULAÇÕES DE VALORES',	'(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6201',	'Devolução de compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Devolução de mercadoria adquirida para ser utilizada em processo de industrialização ou produção rural, cuja entrada tenha sido classificada como \"1.101 - Compra para industrialização ou produção rural\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6202',	'Devolução de compra para comercialização',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem comercializadas, cujas entradas tenham sido classificadas como Compra para comercialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6205',	'Anulação de valor relativo a aquisição de serviço de comunicação',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6206',	'Anulação de valor relativo a aquisição de serviço de transporte',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6207',	'Anulação de valor relativo à compra de energia elétrica',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes da compra de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6208',	'Devolução de mercadoria recebida em transferência para industrialização ou produção rural',	'Devolução de mercadoria recebida em transferência de outro estabelecimento da mesma empresa, para ser utilizada em processo de industrialização ou produção rural.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6209',	'Devolução de mercadoria recebida em transferência para comercialização',	'Classificam-se neste código as devoluções de mercadorias recebidas em transferência de outro estabelecimento da mesma empresa, para serem comercializadas.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6210',	'Devolução de compra para utilização na prestação de serviço',	'Classificam-se neste código as devoluções de mercadorias adquiridas para utilização na prestação de serviços, cujas entradas tenham sido classificadas no código 2.126 - Compra para utilização na prestação de serviço.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6250',	'VENDAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6251',	'Venda de energia elétrica para distribuição ou comercialização',	'Classificam-se neste código as vendas de energia elétrica destinada à distribuição ou comercialização. Também serão classificadas neste código as vendas de energia elétrica destinada a cooperativas para distribuição aos seus cooperados.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6252',	'Venda de energia elétrica para estabelecimento industrial',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento industrial. Também serão classificadas neste código as vendas de energia elétrica destinada a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6253',	'Venda de energia elétrica para estabelecimento comercial',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento comercial. Também serão classificadas neste código as vendas de energia elétrica destinada a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6254',	'Venda de energia elétrica para estabelecimento prestador de serviço de transporte',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de prestador de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6255',	'Venda de energia elétrica para estabelecimento prestador de serviço de comunicação',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6256',	'Venda de energia elétrica para estabelecimento de produtor rural',	'Classificam-se neste código as vendas de energia elétrica para consumo por estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6257',	'Venda de energia elétrica para consumo por demanda contratada',	'Classificam-se neste código as vendas de energia elétrica para consumo por demanda contratada, que prevalecerá sobre os demais códigos deste subgrupo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6258',	'Venda de energia elétrica a não contribuinte',	'Classificam-se neste código as vendas de energia elétrica a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6300',	'PRESTAÇÕES DE SERVIÇOS DE COMUNICAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6301',	'Prestação de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as prestações de serviços de comunicação destinados às prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6302',	'Prestação de serviço de comunicação a estabelecimento industrial',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento industrial. Também serão classificados neste código os serviços de comunicação prestados a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6303',	'Prestação de serviço de comunicação a estabelecimento comercial',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento comercial. Também serão classificados neste código os serviços de comunicação prestados a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6304',	'Prestação de serviço de comunicação a estabelecimento de prestador de serviço de transporte',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento prestador de serviço de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6305',	'Prestação de serviço de comunicação a estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6306',	'Prestação de serviço de comunicação a estabelecimento de produtor rural',	'Classificam-se neste código as prestações de serviços de comunicação a estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6307',	'Prestação de serviço de comunicação a não contribuinte',	'Classificam-se neste código as prestações de serviços de comunicação a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6350',	'PRESTAÇÕES DE SERVIÇOS DE TRANSPORTE',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6351',	'Prestação de serviço de transporte para execução de serviço da mesma natureza',	'Classificam-se neste código as prestações de serviços de transporte destinados às prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6352',	'Prestação de serviço de transporte a estabelecimento industrial',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento industrial. Também serão classificados neste código os serviços de transporte prestados a estabelecimento industrial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6353',	'Prestação de serviço de transporte a estabelecimento comercial',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento comercial. Também serão classificados neste código os serviços de transporte prestados a estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6354',	'Prestação de serviço de transporte a estabelecimento de prestador de serviço de comunicação',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento prestador de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6355',	'Prestação de serviço de transporte a estabelecimento de geradora ou de distribuidora de energia elétrica',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento de geradora ou de distribuidora de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6356',	'Prestação de serviço de transporte a estabelecimento de produtor rural',	'Classificam-se neste código as prestações de serviços de transporte a estabelecimento de produtor rural.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6357',	'Prestação de serviço de transporte a não contribuinte',	'Classificam-se neste código as prestações de serviços de transporte a pessoas físicas ou a pessoas jurídicas não indicadas nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6359',	'Prestação de serviço de transporte a contribuinte ou a não-contribuinte, quando a mercadoria transportada esteja dispensada de emissão de Nota Fiscal ',	'Prestação de serviço de transporte a contribuinte ou a não-contribuinte, quando não existir a obrigação legal de emissão de Nota Fiscal para a mercadoria transportada. (ACR Ajuste SINIEF 03/2004) (DECRETO Nº 26.810/2004) (a partir de 01.01.2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6360',	'Prestação de serviço de transporte a contribuinte substituto em relação ao serviço de transporte  ',	'Prestação de serviço de transporte a contribuinte a quem tenha sido atribuída a condição de contribuinte-substituto em relação ao imposto incidente na prestação dos serviços. (Ajuste SINIEF 03/2008) (Decreto nº 32.653, de 14.11.2008) a partir de 01.05.2008');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6400',	'SAÍDAS DE MERCADORIAS SUJEITAS AO REGIME DE SUBSTITUIÇÃO TRIBUTÁRIA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6401',	'Venda de produção do estabelecimento quando o produto estiver sujeito ao regime de substituição tributária',	'Venda de produto industrializado ou produzido no estabelecimento, quando o produto estiver sujeito ao regime de substituição tributária, bem como a venda de produto industrializado por estabelecimento industrial ou rural de cooperativa, quando o produto estiver sujeito ao referido regime.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6402',	'Venda de produção do estabelecimento de produto sujeito ao regime de substituição tributária, em operação entre contribuintes substitutos do mesmo produto',	'Classificam-se neste código as vendas de produtos sujeitos ao regime de substituição tributária industrializados no estabelecimento, em operações entre contribuintes substitutos do mesmo produto.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6403',	'Venda de mercadoria adquirida ou recebida de terceiros em operação com mercadoria sujeita ao regime de substituição tributária, na condição de contribuinte substituto',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros, na condição de contribuinte substituto, em operação com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6404',	'Venda de mercadoria sujeita ao regime de substituição tributária, cujo imposto já tenha sido retido anteriormente',	'Classificam-se neste código as vendas de mercadorias sujeitas ao regime de substituição tributária, na condição de substituto tributário, exclusivamente nas hipóteses em que o imposto já tenha sido retido anteriormente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6408',	'Transferência de produção do estabelecimento quando o produto estiver sujeito ao regime de substituição tributária',	'Transferência de produto industrializado ou produzido no estabelecimento, para outro estabelecimento da mesma empresa, quando o produto estiver sujeito ao regime de substituição tributária.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6409',	'Transferência de mercadoria adquirida ou recebida de terceiros, sujeita ao regime de substituição tributária',	'Classificam-se neste código as transferências para outro estabelecimento da mesma empresa, de mercadorias adquiridas ou recebidas de terceiros que não tenham sido objeto de qualquer processo industrial no estabelecimento, em operações com mercadorias sujeitas ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6410',	'Devolução de compra para industrialização ou ptrodução rural quando a mercadoria sujeita ao regime de substituição tributária',	'Devolução de mercadoria adquirida para ser utilizada em processo de industrialização ou produção rural, cuja entrada tenha sido classificada como \"Compra para industrialização ou produção rural de mercadoria sujeita ao regime de substituição tributária\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6411',	'Devolução de compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem comercializadas, cujas entradas tenham sido classificadas como Compra para comercialização em operação com mercadoria sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6412',	'Devolução de bem do ativo imobilizado, em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de bens adquiridos para integrar o ativo imobilizado do estabelecimento, cuja entrada tenha sido classificada no código 2.406 - Compra de bem para o ativo imobilizado cuja mercadoria está sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6413',	'Devolução de mercadoria destinada ao uso ou consumo, em operação com mercadoria sujeita ao regime de substituição tributária',	'Classificam-se neste código as devoluções de mercadorias adquiridas para uso ou consumo do estabelecimento, cuja entrada tenha sido classificada no código 2.407 - Compra de mercadoria para uso ou consumo cuja mercadoria está sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6414',	'Remessa de produção do estabelecimento para venda fora do estabelecimento, quando o produto estiver sujeito ao regime de substituição tributária',	'Remessa de produto industrializado ou produzido pelo estabelecimento para ser vendido fora do estabelecimento, inclusive por meio de veículo, quando o mencionado produto estiver sujeito ao regime de substituição tributária.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6415',	'Remessa de mercadoria adquirida ou recebida de terceiros para venda fora do estabelecimento, quando a referida ração com mercadoria sujeita ao regime de substituição tributária',	'Remessa de mercadoria adquirida ou recebida de terceiro para serem vendida fora do estabelecimento, inclusive por meio de veículo, quando a referida mercadorias estiver sujeita ao regime de substituição tributária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6500',	'REMESSAS COM FIM ESPECÍFICO DE EXPORTAÇÃO E EVENTUAIS DEVOLUÇÕES',	'(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6501',	'Remessa de produção do estabelecimento, com fim específico de exportação',	'Saída de produto industrializado ou produzido pelo estabelecimento, remetido com fim específico de exportação a \"trading company\", empresa comercial exportadora ou outro estabelecimento do remetente.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6502',	'Remessa de mercadoria adquirida ou recebida de terceiros, com fim específico de exportação',	'Classificam-se neste código as saídas de mercadorias adquiridas ou recebidas de terceiros, remetidas com fim específico de exportação a trading company, empresa comercial exportadora ou outro estabelecimento do remetente.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6503',	'Devolução de mercadoria recebida com fim específico de exportação',	'Classificam-se neste código as devoluções efetuadas por trading company, empresa comercial exportadora ou outro estabelecimento do destinatário, de mercadorias recebidas com fim específico de exportação, cujas entradas tenham sido classificadas no código 2.501 - Entrada de mercadoria recebida com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6504',	'Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.',	'Remessa de mercadoria para formação de lote de exportação, de produto industrializado ou produzido pelo próprio estabelecimento.\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6505',	'Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação.',	'Remessa de mercadoria, adquirida ou recebida de terceiros, para formação de lote de exportação.\r\n\r\n(ACR Ajuste SINIEF 09/2005) (Dec. 28.868/2006 - a sua aplicação será obrigatória em relação aos fatos geradores ocorridos a partir de 01 de julho de 2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de janeiro a 30 de junho de 2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6551',	'Venda de bem do ativo imobilizado',	'Vendas de bem integrante do ativo imobilizado do estabelecimento. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6552',	'Transferência de bem do ativo imobilizado',	'Transferência de bem do ativo imobilizado para outro estabelecimento da mesma empresa. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6553',	'Devolução de compra de bem para o ativo imobilizado',	'Devolução de bem adquirido para integrar o ativo imobilizado do estabelecimento, cuja entrada foi classificada no código 2.551 - Compra de bem para o ativo imobilizado. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6554',	'Remessa de bem do ativo imobilizado para uso fora do estabelecimento',	'Remessa de bem do ativo imobilizado para uso fora do estabelecimento. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6555',	'Devolução de bem do ativo imobilizado de terceiro, recebido para uso no estabelecimento',	'Saída em devolução, de bem do ativo imobilizado de terceiros, recebidos para uso no estabelecimento, cuja entrada tenha sido classificada no código 2.555 - Entrada de bem do ativo imobilizado de terceiro, remetido para uso no estabelecimento. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6556',	'Devolução de compra de material de uso ou consumo',	'Devolução de mercadoria destinada ao uso ou consumo do estabelecimento, cuja entrada tenha sido classificada no código 2.556 - compra de material para uso ou consumo –a partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6557',	'Transferência de material de uso ou consumo',	'Transferência de material de uso ou consumo para outro estabelecimento da mesma empresa. –a  partir 01.01.2004-  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6600',	'CRÉDITOS E RESSARCIMENTOS DE ICMS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6603',	'Ressarcimento de ICMS retido por substituição tributária',	'Classificam-se neste código os lançamentos destinados ao registro de ressarcimento de ICMS retido por substituição tributária a contribuinte substituído, efetuado pelo contribuinte substituto, nas hipóteses previstas na legislação aplicável.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6650',	'SAÍDAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTE',	'(ACR Ajuste SINIEF 9/2003 - a partir 01.01.2004) –  Decreto Nº 26.174 de 26/11/2003');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6651',	'Venda de combustível ou lubrificante, de produção do estabelecimento, destinados à industrialização subseqüente',	'Venda de combustível ou lubrificante, industrializados no estabelecimento e destinados à industrialização do próprio produto, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 6.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6652',	'Venda de combustível ou lubrificante, de produção do estabelecimento, destinados à comercialização',	'Venda de combustível ou lubrificante, industrializados no estabelecimento e destinados à comercialização, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 6.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6653',	'Venda de combustível ou lubrificante, de produção do estabelecimento, destinados a consumidor ou usuário final',	'Venda de combustível ou lubrificante, industrializados no estabelecimento e destinados a consumo em processo de industrialização de outro produto, à prestação de serviço ou a usuário final, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 6.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6654',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à industrialização subseqüente',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à industrialização do próprio produto, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6655',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à comercialização',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados à comercialização, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6656',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados a consumidor ou usuário final',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados a consumo em processo de industrialização de outro produto, à prestação de serviço ou a usuário final, inclusive aquela decorrente de encomenda para entrega futura, cujo faturamento tenha sido classificado no código 5.922 – \"Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6657',	'Remessa de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para venda fora do estabelecimento',	'Remessa de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para ser vendidos fora do estabelecimento, inclusive por meio de veículos. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6658',	'Transferência de combustível ou lubrificante de produção do estabelecimento',	'Transferência de combustível ou lubrificante, industrializados no estabelecimento, para outro estabelecimento da mesma empresa. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6659',	'Transferência de combustível ou lubrificante adquiridos ou recebidos de terceiros',	'Transferência de combustível ou lubrificante, adquiridos ou recebidos de terceiros, para outro estabelecimento da mesma empresa. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6660',	'Devolução de compra de combustível ou lubrificante adquiridos para industrialização subseqüente',	'Devolução de compra de combustível ou lubrificante, adquiridos para industrialização do próprio produto, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante para industrialização subseqüente\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6661',	'Devolução de compra de combustível ou lubrificante adquiridos para comercialização',	'Devolução de compra de combustível ou lubrificante, adquiridos para comercialização, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante para comercialização\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6662',	'Devolução de compra de combustível ou lubrificante adquiridos por consumidor ou usuário final',	'Devolução de compra de combustível ou lubrificante, adquiridos para consumo em processo de industrialização de outro produto, na prestação de serviço ou por usuário final, cuja entrada tenha sido classificada como \"Compra de combustível ou lubrificante por consumidor ou usuário final\".(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6663',	'Remessa para armazenagem de combustível ou lubrificante',	'Remessa para armazenagem de combustível ou lubrificante. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6664',	'Retorno de combustível ou lubrificante recebidos para armazenagem',	'Remessa, em devolução, de combustível ou lubrificante, recebidos para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6665',	'Retorno simbólico de combustível ou lubrificante recebidos para armazenagem',	'Retorno simbólico de combustível ou lubrificante, recebidos para armazenagem, quando a mercadoria armazenada tenha sido objeto de saída, a qualquer título, e não deva retornar ao estabelecimento depositante. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6666',	'Remessa, por conta e ordem de terceiros, de combustível ou lubrificante recebidos para armazenagem',	'Saída, por conta e ordem de terceiros, de combustível ou lubrificante, recebidos anteriormente para armazenagem. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6667',	'Venda de combustível ou lubrificante a consumidor ou usuário final estabelecido em outra Unidade da Federação diferente da que ocorrer o consumo',	'Venda de combustível ou lubrificante a consumidor ou a usuário final, cujo abastecimento tenha sido efetuado em Unidade da Federação diferente do remetente e do destinatário. ACR Ajuste SINIEF 05/2009 – a partir de 01.07.2009)(Decreto nº 34.490/2009)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6900',	'OUTRAS SAÍDAS DE MERCADORIAS OU PRESTAÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6901',	'Remessa para industrialização por encomenda',	'Classificam-se neste código as remessas de insumos remetidos para industrialização por encomenda, a ser realizada em outra empresa ou em outro estabelecimento da mesma empresa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6902',	'Retorno de mercadoria utilizada na industrialização por encomenda',	'Classificam-se neste código as remessas, pelo estabelecimento industrializador, dos insumos recebidos para industrialização e incorporados ao produto final, por encomenda de outra empresa ou de outro estabelecimento da mesma empresa. O valor dos insumos nesta operação deverá ser igual ao valor dos insumos recebidos para industrialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6903',	'Retorno de mercadoria recebida para industrialização e não aplicada no referido processo',	'Classificam-se neste código as remessas em devolução de insumos recebidos para industrialização e não aplicados no referido processo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6904',	'Remessa para venda fora do estabelecimento',	'Classificam-se neste código as remessas de mercadorias para venda fora do estabelecimento, inclusive por meio de veículos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6905',	'Remessa para depósito fechado ou armazém geral',	'Classificam-se neste código as remessas de mercadorias para depósito em depósito fechado ou armazém geral.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6906',	'Retorno de mercadoria depositada em depósito fechado ou armazém geral',	'Classificam-se neste código os retornos de mercadorias depositadas em depósito fechado ou armazém geral ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6907',	'Retorno simbólico de mercadoria depositada em depósito fechado ou armazém geral',	'Classificam-se neste código os retornos simbólicos de mercadorias recebidas para depósito em depósito fechado ou armazém geral, quando as mercadorias depositadas tenham sido objeto de saída a qualquer título e que não devam retornar ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6908',	'Remessa de bem por conta de contrato de comodato',	'Classificam-se neste código as remessas de bens para o cumprimento de contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6909',	'Retorno de bem recebido por conta de contrato de comodato',	'Classificam-se neste código as remessas de bens em devolução após cumprido o contrato de comodato.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6910',	'Remessa em bonificação, doação ou brinde',	'Classificam-se neste código as remessas de mercadorias a título de bonificação, doação ou brinde.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6911',	'Remessa de amostra grátis',	'Classificam-se neste código as remessas de mercadorias a título de amostra grátis.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6912',	'Remessa de mercadoria ou bem para demonstração',	'Classificam-se neste código as remessas de mercadorias ou bens para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6913',	'Retorno de mercadoria ou bem recebido para demonstração',	'Classificam-se neste código as remessas em devolução de mercadorias ou bens recebidos para demonstração.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6914',	'Remessa de mercadoria ou bem para exposição ou feira',	'Classificam-se neste código as remessas de mercadorias ou bens para exposição ou feira.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6915',	'Remessa de mercadoria ou bem para conserto ou reparo',	'Classificam-se neste código as remessas de mercadorias ou bens para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6916',	'Retorno de mercadoria ou bem recebido para conserto ou reparo',	'Classificam-se neste código as remessas em devolução de mercadorias ou bens recebidos para conserto ou reparo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6917',	'Remessa de mercadoria em consignação mercantil ou industrial',	'Classificam-se neste código as remessas de mercadorias a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6918',	'Devolução de mercadoria recebida em consignação mercantil ou industrial',	'Classificam-se neste código as devoluções de mercadorias recebidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6919',	'Devolução simbólica de mercadoria vendida ou utilizada em processo industrial, recebida anteriormente em consignação mercantil ou industrial',	'Classificam-se neste código as devoluções simbólicas de mercadorias vendidas ou utilizadas em processo industrial, que tenham sido recebidas anteriormente a título de consignação mercantil ou industrial.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6920',	'Remessa de vasilhame ou sacaria',	'Classificam-se neste código as remessas de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6921',	'Devolução de vasilhame ou sacaria',	'Classificam-se neste código as saídas por devolução de vasilhame ou sacaria.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6922',	'Lançamento efetuado a título de simples faturamento decorrente de venda para entrega futura',	'Classificam-se neste código os registros efetuados a título de simples faturamento decorrente de venda para entrega futura.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6923',	'Remessa de mercadoria por conta e ordem de terceiros, em venda à ordem',	'Classificam-se neste código as saídas correspondentes à entrega de mercadorias por conta e ordem de terceiros, em vendas à ordem, cuja venda ao adquirente originário, foi classificada nos códigos 6.118 - Venda de produção do estabelecimento entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem ou 6.119 - Venda de mercadoria adquirida ou recebida de terceiros entregue ao destinatário por conta e ordem do adquirente originário, em venda à ordem.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6924',	'Remessa para industrialização por conta e ordem do adquirente da mercadoria, quando esta não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as saídas de insumos com destino a estabelecimento industrializador, para serem industrializados por conta e ordem do adquirente, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente dos mesmos.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6925',	'Retorno de mercadoria recebida para industrialização por conta e ordem do adquirente da mercadoria, quando aquela não transitar pelo estabelecimento do adquirente',	'Classificam-se neste código as remessas, pelo estabelecimento industrializador, dos insumos recebidos, por conta e ordem do adquirente, para industrialização e incorporados ao produto final, nas hipóteses em que os insumos não tenham transitado pelo estabelecimento do adquirente. O valor dos insumos nesta operação deverá ser igual ao valor dos insumos recebidos para industrialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6929',	'Lançamento efetuado em decorrência de emissão de documento fiscal relativo a operação ou prestação também registrada em equipamento Emissor de Cupom Fiscal - ECF',	'Classificam-se neste código os registros relativos aos documentos fiscais emitidos em operações ou prestações que também tenham sido registradas em equipamento Emissor de Cupom Fiscal - ECF.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6931',	'Lançamento efetuado em decorrência da responsabilidade de retenção do imposto por substituição tributária, atribuída ao remetente ou alienante da mercadoria, pelo serviço de transporte realizado por t',	'Classificam-se neste código exclusivamente os lançamentos efetuados pelo remetente ou alienante da mercadoria quando lhe for atribuída a responsabilidade pelo recolhimento do imposto devido pelo serviço de transporte realizado por transportador autônomo ou por transportador não inscrito na unidade da Federação onde iniciado o serviço.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6932',	'Prestação de serviço de transporte iniciada em unidade da Federação diversa daquela onde inscrito o prestador',	'Classificam-se neste código as prestações de serviço de transporte que tenham sido iniciadas em unidade da Federação diversa daquela onde o prestador está inscrito como contribuinte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6933',	'Prestação de serviço tributado pelo Imposto Sobre Serviços de Qualquer Natureza',	'Prestação de serviço, cujo imposto   é de competência municipal, desde que informado em nota fiscal modelo 1 ou 1-A. (ACR Ajuste SINIEF 03/2004 e Ajuste SINIEF 06/2005) (DECRETO Nº 26.868/2006)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('6949',	'Outra saída de mercadoria ou prestação de serviço não especificado',	'Classificam-se neste código as outras saídas de mercadorias ou prestações de serviços que não tenham sido especificados nos códigos anteriores.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7000',	'SAÍDAS OU PRESTAÇÕES DE SERVIÇOS PARA O EXTERIOR',	'Classificam-se, neste grupo, as operações ou prestações em que o destinatário esteja localizado em outro país');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7100',	'VENDAS DE PRODUÇÃO PRÓPRIA OU DE TERCEIROS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7101',	'Venda de produção do estabelecimento',	'Venda de produto industrializado ou produzido pelo estabelecimento, bem como a de mercadoria por estabelecimento industrial ou produtor rural de cooperativa destinada a seus cooperados ou a estabelecimento de outra cooperativa.\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7102',	'Venda de mercadoria adquirida ou recebida de terceiros',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, que não tenham sido objeto de qualquer processo industrial no estabelecimento. Também serão classificadas neste código as vendas de mercadorias por estabelecimento comercial de cooperativa.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7105',	'Venda de produção do estabelecimento, que não deva por ele transitar',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento, armazenados em depósito fechado, armazém geral ou outro sem que haja retorno ao estabelecimento depositante.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7106',	'Venda de mercadoria adquirida ou recebida de terceiros, que não deva por ele transitar',	'Classificam-se neste código as vendas de mercadorias adquiridas ou recebidas de terceiros para industrialização ou comercialização, armazenadas em depósito fechado, armazém geral ou outro, que não tenham sido objeto de qualquer processo industrial no estabelecimento sem que haja retorno ao estabelecimento depositante. Também serão classificadas neste código as vendas de mercadorias importadas, cuja saída ocorra do recinto alfandegado ou da repartição alfandegária onde se processou o desembaraço aduaneiro, com destino ao esta');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7127',	'Venda de produção do estabelecimento sob o regime de drawback',	'Classificam-se neste código as vendas de produtos industrializados no estabelecimento sob o regime de drawback , cujas compras foram classificadas no código 3.127 - Compra para industrialização sob o regime de drawback.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7200',	'DEVOLUÇÕES DE COMPRAS PARA INDUSTRIALIZAÇÃO, COMERCIALIZAÇÃO OU ANULAÇÕES DE VALORES',	'(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7201',	'Devolução de compra para industrialização ou produção rural (NR Ajuste SINIEF 05/2005) (Decreto 28.868/2006)',	'Devolução de mercadoria adquirida para ser utilizada em processo de industrialização ou produção rural, cuja entrada tenha sido classificada como \"1.101 - Compra para industrialização ou produção rural\".\r\n\r\n(NR Ajuste SINIEF 05/2005) (Dec.28.868/2006 - Efeitos a partir de 01/01/2006, ficando facultada ao contribuinte a sua adoção para fatos geradores ocorridos no período de 01 de novembro a 31 de dezembro de 2005)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7202',	'Devolução de compra para comercialização',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem comercializadas, cujas entradas tenham sido classificadas como Compra para comercialização.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7205',	'Anulação de valor relativo à aquisição de serviço de comunicação',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de comunicação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7206',	'Anulação de valor relativo a aquisição de serviço de transporte',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes das aquisições de serviços de transporte.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7207',	'Anulação de valor relativo à compra de energia elétrica',	'Classificam-se neste código as anulações correspondentes a valores faturados indevidamente, decorrentes da compra de energia elétrica.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7210',	'Devolução de compra para utilização na prestação de serviço',	'Classificam-se neste código as devoluções de mercadorias adquiridas para utilização na prestação de serviços, cujas entradas tenham sido classificadas no código 3.126 - Compra para utilização na prestação de serviço.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7211',	'Devolução de compras para industrialização sob o regime de drawback',	'Classificam-se neste código as devoluções de mercadorias adquiridas para serem utilizadas em processo de industrialização sob o regime de drawback e não utilizadas no referido processo, cujas entradas tenham sido classificadas no código 3.127 - Compra para industrialização sob o regime de drawback.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7250',	'VENDAS DE ENERGIA ELÉTRICA',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7251',	'Venda de energia elétrica para o exterior',	'Classificam-se neste código as vendas de energia elétrica para o exterior.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7300',	'PRESTAÇÕES DE SERVIÇOS DE COMUNICAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7301',	'Prestação de serviço de comunicação para execução de serviço da mesma natureza',	'Classificam-se neste código as prestações de serviços de comunicação destinados às prestações de serviços da mesma natureza.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7358',	'Prestação de serviço de transporte',	'Classificam-se neste código as prestações de serviços de transporte destinado a estabelecimento no exterior.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7500',	'EXPORTAÇÃO DE MERCADORIAS RECEBIDAS COM FIM ESPECÍFICO DE EXPORTAÇÃO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7501',	'Exportação de mercadorias recebidas com fim específico de exportação',	'Classificam-se neste código as exportações das mercadorias recebidas anteriormente com finalidade específica de exportação, cujas entradas tenham sido classificadas nos códigos 1.501 - Entrada de mercadoria recebida com fim específico de exportação ou 2.501 - Entrada de mercadoria recebida com fim específico de exportação.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7550',	'OPERAÇÕES COM BENS DE ATIVO IMOBILIZADO E MATERIAIS PARA USO OU CONSUMO',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7551',	'Venda de bem do ativo imobilizado',	'Classificam-se neste código as vendas de bens integrantes do ativo imobilizado do estabelecimento.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7553',	'Devolução de compra de bem para o ativo imobilizado',	'Classificam-se neste código as devoluções de bens adquiridos para integrar o ativo imobilizado do estabelecimento, cuja entrada foi classificada no código 3.551 - Compra de bem para o ativo imobilizado.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7556',	'Devolução de compra de material de uso ou consumo',	'Classificam-se neste código as devoluções de mercadorias destinadas ao uso ou consumo do estabelecimento, cuja entrada tenha sido classificada no código 3.556 - Compra de material para uso ou consumo.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7650',	'SAÍDAS DE COMBUSTÍVEIS, DERIVADOS OU NÃO DE PETRÓLEO, E LUBRIFICANTES',	'(a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7651',	'Venda de combustível ou lubrificante de produção do estabelecimento',	'Venda de combustível ou lubrificante industrializados no estabelecimento e destinados ao exterior. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7654',	'Venda de combustível ou lubrificante adquiridos ou recebidos de terceiros',	'Venda de combustível ou lubrificante, adquiridos ou recebidos de terceiros, destinados ao exterior. (a partir 01.01.2004 -  Decreto Nº 26.174 de 26/11/2003)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7667',	'Venda de combustível ou lubrificante a consumidor ou usuário final',	'Venda de combustível ou lubrificante a consumidor ou a usuário final, cuja operação tenha sido equiparada a uma exportação. ACR Ajuste SINIEF 05/2009 – a partir de 01.07.2009)(Decreto nº 34.490/2009)');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7900',	'OUTRAS SAIDAS DE MERCADORIA OU PRESTAÇÕES DE SERVIÇOS',	' ');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7930',	'Lançamento efetuado a título de devolução de bem cuja entrada tenha ocorrido sob amparo de regime especial aduaneiro de admissão temporária',	'Classificam-se neste código os lançamentos efetuados a título de saída em devolução de bens cuja entrada tenha ocorrido sob amparo de regime especial aduaneiro de admissão temporária.');
INSERT INTO cfop (id, descricao, aplicacao) VALUES ('7949',	'Outra saída de mercadoria ou prestação de serviço não especificado',	'Classificam-se neste código as outras saídas de mercadorias ou prestações de serviços que não tenham sido especificados nos códigos anteriores.');

/*EOF*/