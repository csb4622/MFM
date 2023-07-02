using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Receiver: Atom
{
    public Receiver(Site space)
        : this(space, 0, 0)
    {
    }

    public Receiver(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 80, 0, 16, 16, Color.Black, xPosition, yPosition)
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
            var _ = new Receiver(_space, XPosition, aboveY);
        }
        else if (belowY < _space.Height && _space.GetAtom(XPosition, belowY) == null)
        {
            var _ = new Receiver(_space, XPosition, belowY);
        }
        else
        {
            var leftX = XPosition - 1;
            if (leftX >= 0)
            {
                var atom = _space.GetAtom(leftX, YPosition);
                if (atom is Data)
                {
                    _space.RemoveAtom(leftX, YPosition);
                }
            }
        }
        
    }
}