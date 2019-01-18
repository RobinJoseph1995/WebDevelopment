using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComputerWorld.Data;
using ComputerWorld.Models;
using ComputerWorld.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerWorld.Areas.Admin.Controllers

{
    [Authorize(Roles = SD.SuperAdminEndUser)]
    [Area("Admin")]  //if we dont do this then it will not identify the controller
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;   //we need database oject for CRUD operations
        public ProductTypesController(ApplicationDbContext db)   //this is the contructor for dependency injection
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());  //this is for the view to expect a list of product types
        }
        //get create action method
        public IActionResult Create() 
        {
            return View();
        }

        //post create action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)  //we use this to check that all the required defined in model is valid
            {
                _db.Add(productTypes);  //if it is valid we will save it to database
                await _db.SaveChangesAsync();  //we use await so our code will continue to run while it is been saved to db
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }


        //now edit

        //get create action method
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null)           
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);  
            if (productType==null)
            {
                return NotFound();
            }

            return View(productType);  
        }

        //post edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            if(id!=productTypes.Id)
            {
                NotFound();
            }
            if (ModelState.IsValid)  //we use this to check that all the required defined in model is valid
            {
                _db.Update(productTypes); //if it is valid we will save it to database
                await _db.SaveChangesAsync();  //we use await so our code will continue to run while it is been saved to db
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes); //if it is not valid then it will pass through product type so it will display what was not valid
        }

        //now details

        //get create action method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //now delete

        //get create action method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        //post delete action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)  //if you wanna change the name of delete here then on the [httpost] you need to add [HttpPost, ActionName"Delete"]
        {
            var productTypes = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productTypes);
            await _db.SaveChangesAsync(); 

                return RedirectToAction(nameof(Index));
            
        }

    }
}