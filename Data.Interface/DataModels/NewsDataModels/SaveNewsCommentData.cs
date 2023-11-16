﻿namespace Data.Interface.DataModels.NewsDataModels
{
    public class SaveNewsCommentData
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedTime { get; set; }
        public string AuthorName { get; set; }
    }


}
