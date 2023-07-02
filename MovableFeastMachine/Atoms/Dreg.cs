using System;
using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Dreg: Atom
{
    public Dreg(Site space)
        : this(space, 0, 0)
    {
    }

    public Dreg(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 16, 0, 16, 16, Color.White, xPosition, yPosition)
    {
        _diffuseRadius = 4;
        _space.SetAtom(xPosition, yPosition, this);
    }

    public override void Update(int deltaTime)
    {
        var chance = Random.Shared.Next(0, 10000);

        if (chance < 1000)
        {
            Diffuse();
        }
        else if (chance < 1500)
        {
            CreateRes();
        }
        else if (chance < 1510)
        {
            CreateDreg();
        }
        else if (chance < 4000)
        {
            RemoveAtom();
        }
    }

    private void RemoveAtom()
    {
        for (var x = XPosition - _diffuseRadius; x <= XPosition + _diffuseRadius; x++)
        {
            for (var y = YPosition - _diffuseRadius; y <= YPosition + _diffuseRadius; y++)
            {
                if ((Math.Abs(x - XPosition) + Math.Abs(y - YPosition)) <= _diffuseRadius)
                {
                    if (_space.GetAtom(x, y) != null)
                    {
                        var totalChance = 100 * (Math.Abs(x-XPosition) + Math.Abs(y-YPosition));
                        var chance = Random.Shared.Next(0, totalChance) / (float)totalChance;
                        if (chance > .98)
                        {
                            _space.RemoveAtom(x, y);
                        }
                    }
                }
            }
        }
    }
    
    private void CreateRes()
    {
        for (var x = XPosition - _diffuseRadius; x <= XPosition + _diffuseRadius; x++)
        {
            for (var y = YPosition - _diffuseRadius; y <= YPosition + _diffuseRadius; y++)
            {
                if ((Math.Abs(x - XPosition) + Math.Abs(y - YPosition)) <= _diffuseRadius)
                {
                    if (_space.GetAtom(x, y) == null)
                    {
                        var totalChance = 100 * (Math.Abs(x-XPosition) + Math.Abs(y-YPosition));
                        var chance = Random.Shared.Next(0, totalChance) / (float)totalChance;
                        if (chance > .98)
                        {
                            var _ = new Res(_space, x, y);
                        }
                    }
                }
            }
        }
    }
    private void CreateDreg()
    {
        for (var x = XPosition - _diffuseRadius; x <= XPosition + _diffuseRadius; x++)
        {
            for (var y = YPosition - _diffuseRadius; y <= YPosition + _diffuseRadius; y++)
            {
                if ((Math.Abs(x - XPosition) + Math.Abs(y - YPosition)) <= _diffuseRadius)
                {
                    if (_space.GetAtom(x, y) == null)
                    {
                        var totalChance = 100 * (Math.Abs(x-XPosition) + Math.Abs(y-YPosition));
                        var chance = Random.Shared.Next(0, totalChance) / (float)totalChance;
                        if (chance > .98)
                        {
                            var _ = new Dreg(_space, x, y);
                        }
                    }
                }
            }
        }
    }
}