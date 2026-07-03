using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Entities;
using RestaurantManagement.Repos;
using RestaurantManagement.Shared;

namespace RestaurantManagement.Web.Controllers
{
    [Authorize(Roles = "admin,chef")]
    public class KitchenTaskController(KitchenTaskRepo kitchenTaskRepo,UserInfoRepo menuItemsRepo, CurrentUserHelper currentUserHelper) : Controller
    {
        public IActionResult Index()
        {

            var result = kitchenTaskRepo.GetAll();
            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(result.Data);
        }


        public IActionResult Detail(int dataId)
        {
            //var itemName = menuItemsRepo.GetAll();
            //if (itemName.HasError)
            //{
            //    TempData["Error"] = itemName.Message;
            //    return RedirectToAction("Index");
            //}

            //ViewBag.itemName = itemName.Data;

            //if (dataId == -1)
            //{
            //    return View(new KitchenTask());
            //}

            var result = kitchenTaskRepo.GetById(dataId);

            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }

            return View(result.Data);

        }

        [HttpPost]
        public IActionResult Detail(KitchenTask model)
        {
            //var resultLocation = MenuItemsRepo.GetAll();

            ModelState.Remove("OrderItemss");
            ModelState.Remove("MenuItemss");
            ModelState.Remove("ChefID");
            ModelState.Remove("StartedAt");
            ModelState.Remove("CompletedAt");
            ModelState.Remove("UpdatedBy");

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            var result = kitchenTaskRepo.Save(model);
            if (result.HasError)
            {

                ViewBag.Error = result.Message;
                return View(model);
            }
            else
            {
                if (result.Data != null) 
                    TempData["Success"] = $"Order No: #{result.Data.ID} saved successfully.";

                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        //[Authorize(Roles = "chef")]
        public IActionResult CompletedOrder()
        {
            
            int chefId = currentUserHelper.UserId;

            var result = kitchenTaskRepo.GetByChef(chefId);
            if (result.HasError)
            {
                TempData["Error"] = result.Message;
                return View(new List<KitchenTask>());
            }

            return View(result.Data);
        }



    }
}
