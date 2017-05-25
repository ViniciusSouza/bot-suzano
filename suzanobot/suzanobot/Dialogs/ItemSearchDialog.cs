using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using suzanobot.Services;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class ItemSearchDialog : IDialog
    {

        private AzureSearchClient clientSearch;

        private static readonly string[] TopRefiners = { "keyword" }; // define your own custom refiners 

        public ItemSearchDialog(AzureSearchClient _clientSearch)
        {
            clientSearch = _clientSearch;
        }

        public Task StartAsync(IDialogContext context)
        {
            throw new NotImplementedException();
        }

        //protected override string[] GetTopRefiners()
        //{
        //    return TopRefiners;
        //}

    }
}