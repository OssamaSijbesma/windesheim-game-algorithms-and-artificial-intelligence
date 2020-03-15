using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.FuzzyLogic
{
    abstract class FuzzySet
    {
        private double DOM;
        private double RepresentativeValue;

        public FuzzySet(double representativeValue) 
        {
            DOM = 0.0;
            RepresentativeValue = representativeValue;
        }

        public abstract double CalculateDOM(double value);

        public virtual void ORwithDOM(double value) 
        {
            if (value > DOM)
                DOM = value;
        }

        public virtual double GetRePresentativeVal() => RepresentativeValue;
        public virtual double GetDOM() => DOM;
        public virtual void SetDOM(double value) => DOM = value;
        public virtual void ClearDOM() => DOM = 0.0;
    }
}
