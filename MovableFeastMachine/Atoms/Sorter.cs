using System;
using Microsoft.Xna.Framework;

namespace MovableFeastMachine.Atoms;

public class Sorter: Atom
{
    private int _lastDataMoved;
    
    public Sorter(Site space)
        : this(space, 0, 0)
    {
    }

    public Sorter(Site space, int xPosition, int yPosition)
        : base(space, "Atoms", 32, 0, 16, 16, Color.Red, xPosition, yPosition)
    {
        _lastDataMoved = 0;
        _diffuseRadius = 4;
        _space.SetAtom(xPosition, yPosition, this);
    }

    public override void Update(int deltaTime)
    {
        var chance = Random.Shared.Next(0, 100);

        if (chance < 10)
        {
            Diffuse();
        }
        else if (chance < 50)
        {
            CreateSorter();
        }
        MoveData();
        
    }
  
    private void CreateSorter()
    {
        for (var x = XPosition - _diffuseRadius; x <= XPosition + _diffuseRadius; x++)
        {
            for (var y = YPosition - _diffuseRadius; y <= YPosition + _diffuseRadius; y++)
            {
                if ((Math.Abs(x - XPosition) + Math.Abs(y - YPosition)) <= _diffuseRadius)
                {
                    var foundAtom = _space.GetAtom(x, y);
                    if (foundAtom is Res)
                    {
                        var totalChance = 100 * (Math.Abs(x-XPosition) + Math.Abs(y-YPosition));
                        var chance = Random.Shared.Next(0, totalChance) / (float)totalChance;
                        if (chance > .98)
                        {
                            _space.RemoveAtom(x, y);
                            var _ = new Sorter(_space, x, y);
                        }
                    }
                }
            }
        }
    }
    
    private void MoveData()
    {
        // Put bigger data under me
        {
            for (var x = XPosition - 3; x < XPosition; ++x)
            {
                for (var y = YPosition - 3; y < YPosition; ++y)
                {
                    var foundAtom = _space.GetAtom(x, y);
                    if (foundAtom is Data data)
                    {
                        if (data.Payload > _lastDataMoved)
                        {
                            for (var newX = XPosition + 1; newX < XPosition + 3; ++newX)
                            {
                                for (var newY = YPosition + 1; newY < YPosition + 3; ++newY)
                                {
                                    if (_space.GetAtom(newX, newY) == null)
                                    {
                                        data.MoveTo(newX, newY);
                                        _lastDataMoved = data.Payload;
                                        var percentage = _lastDataMoved / 10000f;
                                        var red = 128 + (128 * percentage);
                                        SetColor((byte)red, 0, 0);
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        // Put small data over me
        {
            for (var x = XPosition - 3; x < XPosition; ++x)
            {
                for (var y = YPosition + 1; y < YPosition+3; ++y)
                {
                    var foundAtom = _space.GetAtom(x, y);
                    if (foundAtom is Data data)
                    {
                        if (data.Payload < _lastDataMoved)
                        {
                            for (var newX = XPosition + 1; newX < XPosition + 3; ++newX)
                            {
                                for (var newY = YPosition -3; newY < YPosition; ++newY)
                                {
                                    if (_space.GetAtom(newX, newY) == null)
                                    {
                                        data.MoveTo(newX, newY);
                                        _lastDataMoved = data.Payload;
                                        var percentage = _lastDataMoved / 10000f;
                                        var red = 128 + (128 * percentage);
                                        SetColor((byte)red, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }        
    }
}