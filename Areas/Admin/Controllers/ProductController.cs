using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Shop.Data;
using Shop.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static NuGet.Packaging.PackagingConstants;

namespace Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c => c.ProductTypes).ToList());
        }
        [HttpPost]
        public IActionResult Index(decimal lowAmount, decimal largeAmount)
        {
            var products =_db.Products.Include(c => c.ProductTypes)
                .Where(c=>c.Price >= lowAmount && c.Price<=largeAmount).ToList();

            return View(products);
        }

        public IActionResult Create()
        {
           
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,Name,Description , Price ,ProductColor ,ProductSize,ProductTypeId ")] Products book)
        {

            if (file != null)
            {
                string filename = file.FileName;
                //  string  ext = Path.GetExtension(file.FileName);
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                { await file.CopyToAsync(filestream); }

                book.Image = filename;
            }

            _db.Add(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }



        /*     [HttpPost]


             public async Task<IActionResult> Create(Products product)
             {
                 if (ModelState.IsValid)
                 {


                     if (product.Image != null)
                     {
                         *//* string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                          string uploadsFolder = Path.Combine(_he.WebRootPath, "Images/");
                          string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                          using (var fileStream = new FileStream(filePath, FileMode.Create))
                          {
                              await image.CopyToAsync(fileStream);
                          }*/


        /*string folder = "Images/";
        folder += products.Image.FileName + Guid.NewGuid().ToString();
        string name = Path.Combine(_he.WebRootPath, folder);
        await products.Image.CopyToAsync(new FileStream(name, FileMode.Create));*/

        /* var name = Path.Combine(_he.WebRootPath + "~/wwwroot/Images", Path.GetFileName(image.FileName));
         await image.CopyToAsync(new FileStream(name, FileMode.Create));
         products.Image = "Images/" + image.FileName;*//*

        string wwwRootPath = _he.WebRootPath;
        string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
        string extension = Path.GetExtension(product.ImageFile.FileName);
        string path = Path.Combine(wwwRootPath + "/images/", fileName);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await product.ImageFile.CopyToAsync(fileStream);
        }
    }
    if (product.Image == null) {

        product.Image = "/Images/product-6.png";



    }
    _db.Products.Add(product);
    await _db.SaveChangesAsync();
       TempData["save"] = "Product has been saved successfully!";
    return RedirectToAction(nameof(Index));
}


return View(product);
}
*/

        // same methode de create dans autre methode 




        /*  public async Task<IActionResult> Create(productviewsmodel product1)
          {
              if (ModelState.IsValid)
              {
                  String filename = "";
                  if (product1.photo != null)
                  {
                      *//*String uploadfolder = Path.Combine(_he.WebRootPath, "Images");
                      filename = Guid.NewGuid().ToString() + "_" + product1.photo.FileName;
                      String filepath = Path.Combine(uploadfolder, filename);
                      product1.photo.CopyTo(new FileStream(filepath, FileMode.Create));

                  }
                  Products p = new Products
                  {
                      Name = product1.Name,
                      Price = product1.Price,
                      Description = product1.Description,
                      Image = filename,
                      ProductColor = product1.ProductColor,
                      ProductSize = product1.ProductSize,
                      ProductTypeId = product1.ProductTypeId
                  };*//*

                      var name = Path.Combine(_he.WebRootPath + "Images", Guid.NewGuid().ToString() + "_" + product1.photo.FileName);

                      filename = Guid.NewGuid().ToString() + "_" + product1.photo.FileName;
                      String filepath = Path.Combine(Path.Combine(_he.WebRootPath, "Images") , filename);

                      await product1.photo.CopyToAsync(new FileStream(filepath, FileMode.Create));

                  }


                      _db.Products.Add(new Products
                      {
                          Name = product1.Name,
                          Price = product1.Price,
                          Description = product1.Description,
                          Image = filename,
                          ProductColor = product1.ProductColor,
                          ProductSize = product1.ProductSize,
                          ProductTypeId = product1.ProductTypeId
                      });
                  await _db.SaveChangesAsync();

                  return RedirectToAction(nameof(Index));
              }


              return View();
          }
  */


        // Get Action


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");

            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c=>c.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //Post action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "~/wwwroot/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "Images/" + image.FileName;


                }
                if (image == null)
                {

                    products.Image = "~/Images/product-6.png";



                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product has been saved successfully!";
                return RedirectToAction(nameof(Index));
            }


            return View(products);
        }









        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(Products products)
        {

            return RedirectToAction(nameof(Index));

        }



        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.ProductTypes).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //Post action

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Products products, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != products.Id)
            {
                return NotFound();
            }

            var product = _db.Products.FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
                TempData["save"] = "Warning: Product type has been deleted!";
                return RedirectToAction(nameof(Index));
            }
            return View(products);
        }
    }
}
