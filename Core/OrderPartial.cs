using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class Order
    {
        public double Sum => (double)(Pen.Price * Count);
    }
}
