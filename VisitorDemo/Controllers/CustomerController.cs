using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisitorDemo.DataLayer;
using VisitorDemo.Models;
using VisitorDemo.Utilities;

namespace VisitorDemo.Controllers
{
    public class CustomerController : Controller
    {
        private BONorthWind bo = new BONorthWind();
        private PagedList<Customer> _pagedCustomers = null;
        private int _pageSize = 8;
        public ActionResult ViewOrders(string id ="") {

            return RedirectToAction("Index", "Order", new { id = id });
           
        }
        
        private void InitCustomers(){

            if (_pagedCustomers == null)
            {
                var customers = bo.GetCustomers();
                _pagedCustomers = new PagedList<Customer>(customers, _pageSize);
            }
        
        }
        //
        // GET: /Customer/
        public ActionResult Index(int page=1)
        {
            InitCustomers();
            _pagedCustomers.CurrentPage = page;
            return View(_pagedCustomers);
        }

        //
        // GET: /Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
