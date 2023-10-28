using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmakeio.Data;
using Pharmakeio.Models;

namespace Pharmakeio.Controllers
{
    public class PharmacistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PharmacistController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult GetCreateView()
        {
            ViewBag.AllPharmacists = _context.Pharmacists.ToList();
            return View("Create");
        }


        [HttpGet]
        public ActionResult GetIndexView(string? search)
        {
            var Pharmacists = _context.Pharmacists.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            Pharmacists = _context.Pharmacists.Where(p => p.FullName.Contains(search));
            ViewBag.Search = search;
            return View("Index", Pharmacists);
        }
        [HttpGet]
        public ActionResult GetDetailsView(int id)
        {
            var pharm = _context.Pharmacists.Find(id);
            ViewBag.Pharmacist = pharm;
            if (pharm == null)
            {
                return NotFound();
            }
            else 
            return View("Details", pharm);
        }
        [HttpGet]
        public ActionResult GetDeleteView(int id)
        {
            var pharm = _context.Pharmacists.Find(id);
            ViewBag.Pharmacist = pharm;
            if (pharm == null)
            {
                return NotFound();
            }
            else
                return View("Delete", pharm);
        }
        [HttpGet]
        public ActionResult GetEditView(int id)
        {
            var pharm = _context.Pharmacists.Find(id);

            if (pharm == null)
                return NotFound();

            else
            return View("Edit", pharm);
        }













        [HttpPost]
        public ActionResult AddNew(Pharmacist pharm, IFormFile? imageFormFile)
        {

            if (ModelState.IsValid == true)
            {
                if (imageFormFile == null)
                {
                    pharm.ImagePath = "\\images\\No_Image_Available.png";
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    string imgName = guid + imgExtension;
                    pharm.ImagePath = $"\\images\\pharmacist\\{imgName}";
                    string imgFullPath = _webHostEnvironment.WebRootPath + pharm.ImagePath;

                    FileStream fs = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(fs);
                    fs.Dispose();
                }

                _context.Pharmacists.Add(pharm);// عشان اضيف حد جديد في جدول الموظفين الخاص ب الكونتكست
                _context.SaveChanges(); // عشان احفظ التغيرات اللي حصلت في الكونتكست

                return Redirect("GetIndexView");
            }
            else
            {
                ViewBag.AllPharmacists = _context.Pharmacists.ToList();

                return View("Create", pharm);
            }
        }

        [HttpPost]
        public ActionResult DeletePharmacist(int id) 
        {
            var pharm = _context.Pharmacists.Find(id);
            if (pharm.ImagePath != "\\images\\No_Image_Available.png")
            {
                string imgPath = _webHostEnvironment.WebRootPath + pharm.ImagePath;

                if (System.IO.File.Exists(imgPath))
                    System.IO.File.Delete(imgPath);
            }
            _context.Pharmacists.Remove(pharm);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }






        public ActionResult EditPharmacist(Pharmacist pharm, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFormFile != null)
                {
                    if (pharm.ImagePath != "\\images\\No_Image_Available.png")
                    {
                        string imgPath = _webHostEnvironment.WebRootPath + pharm.ImagePath;
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }

                    Guid imgGuid = Guid.NewGuid(); // sdf54-xym9t-71miw-kjk99-nb12k
                    string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                    string imgName = imgGuid + imgExtension; // sdf54-xym9t-71miw-kjk99-nb12k.png
                    pharm.ImagePath = "\\images\\medicine\\" + imgName;

                    string imgFullPath = _webHostEnvironment.WebRootPath + pharm.ImagePath;

                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }

                _context.Pharmacists.Update(pharm);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit", pharm);
            }


        }



    }
}
