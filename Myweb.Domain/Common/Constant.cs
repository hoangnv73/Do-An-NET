using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myweb.Domain.Common
{
    public class Constant
    {
    }

    public static class USER_STATUS
    {
        /// <summary>
        /// Pending
        /// </summary>
        public const int Pending = 1;

        /// <summary>
        /// Active
        /// </summary>
        public const int Active = 2;

        /// <summary>
        /// Paused
        /// </summary>
        public const int Paused = 3;

        /// <summary>
        /// Blocked
        /// </summary>
        public const int Blocked = 4;
    }

    public static class ORDER_STATUS
    {
        /// <summary>
        /// Pending
        /// </summary>
        public const int Pending = 1;

        /// <summary>
        /// Processing
        /// </summary>
        public const int Processing = 2;

        /// <summary>
        /// Shipping
        /// </summary>
        public const int Shipping = 3;

        /// <summary>
        /// Delivered
        /// </summary>

        public const int Delivered = 4;

        /// <summary>
        /// Cancelled
        /// </summary>

        public const int Cancelled = 5;
    }

    public static class PRODUCT_STATUS
    {
        /// <summary>
        /// InStock
        /// </summary>
        public const int InStock = 1;

        /// <summary>
        /// OutStock
        /// </summary>
        public const int OutStock = 2;

        /// <summary>
        /// ComingSoon
        /// </summary>
        public const int ComingSoon = 3;
    }
}
