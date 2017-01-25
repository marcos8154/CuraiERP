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

namespace EM3.UserControls.Estoquev.Caracteristica
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

            cbAtributo.AddItem("Cor");
            cbAtributo.AddItem("Tamanho");
            cbAtributo.AddItem("Sabor");
            cbAtributo.AddItem("Comprimento");
            cbAtributo.AddItem("Largura");
            cbAtributo.AddItem("Estampa");
            cbAtributo.AddItem("Polegada");
            cbAtributo.AddItem("Diâmetro");
            cbAtributo.AddItem("Altura");
            cbAtributo.AddItem("Formato");
            cbAtributo.AddItem("Amperagem");
            cbAtributo.AddItem("Voltagem");
            cbAtributo.AddItem("Material");
            cbAtributo.AddItem("Capacidade");
            cbAtributo.AddItem("RPM");
            cbAtributo.AddItem("Peso");

            cbAtributo.SelectedIndex = 0;
        }

        private void btSalvar_OnClick()
        {
            Salvar(true);
        }

        private void Salvar(bool close)
        {
            if (Caracteristica == null) Caracteristica = new Caracteristicas();

            Caracteristica.Id = int.Parse(txCodigo.Text);
            Caracteristica.Atributo = cbAtributo.Text;
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
            cbAtributo.Text = Caracteristica.Atributo;
            txValor.Text = Caracteristica.Valor;

            cabecalho.Title = "Alterar característica (" + Caracteristica.Atributo + ")";
        }
    }
}
