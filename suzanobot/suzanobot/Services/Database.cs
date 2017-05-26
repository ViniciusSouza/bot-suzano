using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using suzanobot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace suzanobot.Services
{
    public static class Database
    {
        public static List<Documento> documentos;

        public static void LoadJson()
        {
            StreamReader file = File.OpenText(@"C:\Users\visouza\Repos\suzano-bot\suzanobot\suzanobot\Data\documentos.json");
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
    }
}