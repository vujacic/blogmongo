using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPBlogoviTest.Models.Entiteti
{
    public class Comment
    {
        ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        BsonDateTime DateCreated { get; set; }
        public int Likes { get; set; }
    }
}