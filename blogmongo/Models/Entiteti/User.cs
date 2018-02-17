using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Bson;
using MongoDB.Driver;

namespace NBPBlogoviTest.Models.Entiteti
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Opis { get; set; }      
        public List<BlogPost> Blogs { get; set; }   
        public List<BlogPost> Favorites { get; set; }
    }
}