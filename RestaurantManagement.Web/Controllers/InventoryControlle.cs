using EventManagement.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Web.Controllers
{
    [Authorize(Roles = "chef")]
    public class InventoryController(InventoryRepo inventoryRepo,UserInfoRepo menuItemsRepo, CurrentUserHelper currentUserHelper) : Controller
    {
        public IActionResult Index()
        {
            var result = inventoryRepo.GetAll();
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("Index", "Home");
            }

            return View(result.Data);
           
        }


        public IActionResult Detail()
        {
            var result = inventoryRepo.GetAll();

            if (result.HasError)
            {
                ViewBag.Error = result.Message;

                return View(new Inventory());
            }

            ViewBag.ProductList = result.Data.Select(e => new SelectListItem
            {
                Text = e.ProductName,
                Value = e.ID.ToString()
            }).ToList();

            //var inv = new Inventory();

            return View(new Inventory());
        }

        [HttpPost]
        public IActionResult Detail(Inventory model)
        {
            ModelState.Remove("ProductName");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("UpdatedBy");
            ModelState.Remove("Unit");

            var productResult = inventoryRepo.GetAll();
            ViewBag.ProductList = productResult.Data.Select(e => new SelectListItem
            {
                Text = e.ProductName,
                Value = e.ID.ToString()
            }).ToList();

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var result = inventoryRepo.Save(model);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }

            TempData["Success"] = "saved successfully.";
            return RedirectToAction("Detail");
        }
    }



}

