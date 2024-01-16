
using Microsoft.EntityFrameworkCore;
using SmartWareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnits
{
    public  class ArticlesUnit:IArticlesUnit
    {
        private readonly SmartwareContext _context;
        public ArticlesUnit(SmartwareContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _context.Articles.ToListAsync();
        }
    }
}
