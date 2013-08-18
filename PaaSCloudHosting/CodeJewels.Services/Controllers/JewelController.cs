using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CodeJewels.Data;
using CodeJewels.Services.Models;

namespace CodeJewels.Services.Controllers
{
    public class JewelController : BaseController
    {
        [HttpPost]
        [ActionName("addjewel")]
        public HttpResponseMessage AddCodeJewel([FromBody] CodeJewel code)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeJewelEntities();
                using (context)
                {
                    context.CodeJewels.Add(code);
                    context.SaveChanges();
                    CodeJewelModel model = DesirializeCodeJewel(code);
                    return model;
                }
            });
            return response;
        }

        [HttpGet]
        [ActionName("getall")]
        public HttpResponseMessage GetAllCodeJewels()
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeJewelEntities();
                using (context)
                {
                    var jewels = context.CodeJewels;
                    HashSet<CodeJewelModel> models = new HashSet<CodeJewelModel>();
                    foreach (var codeJewel in jewels)
                    {
                        models.Add(DesirializeCodeJewel(codeJewel));
                    }

                    return models;
                }
            });
            return response;
        }

        [HttpGet]
        [ActionName("bysource")]
        public HttpResponseMessage SearchByCriteriaSource(string source)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeJewelEntities();
                using (context)
                {
                    var jewels = context.CodeJewels.Include("Category").Include("Votes").Where(code => code.SourceCode.Contains(source));
                    HashSet<CodeJewelModel> models = new HashSet<CodeJewelModel>();
                    foreach (var codeJewel in jewels)
                    {
                        models.Add(DesirializeCodeJewelToFull(codeJewel));
                    }

                    return models;
                }
            });
            return response;
        }

        [HttpGet]
        [ActionName("bycategory")]
        public HttpResponseMessage SearchByCriteriaCategory(string category)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeJewelEntities();
                using (context)
                {
                    var jewels = context.CodeJewels.Include("Category").Include("Votes").Where(code => code.Category.Name == category);
                    HashSet<CodeJewelModel> models = new HashSet<CodeJewelModel>();
                    foreach (var codeJewel in jewels)
                    {
                        models.Add(DesirializeCodeJewelToFull(codeJewel));
                    }

                    return models;
                }
            });
            return response;
        }
        private CodeJewelModel DesirializeCodeJewelToFull(CodeJewel codeJewel)
        {

            double averageVote = 0;
            if (codeJewel.Votes.Count > 0)
            {
                averageVote = codeJewel.Votes.Average(v => v.Value);
            }

            string category = "No Category";
            if (codeJewel.Category != null)
            {
                category = codeJewel.Category.Name;
            }

            CodeJewelModel modelFull = new CodeJewelModel
            {
                AuthorName = codeJewel.AuthorMail,
                CodeJewel = codeJewel.SourceCode,
                Id = codeJewel.Id,
                AverageVote = averageVote,
                category = category
            };

            return modelFull;
        }

        private CodeJewelModel DesirializeCodeJewel(CodeJewel code)
        {
            CodeJewelModel model = new CodeJewelModel
            {
                AuthorName = code.AuthorMail,
                CodeJewel = code.SourceCode,
                Id = code.Id
            };

            return model;
        }
    }
}
