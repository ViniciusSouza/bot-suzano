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
		public static string nomeBot = "Suzan bot";

		//===============================================
		//TITULO OPÇÕES INICIAIS
		//===============================================
		public static string tituloOpcaoInicial = "Olá, vou te ajudar a encontrar documentos para os seguintes assuntos:";

		//===============================================
		//DESCRIÇÃO OPÇÕES INICIAIS
		//===============================================
		public static string descricaoOpcaoInicial = "Se tiver um problema diferente, clique em 'Outros' e digite sua pergunta.";

		public static string mensagemNaoExisteBancoFrentes = "Não existem frentes no banco de dados. Contate o administrador do bot.";

		public static string mensagemConversaCompletada = "Obrigado por utilizar o Suzan bot! Espero ter ajudado. <br />Caso tenha mais alguma dúvida, pode me chamar novamente.";

		public static string mensagemConversaNaoCompletada = "Sinto não poder te ajudar :/ . Ainda bem que conto com um time muito bom, disponível através do número/contato";

		public static string mensagemEscolhaCategoria = "Selecione a categoria referente ao tópico '{0}'.";

		public static string mensagemEscolhaItem = "Selecione o item referente a categoria '{0}'.";
	}
}