
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
	id              int          not null,
	codigo_fiscal   int          not null,
	descricao       varchar(300) not null,

	primary key(id)
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
	cfop_id                int            not null,
	movimento_id           int            not null,

	primary key(id),
	foreign key(movimento_id) references movimentos  (id),
	foreign key(cfop_id)      references cfop        (id)
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
	cfop_id                  int           not null,  /* TABELA DE CFOP */

	primary key(id),
	foreign key(classe_imposto_id)  references classes_imposto  (id),
	foreign key(tipos_movimento_id) references tipos_movimento  (id),
	foreign key(cfop_id)            references cfop             (id)

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

/*EOF*/