using System.Dynamic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TutorialService.Data;
using TutorialService.Models;

namespace TutorialService.Controllers
{
    public class ExamplesController : Controller
    {
        private readonly TutorialServiceContext _context;
        public ExamplesController(TutorialServiceContext context)
        {
            _context = context;
        }

        // GET: Examples

        public async Task<IActionResult> Index(string exampleCreator, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> creatorQuery = from ex in _context.Example
                                            orderby ex.Creator
                                            select ex.Creator;

            var examples = from ex in _context.Example
                         select ex;

            if (!String.IsNullOrEmpty(searchString))
            {
                examples = examples.Where(s => s.Name!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(exampleCreator))
            {
                examples = examples.Where(x => x.Creator == exampleCreator);
            }

            var exampleCreatorVM = new ExampleCreatorViewModel
            {
                Creators = new SelectList(await creatorQuery.Distinct().ToListAsync()),
                Examples = await examples.ToListAsync()
            };

            return View(exampleCreatorVM);
        }

        // GET: Examples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Example == null)
            {
                return NotFound();
            }

            var example = await _context.Example
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Group)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (example == null)
            {
                return NotFound();
            }

            return View(example);
        }

        // GET: Examples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Examples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Example example)
        {
            if (ModelState.IsValid)
            {
                _context.Add(example);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Unable to save changes. " +
                       "Model state invalid.");
            ModelState.AddModelError("", example.Name);
            return View(example);
        }

        // GET: Examples/AddEnrollment/5
        public async Task<IActionResult> AddEnrollment(int id)
        {
            if (_context.Example == null || _context.Enrollment == null)
            {
                return NotFound();
            }

            ViewModel mymodel = new()
            {
                Id = id,
                Enrollment = await _context.Enrollment
                .FirstOrDefaultAsync(en => en.ExampleID == id)
            };

            if (mymodel == null)
            {
                return NotFound();
            }

            return View(mymodel);
        }

        // POST: Examples/AddEnrollment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEnrollment(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Unable to save changes. " +
                       "Model state invalid.");
            ModelState.AddModelError("", enrollment.Group.Name);
            return View(enrollment);
        }

        // GET: Examples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Example == null)
            {
                return NotFound();
            }

            var example = await _context.Example
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Group)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (example == null)
            {
                return NotFound();
            }
            return View(example);
        }

        // POST: Examples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Example example)
        {
            if (id != example.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(example);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                    if (!ExampleExists(example.Id))
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
            ModelState.AddModelError("", "Unable to save changes. " +
                       "Model state invalid. ");
            ModelState.AddModelError("", example.Name);
            return View(example);
        }

        // GET: Examples/EditEnrollment/5
        public async Task<IActionResult> EditEnrollment(int? id)
        {
            if (id == null || _context.Enrollment == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);

            if (enrollment == null)
            {
                return NotFound();
            }
            return View(enrollment);
        }

        // POST: Examples/EditEnrollment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEnrollment(int id, Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                    if (!ExampleExists(enrollment.EnrollmentID))
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
            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            return View(enrollment);
        }

        // GET: Examples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Example == null)
            {
                return NotFound();
            }

            var example = await _context.Example
                .FirstOrDefaultAsync(m => m.Id == id);
            if (example == null)
            {
                return NotFound();
            }

            return View(example);
        }

        // POST: Examples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Example == null)
            {
                return Problem("Entity set 'TutorialServiceContext.Example'  is null.");
            }
            var example = await _context.Example.FindAsync(id);
            if (example != null)
            {
                _context.Example.Remove(example);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Examples/RemoveEnrollment/5
        public async Task<IActionResult> RemoveEnrollment(int? id)
        {
            if (id == null || _context.Enrollment == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(g => g.Group)
                .Include(e => e.Example)
                    .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Examples/RemoveEnrollment/5
        [HttpPost, ActionName("RemoveEnrollment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveEnrollmentConfirmed(int id)
        {
            if (_context.Enrollment == null)
            {
                return Problem("Entity set 'TutorialServiceContext.Example'  is null.");
            }
            var enrollment = await _context.Enrollment.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollment.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExampleExists(int id)
        {
          return (_context.Example?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
