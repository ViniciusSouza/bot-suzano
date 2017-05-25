using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Search.Dialogs;
using Search.Services;

namespace suzanobot.Dialogs
{
    [Serializable]
    public class ItemSearchDialog : SearchDialog
    {

        private static readonly string[] TopRefiners = { "refiner1", "refiner2", "refiner3" }; // define your own custom refiners 

        public ItemSearchDialog(ISearchClient searchClient) : base(searchClient, multipleSelection: true)
    {
        }

        protected override string[] GetTopRefiners()
        {
            return TopRefiners;
        }

    }
}