using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webCore.Models.Repo
{
    public class BookRepo : IBookStoreRepo<Book>
    {
        List<Book> books;
        public BookRepo()
        {
            books = new List<Book>(){
                new Book { Id=1,Title="C# Programing",Description="No description"},
                new Book { Id=2,Title="React",Description="React description"},
                new Book { Id=1,Title="Java",Description="No data"},
            };
        }
        public void Add(Book entity)
        {
            books.Add(entity);
        }

        public void Delete(int id)
        {
            books.Remove(Find(id));
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(Book entity,int id)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
        }
    }
}
