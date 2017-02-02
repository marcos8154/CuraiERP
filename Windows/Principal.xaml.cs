using Base.Controller;
using Base.Interfaces;
using Base.UserControls.Configuracoes;
using Base.UserControls.Configuracoes.CadastroUsuarios;
using Base.UserControls.Configuracoes.GruposUsrXPermissoes;
using Base.UserControls.Configuracoes.GruposUsuarios;
using Base.UserControls.CRM.Cliente;
using Base.UserControls.Estoquev.Armazem;
using Base.UserControls.Estoquev.Caracteristica;
using Base.UserControls.Estoquev.GruposProdutos;
using Base.UserControls.Estoquev.LocaisEstoque;
using Base.UserControls.Estoquev.Marca;
using Base.UserControls.Estoquev.Produto;
using Base.UserControls.Estoquev.UnidadesModulo;
using Base.UserControls.Faturamento.Pedido_venda;
using Base.UserControls.Financeiro.ClasseImposto;
using Base.UserControls.Financeiro.Condicoes_pag;
using Base.UserControls.Financeiro.Conta_bancarias;
using Base.UserControls.Financeiro.Operadora_cartao;
using Base.UserControls.Financeiro.Tabela_preco;
using Base.UserControls.Financeiro.TiposMovimento;
using Base.Util;
using Base.Windows.Selecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Base.Windows
{
    /// <summary>
    /// Lógica interna para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        public Principal()
        {
            InitializeComponent();

            tabControl.IsSynchronizedWithCurrentItem = true;
            lbData.Content = UsuariosController.DataBase.ToShortDateString();
            lbDB_type.Content = "Curae / " + Commons.GetDB_Type;
            lbNome_usuario.Content = UsuariosController.UsuarioAtual.Nome;
            lbNome_empresa.Content = EmpresasController.Find(UsuariosController.Empresa_atual_id).Nome_fantasia;
        }

        private void btn_OnClick()
        {
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            UsuariosController.DisconnectUser(UsuariosController.UsuarioAtual.Id);
            System.Environment.Exit(0);
        }

        private void btClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TabItem item = (TabItem)tabControl.SelectedItem;
            if (item.Header.Equals("Início"))
                return;

            tabControl.Items.Remove(tabControl.SelectedItem);
        }

        private void NormalButton_OnClick()
        {
            new SelecionarUnidade().ShowDialog();
        }

        private void btUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UsuariosContainer uc = new UsuariosContainer();
            if (UsuariosController.ValidaPermissao(uc.Tela_id, Enums.TipoPermissao.ACESSO))
                Util.Navigation.AddTabItem(tabControl, uc, "Usuários");
        }

        private void btGrupos_usuairos_Click(object sender, RoutedEventArgs e)
        {
            Grupos_usuariosContainer gc = new Grupos_usuariosContainer();
            if (UsuariosController.ValidaPermissao(gc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, gc, "Grupos de usuários");
        }

        private void btGruposXPermissoes_Click(object sender, RoutedEventArgs e)
        {
            GruposUsuariosXPermissoes guxp = new GruposUsuariosXPermissoes();
            if (UsuariosController.ValidaPermissao(guxp.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, guxp, "Grupos de usuários X Permissões");
        }

        private void btEmpresa_Click(object sender, RoutedEventArgs e)
        {
            EmpresasContainer ec = new EmpresasContainer();
            if (UsuariosController.ValidaPermissao(ec.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, ec, "Empresas");
        }

        private void btUnidades_Click(object sender, RoutedEventArgs e)
        {
            UnidadesContainer uc = new UnidadesContainer();
            if (UsuariosController.ValidaPermissao(uc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, uc, "Unidades");
        }

        private void btCaracteristicas_Click(object sender, RoutedEventArgs e)
        {
            CaracteristicasContainer cc = new CaracteristicasContainer();
            if (UsuariosController.ValidaPermissao(cc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, cc, "Características");
        }

        private void btArmazens_Click(object sender, RoutedEventArgs e)
        {
            ArmazemContainer ac = new ArmazemContainer();
            if (UsuariosController.ValidaPermissao(ac.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, ac, "Gestão de armazéns");
        }

        private void btLocaisEstoque_Click(object sender, RoutedEventArgs e)
        {
            LocaisEstoqueContainer lec = new LocaisEstoqueContainer();
            if (UsuariosController.ValidaPermissao(lec.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, lec, "Locais de estoque");
        }

        private void btGrupos_produtos_Click(object sender, RoutedEventArgs e)
        {
            Grupos_produtoContainer gpc = new Grupos_produtoContainer();
            if (UsuariosController.ValidaPermissao(gpc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, gpc, "Grupos de produtos");
        }

        private void btTipos_mov_Click(object sender, RoutedEventArgs e)
        {
            TmvContainer tmvc = new TmvContainer();
            if (UsuariosController.ValidaPermissao(tmvc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, tmvc, "Tipos de movimento");
        }

        private void btClasses_imp_Click(object sender, RoutedEventArgs e)
        {
            CLImpContainer climpc = new CLImpContainer();
            if (UsuariosController.ValidaPermissao(climpc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, climpc, "Classes de imposto");
        }

        private void btCondicoesPagamento_Click(object sender, RoutedEventArgs e)
        {
            Condicoes_pagContainer cpc = new Condicoes_pagContainer();
            if (UsuariosController.ValidaPermissao(cpc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, cpc, "Condições de pagamento");
        }

        private void btOperadoras_cartao_Click(object sender, RoutedEventArgs e)
        {
            Operadora_cartaoContainer opc_C = new Operadora_cartaoContainer();
            if (UsuariosController.ValidaPermissao(opc_C.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, opc_C, "Operadoras de cartão");
        }

        private void btContas_bancarias_Click(object sender, RoutedEventArgs e)
        {
            Contas_bancContainer cbc = new Contas_bancContainer();
            if (UsuariosController.ValidaPermissao(cbc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, cbc, "Contas bancárias");
        }

        private void Paginador_OnPageChange(int page)
        {
         //   new MsgAlerta(page.ToString());
        }

        private void btProdutos_Click(object sender, RoutedEventArgs e)
        {
            ProdutosContainer pc = new ProdutosContainer();
            if (UsuariosController.ValidaPermissao(pc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, pc, "Produtos");
        }

        private void btMarcas_Click(object sender, RoutedEventArgs e)
        {
            MarcasContainer mc = new MarcasContainer();
            if (UsuariosController.ValidaPermissao(mc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, mc, "Marcas");
        }

        private void btTabelas_preco_Click(object sender, RoutedEventArgs e)
        {
            Tabela_precoContainer tpc = new Tabela_precoContainer();
            if (UsuariosController.ValidaPermissao(tpc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, tpc, "Tabelas de preços");
        }

        private void btPedidos_venda_Click(object sender, RoutedEventArgs e)
        {
            Pedidos_vendaContainer pvc = new Pedidos_vendaContainer();
            if (UsuariosController.ValidaPermissao(pvc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, pvc, "Pedidos de venda");
        }

        private void btClientes_Click(object sender, RoutedEventArgs e)
        {
            ClientesContainer cc = new ClientesContainer();
            if (UsuariosController.ValidaPermissao(cc.Tela_id, Enums.TipoPermissao.ACESSO))
                Navigation.AddTabItem(tabControl, cc, "Clientes");
        }
    }
}
