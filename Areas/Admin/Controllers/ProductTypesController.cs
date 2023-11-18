using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Threading.Tasks;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           
            var data = _db.ProductTypes.ToList();
           // var data = await _db.ProductTypes.ToListAsync();
            return View(data);
        }




          public IActionResult Create()
         {

             return View();
         }


       
        [HttpPost]
        [ValidateAntiForgeryToken]
         
          public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }



        // Get Action
        public IActionResult Edit(int? id )
        { 
        
            if ( id == null )
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if ( productType == null )
            {
                return NotFound();
            }

            return View(productType);
        }


        //Post action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type has been updated successfully!";
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(productTypes);
        }


      



       


        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        } 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
           
             return RedirectToAction(nameof(Index));
         
        }



        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Post action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductTypes productTypes, int? id)
        {
            if (id == null) {
                return NotFound();
            }

            if (id != productTypes.Id)
            {
                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);

            if(productType == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                TempData["save"] = "Warning: Product type has been deleted!";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }



    }
}
