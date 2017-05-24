using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureSearchImporter
{
    [SerializePropertyNamesAsCamelCase]
    public class Documento
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string ID { get; set; }

        [IsFilterable, IsSortable, IsFacetable]
        public string Categoria { get; set; }

        [IsFilterable]
        public string Item { get; set; }

        public string Pergunta { get; set; }

        public string Requisitos { get; set; }

        [IsSearchable]
        public string Keyword { get; set; }

        [IsFilterable]
        public string Observacao { get; set; }

        [IsFilterable]
        public string Breadcrumb { get; set; }

    }
}