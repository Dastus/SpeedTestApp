using System;
using System.Web.Mvc;
using SpeedTestApp.BL.Service;
using SpeedTestApp.Models;
using System.Runtime.Caching;
using SpeedTestApp.UI.ViewModels;

namespace SpeedTestApp.Controllers
{
    public class HomeController : Controller
    {
        private ISiteManager siteManager;
        private IPageManager pageManager;
        private IMeasureManager measureManager;        
        private MemoryCache memoryCache = MemoryCache.Default;

        public HomeController(ISiteManager siteManager, IPageManager pageManager, IMeasureManager measureManager)
        {
            this.siteManager = siteManager;
            this.pageManager = pageManager;            
            this.measureManager = measureManager;
        } 

        // GET: Home
        public ActionResult Index()
        {            
            if (memoryCache != null)
            { 
                return View(memoryCache.Get("vm"));
            }
            return View();
        }
        
        public ActionResult Measure(string filter)
        {
            if (filter != "" && filter != null)
            {                
                string siteUrl = filter;

                Site site = measureManager.MeasureResponse(siteUrl);

                if (site != null)
                {
                    MeasuresViewModel vm = siteManager.GetViewModel(site);
                    memoryCache.Remove("vm");
                    memoryCache.Add("vm", vm, DateTime.Now.AddMinutes(10));
                    return PartialView("_PartialResult", vm);
                }
                else
                    ViewBag.ErrorMessage = $"Can't access {siteUrl}";
            }
            return PartialView("_PartialResult");
        }

        [HttpGet]
        public ActionResult PageHistory(int? id)
        {
            Models.Page page = null;
            try
            {
                page = pageManager.GetPageById(id.Value);
                return View(page);
            }
            catch (Exception)
            {                
            }
            return new HttpNotFoundResult();
        }

    }
}