using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerWorld.Data;
using ComputerWorld.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWorld.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ContactController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: PCWorldContacts
        public async Task<IActionResult> Index()
        {
            return View(await _db.Contact.ToListAsync());
        }

        // GET: PCWorldContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pCWorldContacts = await _db.Contact
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pCWorldContacts == null)
            {
                return NotFound();
            }

            return View(pCWorldContacts);
        }

        // GET: PCWorldContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ThankYou()
        {
            return View();
        }


        // POST: PCWorldContacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,Email,Message,PhoneNumber")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _db.Add(contact);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYou));
            }
            return View(contact);
        }




    }
}