using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticPersistence.Abstract
{
    public interface IElasticsearchService
    {
        Task CheckIndex(string index);
        //Task InsertDocument(string index, object document);
        void InsertDocument(Permission document, string id);
        void InsertDocuments(IEnumerable<Permission> documents, string id);
        Task DeleteDocumentById(int id);
        Task<Permission> GetDocument(int id);
        Task<IEnumerable<Permission>> GetDocuments();
    }
}
