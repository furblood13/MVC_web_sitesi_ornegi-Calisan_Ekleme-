using CalisanBilgiler.DataErisimKatmani_DAL_;
using CalisanBilgiler.Models;
using CalisanBilgiler.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;

namespace CalisanBilgiler.Controllers
{
    public class CalisanController : Controller
    {
        private readonly CalisanDbContext _context;

        public CalisanController(CalisanDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var calisans=_context.Calisans.ToList();
			List<CalisanViewModel> calisanList = new List<CalisanViewModel>();
			if (calisans != null)
            {   
                
                foreach (var calisan in calisans)
                {
                    var CalisanViewModel=new CalisanViewModel()
                    {
                        Id=calisan.Id,
                        FirstName=calisan.FirstName,
                        LastName=calisan.LastName,
                        DateOfBirth=calisan.DateOfBirth,
                        Email=calisan.Email,
                        Salary=calisan.Salary,

                    };
					calisanList.Add(CalisanViewModel);
				}
				return View(calisanList);
			}
            return View(calisanList);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CalisanViewModel calisanData) {
            try
            {
                if (ModelState.IsValid)
                {
                    var calisan = new Calisan()
                    {
                        FirstName = calisanData.FirstName,
                        LastName = calisanData.LastName,
                        DateOfBirth = calisanData.DateOfBirth,
                        Email = calisanData.Email,
                        Salary = calisanData.Salary,
                    };
                    _context.Calisans.Add(calisan);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Calisan Basariyla Kaydedildi";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Data Dogru Degil";
                    return View();
                }
            }
            catch (Exception ex)
            {

				TempData["errorMessage"] = ex.Message;
				return View();
			}
            
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var calisan = _context.Calisans.SingleOrDefault(x => x.Id == Id);
                if (calisan != null)
                {
                    var calisanView = new CalisanViewModel()
                    {
                        Id = calisan.Id,
                        FirstName = calisan.FirstName,
                        LastName = calisan.LastName,
                        DateOfBirth = calisan.DateOfBirth,
                        Email = calisan.Email,
                        Salary = calisan.Salary,

                    };
                    return View(calisanView);
                }
                else
                {
                    TempData["errorMessage"] = $"Bu Id'li bir calisan yok: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
				TempData["errorMessage"] = ex.Message;
				return RedirectToAction("Index");
            }
          
        }
        [HttpPost]
        public IActionResult Edit(CalisanViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var calisan = new Calisan()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Salary = model.Salary,

                    };
                    _context.Calisans.Update(calisan);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Calisan Bilgileri Basariyla Guncellendi";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Bilgiler hatali";
                    return View();
                }
            }
            catch (Exception ex)
            {
				TempData["errorMessage"] = ex.Message;
				return View();

			}
         
        }

		[HttpGet]
		public IActionResult Delete(int Id)
		{
			try
			{
				var calisan = _context.Calisans.SingleOrDefault(x => x.Id == Id);
				if (calisan != null)
				{
					var calisanView = new CalisanViewModel()
					{
						Id = calisan.Id,
						FirstName = calisan.FirstName,
						LastName = calisan.LastName,
						DateOfBirth = calisan.DateOfBirth,
						Email = calisan.Email,
						Salary = calisan.Salary,

					};
					return View(calisanView);
				}
				else
				{
					TempData["errorMessage"] = $"Bu Id'li bir calisan yok: {Id}";
					return RedirectToAction("Index");
				}
			}
			catch (Exception ex)
			{
				TempData["errorMessage"] = ex.Message;
				return RedirectToAction("Index");
			}

		}
        [HttpPost]
        public IActionResult Delete(CalisanViewModel model)
        {
            try
            {
                var calisan = _context.Calisans.SingleOrDefault(x => x.Id == model.Id);
                if (calisan != null)
                {
                    _context.Calisans.Remove(calisan);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Calisan Basariyla Silindi";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Bu Id'li bir calisan yok: {model.Id}";
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
				TempData["errorMessage"] = ex.Message;
				return View();
			}
           
        }
	}
}
