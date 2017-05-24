using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;

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
        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [IsFilterable]
        public string Observacao { get; set; }

        [IsFilterable]
        public string Breadcrumb { get; set; }

    }
}