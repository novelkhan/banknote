//using banknote.Controllers;
//using banknote.Data;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace banknote.Components
//{
//    public class PersonViewComponent:ViewComponent
//    {
//        private readonly ApplicationDbContext _context;

//        public PersonViewComponent(ApplicationDbContext context)
//        {
//            _context = context;
//        }
//        public async Task<IViewComponentResult> InvokeAsync(int? id)
//        {
//            var person = await _context.Person.FirstOrDefaultAsync(m => m.Id == id);
//            return View(person);
//        }
//    }
//}
