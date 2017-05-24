using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AzureSearchImporter
{
    class Program
    {
        private const string GeoNamesIndex = "fiscal";
        private static ISearchServiceClient _searchClient;

        private static ISearchIndexClient _indexClient;


        static void Main(string[] args)
        {

            string searchServiceName = "";

            string apiKey = "";

            // Create an HTTP reference to the catalog index

            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));

            _indexClient = _searchClient.Indexes.GetClient(GeoNamesIndex);

            UploadDocuments(_indexClient, "C:\\Users\\visouza\\Repos\\suzano-bot\\suzanobot\\AzureSearchImporter\\data\\fiscal.txt");



        }

        private static void UploadDocuments(ISearchIndexClient indexClient, string filename)
        {

            using (var streamReader = File.OpenText(filename))
            {
                var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var documentos = new List<Documento>();
                foreach (var line in lines)
                {
                    var documento = ProcessLine(line);
                    documentos.Add(documento);
                }

                var batch = IndexBatch.Upload(documentos);

                try
                {
                    indexClient.Documents.Index(batch);
                }
                catch (IndexBatchException e)
                {

                    // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                    // the batch. Depending on your application, you can take compensating actions like delaying and
                    // retrying. For this simple demo, we just log the failed document keys and continue.

                    Console.WriteLine(
                        "Failed to index some of the documents: {0}",
                        String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key))
                        );
                }

                Console.WriteLine("Waiting for documents to be indexed...\n");
                Thread.Sleep(2000);

            }
        }

        private static Documento ProcessLine(string line)
        {
            var columns = line.Split('\t');
            var documento = new Documento();

            documento.ID = Guid.NewGuid().ToString();
            documento.Categoria = columns[1];
            documento.Item = columns[2];
            documento.Pergunta = columns[3];
            documento.Requisitos = columns[4];
            documento.Keyword = columns[5];
            documento.Observacao = columns[6];

            return documento;
        }
    }
}