using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webCore.Models;
using webCore.Models.Repo;
using webCore.ViewModels;

namespace webCore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepo<Book> bookRepo;
        private readonly IBookStoreRepo<Author> authRepo;

        public BookController(IBookStoreRepo<Book> bookRepo,IBookStoreRepo<Author> authRepo)
        {
            this.bookRepo = bookRepo;
            this.authRepo = authRepo;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = bookRepo.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var modal = new BookAuthorViewModel
            {
                Authors = FillSelelctList()
            };
            return View(modal);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            var modal = new BookAuthorViewModel
                        {
                            Authors = FillSelelctList()
                        };
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select an author from the list !";
                        
                        return View(modal);
                    }
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = authRepo.Find(model.AuthorId)
                    };
                    bookRepo.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("","You have to feel all the required fields!");
            return View(modal);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepo.Find(id);
            var authId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewmodel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authId,
                Authors = authRepo.List().ToList()
            };
            return View(viewmodel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {
            try
            {
                Book book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = authRepo.Find(model.AuthorId)
                };
                bookRepo.Update(book,model.BookId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepo.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                bookRepo.Delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> FillSelelctList()
        {
            var authors = authRepo.List().ToList();
            authors.Insert(0, new Author { Id = -1, Fullname = "--- Please select an author ---" });
            return authors;
        }
    }
}
