using CRUD_Code_First_Approach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CRUD_Code_First_Approach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StudentDBContext studentDb;

        public HomeController(ILogger<HomeController> logger, StudentDBContext studentDb)
        {
            _logger = logger;
            this.studentDb = studentDb;
        }

        //private readonly StudentDBContext studentDb;
        //public HomeController(StudentDBContext studentDb)
        //{
        //    this.studentDb = studentDb;

        //}
        public async Task<IActionResult> Index()
        {
            var data = await studentDb.Students.ToListAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDb.Students.AddAsync(std);
                await studentDb.SaveChangesAsync();
                TempData["insert_success"] = "Inserted Successfully......";
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if(id == null || studentDb.Students == null)
            {
                return NotFound();
            }
            var data = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }
            var data = await studentDb.Students.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                studentDb.Students.Update(std);
                await studentDb.SaveChangesAsync();
                TempData["update_success"] = "Updateded Successfully......";
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDb.Students == null)
            {
                return NotFound();
            }
            var data = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var data = await studentDb.Students.FindAsync(id);

            if (data != null)
            {
                studentDb.Students.Remove(data);
            }
            await studentDb.SaveChangesAsync();
            TempData["delete_success"] = "Deleteded Successfully......";
            return RedirectToAction("Index");

        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
