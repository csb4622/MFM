using System;
using Microsoft.Xna.Framework;


namespace MovableFeastMachine.Atoms;

public class Data: Atom
{
    private int _payload;
    
    public Data(Site space)
        : this(space, 0, 0)
    {
    }


    public int Payload => _payload;

    public Data(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 48, 0, 16, 16, Color.White, xPosition, yPosition)
    {
        _diffuseRadius = 2;
        _space.SetAtom(xPosition, yPosition, this);
        
        _payload = Random.Shared.Next(0, 10000);
        var percentage = _payload / 10000f;
        var blue = 128 + (128 * percentage);
        SetColor(0, 0, (byte)blue);
    }

    public override void Update(int deltaTime)
    {
        var chance = Random.Shared.Next(0, 100);

        if (chance > 95)
        {
            Diffuse();
        }
    }
}