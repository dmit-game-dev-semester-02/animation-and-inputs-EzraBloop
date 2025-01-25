using System;
using Microsoft.Xna.Framework.Graphics;

namespace assignment01;
    
/// <summary>
/// Represents a cel animated texture.
/// //Represents a sprite sheet
/// </summary>
public class CelAnimationSequenceMultiRow
{
    // The texture containing the animation sequence...
    protected Texture2D texture;

    // The length of time a cel is displayed...
    protected float celTime;

    // Sequence metrics
    protected int celWidth;
    public int rowToAnimate;

    // Calculated count of cels in the sequence
    protected int celColumnCount;
    protected int celRowCount;
    protected int celHeight;

    /// <summary>
    /// Constructs a new CelAnimationSequence.
    /// </summary>        
    public CelAnimationSequenceMultiRow(Texture2D texture, int celWidth, int celHeight, float celTime,  int rowToAnimate) //situation one, add "int celHeight"
    {
        this.texture = texture;
        this.celWidth = celWidth;
        this.celHeight = celHeight; // needed for situation one 
        this.celTime = celTime;
        this.rowToAnimate = rowToAnimate;

        

        //situation one: one animation, multiple rows
        //celHeight = Texture.Height;
        celColumnCount = Texture.Width / celWidth;
        //celRowCount = Texture.Height / celHeight;

    }

    /// <summary>
    /// All frames in the animation arranged horizontally.
    /// </summary>
    public Texture2D Texture
    {
        get { return texture; }
    }

    /// <summary>
    /// Duration of time to show each cel.
    /// </summary>
    public float CelTime
    {
        get { return celTime; }
    }

    /// <summary>
    /// Gets the number of cels in the animation.
    /// </summary>
    public int CelColumnCount
    {
        get { return celColumnCount; }
    }

    /// <summary>
    /// Gets the width of a frame in the animation.
    /// </summary>
    public int CelWidth
    {
        get { return celWidth; }
    }

    /// <summary>
    /// Gets the height of a frame in the animation.
    /// </summary>
    public int CelHeight
    {
        get { return celHeight; }
    }
}
