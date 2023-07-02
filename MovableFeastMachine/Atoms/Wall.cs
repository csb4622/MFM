using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Wall: Atom
{
    public Wall(Site space)
        : this(space, 0, 0)
    {
    }

    public Wall(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 96, 0, 16, 16, Color.White, xPosition, yPosition)
    {
        _diffuseRadius = 0;
        _space.SetAtom(xPosition, yPosition, this);
    }
  
    public override void Update(int deltaTime)
    {}
}