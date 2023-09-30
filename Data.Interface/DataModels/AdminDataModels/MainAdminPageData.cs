using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.DataModels.AdminDataModels
{
    public class MainAdminPageData
    {
        public List<LastNews> LastPublishedNews { get; set; }
        public List<LastComment> LastComments { get; set; }
        public List<LastRegisteredUser> LastRegisteredUsers { get; set; }
        public SiteStats SiteStats { get; set; } 
    }

    public class LastNews
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public CommentAuthorData Creator { get; set; }
    }

    public class LastComment
    {
        public int Id { get; set; }
        public CommentAuthorData Creator { get; set; }
        public string Text { get; set; }
        public int NewsId { get; set; }
    }

    public class LastRegisteredUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

    public class SiteStats
    {
        public int NewsCount { get; set; }
        public int NewsCommentsCount { get; set; }
        public int UsersCount { get; set; }
    }
}
