using EM3.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EM3.UserControls.Estoque.Caracteristica
{
    /// <summary>
    /// Interação lógica para CCaracteristicas.xam
    /// </summary>
    public partial class CCaracteristicas : UserControl
    {
        public delegate void Complete();
        public event Complete OnComplete;

        private Caracteristicas Caracteristica;

        public CCaracteristicas()
        {
            InitializeComponent();
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (Caracteristica == null) Caracteristica = new Caracteristicas();

            Caracteristica.Id = int.Parse(txCodigo.Text);
            Caracteristica.Atributo = cbAtributos.Text;
            Caracteristica.Valor = txValor.Text;

            if (CaracteristicasController.Save(Caracteristica))
            {
                if (close)
                    Close();
                else
                    LimparCampos();
            }
        }

        private void LimparCampos()
        {
            Caracteristica = new Caracteristicas();
            txCodigo.Text = "0";
            txValor.Text = string.Empty;
        }

        private void Close()
        {
            if (OnComplete != null) OnComplete();
        }

        private void btSalvarEContinuar_OnClick()
        {
            Salvar(false);
        }

        private void btCancelar_OnClick()
        {
            Close();
        }

        public void Load(int id)
        {
            Caracteristica = CaracteristicasController.Find(id);

            txCodigo.Text = Caracteristica.Id.ToString();
            cbAtributos.Text = Caracteristica.Atributo;
            txValor.Text = Caracteristica.Valor;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbAtributos.AddItem("Cor");
            cbAtributos.AddItem("Tamanho");
            cbAtributos.AddItem("Sabor");
            cbAtributos.AddItem("Comprimento");
            cbAtributos.AddItem("Largura");
            cbAtributos.AddItem("Estampa");
            cbAtributos.AddItem("Polegada");
            cbAtributos.AddItem("Diâmetro");
            cbAtributos.AddItem("Altura");
            cbAtributos.AddItem("Formato");
            cbAtributos.AddItem("Amperagem");
            cbAtributos.AddItem("Voltagem");
            cbAtributos.AddItem("Material");
            cbAtributos.AddItem("Capacidade");
            cbAtributos.AddItem("RPM");
            cbAtributos.AddItem("Peso");

            cbAtributos.SelectedIndex = 0;
        }
    }
}
