using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAndProvider.Data;
using RestaurantAndProvider.Models;

namespace RestaurantAndProvider.Controllers
{
    [Authorize]
    public class RestaurantsController : Controller
    {
        private readonly RestaurantAndProviderContext _context;

        public RestaurantsController(RestaurantAndProviderContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageIndex = 1, int pageSize = 3)
        {
            var restaurants = from r in _context.Restaurant.Include(r => r.Provider)
                              select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(x => x.Name.Contains(searchString));
            }
            if (sortOrder == "desc")
            {
                restaurants = restaurants.OrderByDescending(x => x.Name);
            }
            else
            {
                restaurants = restaurants.OrderBy(x => x.Name);
            }
            var totalItems = await restaurants.CountAsync();
            var restaurantsInPage = await restaurants.Skip((pageIndex - 1) * pageSize).
                Take(pageSize).ToListAsync();
            var pageCount = (int) Math.Ceiling(totalItems / (double) pageSize);
            var viewModel = new RestaurantPageVM
            {
                Restaurants = restaurantsInPage,
                SearchString = searchString,
                SortOrder = sortOrder,
                PageIndex = pageIndex,
                PageCount = pageCount,
            };
            return View(viewModel);
        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ProviderId"] = new SelectList(_context.Provider, "ProviderId", "Name");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Ratings,ProviderId")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProviderId"] = new SelectList(_context.Provider, "ProviderId", "Name", restaurant.ProviderId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            ViewData["ProviderId"] = new SelectList(_context.Provider, "ProviderId", "Name", restaurant.ProviderId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Ratings,ProviderId")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProviderId"] = new SelectList(_context.Provider, "ProviderId", "Name", restaurant.ProviderId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .Include(r => r.Provider)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurant.Remove(restaurant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.Id == id);
        }
    }
}
