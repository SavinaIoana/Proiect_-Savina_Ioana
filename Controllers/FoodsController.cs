using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect__Savina_Ioana.Data;
using Proiect__Savina_Ioana.Models;

namespace Proiect__Savina_Ioana.Controllers
{
    public class FoodsController : Controller
    {
        private readonly RestaurantContext _context;

        public FoodsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Foods
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DishSortParm"] = String.IsNullOrEmpty(sortOrder) ? "dish_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var foods = from b in _context.Food
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.Dish.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "dish_desc":
                    foods = foods.OrderByDescending(b => b.Dish);
                    break;
                case "Price":
                    foods = foods.OrderBy(b => b.Price);
                    break;
                case "price_desc":
                    foods = foods.OrderByDescending(b => b.Price);
                    break;
                default:
                    foods = foods.OrderBy(b => b.Dish);
                    break;
            }
            int pageSize = 2;
            return View(await PaginatedList<Food>.CreateAsync(foods.AsNoTracking(), pageNumber ??1, pageSize));
        }

        // GET: Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var food = await _context.Food
            .Include(s => s.Orders)
            .ThenInclude(e => e.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Dish,Chef,Price")] Food food)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Food.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Food>(
            studentToUpdate,
            "",
            s => s.Chef, s => s.Dish, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }
        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Food.Remove(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.ID == id);
        }
    }
}
