using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBiz.Models
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string DescTitle { get; set; }
        public string Desc { get; set; }
        public string Client { get; set; }
        public DateTime Date { get; set; }
        public string ProjectUrl { get; set; }

        public List<PortfolioImage> PortfolioImages { get; set; } = new List<PortfolioImage>();
        [NotMapped]
        public IFormFile PosterFile { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        public Category Category { get; set; }
    }
}
