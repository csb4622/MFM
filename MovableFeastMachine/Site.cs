using System.Collections.Generic;
using MovableFeastMachine.Atoms;

namespace MovableFeastMachine;

public class Site
{
    public int TileWidth => 16;
    public int TileHeight => 16;

    public int Width => 50;
    public int Height => 30;

    
    private IDictionary<int, IAtom> _filledPositions;

    public Site()
    {
        _filledPositions = new Dictionary<int, IAtom>(Width * Height);
    }

    public IEnumerable<IAtom> GetAtoms()
    {
        return _filledPositions.Values;
    }


    public IAtom? GetAtom(int x, int y)
    {
        return _filledPositions.TryGetValue(y * Width + x, out var atom) ? atom : null;
    }
    
    public void SetAtom(int x, int y, IAtom atom)
    {
        _filledPositions[y * Width + x] = atom;
    }

    public bool RemoveAtom(int x, int y)
    {
        return _filledPositions.Remove(y * Width + x);
    }

}