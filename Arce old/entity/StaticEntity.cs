﻿ using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arce.entity
{
    abstract class StaticEntity : BaseGameEntity
    {
        public StaticEntity(Vector2D pos) : base(pos) 
        { 
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }
    }
}
