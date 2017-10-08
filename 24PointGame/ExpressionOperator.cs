using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24PointGame
{
    public class ExpressionOperator
    {
        public double Num_left
        {
            get
            {
                switch (_expression)
                {
                    case Expressions.NDivision:
                    case Expressions.NSubtraction: return _b;
                    default: return _a;
                }
            }
        }
        public double Num_right
        {
            get
            {
                switch (_expression)
                {
                    case Expressions.NDivision:
                    case Expressions.NSubtraction: return _a;
                    default: return _b;
                }
            }
        }
        private readonly double _a;
        private readonly double _b;
        private readonly Expressions _expression;
        public ExpressionOperator(double num_a, double num_b, Expressions expression)
        {
            _a = num_a;
            _b = num_b;
            _expression = expression;
        }


        public double GetResult()
        {
            switch (_expression)
            {
                case Expressions.Addition: return _a + _b;
                case Expressions.Subtraction: return _a - _b;
                case Expressions.NSubtraction: return _b - _a;
                case Expressions.Multiplication: return _a * _b;
                case Expressions.Division: return _b == 0 ? Single.NaN : _a / _b;
                case Expressions.NDivision: return _a == 0 ? Single.NaN : _b / _a;
                default: return Single.NaN;
            }
        }

        public string GetExpressionString()
        {
            switch (_expression)
            {
                case Expressions.Addition: return $"{_a}+{_b}";
                case Expressions.Subtraction: return $"{_a}-{_b}";
                case Expressions.NSubtraction: return $"{_b}-{_a}";
                case Expressions.Multiplication: return $"{_a}*{_b}";
                case Expressions.Division: return $"{_a}/{_b}";
                case Expressions.NDivision: return $"{_b}/{_a}";
                default: return "";
            }
        }

        public string GetExpressionString(object left, object right)
        {
            switch (_expression)
            {
                case Expressions.Addition: return $"{left}+{right}";
                case Expressions.Subtraction: return $"{left}-{right}";
                case Expressions.NSubtraction: return $"{right}-{left}";
                case Expressions.Multiplication: return $"{left}*{right}";
                case Expressions.Division: return $"{left}/{right}";
                case Expressions.NDivision: return $"{right}/{left}";
                default: return "";
            }

        }
    }
}
