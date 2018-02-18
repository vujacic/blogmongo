using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using blogmongo.Models.Entiteti;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace blogmongo.Models
{
    public class Mongo
    {
        private IMongoDatabase database;
        public Mongo()
        {
            var connectionString = "mongodb://localhost/?safe=true";
            var client = new MongoClient(connectionString);           
            this.database = client.GetDatabase("sajt");
            
        }

        public List<BlogPost> vratiSveBlogoveAutora(string id)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            ObjectId novi = new ObjectId(id);
            var lista =  collection.Find (x => x.Author == novi);
            return lista.ToList<BlogPost>();
        }

        public List<BlogPost> vratiFavoriteAutora(string id)
        {
            var collection = this.database.GetCollection<User>("useri");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Id == novi);
            List<User> korisnici = lista.ToList<User>();
            return korisnici[0].Favorites;
        }

        public BlogPost vratiJedanBlog(string id)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Id == novi);
            return lista.ToList<BlogPost>()[0];

        }

        public User vratiUsera(string id)
        {
            var collection = this.database.GetCollection<User>("useri");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Id == novi);
            return lista.ToList<User>()[0];
        }

        public void kreirajBlog(BlogNew bn)
        {
            BlogPost bp = new BlogPost { Title = bn.title, Description = bn.description, Tags = bn.tagovi, Author = new ObjectId(bn.autorId) };
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            collection.InsertOne(bp);
        }

        public void dodajKomentar(string user, string mess, BsonDateTime date, string idBloga)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            BlogPost bp = vratiJedanBlog(idBloga);
            Comment com = new Comment { Username = user, Message = mess, DateCreated = date };
            List<Comment> novaListaKomentara;
            if (bp.Comments != null)
            {
                novaListaKomentara = bp.Comments;
                novaListaKomentara.Add(com);
            }
            else
            {
                novaListaKomentara = new List<Comment>();
                novaListaKomentara.Add(com);
            }
            ObjectId oid = new ObjectId(idBloga);

            var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
            var update = Builders<BlogPost>.Update.Set("Comments", novaListaKomentara);

            collection.UpdateOne(filter, update);
            
        }

        public void lajkuj(string idBloga, string idKomentara, bool komentar)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            BlogPost bp = vratiJedanBlog(idBloga);
            if (!komentar)
            {
                int brLikova = bp.Likes++;
                ObjectId oid = new ObjectId(idBloga);

                var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
                var update = Builders<BlogPost>.Update.Set("Likes", brLikova);

                collection.UpdateOne(filter, update);
            }
            else
            {
                bp.Comments.Find(x => x.Id == new ObjectId(idKomentara)).Likes++;

                ObjectId oid = new ObjectId(idBloga);

                var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
                var update = Builders<BlogPost>.Update.Set("Comments", bp.Comments);

                collection.UpdateOne(filter, update);
            }
        }

    }
}