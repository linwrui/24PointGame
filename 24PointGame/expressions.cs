using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24PointGame
{
    public enum Expressions
    {
        /// <summary>
        /// 加法(a+b)
        /// </summary>
        Addition,

        /// <summary>
        /// 减法(a-b)
        /// </summary>
        Subtraction,

        /// <summary>
        /// 减法(b-a)
        /// </summary>
        NSubtraction,

        /// <summary>
        /// 乘法(a*b)
        /// </summary>
        Multiplication,

        /// <summary>
        /// 除法(a/b)
        /// </summary>
        Division,

        /// <summary>
        /// 除法(b/a)
        /// </summary>
        NDivision
    }
}
