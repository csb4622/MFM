using System;
using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Res: Atom
{
    public Res(Site space)
        : this(space, 0, 0)
    {
    }

    public Res(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 0, 0, 16, 16, Color.White, xPosition, yPosition)
    {
        _diffuseRadius = 4;
        _space.SetAtom(xPosition, yPosition, this);
    }

    public override void Update(int deltaTime)
    {
        var chance = Random.Shared.Next(0, 100);

        if (chance > 90)
        {
            Diffuse();
        }
    }
}