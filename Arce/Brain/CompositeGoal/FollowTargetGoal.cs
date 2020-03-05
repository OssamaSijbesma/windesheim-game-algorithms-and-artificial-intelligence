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
            Activate();

            // When the target changes reset the path
            if (oldTarget != GameWorld.Instance.Target)
            {
                // Set old target
                oldTarget = GameWorld.Instance.Target;

                // Get new path
                LinkedList<Vertex> newPath = GameWorld.Instance.navigationGraph.Dijkstra(DynamicEntity.Pos, GameWorld.Instance.Target);

                // Clear current targets
                Path.Clear();

                // Set the path for the entity
                foreach (Vertex vertex in newPath)
                    Path.AddFirst(vertex.coordinate);
            }

            // Remove waypoints if the hero gets close
            if (Path.Count > 1 && Vector2.Subtract(Path.First(), DynamicEntity.Pos).Length() < 16)
                Path.RemoveFirst();

            AddSubgoal(new TraverseVertexGoal(DynamicEntity, Path.First()));

            Subgoals.ForEach(g => g.Process());

            return GoalStatus;
        }

        public override void Terminate()
        {
            GoalStatus = GoalStatus.Completed;
        }
    }
}
