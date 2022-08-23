using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppDev.Data;
using AppDev.Models;

namespace AppDev.Views
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;

        public BooksController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await context.Books
                .Include(b => b.Category)
                .ToListAsync();
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Books == null)
            {
                return NotFound();
            }

            var book = await context.Books
                .Include(b => b.Category)
                .Include(b => b.StoreOwner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
