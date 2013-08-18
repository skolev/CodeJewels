using CodeJewels.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeJewels.Services.Controllers
{
    public class VoteController : BaseController
    {
        private const double MinVoteValue = 0;
        [HttpPost]
        [ActionName("addvote")]
        public HttpResponseMessage AddVote(int id, [FromBody] Vote vote)
        {
            var response = PerformOperation(() =>
            {
                var context = new CodeJewelEntities();
                using (context)
                {
                    var jewel = context.CodeJewels.FirstOrDefault(j => j.Id == id);
                    if (jewel == null)
                    {
                        throw new InvalidOperationException("The jewel does not exists!");
                    }
                    jewel.Votes.Add(vote);
                    context.Votes.Add(vote);
                    context.SaveChanges();

                    return vote;
                }
            });
            return response;
        }
    }
}
