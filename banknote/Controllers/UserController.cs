using banknote.Data;
using banknote.Models;
using banknote.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace banknote.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public UserController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public IActionResult AddPicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPicture(Picture picture, List<IFormFile> Image)
        {
            var userId = _userService.GetUserId();
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                var newUser = new User{ UserId = userId };
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                user = await _context.Users.FindAsync(userId);
            }

            //var newPicture = new Picture { };

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        picture.Image = stream.ToArray();
                    }
                }
            }


            if (user != null)
            {
                if (user.Pictures == null)
                {
                    user.Pictures = new List<Picture>();
                    user.Pictures.Add(picture);
                }
                else
                {
                    user.Pictures.Add(picture);
                }
            }

            if (user.Pictures != null)
            {
                //_context.Entry(person).State = EntityState.Added; // added row

                try
                {
                    //_context.Entry(user).State = EntityState.Modified;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }


            TempData["AlertMsg"] = "Image has been succesfully Uploaded";


            //return RedirectToAction(nameof(Index));
            return RedirectToAction("MyImages", "User");
        }


        public async Task<IActionResult> MyImages()
        {
            var userId = _userService.GetUserId();
            var user = await _context.Users.Include(r => r.Pictures).AsNoTracking().FirstOrDefaultAsync(m => m.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }



        [HttpGet]
        public async Task<IActionResult> EditImage(int id)
        {
            var image = await _context.Pictures.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }



        [HttpPost]
        public async Task<IActionResult> EditImage(Picture picture, List<IFormFile> Image)
        {
            var userId = _userService.GetUserId();
            picture.UserId = userId;

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        picture.Image = stream.ToArray();
                    }
                }
            }

            _context.Pictures.Update(picture);
            _context.SaveChanges();

            return RedirectToAction("MyImages", "User");

        }



        public ActionResult Details(int id)
        {

            Picture picture = _context.Pictures.Find(id);
            return View(picture);
        }




        public ActionResult DeleteImage(int id)
        {
            var userId = _userService.GetUserId();

            var picture = _context.Pictures.Where(p => p.UserId == userId).FirstOrDefault(x => x.Id == id);

            if (picture == null) { return NotFound();}

            _context.Pictures.Remove(picture);
            _context.SaveChanges();

            return RedirectToAction("MyImages", "User");
        }



        private bool UserExists(String id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
