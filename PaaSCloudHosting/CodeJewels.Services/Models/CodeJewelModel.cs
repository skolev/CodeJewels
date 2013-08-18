using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CodeJewels.Services.Models
{
    [DataContract(Name = "CodeJewel")]
    public class CodeJewelModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "category")]
        public string category { get; set; }

        [DataMember(Name = "sourceCode")]
        public string CodeJewel { get; set; }

        [DataMember(Name = "postedBy")]
        public string AuthorName { get; set; }

        [DataMember(Name = "avgVote")]
        public double AverageVote { get; set; }
    }
}