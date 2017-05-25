using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace suzanobot.Model
{
    [Serializable]
    public class Documento
    {
        public string ID { get; set; }
        public string Categoria { get; set; }
        public string Item { get; set; }
        public string Pergunta { get; set; }
        public string Requisitos { get; set; }
        public string Keywords { get; set; }
        public string Observacao { get; set; }
        public string Breadcrumb { get; set; }

    }
}