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
        private const string indexName = "csc";
        private static ISearchServiceClient _searchClient;

        private static ISearchIndexClient _indexClient;


        static void Main(string[] args)
        {

            string searchServiceName = "search-suzano-bot";

            string apiKey = "380FF6176DF4212E336643BA3F23CE4A";

            bool DeleteToCreate = false;

            // Create an HTTP reference to the catalog index
            _searchClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
            _indexClient = _searchClient.Indexes.GetClient(indexName);

            // To process a file I'm deleting the given index_name, and recreating the index based on the Class definition.
            // If the file doesn't have the full data, the data existent over the Azure Side will be lost.
            if (DeleteToCreate)
            {
                DeleteHotelsIndexIfExists(_searchClient, indexName);
                CreateHotelsIndex(_searchClient, indexName);
            }

            UploadDocuments(_indexClient, "C:\\Users\\visouza\\Repos\\suzano-bot\\suzanobot\\AzureSearchImporter\\data\\fiscal.txt", true);

        }

        private static void CreateHotelsIndex(ISearchServiceClient serviceClient, string index_name)

        {
            var definition = new Index()
            {
                Name = index_name,
                Fields = FieldBuilder.BuildForType<Documento>()
            };

            serviceClient.Indexes.Create(definition);
        }



        private static void DeleteHotelsIndexIfExists(ISearchServiceClient searchClient, string index_name)
        {
            if (searchClient.Indexes.Exists("index_name"))
            {
                searchClient.Indexes.Delete("index_name");
            }
        }

        private static void UploadDocuments(ISearchIndexClient indexClient, string filename, bool isFirstLineHeaders)
        {

            using (var streamReader = File.OpenText(filename))
            {
                var count = 0;
                var lines = streamReader.ReadToEnd().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var documentos = new List<Documento>();
                foreach (var line in lines)
                {
                    Documento documento = null;
                    if (!isFirstLineHeaders || isFirstLineHeaders && count > 0)
                        documento = ProcessLine(line);

                    if (documento != null)
                        documentos.Add(documento);
                    count++;
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

            try
            {
                documento.ID = Guid.NewGuid().ToString();
                documento.Categoria = columns[1];
                documento.Item = columns[2];
                documento.Pergunta = columns[3];
                documento.Requisitos = columns[4];
                documento.Keywords = columns[5];
                documento.Observacao = columns[6];
                documento.Breadcrumb = columns[7];

            }
            catch (IndexOutOfRangeException e)
            {
                documento.Breadcrumb = string.Empty;
                Console.WriteLine("Index não encontrado, provavelmente alguma informação não está presente no arquivo informado: " + e.Message);
            }
            return documento;
        }
    }
}