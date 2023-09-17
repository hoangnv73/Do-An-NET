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

    public class COMMON_STATUS
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public COMMON_STATUS(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public COMMON_STATUS()
        {

        }
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

    public class ORDER_STATUS 
    {
        public static COMMON_STATUS Pending = new COMMON_STATUS(1, "Pending");
        public static COMMON_STATUS Processing = new COMMON_STATUS(2, "Processing");
        public static COMMON_STATUS Shipping = new COMMON_STATUS(3, "Shipping");
        public static COMMON_STATUS Delivered = new COMMON_STATUS(4, "Delivered");
        public static COMMON_STATUS Cancelled = new COMMON_STATUS(5, "Cancelled");

        public static List<COMMON_STATUS> GetList()
        {
            var result = new List<COMMON_STATUS>
            {
                Pending,
                Processing,
                Shipping,
                Delivered,
                Cancelled
            };
            return result;
        }
        public static COMMON_STATUS GetDetail(int id)
        {
            var result = GetList().FirstOrDefault(x => x.Id == id);
            return result;
        }
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

    public static class COUPON_STATUS
    {
        // Trừ Phần trăm
        public const int Percent = 1;
        // Trừ Tiền
        public const int Money = 2;
    }
}
