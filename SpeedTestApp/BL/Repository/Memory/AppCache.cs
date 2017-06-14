using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using SpeedTestApp.Models;

namespace SpeedTestApp.BL.Repository.Memory
{
    /// <summary>
    /// Emulates DB with 10 minutes lifetime for Site objects
    /// </summary>
    public sealed class AppCache
    {
        private static readonly AppCache instance = new AppCache();
        private Dictionary<int, string> siteUrls = new Dictionary<int, string>();
        private int id;    

        static AppCache()
        { }

        private AppCache()
        { }         
        
        public static AppCache Instance
        {
            get
            {
                return instance;
            }
        }

        public int GetId()
        {
            id++;
            return id;
        }

        public Site GetStoredSiteByPageId(int id)
        {
            return (siteUrls.ContainsKey(id)) ? GetValue(siteUrls[id]): null;           
        }

        public void UpdateSitesDict(int Id, string siteUrl)
        {
            siteUrls.Add(Id, siteUrl);
        }

        public Site GetValue(string siteUrl)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            return memoryCache.Get(siteUrl) as Site;
        }

        public bool Add(Site value)
        {
            MemoryCache memoryCache = MemoryCache.Default;            
            return memoryCache.Add(value.Url, value, DateTime.Now.AddMinutes(10));
        }

        public void Update(Site value)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            memoryCache.Set(value.Url, value, DateTime.Now.AddMinutes(10));
        }

        public void Delete(string siteUrl)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(siteUrl))
            {
                memoryCache.Remove(siteUrl);
            }
        }
    }
}