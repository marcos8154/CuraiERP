using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EM3.Enums
{
    public enum StatusRetorno
    {
        OPERACAO_OK = 600,
        NAO_ENCONTRADO = 404,
        FALHA_INTERNA = 800,
        FALHA_VALIDACAO = 550,
        NAO_AUTORIZADO_LS = 900
    }
}
