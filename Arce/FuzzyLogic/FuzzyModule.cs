using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.FuzzyLogic
{
    class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> variables;
        private List<FuzzyRule> rules;

        public FuzzyModule() 
        {
            variables = new Dictionary<string, FuzzyVariable>();
            rules = new List<FuzzyRule>();
        }

        // Creates a new "empty" fuzzy variable and returns a reference to it.
        public FuzzyVariable CreateFLV(string name) 
        {
            return null;
        }

        // Adds a rule to the module
        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence) 
        {
        
        }

        // Call the Fuzzify method of the named FLV
        public void Fuzzify(string nameFLV, double value) 
        {
        }

        // Returns a crisp value
        public double DeFuzzify(string key)
        {
            return 0;
        }
    }
}
