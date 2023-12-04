using Domain.Entities;
using Elastic.Clients.Elasticsearch;
using ElasticPersistence.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticPersistence.Persistence
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticsearchClient _elasticsearchClient;
        private readonly string _index;
        
        public ElasticsearchService(ElasticsearchClient elasticSearchClient, string index)
        {
            _elasticsearchClient = elasticSearchClient;
            _index = index;
        }

        public Task CheckIndex(string index)
        {
            var result = _elasticsearchClient.Indices.Exists(index);
            if(result.Exists)
            {
                return Task.CompletedTask;
            }
            else
            {
                _elasticsearchClient.Indices.Create(_index);
                return Task.CompletedTask;
            }
        }

        public Task DeleteDocumentById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Permission> GetDocument(int id)
        {
            try
            {
                var response = await _elasticsearchClient.GetAsync<Permission>(id);
                if (response.IsSuccess())
                {
                    return response.Source;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Permission>> GetDocuments()
        {
            try
            {
                var response = await _elasticsearchClient.SearchAsync<Permission>();
                if (response.IsSuccess())
                {
                    return response.Documents;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertDocument(Permission document, string id)
        {
            _elasticsearchClient.Index(document, _index, cfg => cfg.Id(id));
        }

        public void InsertDocuments(IEnumerable<Permission> document, string id)
        {
            var x = _elasticsearchClient.IndexMany(document, _index);
        }
        public Task InsertDocument(string index, object document)
        {
            throw new NotImplementedException();
        }

    }
}
