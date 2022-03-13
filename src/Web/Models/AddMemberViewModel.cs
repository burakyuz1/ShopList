using ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class AddMemberViewModel
    {
        public int ListId { get; set; }
        public int MemberId { get; set; }
        public List<SelectListItem> NonMembers { get; set; }
        public List<Member> Members { get; set; }
    }
    public class Member
    {
        public string Name { get; set; }
    }


}
