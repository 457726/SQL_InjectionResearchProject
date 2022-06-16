using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SQL_InjectionResearchProject.Models;
using System.Collections.Generic;

namespace SQL_InjectionResearchProject.Controllers
{
    public class SQLController : Controller
    {
        DataAccess dataAcces = new();
        public ActionResult Index()
        {
            var list = JsonConvert.DeserializeObject<List<DataModel>>(dataAcces.GetData());
            return View(list);
        }

        public ActionResult Details(int id)
        {
            var datamodel = JsonConvert.DeserializeObject<DataModel>(dataAcces.GetProductData(id));
            return View(datamodel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string productname, string description, string brand, int price)
        {
            try
            {
                dataAcces.AddData(productname, description, brand, price);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            var datamodel = JsonConvert.DeserializeObject<DataModel>(dataAcces.GetProductData(id));
            return View(datamodel);
        }

        [HttpPost]
        public ActionResult Edit(int id, string productname, string description, string brand, int price)
        {
                dataAcces.UpdateData(productname, description,brand,price,id);
                return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            var datamodel = JsonConvert.DeserializeObject<DataModel>(dataAcces.GetProductData(id));
            return View(datamodel);
        }
        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                dataAcces.DeleteData(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete));
            }
        }
    }
}
