using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace assignment01;

public class Assignment01 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _background, _block;
    private CelAnimationSequence _marioWalk, _marioWalkBehind, _birds;
    private CelAnimationPlayer _animation01, _animation02, _animation03;
    private int _backgroundWidth = 1200;
    private int _backgroundHeight =811;
    private KeyboardState _kbPreviousState;
    private bool isMovingLeft = false;
    private bool isConfused = false;
    private bool isMovingUp = false;
    private float _x, _y;



    public Assignment01()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = _backgroundWidth;
        _graphics.PreferredBackBufferHeight = _backgroundHeight;
        _graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        //Textures
        _background = Content.Load<Texture2D>("WiseWisterwood");
        _block = Content.Load<Texture2D>("SaveBlock");
        //Sprite sheets
         Texture2D spriteSheet1 = Content.Load<Texture2D>("MarioWalk");
        _marioWalk = new CelAnimationSequence(spriteSheet1, 128, 1 /8f);
        _animation01 = new CelAnimationPlayer();
        _animation01.Play(_marioWalk);

        Texture2D spriteSheet3 = Content.Load<Texture2D>("MarioWalkBehind");
        _marioWalkBehind = new CelAnimationSequence(spriteSheet3, 128, 1 /8f);
        _animation03 = new CelAnimationPlayer();
        _animation03.Play(_marioWalkBehind);


         Texture2D spriteSheet2 = Content.Load<Texture2D>("Birds");
        _birds = new CelAnimationSequence(spriteSheet2, 128, 1 /8f);
        _animation02 = new CelAnimationPlayer();
        _animation02.Play(_birds);
    }
    

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();

        if(kbCurrentState.IsKeyDown(Keys.Right))
        {
             _animation01.Update(gameTime);
             isMovingLeft = false;
        }
        if(kbCurrentState.IsKeyDown(Keys.Left))
        {
            _animation01.Update(gameTime);
            isMovingLeft = true;
        }
        if(kbCurrentState.IsKeyDown(Keys.Up))
        {
            _animation03.Update(gameTime);
            isMovingUp = true;
        }
        else 
        {
            isMovingUp = false;
        }
        if(kbCurrentState.IsKeyDown(Keys.Down))
        {
            _animation01.Update(gameTime);
        }
    
        //Play the animation
        if(kbCurrentState.IsKeyDown(Keys.Space))
        {
            _animation02.Update(gameTime);
            isConfused = true;
        }
        else
        {
            isConfused = false;
            
        }
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, Vector2.Zero, Color.White);
        _spriteBatch.Draw(_block, new Vector2(742, 444), Color.White);


       
        if(isMovingUp == true){
            _animation03.Draw(_spriteBatch, new Vector2(0, 811 - _marioWalk.CelHeight), SpriteEffects.None);
        }
        else if (isMovingLeft == true){
            _animation01.Draw(_spriteBatch, new Vector2(0, 811 - _marioWalk.CelHeight), SpriteEffects.FlipHorizontally);
        }
        else {
             _animation01.Draw(_spriteBatch, new Vector2(0, 811 - _marioWalk.CelHeight), SpriteEffects.None);
        }
       
        if (isConfused == true){
            _animation02.Draw(_spriteBatch, new Vector2(0, 811 - _marioWalk.CelHeight - (_birds.CelHeight /2) ), SpriteEffects.None);
        }
        

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
