using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.ViewModels.Page;
using Sait2022.ViewModels.Question;

namespace Sait2022.Controllers
{
    public class QuestionController : Controller
    {
        private readonly SaitDbContext _context;

        IWebHostEnvironment _appEnvironment;
        public QuestionController(SaitDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Question
        public async Task<IActionResult> Index(string searchString, string currentFilter, int page=1)
        {
            int pageSize = 10;   // количество элементов на странице

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var saitDbContext = _context.Questions.Include(q => q.QuestionsTopic).Include(q => q.Rangs).OrderBy(x => x.Id);

            if (!String.IsNullOrEmpty(searchString))
            {
                if(long.TryParse(searchString, out var number))
                {
                    saitDbContext = (IOrderedQueryable<Questions>)saitDbContext.Where(s => s.Id.Equals(long.Parse(searchString)));
                }
                else
                {
                    saitDbContext = (IOrderedQueryable<Questions>)saitDbContext.Where(s => s.ValueQuest.Contains(searchString));
                }
                
            }

            var count = await saitDbContext.CountAsync();
            var items = await saitDbContext.Skip((int)((page - 1) * pageSize)).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            QuestionsViewModel viewModel = new QuestionsViewModel
            {
                PageViewModel = pageViewModel,
                Questions = items
            };

            return View(viewModel);
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
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "RangQuest");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionTopcId,RangsId,AnswerId,Name,Path,NamePict,PathPict,NumberQuest,ValueAnswer,ValueQuest,Id")] Questions questions, [FromForm] IFormFile uploadedFile, [FromForm] IFormFile uploadedFile2)
        {
            if (ModelState.IsValid)
            {
                var quest = _context.Questions.OrderBy(x => x.Id).Where(x => x.QuestionTopcId == questions.QuestionTopcId & x.RangsId == questions.RangsId).LastOrDefault();
                if (quest == null)
                {
                    questions.NumberQuest = 1;
                }
                else
                {
                    if (quest.QuestionTopcId.Equals(questions.QuestionTopcId) & quest.RangsId.Equals(questions.RangsId))
                    {
                        questions.NumberQuest = quest.NumberQuest + 1;
                    }
                    else
                    {
                        questions.NumberQuest = 1;
                    }

                }
                if (uploadedFile != null)
                {
                    // путь к папке Files
                    string path = "/Files/" + uploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    questions.Name = uploadedFile.FileName;
                    questions.Path = path;
                }
                if (uploadedFile2 != null)
                {
                    // путь к папке Pictures
                    string path2 = "/Pictures/" + uploadedFile2.FileName;
                    // сохраняем файл в папку Pictures в каталоге wwwroot
                    using (var fileStream2 = new FileStream(_appEnvironment.WebRootPath + path2, FileMode.Create))
                    {
                        await uploadedFile2.CopyToAsync(fileStream2);
                    }
                    questions.NamePict = uploadedFile2.FileName;
                    questions.PathPict = path2;
                }

                _context.Add(questions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionTopcId"] = new SelectList(_context.QuestionsTopics, "Id", "Topic", questions.QuestionTopcId);
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "RangQuest", questions.RangsId);
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
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "RangQuest", questions.RangsId);
            return View(questions);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("QuestionTopcId,RangsId,AnswerId,Name,Path,NamePict,PathPict,NumberQuest,ValueAnswer,ValueQuest,Id")] Questions questions, [FromForm] IFormFile uploadedFile, [FromForm] IFormFile uploadedFile2)
        {
            if (id != questions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var filesLast = _context.Questions.OrderBy(x => x.Id).Where(x => x.Id == questions.Id).AsNoTracking().LastOrDefault();
                                        
                    if (uploadedFile != null)
                    {
                        // путь к папке Files
                        string path = "/Files/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        questions.Name = uploadedFile.FileName;
                        questions.Path = path;
                    }
                    else
                    {
                        questions.Path = filesLast.Path;
                        questions.Name = filesLast.Name;
                    }
                    if (uploadedFile2 != null)
                    {
                        // путь к папке Pictures
                        string path2 = "/Pictures/" + uploadedFile2.FileName;
                        // сохраняем файл в папку Pictures в каталоге wwwroot
                        using (var fileStream2 = new FileStream(_appEnvironment.WebRootPath + path2, FileMode.Create))
                        {
                            await uploadedFile2.CopyToAsync(fileStream2);
                        }
                        questions.NamePict = uploadedFile2.FileName;
                        questions.PathPict = path2;
                    }
                    else
                    {
                        questions.PathPict = filesLast.PathPict;
                        questions.NamePict = filesLast.NamePict;
                    }
                    var quest = _context.Questions.OrderBy(x => x.Id).Where(x => x.Id == questions.Id & x.RangsId == questions.RangsId).AsNoTracking().LastOrDefault();
                    var questR = _context.Questions.OrderBy(x => x.Id).Where(x => x.RangsId == questions.RangsId & x.QuestionTopcId == questions.QuestionTopcId).AsNoTracking().LastOrDefault();

                    if (questR == null)
                    {
                        questions.NumberQuest = 0;
                    }
                    if (quest == null)
                    {
                        questions.NumberQuest = 1;
                    }
                    else
                    {
                        if (!quest.RangsId.Equals(questions.RangsId))
                        {
                            questions.NumberQuest = questR.NumberQuest + 1;
                        }
                        if (!quest.QuestionTopcId.Equals(questions.QuestionTopcId))
                        {
                            questions.NumberQuest = quest.NumberQuest + 1;
                        }
                        else
                        {
                            questions.NumberQuest = quest.NumberQuest;
                        }
                    }

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
            ViewData["RangsId"] = new SelectList(_context.Rangs, "Id", "RangQuest", questions.RangsId);
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
