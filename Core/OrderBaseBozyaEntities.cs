using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    partial class OrderBaseBozyaEntities
    {
        private static OrderBaseBozyaEntities _context;

        public static OrderBaseBozyaEntities GetContext()
        {
            if (_context == null)
                _context = new OrderBaseBozyaEntities();

            return _context;
        }
    }
}
