using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedTestApp.BL.Repository.Memory
{
    public sealed class PageIDGenerator
    {
        private int id = 0;
        private static readonly PageIDGenerator instance = new PageIDGenerator();

        static PageIDGenerator()
        {}

        private PageIDGenerator()
        {}

        public static PageIDGenerator Instance
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

    }
}