using System;
using Microsoft.AspNet.Mvc;
using Lucene.Net.Store;
using Microsoft.AspNet.Loader.IIS;
using Lucene.Net.Search;
using Microsoft.WindowsAzure.Storage;
using Lucene.Net.Store.Azure;
using Microsoft.WindowsAzure;
using Lucene.Net.QueryParsers;
using System.Collections.Generic;
using ReversePackageSearch.Models;
using Microsoft.Framework.OptionsModel;

namespace ReversePackageSearch
{
    public class SearchController
    {
        private BlobStorageOptions _options;

        public SearchController(IOptionsAccessor<BlobStorageOptions> optionsAccessor)
        {
            _options = optionsAccessor.Options;
        }

	    public IActionResult Get(string searchTerm)
	    {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return new JsonResult(null);
            }
            if (searchTerm.StartsWith("*") || searchTerm.StartsWith("?"))
            {
                return new JsonResult(null);
            }
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount.TryParse(_options.ConnectionString, out cloudStorageAccount);

            var azureDirectory = new AzureDirectory(cloudStorageAccount, "TestCatalog");
            var searcher = new IndexSearcher(azureDirectory);

            var analyzer = new NuGet.Indexing.IdentifierAnalyzer();

            var queryParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_30, new[] { "Type", "ReturnType" }, analyzer);
            Query query = queryParser.Parse(searchTerm);

            //execute the query
            var hits = searcher.Search(query, 50);

            var packages = new List<SearchResult>();

            foreach (var hit in hits.ScoreDocs)
            {
                var doc = searcher.Doc(hit.Doc);
                var result = new SearchResult
                {
                    FullTypeName = doc.GetField("Type").StringValue,
                    PackageName = doc.GetField("Package").StringValue
                };

                if (doc.GetField("Signature") != null)
                {
                    result.Signature = doc.GetField("Signature").StringValue;
                }

                if (doc.GetField("ReturnType") != null)
                {
                    result.ReturnType = doc.GetField("ReturnType").StringValue;
                }

                packages.Add(result);
            }

            return new JsonResult(packages);
	    }
    }
}