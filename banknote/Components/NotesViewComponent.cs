//using banknote.Controllers;
//using banknote.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace banknote.Components
//{
//    public class NotesViewComponent : ViewComponent
//    {
//        private readonly ApplicationDbContext _context;

//        public NotesViewComponent(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<IViewComponentResult> InvokeAsync(int? id)
//        {
//            int i = 0;
//            var person = await _context.Person.Include(r => r.ResearvedNotes).FirstOrDefaultAsync(m => m.Id == id);
//            foreach (var note in person.ResearvedNotes) { i++; }
//            ViewBag.AvailableNotes = i;
//            return View();
//        }
//    }
//}
