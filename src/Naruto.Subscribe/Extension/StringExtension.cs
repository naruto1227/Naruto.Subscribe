using System;
using System.Collections.Generic;
using System.Text;

namespace Naruto.Subscribe.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 检查字符串不为空  否则报错
        /// </summary>
        /// <param name="str"></param>
        public static void CheckNullOrEmpty(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentNullException("值不能为空");
            }
        }

        public static void CheckNull(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("值不能为空");
            }
        }
    }
}
