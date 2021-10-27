﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UIText
// Assembly: Terraria, Version=1.3.5.3, Culture=neutral, PublicKeyToken=null
// MVID: 68659D26-2BE6-448F-8663-74FA559E6F08
// Assembly location: C:\Users\mikeyisbaeyt\Downloads\depotdownloader-2.4.5\depots\105601\6707058\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UIText : UIElement
  {
    private object _text = (object) "";
    private float _textScale = 1f;
    private Vector2 _textSize = Vector2.Zero;
    private bool _isLarge;
    private Color _color = Color.White;

    public string Text => this._text.ToString();

    public Color TextColor
    {
      get => this._color;
      set => this._color = value;
    }

    public UIText(string text, float textScale = 1f, bool large = false) => this.InternalSetText((object) text, textScale, large);

    public UIText(LocalizedText text, float textScale = 1f, bool large = false) => this.InternalSetText((object) text, textScale, large);

    public override void Recalculate()
    {
      this.InternalSetText(this._text, this._textScale, this._isLarge);
      base.Recalculate();
    }

    public void SetText(string text) => this.InternalSetText((object) text, this._textScale, this._isLarge);

    public void SetText(LocalizedText text) => this.InternalSetText((object) text, this._textScale, this._isLarge);

    public void SetText(string text, float textScale, bool large) => this.InternalSetText((object) text, textScale, large);

    public void SetText(LocalizedText text, float textScale, bool large) => this.InternalSetText((object) text, textScale, large);

    private void InternalSetText(object text, float textScale, bool large)
    {
      Vector2 vector2 = new Vector2((large ? Main.fontDeathText : Main.fontMouseText).MeasureString(text.ToString()).X, large ? 32f : 16f) * textScale;
      this._text = text;
      this._textScale = textScale;
      this._textSize = vector2;
      this._isLarge = large;
      this.MinWidth.Set(vector2.X + this.PaddingLeft + this.PaddingRight, 0.0f);
      this.MinHeight.Set(vector2.Y + this.PaddingTop + this.PaddingBottom, 0.0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      CalculatedStyle innerDimensions = this.GetInnerDimensions();
      Vector2 pos = innerDimensions.Position();
      if (this._isLarge)
        pos.Y -= 10f * this._textScale;
      else
        pos.Y -= 2f * this._textScale;
      pos.X += (float) (((double) innerDimensions.Width - (double) this._textSize.X) * 0.5);
      if (this._isLarge)
        Utils.DrawBorderStringBig(spriteBatch, this.Text, pos, this._color, this._textScale);
      else
        Utils.DrawBorderString(spriteBatch, this.Text, pos, this._color, this._textScale);
    }
  }
}
