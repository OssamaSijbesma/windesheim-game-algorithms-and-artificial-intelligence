using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.FuzzyLogic
{
    class LeftShoulder : FuzzySet
    {
        private double peakPoint;
        private double leftOffset;
        private double rightOffset;
        private double midPoint;

        public LeftShoulder(double peak, double leftOffset, double rightOffset) : base(peak - (leftOffset / 2))
        {
            this.peakPoint = peak;
            this.leftOffset = leftOffset;
            this.rightOffset = rightOffset;
            this.midPoint = peak - (leftOffset / 2);
        }

        public override double CalculateDOM(double value)
        {
            if (0 == leftOffset && value == midPoint)
                return 1.0;

            // Find DOM if right of center
            if (value >= midPoint && value < (midPoint + rightOffset))
            {
                double grad = 1.0 / -rightOffset;
                return grad * (value - midPoint) + 1.0;
            }

            // Find DOM if left of center
            if (value < midPoint)
                return 1.0;

            // Out of range of this FLV, return zero
            return 0.0;
        }

        public override void ORwithDOM(double value)
        {
            throw new NotImplementedException();
        }
    }
}
