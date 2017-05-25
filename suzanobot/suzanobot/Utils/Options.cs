using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suzanobot.Utils
{
	public class Options
	{
		//===============================================
		//NOME BOT
		//===============================================
		public static string nomeBot = "Suzano CSC bot";

		//===============================================
		//OPÇÕES INICIAIS
		//===============================================
		public static string[] initialOptions = new[]
		{
			"Suporte a assuntos relacionados a Nota Fiscal",
			"Atendimentos auditorias, fiscalizações etc",
			"Outros"
		};

		//===============================================
		//OPÇÕES REFERENTES A NOTA FISCAL
		//===============================================
		public static string[] optionsNotaFiscal = new[]
		{
			"Solicitação de pagamento e duvidas",
			"Nota travada SAP",
			"Cancelamento de nota fiscal"
		};

		public static string[] optionsSolicitacaoPagamentoNF = new[]
		{
			"Tributação",
			"Alíquota",
			"Ajuste cadastro cliente/fornecedor",
			"Informações para solicitação de pagamento",
			"Emissão NF Complementar",
			"Emissão NF Importação",
            "Emissão de guias antecipadas"
		};

        public static string[] optionsTravadaSAPNF = new[]
        {
            "Aprocação de Nota Fiscal",
            "Recusa de Nota Fiscal",
            "Com recopi",
            "Reimpressão de Nota Fiscal"
        };

        public static string[] optionsCancelamentoNF = new[]
        {
            "Emissão superior a 24 horas",
            "Emissão inferior a 24 horas"
        };

    }
}