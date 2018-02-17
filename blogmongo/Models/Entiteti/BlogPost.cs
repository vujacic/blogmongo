using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;

namespace blogmongo.Models.Entiteti
{
    public class BlogPost
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ObjectId Author { get; set; }  // ili MongoDBRef
        public List<string> Tags { get; set; }
        public int Likes { get; set; }
        public List<Comment> Comments { get; set; }

    }
}