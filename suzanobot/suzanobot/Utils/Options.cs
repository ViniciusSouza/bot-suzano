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
		//TITULO OPÇÕES INICIAIS
		//===============================================
		public static string tituloOpcaoInicial = "Olá, vou te ajudar a encontrar documentos para os seguintes assuntos:";

		//===============================================
		//DESCRIÇÃO OPÇÕES INICIAIS
		//===============================================
		public static string descricaoOpcaoInicial = "Se tiver um problema diferente, clique em 'Outros' e digite sua pergunta.";

		//===============================================
		//OPÇÕES INICIAIS
		//===============================================
		/// <summary>
		///0 = Suporte a assuntos relacionados a Nota Fiscal; 
		///1 = Atendimentos auditorias, fiscalizações etc; 
		///2 = Outros; 
		/// </summary>
		public static string[] initialOptions = new[]
		{
			"Suporte a assuntos relacionados a Nota Fiscal",
			"Atendimentos, auditorias, fiscalizações etc",
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

		public static string[] optionsDuvidaNF = new[]
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

		//===============================================
		//DESCRIÇÃO ATENDIMENTOS, AUDITORIAS, FISCALIZAÇÕES
		//===============================================
		public static string descricaoOpcaoAtendimento = "Solicitação de documentos:";

		//===============================================
		//OPÇÕES REFERENTES A ATENDIMENTOS, AUDITORIA, FISCALIZAÇÃO
		//===============================================
		/// <summary>
		///0 = Certidão negativa de débito; 
		///1 = Comprovante de recolhimento de impostos; 
		///2 = Recibo de entrega de obrigação; 
		///3 = Notas fiscais emitidas pela Suzano; 
		/// </summary>
		public static string[] optionsSolicitacaoDocumentos = new[]
		{
			"Certidão negativa de débito",
			"Comprovante de recolhimento de impostos",
			"Recibo de entrega de obrigação",
			"Notas fiscais emitidas pela Suzano"
		};

		//===============================================
		//OPÇÕES REFERENTES A OUTROS
		//===============================================
		/// <summary>
		///0 = Solicitação de informações; 
		///1 = Cadastro de Cliente/Fornecedor; 
		///2 = Solicitação de Procuração; 
		///3 = Autorização para Documentos Fiscais – AIDF; 
		///4 = Informe de Rendimentos Prestador de Serviços; 
		///5 = Documentos Processos Digitais; 
		/// </summary>
		public static string[] optionsOutros = new[]
		{
			"Solicitação de informações",
			"Cadastro de Cliente/Fornecedor",
			"Solicitação de Procuração",
			"Autorização para Documentos Fiscais – AIDF",
			"Informe de Rendimentos Prestador de Serviços",
			"Documentos Processos Digitais"
		};
	}
}