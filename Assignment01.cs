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
    private CelAnimationPlayerMultiRow _animationMulti, _animationMulti1, _animationMulti2;
    private CelAnimationSequenceMultiRow _marioDuck, _marioHurt, _marioWobble;
    private int _backgroundWidth = 1200;
    private int _backgroundHeight =811;
    private bool isMovingLeft = false, isConfused = false, isMovingUp = false, isCrouched = false, isWobble = false, isHurt = false;
    private float _x = 0, _y = 811;
    private float _speedX = 2, _speedY = 2;



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

        Texture2D spriteSheet4 = Content.Load<Texture2D>("MarioMulti");
        _marioDuck = new CelAnimationSequenceMultiRow(spriteSheet4, 128, 124, 1 /8f, 0);
        _animationMulti = new CelAnimationPlayerMultiRow();
        _animationMulti.Play(_marioDuck);

        _marioHurt = new CelAnimationSequenceMultiRow(spriteSheet4, 128, 124, 1 /8f, 1);
        _animationMulti1 = new CelAnimationPlayerMultiRow();
        _animationMulti1.Play(_marioHurt);

        _marioWobble = new CelAnimationSequenceMultiRow(spriteSheet4, 128, 124, 1 /8f, 2);
        _animationMulti2 = new CelAnimationPlayerMultiRow();
        _animationMulti2.Play(_marioWobble);
    }
    
    

    protected override void Update(GameTime gameTime)
    {
        KeyboardState kbCurrentState = Keyboard.GetState();

        if(kbCurrentState.IsKeyDown(Keys.D))
        {
             _animation01.Update(gameTime);
             isMovingLeft = false;
             _x += _speedX;
        }
        if(kbCurrentState.IsKeyDown(Keys.A))
        {
            _animation01.Update(gameTime);
            isMovingLeft = true;
            _x -= _speedX;
        }
        if(kbCurrentState.IsKeyDown(Keys.W))
        {
            _animation03.Update(gameTime);
            isMovingUp = true;
            _y -= _speedY;
        }
        else 
        {
            isMovingUp = false;
        }
        if(kbCurrentState.IsKeyDown(Keys.S))
        {
            _animation01.Update(gameTime);
            _y += _speedY;
        }
        if(kbCurrentState.IsKeyDown(Keys.C))
        {
            _animationMulti.Update(gameTime);
            isCrouched = true;
        }
        else
        {
            isCrouched = false;
        }

        if(kbCurrentState.IsKeyDown(Keys.Z))
        {
            _animationMulti1.Update(gameTime);
            isHurt = true;
        }
        else
        {
            isHurt = false;
        }
        if(kbCurrentState.IsKeyDown(Keys.X))
        {
            _animationMulti2.Update(gameTime);
            isWobble = true;
        }
        else
        {
            isWobble = false;
        }
    
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
       
        if (isCrouched == true){
            _animationMulti.Draw(_spriteBatch, new Vector2(_x,_y - _marioWalk.CelHeight), SpriteEffects.None);
        }
        else  if (isHurt == true){
            _animationMulti1.Draw(_spriteBatch, new Vector2(_x,_y - _marioWalk.CelHeight), SpriteEffects.None);
        } 
        else  if (isWobble == true){
            _animationMulti2.Draw(_spriteBatch, new Vector2(_x,_y - _marioWalk.CelHeight), SpriteEffects.None);
        }
        else if(isMovingUp == true){
            _animation03.Draw(_spriteBatch, new Vector2(_x, _y - _marioWalk.CelHeight), SpriteEffects.None);
        }
        else if (isMovingLeft == true){
            _animation01.Draw(_spriteBatch, new Vector2(_x, _y - _marioWalk.CelHeight), SpriteEffects.FlipHorizontally);
        }
        else {
             _animation01.Draw(_spriteBatch, new Vector2(_x, _y - _marioWalk.CelHeight), SpriteEffects.None);
        }
       
        if (isConfused == true){
            _animation02.Draw(_spriteBatch, new Vector2(_x, _y - _marioWalk.CelHeight - (_birds.CelHeight /2) ), SpriteEffects.None);
        }

        

        if (_x < 0){
            _speedX += 0;
            _x += 2;
        } 
        if (_x + _marioWalk.CelWidth > _backgroundWidth ){
            _speedX += 0;
            _x -= 2;
        } 

        if (_y - _marioWalk.CelHeight < 540){
            _speedY += 0;
            _y += 2;
        } 
        if (_y > _backgroundHeight ){
            _speedY += 0;
            _y -= 2;
        } 
        
        _spriteBatch.Draw(_block, new Vector2(742, 444), Color.White);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}
