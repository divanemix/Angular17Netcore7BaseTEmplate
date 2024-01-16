
using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

//using System.DirectoryServices;
//using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Logging;
using RepositoryUnits;
using System.Data;
using System.Reflection.PortableExecutable;
using System.DirectoryServices;
using System.Runtime.Versioning;
using SmartWareData.Models;

namespace SmartWare
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IArticlesUnit _articleUnit;

        public ArticlesController(IArticlesUnit articlesUnit,
            ILogger<ArticlesController> logger,
            IMapper mapper,
            IConfiguration config)
        {

            _articleUnit = articlesUnit;
            _logger = logger;
            _mapper = mapper;
            _config = config;
        }

        [Authorize]
        [Produces("application/json")]
        [Route("all-articles")]
        [HttpGet]
        public async Task<IActionResult> getAllArticles()
        {
            IEnumerable<Article> list = await _articleUnit.GetAllArticles();
            List<Article> articles = list.ToList();
            List<ArticleDTO> artDTOs = _mapper.Map<List<Article>, List<ArticleDTO>>(articles);
            return Ok(artDTOs);
        }

    }
}