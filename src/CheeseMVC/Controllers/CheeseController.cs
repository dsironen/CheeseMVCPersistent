using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CheeseMVC.ViewModels;
using CheeseMVC.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.Models;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        private CheeseDbContext context;

        public CheeseController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IList<Cheese> cheeses = context.Cheeses.Include(c => c.Category).ToList();

            return View(cheeses);
        }

        public IActionResult Add()
        {
            IEnumerable<CheeseCategory> categories = context.Categories.ToList();
            AddCheeseViewModel addCheeseViewModel = new AddCheeseViewModel(categories);
            return View(addCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCheeseViewModel addCheeseViewModel)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = context.Categories.Single(c => c.ID == addCheeseViewModel.CategoryID);
                // Add the new cheese to my existing cheeses
                Cheese newCheese = new Cheese
                {
                    Name = addCheeseViewModel.Name,
                    Description = addCheeseViewModel.Description,
                    Category = newCategory
                };

                context.Cheeses.Add(newCheese);
                context.SaveChanges();

                return Redirect("/Cheese");
            }

            return View(addCheeseViewModel);
        }

        public IActionResult Remove()
        {
            return View(context.Cheeses.ToList());
        }

        [HttpPost]
        public IActionResult Remove(int[] cheeseIds)
        {
            foreach (int cheeseId in cheeseIds)
            {
                Cheese theCheese = context.Cheeses.Single(c => c.ID == cheeseId);
                context.Cheeses.Remove(theCheese);
            }

            context.SaveChanges();

            return Redirect("/");
        }

        public IActionResult Edit(int id)
        {
            EditCheeseViewModel editCheeseViewModel = new EditCheeseViewModel(id, context.Categories.ToList());
            return View(editCheeseViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCheeseViewModel editCheeseViewModel)
        {
            if(ModelState.IsValid)
            {
                Cheese existingCheese = context.Cheeses.Single(c => c.ID == editCheeseViewModel.ID);
                existingCheese.Name = editCheeseViewModel.Name;
                existingCheese.Description = editCheeseViewModel.Description;
                existingCheese.CategoryID = editCheeseViewModel.CategoryID;

                context.SaveChanges();
                return Redirect("/Cheese/Index");
            }

            return View(editCheeseViewModel);
        }
    }
}
