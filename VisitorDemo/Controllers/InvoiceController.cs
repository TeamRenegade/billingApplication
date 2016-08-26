using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VisitorDemo.DataLayer;
using VisitorDemo.Models;

namespace VisitorDemo.Controllers
{
    public class InvoiceController : Controller,IVisitor
    {
        //
        // GET: /Invoice/
        private NORTHWNDEntities _dbContext = new NORTHWNDEntities();
     
        private List<string> _htmlsectionList = new List<string>();

        /// <summary>
        /// Method:Index
        /// Purpose:Returns the Invoice view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id=0)
        {
            Order od = _dbContext.Orders.Find(id);
            if (od == null)
                return HttpNotFound();
            Invoice newInvoice = new Invoice(od);
            ViewBag.CustomerID = od.CustomerID;
            newInvoice.accept(this);
            ViewBag.OrderID = id;
            return View(_htmlsectionList);
        }


        public ActionResult BackToOrder(string id, int page = 1)
        {
            return RedirectToAction("Index", "Order", new { id = id, page = page });

        }

        /// <summary>
        /// Method:Save
        /// Purpose:Handler to save the Invoice in XML format
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Save(int id=0)
        {
            Order od = _dbContext.Orders.Find(id);
            if (od == null)
                return HttpNotFound();
            Invoice newInvoice = new Invoice(od);
            SaveInvoiceInXML saveVisitor = new SaveInvoiceInXML();
            newInvoice.accept(saveVisitor);
            string filePath = string.Format("{0}Order_{1}_{2}.xml", Request.PhysicalApplicationPath, od.CustomerID, od.OrderID);
            try
            {
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                saveVisitor.InvoiceRoot.Save(filePath);
            }
            catch (Exception ex) {

                throw ex;
            }
            return View(od);
        }


        public void visit(HeaderElement headerElement)
        {

            _htmlsectionList.Add(RenderRazorViewToString("_Header", headerElement.Header));
        }
        public void visit(OrderElement orderElement)
        {
            _htmlsectionList.Add(RenderRazorViewToString("_Order", orderElement.CurrentOrder));
        }

        public void visit(FooterElement footerElement)
        {
            _htmlsectionList.Add(RenderRazorViewToString("_Footer", footerElement));
        }


        public void visit(InvoiceElement invoiceElement)
        {
            //Do nothing...  //throw new NotImplementedException();
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}