using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Pharmakeio.Data;
using Pharmakeio.Models;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Pharmakeio.Controllers
{
    public class PharmaceuticalDepartmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PharmaceuticalDepartmentController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult GetCreateView()
        {
            return View("Create");
        }
        [HttpGet]
        public ActionResult GetIndexView(string ?search)
        {
            var departs = _context.PharmaceuticalDepartments.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                departs = _context.PharmaceuticalDepartments.Where(d=>d.Name.Contains(search));
            ViewBag.Search = search;
            return View("Index", departs);
        }
        [HttpGet]
        public ActionResult GetDetailsView(int id)
        {
            var dept = _context.PharmaceuticalDepartments.Find(id);
            ViewBag.PharmaceuticalDepartment = dept;
            if (dept == null)
            {
                return NotFound();
            }
            else 

            return View("Details", dept);
        }
        [HttpGet]
        public ActionResult GetDeleteView(int id)
        {
            var dept = _context.PharmaceuticalDepartments.Find(id);
            ViewBag.PharmaceuticalDepartment = dept;
            if (dept == null)
            {
                return NotFound();
            }
            else

                return View("Delete", dept);
        }
        [HttpGet]
        public ActionResult GetEditView(int id)
        {
            var dept = _context.PharmaceuticalDepartments.Find(id);
            ViewBag.PharmDepart = dept;
            if (dept == null)
            {
                return NotFound();
            }
            else
                return View("Edit", dept);
        }















        [HttpPost]
        public ActionResult AddNew(PharmaceuticalDepartment depart)
        {
            depart.CreatedAt = DateTime.Now;
            depart.NumberOfProducts = 0;

            if (ModelState.IsValid == true)
            {
                _context.PharmaceuticalDepartments.Add(depart);// عشان اضيف حد جديد في جدول الموظفين الخاص ب الكونتكست
                _context.SaveChanges(); // عشان احفظ التغيرات اللي حصلت في الكونتكست

                return Redirect("GetIndexView");
            }
            else
            {
                // ViewBag.AllDepartments = _context.Departments.ToList();

                return View("Create", depart);
            }
        }

        [HttpPost]
        public ActionResult DeletePharmDept(int id)
        {
            var dept = _context.PharmaceuticalDepartments.Find(id);

            _context.PharmaceuticalDepartments.Remove(dept);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");

        }
        [HttpPost]
        public ActionResult EditPharmDept(PharmaceuticalDepartment dept)
        {
            _context.PharmaceuticalDepartments.Update(dept);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");
        }


    }
}
