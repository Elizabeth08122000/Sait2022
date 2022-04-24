using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;

namespace Sait2022.Controllers
{
    public class RangsController : Controller
    {
        private readonly SaitDbContext _context;

        public RangsController(SaitDbContext context)
        {
            _context = context;
        }

        // GET: Rangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rangs.ToListAsync());
        }

        // GET: Rangs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rangs =  _context.Rangs
                .FirstOrDefault(m => m.Id == id);
            if (rangs == null)
            {
                return NotFound();
            }

            return View(rangs);
        }

        // GET: Rangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RangQuest,Id")] Rangs rangs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rangs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rangs);
        }

        // GET: Rangs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rangs = await _context.Rangs.FindAsync(id);
            if (rangs == null)
            {
                return NotFound();
            }
            return View(rangs);
        }

        // POST: Rangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("RangQuest,Id")] Rangs rangs)
        {
            if (id != rangs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rangs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RangsExists(rangs.Id))
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
            return View(rangs);
        }

        // GET: Rangs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rangs = await _context.Rangs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rangs == null)
            {
                return NotFound();
            }

            return View(rangs);
        }

        // POST: Rangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var rangs = await _context.Rangs.FindAsync(id);
            _context.Rangs.Remove(rangs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RangsExists(long id)
        {
            return _context.Rangs.Any(e => e.Id == id);
        }
    }
}
