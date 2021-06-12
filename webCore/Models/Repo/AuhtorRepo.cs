using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webCore.Models.Repo
{
    public class AuhtorRepo : IBookStoreRepo<Author>
    {
        IList<Author> authors;
        public AuhtorRepo()
        {
            authors = new List<Author>()
            {
                new Author{Id=1,Fullname="Zayd elimrani"},
                new Author{Id=2,Fullname="Ayoub elyoussfi"},
                new Author{Id=3,Fullname="Mohamed Elyounnsi"}
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a => a.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var auth = Find(id);
            authors.Remove(auth);
        }

        public Author Find(int id)
        {
            return authors.SingleOrDefault(a => a.Id == id);
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(Author entity, int id)
        {
            var auth = Find(id);
            auth.Fullname = entity.Fullname;
        }
    }
}
