using SmartWareData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryUnits
{
    public interface IArticlesUnit
    {
        public Task<IEnumerable<Article>> GetAllArticles();
    }
}
