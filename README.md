Suzano is the second largest eucalyptus pulp company in the world and one of the ten largest paper companies in the world. The company has a department called CSC, which stands for Central de Serviços Compartilhados (Shared Services Central). This area is responsible for handling several kinds of internal administrative and financial demands, such as processing invoices, purchases and loans, requesting equipment transfer and so forth.

The main problem is support all doubts of users, because the same process can be util for any areas, but the resolutions is different and in lot of situations the users don't know how identify the best path to solve. This leads employees to call CSC to clarify their doubts and questions, which floods the CSC staff with repetitive queries.

During hackfest, we built Suzan, a bot in the FAQ style, integrated with Sharepoint thrue Rest API. The choice for Sharepoint is based in common technology used at Suzano and provides agility in creating an area for easy content management for users.

 
**Key technologies used:**
* Bot Framework C# SDK
* Sharepoint & Sharepoint REST API
* Github
 
**Core Team:**

- Guilherme José de Araujo Novoa (Developer, Suzano)
- Mario del Matto (Developer Manager, Suzano)
- Vinicius Augusto Matheus Plaza (Developer, Suzano)
- Fernanda Conti (Business, Suzano) 
- Cynthia Zanoni (Technical Evangelist, Microsoft)
- Vinicius Souza (Sr. Technical Evangelist, Microsoft)

<img src="https://user-images.githubusercontent.com/2198735/27110067-147d1214-507d-11e7-9e0f-3b2038093fcd.jpeg" width="350"  />

