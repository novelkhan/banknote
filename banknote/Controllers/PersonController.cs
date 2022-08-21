using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using banknote.Data;
using banknote.Models;

namespace banknote.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
              return _context.Person != null ? 
                          View(await _context.Person.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Person'  is null.");
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            //var person = await _context.Person.FirstOrDefaultAsync(m => m.Id == id);            
            var person = await _context.Person.Include(r => r.ResearvedNotes).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }






        //// GET: Person/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Person == null)
        //    {
        //        return NotFound();
        //    }

        //    var person = await _context.Person
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (person == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(person);
        //}

        //// POST: Person/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Person == null)
        //    {
        //        return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
        //    }
        //    //var person = await _context.Person.FindAsync(id);
        //    var person = await _context.Person.Include(r => r.ResearvedNotes).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        //    if (person != null)
        //    {
        //        _context.Person.Remove(person);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}







        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }

            var person = await _context.Person.Include(r => r.ResearvedNotes).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (person != null)
            {
                if (person.ResearvedNotes != null)
                {
                    foreach (var note in person.ResearvedNotes)
                    {
                        DeleteNoteConfirm(note.Id, note.NoteId);
                    }

                    person.ResearvedNotes = null;
                }

                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }








        //------------------------------------Custom Code-----------------------------------------



        // GET: Note/Add
        public IActionResult Add(int Id)
        {
            return View();
        }




        // POST: Note/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int Id, [Bind("NoteId,NoteName,NoteValue,Id")] Note note)
        {
            if (ModelState.IsValid)
            {
                var person = await _context.Person.FindAsync(Id);
                //person.notes.Add(note);
                if (person != null)
                {
                    if (person.ResearvedNotes == null)
                    {
                        person.ResearvedNotes = new List<Note>();
                        person.ResearvedNotes.Add(note);
                    }
                    else
                    {
                        person.ResearvedNotes.Add(note);
                    }
                }

                if (person.ResearvedNotes != null)
                {
                    //_context.Entry(person).State = EntityState.Added; // added row

                    try
                    {
                        _context.Entry(person).State = EntityState.Modified;
                        _context.Person.Update(person);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonExists(person.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }


                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Person", new { Id });
            }
            return View(note);
        }















        public async Task<IActionResult> DeleteNote(int Id, int NoteId)
        {
            if (NoteId == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.Where(i => i.Id == Id).FirstOrDefaultAsync(m => m.NoteId == NoteId);
            
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("DeleteNote")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNoteConfirmed(int Id, int NoteId)
        {
            if (_context.Note == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Note'  is null.");
            }

            var note = await _context.Note.Where(i => i.Id == Id).FirstOrDefaultAsync(m => m.NoteId == NoteId);
            if (note != null)
            {
                _context.Note.Remove(note);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Person", new { Id });
        }














        // GET: Note/Edit/5
        public async Task<IActionResult> EditNote(int Id, int NoteId)
        {
            if (NoteId == null || _context.Note == null)
            {
                return NotFound();
            }

            //var note = await _context.Note.FindAsync(NoteId);
            var note = await _context.Note.Where(i => i.Id == Id).FirstOrDefaultAsync(m => m.NoteId == NoteId);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNote(int Id, int NoteId ,[Bind("NoteId,NoteName,NoteValue,Id")] Note note)
        {
            if (NoteId != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Person", new { Id });
            }
            return View(note);
        }













        // GET: Note/Details/5
        public async Task<IActionResult> NoteDetails(int Id, int NoteId)
        {
            if (NoteId == null || _context.Note == null)
            {
                return NotFound();
            }

            //var note = await _context.Note.FirstOrDefaultAsync(m => m.Id == id);
            var note = await _context.Note.Where(i => i.Id == Id).FirstOrDefaultAsync(m => m.NoteId == NoteId);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }















        public async void DeleteNoteConfirm(int Id, int NoteId)
        {
            var not = await _context.Note.Where(i => i.Id == Id).FirstOrDefaultAsync(m => m.NoteId == NoteId);
            if (not != null)
            {
                _context.Note.Remove(not);
            }
        }



        //------------------------------------Custom Code-----------------------------------------




        private bool PersonExists(int id)
        {
          return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        private bool NoteExists(int NoteId)
        {
            return (_context.Note?.Any(e => e.NoteId == NoteId)).GetValueOrDefault();
        }
    }
}
