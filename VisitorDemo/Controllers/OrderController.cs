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
    public class OrderController : Controller
    {

        private BONorthWind bo = new BONorthWind();
        private PagedList<Order> _pagedOrders = null;
        private int _pageSize = 8;
        private void InitOrders(string id)
        {
            var orders = bo.GetOrders(id);
            _pagedOrders = new PagedList<Order>(orders, _pageSize);

        }
        //
        // GET: /Order/
        public ActionResult Index(string id,int page=1)
        {
            if (string.IsNullOrEmpty(id))
                return HttpNotFound();
            if(_pagedOrders==null)
              InitOrders(id);
            ViewBag.CustomerID=id;
            _pagedOrders.CurrentPage = page;
            return View(_pagedOrders);
        }

        public ActionResult ShowInvoice(int id)
        {
            
            return RedirectToAction("Index", "Invoice", new { id = id });
        }
	}
}