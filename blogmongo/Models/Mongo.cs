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

        public List<BlogPost> vratiNBlogovaAutora(string id,int pageIndex,int number)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Author == novi).Skip(pageIndex*number).Limit(number);
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

        public List<BlogPost> vratiNFavoritaAutora(string id,int pageIndex,int pageSize)
        {
            var collection = this.database.GetCollection<User>("useri");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Id == novi);
            List<User> korisnici = lista.ToList<User>();
            return korisnici[0].Favorites.Skip(pageIndex * pageSize).Take(pageSize).ToList();
             
        }

        public void brisiBlog(string id,string email)
        {
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            var collectionNova = this.database.GetCollection<User>("useri");
            ObjectId asd = new ObjectId(id);
            var filter = Builders<BlogPost>.Filter.Eq("Id", asd);
            var filterNOvi = Builders<User>.Filter.Eq("Email", email);
            User korisnik = vratiUseraPoEmailu(email);
            korisnik.Blogs.Remove(korisnik.Blogs.Find(x => x.Id.ToString() == id));
            var update = Builders<User>.Update.Set("Blogs", korisnik.Blogs);

            collection.DeleteOne(filter);
            collectionNova.UpdateOne(filterNOvi, update);
        }

        public List<BlogPost> vratiNBlogovaAutoraLista(string id, int pageIndex, int pageSize)
        {
            var collection = this.database.GetCollection<User>("useri");
            ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Id == novi);
            List<User> korisnici = lista.ToList<User>();
            return korisnici[0].Blogs.Skip(pageIndex * pageSize).Take(pageSize).ToList();

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
            BlogPost bp = new BlogPost { Title = bn.title, Description = bn.description, Author = new ObjectId(bn.autorId) };
            var collection = this.database.GetCollection<BlogPost>("blogovi");
            var collectionNova = this.database.GetCollection<User>("useri");
            collection.InsertOne(bp);
            ObjectId iod = new ObjectId(bn.autorId);
            var lista = collectionNova.Find(x => x.Id == iod);
            User korisnik = lista.ToList()[0];
            if (korisnik.Blogs != null)
                korisnik.Blogs.Add(bp);
            else
            {
                korisnik.Blogs = new List<BlogPost>();
                korisnik.Blogs.Add(bp);
            }
            
            var filter = Builders<User>.Filter.Eq("Id", iod);
            var update = Builders<User>.Update.Set("Blogs", korisnik.Blogs);

            collectionNova.UpdateOne(filter, update);


        }

        //public void dodajKomentar(string user, string mess, BsonDateTime date, string idBloga)
        //{
        //    var collection = this.database.GetCollection<BlogPost>("blogovi");
        //    BlogPost bp = vratiJedanBlog(idBloga);
        //    Comment com = new Comment { Username = user, Message = mess, DateCreated = date };
        //    List<Comment> novaListaKomentara;
        //    if (bp.Comments != null)
        //    {
        //        novaListaKomentara = bp.Comments;
        //        novaListaKomentara.Add(com);
        //    }
        //    else
        //    {
        //        novaListaKomentara = new List<Comment>();
        //        novaListaKomentara.Add(com);
        //    }
        //    ObjectId oid = new ObjectId(idBloga);

        //    var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
        //    var update = Builders<BlogPost>.Update.Set("Comments", novaListaKomentara);

        //    collection.UpdateOne(filter, update);
            
        //}

        //public void lajkuj(string idBloga, string idKomentara, bool komentar)
        //{
        //    var collection = this.database.GetCollection<BlogPost>("blogovi");
        //    BlogPost bp = vratiJedanBlog(idBloga);
        //    if (!komentar)
        //    {
        //        int brLikova = bp.Likes++;
        //        ObjectId oid = new ObjectId(idBloga);

        //        var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
        //        var update = Builders<BlogPost>.Update.Set("Likes", brLikova);

        //        collection.UpdateOne(filter, update);
        //    }
        //    else
        //    {
        //        bp.Comments.Find(x => x.Id == new ObjectId(idKomentara)).Likes++;

        //        ObjectId oid = new ObjectId(idBloga);

        //        var filter = Builders<BlogPost>.Filter.Eq("Id", oid);
        //        var update = Builders<BlogPost>.Update.Set("Comments", bp.Comments);

        //        collection.UpdateOne(filter, update);
        //    }
        //}

        public void updateUsera(string ime,string prezime,string opis,string id)
        {
            var collection = this.database.GetCollection<User>("useri");
            ObjectId oid = new ObjectId(id);
            var filter = Builders<User>.Filter.Eq("Id", oid);
            var update = Builders<User>.Update.Set("Ime", ime).Set("Prezime", prezime).Set("Opis", opis);
            collection.UpdateOne(filter, update);
        }

        public void kreirajUsera(string ime, string prezime, string email)
        {
            User novi = new User { Ime = ime, Prezime = prezime, Email = email};
            var collection = this.database.GetCollection<User>("useri");
            collection.InsertOne(novi);
        }

        public User vratiUseraPoEmailu(string email)
        {
            var collection = this.database.GetCollection<User>("useri");
            //ObjectId novi = new ObjectId(id);
            var lista = collection.Find(x => x.Email == email);
            return lista.ToList<User>()[0];


        }

        public void dodajUFavorite(string blogID,string email)
        {
            BlogPost bp = this.vratiJedanBlog(blogID);
            User korisnik = this.vratiUseraPoEmailu(email);
            if (korisnik.Favorites != null)
                korisnik.Favorites.Add(bp);
            else
            {
                korisnik.Favorites = new List<BlogPost>();
                korisnik.Favorites.Add(bp);
            }

            var collection = this.database.GetCollection<User>("useri");
            var filter = Builders<User>.Filter.Eq("Email", email);
            var update = Builders<User>.Update.Set("Favorites", korisnik.Favorites);

            collection.UpdateOne(filter, update);
        }

    }
}