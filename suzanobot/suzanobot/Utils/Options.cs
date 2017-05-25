using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suzanobot.Utils
{
	public class Options
	{
		//===============================================
		//OPÇÕES INICIAIS
		//===============================================
		private string[] initialOptions = new[]
		{
			"Suporte a assuntos relacionados a Nota Fiscal",
			"Atendimentos auditorias, fiscalizações etc",
			"Outros"
		};

		//===============================================
		//OPÇÕES REFERENTES A NOTA FISCAL
		//===============================================
		private string[] optionsNotaFiscal = new[]
		{
			"Solicitação de pagamento e duvidas",
			"Nota travada SAP",
			"Cancelamento de nota fiscal"
		};

		private string[] optionsSolicitacaoPagamentoNF = new[]
		{
			"Tributação",
			"Alíquota",
			"Ajuste cadastro cliente/fornecedor",
			"Informações para solicitação de pagamento",
			"Emissão NF Complementar",
			"Emissão NF Importação",
			"",
			""
		};
	}
}