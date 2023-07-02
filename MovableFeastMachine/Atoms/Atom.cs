using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MovableFeastMachine.Atoms;

public abstract class Atom: IAtom
{
    // Location
    private int _xPosition;
    private int _yPosition;
    private Vector2 _position;
    
    //Drawing Information
    private readonly string _textureName;
    private readonly Texture2D _texture;
    private readonly Rectangle _textureArea;
    private Color _color;

    protected readonly Site _space;
    protected int _diffuseRadius;

    protected Atom(Site space, string textureName, int xOffset, int yOffset, int width, int height, Color color, int xPosition, int yPosition)
    {
        _space = space;
        _textureName = textureName;
        _texture = TextureManager.Current.GetTexture(_textureName);
        _textureArea = new Rectangle(xOffset, yOffset, width, height);
        _color = color;

        _xPosition = xPosition;
        _yPosition = yPosition;
        _position = new Vector2(_xPosition*_space.TileWidth, _yPosition*_space.TileHeight);

        _diffuseRadius = 0;
    }

    public int XPosition => _xPosition;
    public int YPosition => _yPosition;
    public Vector2 Position => _position;
    public Texture2D Texture => _texture;
    public Rectangle TextureArea => _textureArea;

    public Color Color => _color;

    public void SetColor(byte red, byte green, byte blue)
    {
        _color.R = red;
        _color.G = green;
        _color.B = blue;
        
    }
    
    public bool Move(int x, int y)
    {
        return MoveTo(_xPosition + x, _yPosition+y);
    }

    public bool MoveTo(int x, int y)
    {
        if (x >= 0 && x < _space.Width && y >= 0 && y < _space.Height)
        {
            _space.RemoveAtom(_xPosition, _yPosition);
            _xPosition = x;
            _yPosition = y;
            _position.X = _xPosition*_space.TileWidth;
            _position.Y = _yPosition*_space.TileHeight;
            _space.SetAtom(_xPosition, _yPosition, this);
            return true;
        }
        return false;
    }

    public abstract void Update(int deltaTime);
    
    
    protected void Diffuse()
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
                            MoveTo(x, y);
                        }
                    }
                }
            }
        }
    }
}