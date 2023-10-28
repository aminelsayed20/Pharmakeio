using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmakeio.Data;
using Pharmakeio.Models;

namespace Pharmakeio.Controllers
{
    public class MedicineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MedicineController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult GetCreateView()
        {
            ViewBag.AllDepartments = _context.PharmaceuticalDepartments.ToList();
            return View("Create");
        }
        [HttpGet]
        public ActionResult GetIndexView(int deptId, string? search, string sortType, string sortOrder, int pageSize = 20, int pageNumber = 1)
        {
            ViewBag.AllDepartments = _context.PharmaceuticalDepartments.ToList();
            ViewBag.Search = search;
            ViewBag.SelectedDeptId = deptId;

            var medicines = _context.Medicines.AsQueryable();

            
            if (deptId != 0)
            {
                medicines = medicines.Where(m => m.PharmaceuticalDepartmentId == deptId);
            }
            if (!string.IsNullOrEmpty(search)) 
            {
              medicines = medicines.Where(m=>m.Name.Contains(search));
            }
            // sortting
            if (sortType =="Name" && sortOrder == "asc")
            {
                medicines = medicines.OrderBy(m => m.Name);
            }
            else if (sortType == "Name" && sortOrder == "desc")
            {
                medicines = medicines.OrderByDescending(m => m.Name);
            }
            else if (sortType == "ExpiryDate" &&  sortOrder == "asc")
            {
                medicines = medicines.OrderBy(m => m.ExpiryDate);
            }
            else if (sortType == "ExpiryDate" && sortOrder == "desc")
            {
                medicines = medicines.OrderByDescending(m => m.ExpiryDate);
            }
            if (pageSize > 50) pageSize = 50;
            if (pageSize < 1) pageSize = 1;
            if(pageNumber < 1) pageNumber = 1;

            medicines = medicines.Skip(pageSize * (pageNumber-1)).Take(pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;



            return View("Index", medicines);
        }

        [HttpGet]
        public ActionResult GetDetailsView(int id) 
        {
            var med = _context.Medicines.Include(m=>m.PharmaceuticalDepartment).FirstOrDefault(m=>m.Id == id);
            ViewBag.Medicine = med;
            if (med == null)
            {
                return NotFound();

            }
            else 
            return View("Details", med);
        }
        [HttpGet]
        public ActionResult GetDeleteView(int id) 
        {
            var med = _context.Medicines.Include(m => m.PharmaceuticalDepartment).FirstOrDefault(m => m.Id == id);
            ViewBag.Medicine = med;
            if (med == null) return NotFound();
            else 
                return View("Delete", med);
        }
        [HttpGet]
        public ActionResult GetEditView(int id)
        {
            Medicine med = _context.Medicines.Find(id);
            if (med == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllDepartments = _context.PharmaceuticalDepartments.ToList();
                return View("Edit", med);
            }
        }




       
         //////////////////////// Http Post
        
        [HttpPost]
            public ActionResult AddNew(Medicine med, IFormFile? imageFormFile) 
        {
            med.CreatedAt = DateTime.Now;
            med.ExpiryDate = med.ProductionDate.AddMonths(med.ShelfLife);


            if (ModelState.IsValid == true)
            {
                if (imageFormFile==null)
            {
                med.ImagePath = "\\images\\No_Image_Available.png";
            }
            else
            {
                Guid guid = Guid.NewGuid();
                string imgExtension = Path.GetExtension(imageFormFile.FileName);
                string imgName = guid + imgExtension;
                med.ImagePath = $"\\images\\medicine\\{imgName}";
                string imgFullPath = _webHostEnvironment.WebRootPath + med.ImagePath;

                FileStream fs = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(fs);
                fs.Dispose();
            }

            // to increase the counter of department
            PharmaceuticalDepartment de = _context.PharmaceuticalDepartments.Find(med.PharmaceuticalDepartmentId) as PharmaceuticalDepartment;
            de.NumberOfProducts++;



                _context.Medicines.Add(med);// عشان اضيف حد جديد في جدول الموظفين الخاص ب الكونتكست
                _context.PharmaceuticalDepartments.Update(de);
                _context.SaveChanges(); // عشان احفظ التغيرات اللي حصلت في الكونتكست

                return Redirect("GetIndexView");
            }
            else
            {
                ViewBag.AllDepartments = _context.PharmaceuticalDepartments.ToList();

                return View("Create", med);
            }
        }

        [HttpPost]
        public ActionResult EditMedicine (Medicine med, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid)
            {
                med.ExpiryDate = med.ProductionDate.AddMonths(med.ShelfLife);
                med.LastUpdate = DateTime.Now;

                if (imageFormFile != null)
                {
                    if (med.ImagePath != "\\images\\No_Image_Available.png")
                    {
                        string imgPath = _webHostEnvironment.WebRootPath + med.ImagePath;
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }

                    Guid imgGuid = Guid.NewGuid(); // sdf54-xym9t-71miw-kjk99-nb12k
                    string imgExtension = Path.GetExtension(imageFormFile.FileName); // .png
                    string imgName = imgGuid + imgExtension; // sdf54-xym9t-71miw-kjk99-nb12k.png
                    med.ImagePath = "\\images\\medicine\\" + imgName;

                    string imgFullPath = _webHostEnvironment.WebRootPath + med.ImagePath;

                    FileStream fileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(fileStream);
                    fileStream.Dispose();
                }

                _context.Medicines.Update(med);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllDepartments = _context.PharmaceuticalDepartments.ToList();
                return View("Edit", med);
            }


        }

        [HttpPost]
        public ActionResult DeleteMedicine (int id)
        {
            var med = _context.Medicines.Find(id);
            if (med.ImagePath != "\\images\\No_Image_Available.png")
            {
                string imgPath = _webHostEnvironment.WebRootPath + med.ImagePath;
                
                if (System.IO.File.Exists(imgPath))
                    System.IO.File.Delete(imgPath);
            }
            _context.Medicines.Remove(med);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");
        }



    }
}
