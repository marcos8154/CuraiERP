using EM3.Controller;
using EM3.Extensions;
using EM3.Windows;
using EM3.Windows.Selecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Configuracoes.GruposUsrXPermissoes
{
    /// <summary>
    /// Interação lógica para GruposUsuariosXPermissoes.xam
    /// </summary>
    public partial class GruposUsuariosXPermissoes : UserControl
    {
        public string Tela_id = "44";

        public GruposUsuariosXPermissoes()
        {
            InitializeComponent();
            dataGrid.AplicarPadroes();

            dataGrid.IsReadOnly = false;
            dataGrid.Columns[0].IsReadOnly = true;
            dataGrid.Columns[1].IsReadOnly = true;
            dataGrid.Columns[2].IsReadOnly = false;
            dataGrid.Columns[3].IsReadOnly = false;
            dataGrid.Columns[4].IsReadOnly = false;
            dataGrid.Columns[5].IsReadOnly = false;
        }

        private void txCod_grupo_CallSearch()
        {
            SelecionarGrupoUsuarios sg = new SelecionarGrupoUsuarios();
            sg.ShowDialog();

            txCod_grupo.Text = sg.Selecionado.Id.ToString();
            txNome_grupo.Text = sg.Selecionado.Nome;

            if (sg.Selecionado.Id > 0)
            {
                List<PermissaoView> listPermissoes = new List<PermissaoView>();

                List<Telas> telas = TelasController.ListAll();
                List<Permissoes> permssoes = PermissoesController.ListByGrupo(sg.Selecionado.Id);

                foreach (Telas tela in telas)
                {
                    Permissoes permissao = permssoes.FirstOrDefault(e => e.Grupo_usuarios_id == sg.Selecionado.Id && e.Telas_id.Equals(tela.Id)) ?? new Permissoes();

                    PermissaoView permissaoView = new PermissaoView();
                    permissaoView.Id = int.Parse(tela.Id);
                    permissaoView.Tela = tela.Descricao;
                    permissaoView.Acesso = permissao.Acesso;
                    permissaoView.Inserir = permissao.Inserir;
                    permissaoView.Atualizar = permissao.Atualizar;
                    permissaoView.Excluir = permissaoView.Excluir;

                    listPermissoes.Add(permissaoView);
                }

                dataGrid.ItemsSource = listPermissoes;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btSalvar_OnClick()
        {
            try
            {
                if (dataGrid.Items.Count == 0)return;

                btSalvar.Enabled = false;
                Salvar();
                btSalvar.Enabled = true;
            }
            catch (Exception ex)
            {
                new MsgAlerta("Ocorreu um problema nesta estação ao salvar a permissão. Acione o suporte Doware.");
                btSalvar.Enabled = true;
            }
        }

        private void Salvar()
        {
            List<PermissaoView> permissoes = (List<PermissaoView>)dataGrid.ItemsSource;

            WaitWindow ww = new WaitWindow();
            ww.txTitulo.Text = "Aplicando permissões";
            ww.Progresso.Maximum = (double)permissoes.Count;
            ww.Show();

            new Thread(() =>
            {
                if (PermissoesController.Clear(txCod_grupo.Value))
                {
                    foreach (PermissaoView pv in permissoes)
                    {
                        Permissoes permissao = new Permissoes();

                        permissao.Grupo_usuarios_id = txCod_grupo.Value;
                        permissao.Telas_id = pv.Id.ToString();
                        permissao.Acesso = pv.Acesso;
                        permissao.Inserir = pv.Inserir;
                        permissao.Atualizar = pv.Atualizar;
                        permissao.Excluir = pv.Excluir;

                        if (PermissoesController.Add(permissao))

                            ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.Progresso.Incresses(1)), ww);
                    }

                    ww.Dispatcher.Invoke(new Action<WaitWindow>(w => ww.Close()), ww);
                }
            }).Start();
        }

    }

    public class PermissaoView
    {
        public int Id { get; set; }
        public string Tela { get; set; }
        public bool Acesso { get; set; }
        public bool Inserir { get; set; }
        public bool Atualizar { get; set; }
        public bool Excluir { get; set; }
    }
}
