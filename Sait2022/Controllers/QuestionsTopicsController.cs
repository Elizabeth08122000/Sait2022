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
    public class QuestionsTopicsController : Controller
    {
        private readonly SaitDbContext _context;

        public QuestionsTopicsController(SaitDbContext context)
        {
            _context = context;
        }

        // GET: QuestionsTopics
        public async Task<IActionResult> Index()
        {
            return View(await _context.QuestionsTopics.OrderBy(x => x.Id).ToListAsync());
        }

        // GET: QuestionsTopics/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsTopic = await _context.QuestionsTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionsTopic == null)
            {
                return NotFound();
            }

            return View(questionsTopic);
        }

        // GET: QuestionsTopics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionsTopics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Topic,IsUsedNow,Id")] QuestionsTopic questionsTopic)
        {
            if (ModelState.IsValid)
            {
                questionsTopic.IsUsedNow = false;
                _context.Add(questionsTopic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionsTopic);
        }

        // GET: QuestionsTopics/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsTopic = await _context.QuestionsTopics.FindAsync(id);
            if (questionsTopic == null)
            {
                return NotFound();
            }
            return View(questionsTopic);
        }

        // POST: QuestionsTopics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Topic,IsUsedNow,Id")] QuestionsTopic questionsTopic)
        {
            if (id != questionsTopic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    questionsTopic.IsUsedNow = false;
                    _context.Update(questionsTopic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsTopicExists(questionsTopic.Id))
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
            return View(questionsTopic);
        }

        // GET: QuestionsTopics/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questionsTopic = await _context.QuestionsTopics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionsTopic == null)
            {
                return NotFound();
            }

            return View(questionsTopic);
        }

        // POST: QuestionsTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var questionsTopic = await _context.QuestionsTopics.FindAsync(id);
            _context.QuestionsTopics.Remove(questionsTopic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsTopicExists(long id)
        {
            return _context.QuestionsTopics.Any(e => e.Id == id);
        }
    }
}
