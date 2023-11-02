using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using X.PagedList;


namespace WebApplication1.Controllers
{
    public class ComponentsController : Controller
    {
        private readonly ApplicationDbContext _db;



        public ComponentsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Components PageList
        public async Task<IActionResult> Index(string? search = null, int page = 1, int pageSize = 2)
        {
            var result = (await _db.Components.AsNoTracking().ToPagedListAsync(page, pageSize));

            return View(result);
        }

        public async Task<IActionResult> IndexContent(string? search = null)
        {
            var result = _db.Components.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(p => p.PartName != null && p.PartName.Contains(search));
                ViewBag.Search = search;
            }

            return PartialView(result);
        }

        // GET: Components/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Components == null)
            {
                return NotFound();
            }

            var components = await _db.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (components == null)
            {
                return NotFound();
            }

            return View(components);
        }

        // GET: Components/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WP,PartName,Quantity,SurfaceFinishing,ReceiveDate,DeliveryDate")] Components components)
        {
            if (ModelState.IsValid)
            {
                _db.Add(components);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(components);
        }

        // GET: Components/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Components == null)
            {
                return NotFound();
            }

            var components = await _db.Components.FindAsync(id);
            if (components == null)
            {
                return NotFound();
            }
            return View(components);
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WP,PartName,Quantity,SurfaceFinishing,ReceiveDate,DeliveryDate")] Components components)
        {
            if (id != components.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(components);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComponentsExists(components.Id))
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
            return View(components);
        }

        // GET: Components/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Components == null)
            {
                return NotFound();
            }

            var components = await _db.Components
                .FirstOrDefaultAsync(m => m.Id == id);
            if (components == null)
            {
                return NotFound();
            }

            return View(components);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Components == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Components'  is null.");
            }
            var components = await _db.Components.FindAsync(id);
            if (components != null)
            {
                _db.Components.Remove(components);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentsExists(int id)
        {
            return (_db.Components?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
