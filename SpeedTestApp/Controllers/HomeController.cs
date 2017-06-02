using System;
using System.Web.Mvc;
using SpeedTestApp.BL.Service;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Measurers;
using System.Runtime.Caching;
using SpeedTestApp.UI.ViewModels;

namespace SpeedTestApp.Controllers
{
    public class HomeController : Controller
    {
        private ISiteManager siteManager;
        private IPageManager pageManager;
        private IMeasurer measurer;
        private MemoryCache memoryCache = MemoryCache.Default;

        public HomeController(ISiteManager siteManager, IPageManager pageManager, IMeasurer measurer)
        {
            this.siteManager = siteManager;
            this.pageManager = pageManager;
            this.measurer = measurer;
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
            if (filter != "")
            {                
                string siteUrl = filter;
                try
                {
                    Site site = measurer.MeasureResponse(siteUrl);
                    MeasuresViewModel vm = siteManager.GetViewModel(site);
                    memoryCache.Remove("vm");
                    memoryCache.Add("vm", vm, DateTime.Now.AddMinutes(10));
                    return PartialView("_PartialResult", vm);
                }
                catch (System.Net.WebException)
                {
                    ViewBag.ErrorMessage = $"SiteMap for http://{siteUrl} not found";
                    //return PartialView("_PartialResult");
                }
            }
            return PartialView("_PartialResult");
        }

        [HttpGet]
        public ActionResult PageHistory(int? id)
        {
            Page page = null;
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