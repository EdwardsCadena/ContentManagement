using Proyecto.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Interfaces
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetArticles();
        Task<Article> GetArticle(int id);
        Task InsertArticle(Article article, int userId);
        Task<bool> UpdateArticle(Article article);
        Task<bool> DeleteArticle(int id);
    }
}
