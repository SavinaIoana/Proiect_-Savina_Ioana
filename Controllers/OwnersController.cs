using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect__Savina_Ioana.Data;
using Proiect__Savina_Ioana.Models;
using Proiect__Savina_Ioana.Models.RestaurantViewModels;

namespace Proiect__Savina_Ioana.Controllers
{
    public class OwnersController : Controller
    {
        private readonly RestaurantContext _context;

        public OwnersController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Owners
        public async Task<IActionResult> Index(int? id, int? foodID)
        {
            var viewModel = new OwnersIndexData();
            viewModel.Owners = await _context.Owners
            .Include(i => i.OwnedFoods)
            .ThenInclude(i => i.Food)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.OwnerName)
            .ToListAsync();
            if (id != null)
            {
                ViewData["OwnerID"] = id.Value;
                Owners owners= viewModel.Owners.Where(
                i => i.ID == id.Value).Single();
                viewModel.Foods = owners.OwnedFoods.Select(s => s.Food);
            }
            if (foodID != null)
            {
                ViewData["FoodID"] = foodID.Value;
                viewModel.Orders = viewModel.Foods.Where(
                x => x.ID == foodID).Single().Orders;
            }
            return View(viewModel);
        }
        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners
                .FirstOrDefaultAsync(m => m.ID == id);
            if (owners == null)
            {
                return NotFound();
            }

            return View(owners);
        }

        // GET: Owners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,OwnerName,Adress")] Owners owners)
        {
            if (ModelState.IsValid)
            {
                _context.Add(owners);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owners);
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var owner = await _context.Owners
            .Include(i => i.OwnedFoods).ThenInclude(i => i.Food)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (owner == null)
            {
                return NotFound();
            }
            PopulateOwnedFoodData(owner);
            return View(owner);

        }
        private void PopulateOwnedFoodData(Owners owners)
        {
            var allFood = _context.Food;
            var ownedFood = new HashSet<int>(owners.OwnedFoods.Select(c => c.FoodID));
            var viewModel = new List<OwnedFoodData>();
            foreach (var food in allFood)
            {
                viewModel.Add(new OwnedFoodData
                {
                    FoodID = food.ID,
                    Dish = food.Dish,
                    IsOwned = ownedFood.Contains(food.ID)
                });
            }
            ViewData["Food"] = viewModel;
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedFoods)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ownersToUpdate = await _context.Owners
            .Include(i => i.OwnedFoods)
            .ThenInclude(i => i.Food)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Owners>(
            ownersToUpdate,
            "",
            i => i.OwnerName, i => i.Adress))
            {
                UpdateOwnedFoods(selectedFoods, ownersToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateOwnedFoods(selectedFoods, ownersToUpdate);
            PopulateOwnedFoodData(ownersToUpdate);
            return View(ownersToUpdate);
        }
        private void UpdateOwnedFoods(string[] selectedFoods, Owners ownersToUpdate)
        {
            if (selectedFoods == null)
            {
                ownersToUpdate.OwnedFoods = new List<OwnedFood>();
                return;
            }
            var selectedFoodsHS = new HashSet<string>(selectedFoods);
            var ownedFoods = new HashSet<int>
            (ownersToUpdate.OwnedFoods.Select(c => c.Food.ID));
            foreach (var food in _context.Food)
            {
                if (selectedFoodsHS.Contains(food.ID.ToString()))
                {
                    if (!ownedFoods.Contains(food.ID))
                    {
                        ownersToUpdate.OwnedFoods.Add(new OwnedFood
                        {
                            OwnerID =
                       ownersToUpdate.ID,
                            FoodID = food.ID
                        });
                    }
                }
                else
                {
                    if (ownedFoods.Contains(food.ID))
                    {
                        OwnedFood foodToRemove = ownersToUpdate.OwnedFoods.FirstOrDefault(i
                       => i.FoodID == food.ID);
                        _context.Remove(foodToRemove);
                    }
                }
            }
        }
        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owners = await _context.Owners
                .FirstOrDefaultAsync(m => m.ID == id);
            if (owners == null)
            {
                return NotFound();
            }

            return View(owners);
        }

        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owners = await _context.Owners.FindAsync(id);
            _context.Owners.Remove(owners);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OwnersExists(int id)
        {
            return _context.Owners.Any(e => e.ID == id);
        }
    }
}
