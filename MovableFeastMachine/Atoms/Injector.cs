using System;
using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Injector: Atom
{
    public Injector(Site space)
        : this(space, 0, 0)
    {
    }

    public Injector(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 64, 0, 16, 16, Color.Cyan, xPosition, yPosition)
    {
        _diffuseRadius = 0;
        _space.SetAtom(xPosition, yPosition, this);
    }
  
    public override void Update(int deltaTime)
    {
        var aboveY = YPosition - 1;
        var belowY = YPosition + 1;

        if (aboveY >= 0 && _space.GetAtom(XPosition, aboveY) == null)
        {
            var _ = new Injector(_space, XPosition, aboveY);
        }
        else if (belowY < _space.Height && _space.GetAtom(XPosition, belowY) == null)
        {
            var _ = new Injector(_space, XPosition, belowY);
        }
        else
        {
            var chance = Random.Shared.Next(0, 100);
            if (chance > 95)
            {
                var rightX = XPosition + 1;
                if (rightX < _space.Width && _space.GetAtom(rightX, YPosition) == null)
                {
                    var _ = new Data(_space, rightX, YPosition);
                }
            }
        }
        
    }
}