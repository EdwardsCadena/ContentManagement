using Microsoft.EntityFrameworkCore;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;
using Proyecto.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Infrastructure.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ContentManagementContext _context;
        public ArticleRepository(ContentManagementContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Article>> GetArticles()
        {
            var Articles = await _context.Articles.ToListAsync();
            return Articles;
        }
        public async Task<Article> GetArticle(int id)
        {
            var Article = await _context.Articles.FirstOrDefaultAsync(x => x.ArticleId == id);
            return Article;
        }
        public async Task InsertArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateArticle(Article article)
        {
            var result = await GetArticle(article.ArticleId);
            result.Title = article.Title;
            result.Content = article.Content;
            result.UserId = article.UserId;
            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> DeleteArticle(int id)
        {
            var delete = await GetArticle(id);
            _context.Remove(delete);
            int row = await _context.SaveChangesAsync();
            return row > 0;
        }
    }
}
