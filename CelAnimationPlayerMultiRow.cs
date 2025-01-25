using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace assignment01;
    
/// <summary>
/// Controls playback of a CelAnimationSequence.
/// //Note: a cel animation player can only play one animation at a time 
/// </summary>
public class CelAnimationPlayerMultiRow
{
    private CelAnimationSequenceMultiRow celAnimationSequenceMultiRow;
    private int celIndex;
    private float celTimeElapsed;
    private Rectangle celSourceRectangle;

    /// <summary>
    /// Begins or continues playback of a CelAnimationSequence.
    /// </summary>
    public void Play(CelAnimationSequenceMultiRow celAnimationSequence)
    {
        if (celAnimationSequence == null)
        {
            throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");
        }
        // If this animation is already running, do not restart it...
        if (celAnimationSequence != this.celAnimationSequenceMultiRow)
        {
            this.celAnimationSequenceMultiRow = celAnimationSequence;
            //situation one: one animation, multiple rows
            //cellIndexColumn
            //cellIndexRow

            
            celIndex = 0;
            celTimeElapsed = 0.0f;

            celSourceRectangle.X = 0;
            celSourceRectangle.Y = celAnimationSequenceMultiRow.rowToAnimate * celAnimationSequenceMultiRow.CelHeight;
            celSourceRectangle.Width = this.celAnimationSequenceMultiRow.CelWidth;
            celSourceRectangle.Height = this.celAnimationSequenceMultiRow.CelHeight;
        }
    }

    /// <summary>
    /// Update the state of the CelAnimationPlayer.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    public void Update(GameTime gameTime)
    {
        if (celAnimationSequenceMultiRow != null)
        {
            celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (celTimeElapsed >= celAnimationSequenceMultiRow.CelTime)
            {
                celTimeElapsed -= celAnimationSequenceMultiRow.CelTime;

                // Advance the frame index looping as appropriate...
                //celIndexColumn = (celIndexColumn + 1) % celAnimationSequence.CelColumnCount;
                //celIndexRow = (celIndexRow + 1) % celAnimationSequence.CelRowCount;
                celIndex = (celIndex + 1) % celAnimationSequenceMultiRow.CelColumnCount; //situation one:replace cell index with cell index column/row 

                celSourceRectangle.X = celIndex * celSourceRectangle.Width;
                
            }
        }
    }

    /// <summary>
    /// Draws the current cel of the animation.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
    {
        if (celAnimationSequenceMultiRow != null)
        {
            spriteBatch.Draw(celAnimationSequenceMultiRow.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
        }
    }
}