## Customer profile ##
[Suzano](http://www.suzano.com.br) is the second largest eucalyptus pulp company in the world and one of the ten largest paper companies in the world. It was founded in Brazil in 1924 and currently has a workforce of 8,400 employees plus 11,000 contractors. The company’s yearly revenue is approximately US$ 3 billion.

The company exports eucalyptus pulp to 31 countries and paper to more than 60 countries. It has five factories in Brazil, as well as a commercial office in China and subsidiaries in United States, Switzerland, England and Argentina.

Suzano owns 1.2 million hectares of forest areas, 520,000 of which are planted forests. Suzano adopts precision silviculture techniques in these forests to ensure maximum productivity.

 
## Problem statement ##


Suzano has a department called CSC, which stands for Central de Serviços Compartilhados (Shared Services Central). This area is responsible for handling several kinds of internal administrative and financial demands, such as processing invoices, purchases and loans, requesting equipment transfer and so forth.

The platform used by CSC to receive and process these demands is ServiceNow (https://www.servicenow.com/). Suzano’s employees and vendors are expected to access this system directly and fill the specific form related to their particular request.

In practice, most users feel lost when filling the forms, as they use terms which are specific to Accounting or Finance and are not generally known by professionals from other areas. This leads employees to call CSC to clarify their doubts and questions, which floods the CSC staff with repetitive queries.
 

## Solution and steps ##

The proposed solution consists of a FAQ to help users quickly ask questions about how to use the CSC. The CSC serves a large number of users and areas of the company and with this, the content of the Bot will be managed through a portal in Sharepoint, where professionals responsible for area management can document the processes that involve the processing of company documents.

### Dialog Design ###

The design of dialog on bot is very simple, because we don't have a lot of data about all process and areas on CSC and this POC will be based on Fiscal Area. Bellow, you can see the micro interactions on bot dialog.

![Dialog Design](https://user-images.githubusercontent.com/2198735/27110489-20868a74-5080-11e7-945a-8f3559111a2e.jpg)


### Sharepoint API ###

The Sharepoint form was based in table of data below. Process and/or procedure is the action needed for do in CSC platform, that user have doubts about requirements and path (in CSC) to start the procedure.

| Column        | Description   |
| ------------- |:-------------:| 
| Area      | Define the company area, from a defined list of data. i.e. fiscal and HR. | 
| Front      | Define the target process, from a defined list of data. i.e. support and requests. | 
| Category | Define the status by action needed to process. i.e. invoices errors. | 
| Item | Name of process.  | 
| Requirements | Information about requirements to attending the procedure.  | 
| Keyword | Keyword based in process for help users in search.  | 
| Observations | Complementary information about procedure | 
| Path | Path in CSC platform to create the process.  | 

If you are new to SharePoint REST API or you want to know more about REST endpoints in SharePoint visit [this link](https://dev.office.com/sharepoint/docs/sp-add-ins/get-to-know-the-sharepoint-rest-service) and get to know the SharePoint REST service. After create the Sharepoint Form, we've enabled the Web API Rest of the platform, generating an endpoint like:

```https://<SiteName>.sharepoint.com/_api/web?$select=Title```

Because SharePoint Online is very much secured and that doesn’t allow anonymous users to access the information for their site. The next step was enable acess for external client, in this case, the bot. 

To authorizing acess, we need register an Add-In in SharePoint, define permissions and generate an access token. Bellow, you can check the steps:

**Register Add-In**

To register the Add-In in SharePoint, we need set informations about API. Follow the steps bellow, you can create the access in Add-in.

- Navigate and login to SharePoint online site. Then navigate to the Register Add-In page by entering the url as ```https://<sitename>.SharePoint.com/_layouts/15/appregnew.aspx```
- On App Information section, click Generate button next to the Client Id and Client Secret textboxes to generate the respective values. Enter with informations and set RedirectUri for ```https://localhost``` and click in create.

![Register Add-in](https://user-images.githubusercontent.com/2198735/27109585-45613da4-507a-11e7-8e70-42f604398da9.png)


**Permissions to Add-In** 

Once the Add-In is registered, the Add-in need enable access for SharePoint data. We will set the Read permission level to the web scope, so that we will be able to read the web information.

- Navigate to the SharePoint site Then enter the URL ```https://<sitename>.sharepoint.com/_layouts/15/appinv.aspx``` in the browser. 

![Permissions to Add-In](https://user-images.githubusercontent.com/2198735/27109588-456a7fd6-507a-11e7-8720-7b2e64def04d.png)

Enter with informations and include the XML bellow, with new permissions and click to create.

```xml
<AppPermissionRequests AllowAppOnlyPolicy="true">
    <AppPermissionRequest Scope="http://sharepoint/content/sitecollection/web" Right="Read" />
</AppPermissionRequests>
```

Get the access tokens after creation:

![Tokens](https://user-images.githubusercontent.com/2198735/27109586-45635648-507a-11e7-8114-041ab1fa9890.png)


### Bot implementation ###

The core bot functionality was built by using Bot Framework SDK in C#.  The knowledge base of bot is accessed through a Rest API connected to Sharepoint. 

The dialog is based on two entities: category and item. The category used by the user is based on the front that the bot has registered in the base. The front refers to the sector of the company, which in this case dispenses with a previous selection, because at this moment the bot will only serve the fiscal area.

We store some common bot speech in a list of static strings, which is queried at some point in the interaction and to indicate the contact of the sector responsible for CSC support, when the bot does not have the answer.

```csharp
public class Options
{
 //===============================================
 //NAME OF BOT
 //===============================================
 public static string nomeBot = "Suzan bot";

 //===============================================
 //INICIAL OPTIONS
 //===============================================
 public static string tituloOpcaoInicial = "Olá, vou te ajudar a encontrar documentos para os seguintes assuntos:";

 //===============================================
 //DESCRIPTIONS OF ACTIONS
 //===============================================
 public static string descricaoOpcaoInicial = "Se tiver um problema diferente, clique em 'Outros' e digite sua pergunta.";

 public static string mensagemNaoExisteBancoFrentes = "Não existem frentes no banco de dados. Contate o administrador do bot.";

 public static string mensagemConversaCompletada = "Espero ter ajudado com sua dúvida! <br />Caso tenha mais alguma dúvida, pode me chamar novamente.";

 public static string mensagemConversaNaoCompletada = "Sinto não poder te ajudar :/ . Ainda bem que conto com um time muito bom, disponível através do número/contato";

 public static string mensagemEscolhaCategoria = "Selecione a categoria referente ao tópico '{0}'.";

 public static string mensagemEscolhaItem = "Selecione o item referente a categoria '{0}'.";
}

```

The RootDialog manages the dialog and connect with static strings (when necessary) and worked to direct users in the queries doubts.

```csharp
[Serializable]
public class RootDialog: IDialog < object > {
 private ResumptionCookie resumptionCookie;

 public string Frente {
  get;
  set;
 }
 public string Categoria {
  get;
  set;
 }

 public Task StartAsync(IDialogContext context) {
  Database.GetRestData();

  context.Wait(MessageReceivedAsync);

  return Task.CompletedTask;
 }


 public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable < IMessageActivity > result) {
  var message = await result;

  if (this.resumptionCookie == null) {
   this.resumptionCookie = new ResumptionCookie(message);
  }

  await this.WelcomeMessageAsync(context);
 }

 private async Task WelcomeMessageAsync(IDialogContext context) {
  var reply = context.MakeMessage();
  UriBuilder uri = Utils.Utils.GetPlaceHoldImg(56, 640, 330, Options.nomeBot);

  var frentes = Database.GetFrentes().ToArray();

  if (frentes != null && frentes.Length > 0) {
   reply.AddHeroCard(Options.tituloOpcaoInicial, Options.descricaoOpcaoInicial, frentes, new [] {
    uri.ToString()
   });
   await context.PostAsync(reply);
   context.Wait(this.onFrenteSelected);
  } else {
   reply.Text = HttpUtility.HtmlDecode(Options.mensagemNaoExisteBancoFrentes);
   await context.PostAsync(reply);
  }
 }

 private async Task onFrenteSelected(IDialogContext context, IAwaitable < IMessageActivity > result) {
  var message = await result;
  Frente = message.Text;
  var dialog = new CategoriaDialog(Frente);
  context.Call(dialog, CategoriaReplayMessage);
 }

 private async Task CategoriaReplayMessage(IDialogContext context, IAwaitable < string > result) {
  var resultado = await result;

  var text = "";
  if (resultado.Equals("Completed")) {
   text = Options.mensagemConversaCompletada;
  } else {
   text = Options.mensagemConversaNaoCompletada;

  }

  var reply = context.MakeMessage();
  reply.Text = HttpUtility.HtmlDecode(text);
  await context.PostAsync(reply);
  context.Done(string.Empty);
 }
}
```

Below, you can check a category query implementation code snippet:

```csharp
[Serializable]
public class CategoriaDialog : IDialog<string>
{
    public string Frente { get; set; }
    public string Categoria { get; set; }
    public List<string> Categorias { get; set; }

    public CategoriaDialog (string frente)
    {
        Frente = frente;

        Categorias = Database.GetCategorias(Frente);
    }

    public async Task StartAsync(IDialogContext context)
    {
        string message = string.Format(Options.mensagemEscolhaCategoria, Frente);

        PromptDialog.Choice(context, ResumeAfterAnswer, Categorias, message);
    }

    private async Task ResumeAfterAnswer(IDialogContext context, IAwaitable<string> result)
    {
        Categoria = await result;
        var dialog = new ItemDialog(Frente, Categoria);

        context.Call(dialog, itemSelected);
    }

    private async Task itemSelected(IDialogContext context, IAwaitable<string> result)
    {
        var item = await result;
        context.Done(item);
    }
}
```

After select category, the user can find the main situations that generate doubts. To display the queries, which we call "Item" in the bot ecosystem, we send a new query in the API. To optimize the display of a selected item, which is basically a list of requirements and steps to follow within the CSC system, we use a herocard, which will display the content format with markdown.

```csharp
[Serializable]
public class ItemDialog: IDialog < string > {
 public string Categoria {
  get;
  set;
 }
 public string Frente {
  get;
  set;
 }
 public Dictionary < string,
 string > Itens {
  get;
  set;
 }

 public ItemDialog(string frente, string categoria) {
  Categoria = categoria;
  Frente = frente;

  Itens = Database.GetItens(Frente, Categoria);
 }

 public async Task StartAsync(IDialogContext context) {
  string message = string.Format(Options.mensagemEscolhaItem, Categoria);

  //PromptDialog.Choice(context, ResumeAfterAnswer, Itens, message);
  var reply = context.MakeMessage();
  reply.Type = ActivityTypes.Message;
  reply.TextFormat = TextFormatTypes.Markdown;
  reply.Text = message;

  var actions = new List < CardAction > ();

  foreach(var item in Itens) {
   actions.Add(new CardAction() {
    Title = item.Value, Value = string.Format("{0} - {1}", item.Key, item.Value)
   });
  }

  reply.SuggestedActions = new SuggestedActions() {
   Actions = actions
  };
  await context.PostAsync(reply);
  context.Wait(ItemSelected);

 }

 private async Task ItemSelected(IDialogContext context, IAwaitable < IMessageActivity > result) {
  var message = await result;

  var id = "-1";
  try {
   id = message.Text.Split('-')[0].Trim();
  } catch {}

  var documento = Database.getDocumentoByID(id);

  if (documento != null) {
   var reply = context.MakeMessage();
   reply.Type = ActivityTypes.Message;
   reply.TextFormat = "markdown";


   string text = "";
   text = text + string.Format("## {0} ## <br />", documento.Frente);
   text = text + string.Format("### {0} ### <br />", documento.Categoria);
   text = text + string.Format("**Requisitos:** {0} <br /><br />", documento.Requisitos.Replace(";", "<br />"));
   text = text + string.Format("**Observação:** {0} <br />", documento.Observacao);
   text = text + string.Format("**Caminho no CSC:** {0} <br />", documento.Breadcrumb);

   reply.Text = text;

   await context.PostAsync(reply);
   context.Done("Completed");
  } else {
   context.Done("Failed");
  }
 }
}
```
Searches in the Rest API are managed by the class below. Since we use Sharepoint Forms in the backend, when we start a search, we need to define the search URL structure for the items we need to return and then perform the parser in the code.

```csharp
public static class Database
    {
        public static List<Documento> documentos;

        public static void LoadJson()
        {
            var path = HttpContext.Current.Server.MapPath("/");
            var jsonfile = Path.Combine(path, @"Data\documentos.json");

            StreamReader file = File.OpenText(jsonfile);
            var lista = (JArray)JArray.Parse(file.ReadToEnd());
            documentos = new List<Documento>();

            foreach (var obj in lista)
            {
                Documento documento = new Documento();
                documento.ID = obj["ID"].ToString();
                documento.Frente = obj["frente"].ToString();
                documento.Categoria = obj["categoria"].ToString();
                documento.Breadcrumb = obj["caminho"].ToString();
                documento.Item = obj["item"].ToString();
                documento.Keywords = obj["keywords"].ToString();
                documento.Observacao = obj["observacao"].ToString();
                documento.Requisitos = obj["requisitos"].ToString();

                documentos.Add(documento);
            }

        }

        internal static Dictionary<string, string> GetItens(string frente, string categoria)
        {
            if (documentos == null || documentos.Count == 0)
                return null;

            var itens = documentos
                .Where(d => d.Frente.Equals(frente))
                .Where(d => d.Categoria.Equals(categoria))
                .Select(c => new { c.ID, c.Item } ).ToList();

            return itens.ToDictionary(x => x.ID, x => x.Item);
        }

        internal static Documento getDocumentoByID(string id)
        {
            if (documentos == null || documentos.Count == 0)
                return null;

            var documento = documentos.Where(d => d.ID.Equals(id)).First<Documento>();

            return documento;
        }

        internal static List<string> GetCategorias(string frente)
        {
            if (documentos == null || documentos.Count == 0)
                return null;

            var categorias = documentos.Where(d => d.Frente.Equals(frente)).Select(c => c.Categoria).Distinct().ToList();

            return categorias.ToList<string>();
        }

        public static List<string> GetFrentes()
        {
            if (documentos == null || documentos.Count == 0)
                return null;

            var frentes = documentos.Select(c => c.Frente).Distinct().ToList();
            
            return frentes;
		}
		public static void GetRestData()
		{
			var client = new RestClient("URL_WITH_PARAMNS");
			var request = new RestRequest(Method.GET);
			request.AddHeader("postman-token", "token here");
			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("authorization", "Bearer token here");
			request.AddHeader("accept", "application/json;odata=verbose");
			IRestResponse response = client.Execute(request);
			var teste = response.Content;
			var lista = JToken.Parse(teste);

			var values = lista["value"];

			documentos = new List<Documento>();

			foreach (var obj in values)
			{
				Documento documento = new Documento();

				documento.ID = obj["ID"].ToString();
				documento.Frente = obj["Frente"].ToString();
				documento.Categoria = obj["Categoria"].ToString();
				documento.Breadcrumb = obj["CaminhoInfo"].ToString();
				documento.Item = obj["Item"].ToString();
				documento.Keywords = obj["Keywords"].ToString();
				documento.Observacao = obj["Observacao"].ToString();
				documento.Requisitos = obj["Requisitos"].ToString();

				documentos.Add(documento);
			}

		}
	}
```

## Technical delivery ##
The technical delivery of Suzano was based in two main areas:
- Data: Integration with Sharepoint form for maintain the data up to date and manageable by company areas leaders;
- Bot Agent: The agent multi channel for running in Skype for Business and Web;
 
## Conclusion ##

**"The event provided a clear vision of how much we can increase our efficiency and availability in attending to customers and employees. With fast, standardized and quality service we will be able to generate new values for Cia."**

*Mario del Matto, Developer Manager, Suzano*


## Additional resources ##
[Bot Emulator](https://emulator.botframework.com/)