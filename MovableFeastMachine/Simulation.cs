using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MovableFeastMachine.Atoms;

namespace MovableFeastMachine;

public class Simulation : Game
{
    private Site _space;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Simulation()
    {
        this.Window.Title = "Movable Feast Machine";
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        TextureManager.Current.Initialize(Content);
        _space = new Site();
        for (var i = 0; i < 30; i += 2)
        {
            var newDreg = new Dreg(_space, 21, i);
            var newSorter = new Sorter(_space, 20, i);
        }
        var injector = new Injector(_space, 0, 20);
        var receiver = new Receiver(_space, 49, 20);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        var atoms = _space.GetAtoms().ToArray();
        for (var i = 0; i < atoms.Length; ++i)
        {
            atoms[i].Update(gameTime.ElapsedGameTime.Milliseconds);
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        foreach (var atom in _space.GetAtoms())
        {
            _spriteBatch.Draw(atom.Texture, atom.Position, atom.TextureArea, atom.Color);
        }
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}