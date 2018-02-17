using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;

namespace blogmongo.Models.Entiteti
{
    public class Comment
    {
        public ObjectId Id { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public BsonDateTime DateCreated { get; set; }
        public int Likes { get; set; }
    }
}