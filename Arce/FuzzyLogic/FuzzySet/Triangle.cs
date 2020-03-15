using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.FuzzyLogic
{
    class Triangle : FuzzySet
    {
        private double peakPoint;
        private double leftOffset;
        private double rightOffset;

        public Triangle(double peakPoint, double leftOffset, double rightOffset) : base(peakPoint)
        {
            this.peakPoint = peakPoint;
            this.leftOffset = leftOffset;
            this.rightOffset = rightOffset;
        }
        public override double CalculateDOM(double value)
        {
            if ((rightOffset == 0.0 && peakPoint== value) || (leftOffset == 0.0 && peakPoint == value))
                return 1.0;

            // Find DOM if left of center
            if (value <= peakPoint && value >= (peakPoint-leftOffset))
            {
                double grad = 1.0 / leftOffset;
                return grad * (value - (peakPoint - leftOffset));
            }

            // Find DOM if right of center
            if (value > peakPoint && value < (peakPoint + rightOffset))
            {
                double grad = 1.0 / -rightOffset;
                return grad * (value - peakPoint) + 1.0;
            }

            // Out of range of this FLV, return zero
            return 0.0;
        }
    }
}
