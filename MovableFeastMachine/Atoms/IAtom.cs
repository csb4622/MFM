using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MovableFeastMachine.Atoms;

public interface IAtom
{
    int XPosition { get; }
    int YPosition { get; }
    Vector2 Position { get; }

    Texture2D Texture { get; }
    Rectangle TextureArea { get; }
    
    Color Color { get; }
    
    bool Move(int x, int y);
    bool MoveTo(int x, int y);
    void Update(int deltaTime);
}