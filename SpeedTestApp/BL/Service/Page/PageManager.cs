using System;
using SpeedTestApp.Models;
using SpeedTestApp.BL.Repository;
using System.Collections.Generic;

namespace SpeedTestApp.BL.Service
{
    public class PageManager : IPageManager
    {
        private IPageRepository pageRepository;

        public PageManager(IPageRepository repository)
        {
            this.pageRepository = repository;
        }

        public void AddPages(IEnumerable<Page> pages, Site site)
        {
            if (pages == null || site == null)
                throw new ArgumentNullException("Page or Site is Null");
            else
                pageRepository.AddPages(pages, site);
        }

        public Page GetPage(string url, Site site)
        {
            return (url == null) ? null : pageRepository.GetPage(url, site);
        }

        public Page GetPageById(int Id)
        {
            return (Id == 0) ? null : pageRepository.GetPageById(Id);
        }
    }
}