/* 27/11/2016 09:25:44 */
/* AUTO-GENERATED CLASS */
/* DOES NOT ADD PROPERTIES */
/* DOES NOT CHANGE NAME OF PROPERTIES */
/* IMPLEMENTS INTERFACES OR ABSTRACT CLASSES DOES NOT CHANGE THE OPERATION */

using EM3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace EM3
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Ean { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string Ncm { get; set; }
        public int Anp { get; set; }
        public string Data_cadastro { get; set; }
        public string Ultima_alteracao { get; set; }
        public bool Fabricado { get; set; }
        public bool Insumo { get; set; }
        public bool Fracionado { get; set; }
        public bool Inativo { get; set; }
        public bool Para_balanca { get; set; }
        public int Marca_id { get; set; }
        public string Fabricante { get; set; }
        public string Cod_fabricante { get; set; }
        public int Unidade1 { get; set; }
        public int Fator_conversao { get; set; }
        public int Unidade2 { get; set; }
        public int Classe_imposto_id { get; set; }
        public int Cest_id { get; set; }
        public int Grupo_produtos_id { get; set; }
        public int Validade { get; set; }
        public decimal Peso_liquido { get; set; }
        public decimal Ponto_pedido { get; set; }
        public int Fornecedor_padrao { get; set; }
        public decimal Custo_standard { get; set; }
        public decimal Preco_venda { get; set; }
        public decimal Ult_preco { get; set; }
        public decimal Comissao { get; set; }
        public int Prod_equivalente { get; set; }
        public int Empresa_padrao { get; set; }
        public int Garantia_loja { get; set; }
        public int Garantia_forn { get; set; }
        public int Foto_id { get; set; }
        public int Origem { get; set; }
        public string Ultima_compra { get; set; }

        public Unidades Unidades { get; set; }
        public Classes_imposto Classes_imposto { get; set; }
        public Grupos_produtos Grupos_produtos { get; set; }
        public Fornecedores Fornecedores { get; set; }
        public Marcas Marcas { get; set; }
        public Cest Cest { get; set; }
    }
}
