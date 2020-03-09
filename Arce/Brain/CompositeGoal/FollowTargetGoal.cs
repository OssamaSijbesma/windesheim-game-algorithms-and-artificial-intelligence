using Arce.Entity;
using Arce.NavigationGraph;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.Brain
{
    class FollowTargetGoal : CompositeGoal
    {
        private LinkedList<Vector2> Path;
        private Vector2 target;

        public FollowTargetGoal(DynamicGameEntity dynamicGameEntity) : base(dynamicGameEntity)
        {
            GoalStatus = GoalStatus.Inactive;
            Path = new LinkedList<Vector2>();
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;

            // Set target
            target = GameWorld.Instance.Target;

            // Get new path
            LinkedList<Vertex> newPath = GameWorld.Instance.navigationGraph.Dijkstra(DynamicEntity.Pos, GameWorld.Instance.Target);

            // Convert the Vertex into Vector2
            foreach (Vertex vertex in newPath)
                Path.AddFirst(vertex.coordinate);
        }

        public override GoalStatus Process()
        {
            // Activate goal when inactive
            if (GoalStatus == GoalStatus.Inactive) Activate();

            // Stop condition
            if (Path.Count == 0) Terminate();

            // When the target changes the goal has failed
            if (target != GameWorld.Instance.Target) GoalStatus = GoalStatus.Failed;

            // When completed or failed return
            if (GoalStatus == GoalStatus.Completed || GoalStatus == GoalStatus.Failed) return GoalStatus;

            // Remove all completed subgoals
            Subgoals.RemoveAll(g => g.GoalStatus == GoalStatus.Completed);

            // Add subgoal 
            if (Path.Count != 0 && !Subgoals.OfType<TraverseVertexGoal>().Any())
            {
                AddSubgoal(new TraverseVertexGoal(DynamicEntity, Path.First(), Path.Count));
                Path.RemoveFirst();
            }

            // Process all subgoals
            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }

        public override string ToString()
        {
            return "Follow Target" + Environment.NewLine + base.ToString();
        }
    }
}
