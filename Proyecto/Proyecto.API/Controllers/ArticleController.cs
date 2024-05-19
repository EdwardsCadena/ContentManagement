using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Proyecto.API.Response;
using Proyecto.Core.DTOs;
using Proyecto.Core.Entities;
using Proyecto.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Proyecto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _ArticleRepository;
        private readonly IMapper _maper;
        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            _ArticleRepository = articleRepository;
            _maper = mapper;
        }
        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _ArticleRepository.GetArticles();
            var articlesdto = _maper.Map<IEnumerable<ArticleDTOs>>(articles);
            return Ok(articlesdto);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _ArticleRepository.GetArticle(id);
            var articles = _maper.Map<ArticleDTOs>(article);
            return Ok(articles);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task<IActionResult> PostArticle(ArticleDTOs articledto)
        {
            var arti = _maper.Map<Article>(articledto);
            await _ArticleRepository.InsertArticle(arti);
            return Ok(articledto);
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, ArticleDTOs artidto)
        {
            var artiup = _maper.Map<Article>(artidto);
            artiup.ArticleId = id;
            var Update = await _ArticleRepository.UpdateArticle(artiup);
            var updatedto = new ApiResponse<bool>(Update);
            return Ok(updatedto);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _ArticleRepository.DeleteArticle(id);
            var deletealquiler = new ApiResponse<bool>(result);
            return Ok(deletealquiler);
        }
    }
}
