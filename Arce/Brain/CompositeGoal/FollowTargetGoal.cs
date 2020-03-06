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
        private Vector2 oldTarget;

        public FollowTargetGoal(DynamicGameEntity dynamicGameEntity) : base(dynamicGameEntity)
        {
            GoalStatus = GoalStatus.Inactive;
            Path = new LinkedList<Vector2>();
        }

        public override void Activate()
        {
            GoalStatus = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {

            Subgoals.RemoveAll(g => g.GoalStatus == GoalStatus.Completed);

            Activate();

            // When the target changes reset the path
            if (oldTarget != GameWorld.Instance.Target)
            {
                // Set old target
                oldTarget = GameWorld.Instance.Target;

                // Get new path
                LinkedList<Vertex> newPath = GameWorld.Instance.navigationGraph.Dijkstra(DynamicEntity.Pos, GameWorld.Instance.Target);

                // Clear current subgoals and path
                Subgoals.Clear();
                Path.Clear();

                // Convert the Vertex into Vector2
                foreach (Vertex vertex in newPath)
                    Path.AddFirst(vertex.coordinate);
            }

            if (Path.Count != 0 && !Subgoals.OfType<TraverseVertexGoal>().Any())
            {
                AddSubgoal(new TraverseVertexGoal(DynamicEntity, Path.First(), Path.Count));
                Path.RemoveFirst();
            }

            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
            
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Follow Target");
            Subgoals.ForEach(g => stringBuilder.AppendLine($"  {g.ToString()}"));
            return stringBuilder.ToString();
        }
    }
}
