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
    public class ProvidersController : Controller
    {
        private readonly RestaurantAndProviderContext _context;

        public ProvidersController(RestaurantAndProviderContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index(string searchString, string sortOrder, int pageIndex = 1, int pageSize = 3)
        {
            var providers = from p in _context.Provider
                            select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                providers = providers.Where(x => x.Name.Contains(searchString));
            }
            if (sortOrder == "desc")
            {
                providers = providers.OrderByDescending(x => x.Name);
            }
            else
            {
                providers = providers.OrderBy(x => x.Name);
            }
            var totalItems = await providers.CountAsync();
            var providersInPage = await providers.Skip((pageIndex - 1) * pageSize).
                Take(pageSize).ToListAsync();
            var pageCount = (int)Math.Ceiling(totalItems / (double)pageSize);
            var viewModel = new ProviderPageVM
            {
                Providers = providersInPage,
                SearchString = searchString,
                SortOrder = sortOrder,
                PageIndex = pageIndex,
                PageCount = pageCount,
            };
            return View(viewModel);
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderId,Name,Location,Phone")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Providers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProviderId,Name,Location,Phone")] Provider provider)
        {
            if (id != provider.ProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.ProviderId))
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
            return View(provider);
        }

        // GET: Providers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            if (provider != null)
            {
                _context.Provider.Remove(provider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderId == id);
        }
    }
}
