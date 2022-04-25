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
    public class QuestionController : Controller
    {
        private readonly SaitDbContext _context;

        public QuestionController(SaitDbContext context)
        {
            _context = context;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            var saitDbContext = _context.Questions.Include(q => q.QuestionsTopic).Include(q => q.Rangs);
            return View(await saitDbContext.ToListAsync());
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.QuestionsTopic)
                .Include(q => q.Rangs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            ViewData["QuestionTopcId"] = new SelectList(_context.QuestionsTopics, "Id", "Topic");
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "Id");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionTopcId,RangsId,NumberQuest,ValueQuest,Id")] Questions questions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionTopcId"] = new SelectList(_context.QuestionsTopics, "Id", "Topic", questions.QuestionTopcId);
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "Id", questions.RangsId);
            return View(questions);
        }

        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }
            ViewData["QuestionTopcId"] = new SelectList(_context.QuestionsTopics, "Id", "Topic", questions.QuestionTopcId);
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "Id", questions.RangsId);
            return View(questions);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("QuestionTopcId,RangsId,NumberQuest,ValueQuest,Id")] Questions questions)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questions.Id))
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
            ViewData["QuestionTopcId"] = new SelectList(_context.QuestionsTopics, "Id", "Topic", questions.QuestionTopcId);
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "Id", questions.RangsId);
            return View(questions);
        }

        // GET: Question/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _context.Questions
                .Include(q => q.QuestionsTopic)
                .Include(q => q.Rangs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questions == null)
            {
                return NotFound();
            }

            return View(questions);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var questions = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionsExists(long id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
