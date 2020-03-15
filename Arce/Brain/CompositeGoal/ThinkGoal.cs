using Arce.Entity;
using Arce.FuzzyLogic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class ThinkGoal : CompositeGoal
    {
        private Vector2 oldTarget;
        private FuzzyModule fuzzyModule;

        public ThinkGoal(ConsciousGameEntity entity) : base(entity)
        {
            fuzzyModule = new FuzzyModule();

            FuzzyVariable hunger = fuzzyModule.CreateFLV("Hunger");
            FzSet starving = hunger.AddLeftShoulder("Starving", 0.0, 0.1, 0.3);
            FzSet content = hunger.AddTriangle("Content", 0.1, 0.5, 0.8);
            FzSet full = hunger.AddRightShoulder("Full", 0.5, 0.8, 1.0);

            FuzzyVariable sleep = fuzzyModule.CreateFLV("Sleep");
            FzSet tired = sleep.AddLeftShoulder("Tired", 0.0, 0.1, 0.3);
            FzSet sleepy = sleep.AddTriangle("Sleepy", 0.1, 0.3, 0.5);
            FzSet awake = sleep.AddRightShoulder("Awake", 0.3, 0.5, 1.0);

            FuzzyVariable desirability = fuzzyModule.CreateFLV("Desirability");
            FzSet undesirable = desirability.AddLeftShoulder("Undesirable", 0, 0.25, 0.5);
            FzSet desirable = desirability.AddTriangle("Desirable", 0.25, 0.5, 0.75);
            FzSet veryDesirable = desirability.AddRightShoulder("VeryDesirable", 0.5, 0.75, 1.0);

            fuzzyModule.AddRule(new FzOR(starving, tired), undesirable);
            fuzzyModule.AddRule(new FzAND(content, sleepy), undesirable);
            fuzzyModule.AddRule(new FzAND(full, sleepy), desirable);
            fuzzyModule.AddRule(new FzAND(content, awake), desirable);
            fuzzyModule.AddRule(new FzAND(full, awake), veryDesirable);
        }

        public double GetDesirability(double hunger, double sleep)
        {
            fuzzyModule.Fuzzify("Hunger", hunger);
            fuzzyModule.Fuzzify("Sleep", sleep);
            return fuzzyModule.DeFuzzify("Desirability"); ;
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            if (GoalStatus == GoalStatus.Inactive) Activate();

            // Remove all completed subgoals
            Subgoals.RemoveAll(g => g.GoalStatus == GoalStatus.Completed || g.GoalStatus == GoalStatus.Failed);

            // If hunger or sleep is below a certain point attent to vitality
            if (!Subgoals.OfType<VitalityGoal>().Any() && GetDesirability(Entity.Hunger, Entity.Sleep) < 0.5)
                AddSubgoal(new VitalityGoal(Entity));

            Console.WriteLine($"Des: {GetDesirability(Entity.Hunger, Entity.Sleep)}        hung: {Entity.Hunger}, sleep: { Entity.Sleep}");

            // If the target changes follow the new target
            if (oldTarget != GameWorld.Instance.Target)
            {
                AddSubgoal(new FollowTargetGoal(Entity));
                oldTarget = GameWorld.Instance.Target;
            }

            // Process all subgoals
            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
        }

        public override string ToString()
        {
            return "Think" + Environment.NewLine + base.ToString();
        }
    }
}
