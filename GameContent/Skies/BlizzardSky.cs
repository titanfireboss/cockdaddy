﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.BlizzardSky
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Users\mikeyisbaeyt\Downloads\depotdownloader-2.4.5\depots\105601\6707058\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace Terraria.GameContent.Skies
{
  public class BlizzardSky : CustomSky
  {
    private UnifiedRandom _random = new UnifiedRandom();
    private bool _isActive;
    private bool _isLeaving;
    private float _opacity;

    public override void OnLoad()
    {
    }

    public override void Update(GameTime gameTime)
    {
      if (Main.gamePaused || !Main.hasFocus)
        return;
      if (this._isLeaving)
      {
        this._opacity -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        if ((double) this._opacity >= 0.0)
          return;
        this._isActive = false;
        this._opacity = 0.0f;
      }
      else
      {
        this._opacity += (float) gameTime.ElapsedGameTime.TotalSeconds;
        if ((double) this._opacity <= 1.0)
          return;
        this._opacity = 1f;
      }
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      if ((double) minDepth >= 1.0 && (double) maxDepth != 3.40282346638529E+38)
        return;
      float num = Math.Min(1f, Main.cloudAlpha * 2f);
      Color color = new Color(new Vector4(1f) * Main.ColorOfTheSkies.ToVector4()) * this._opacity * 0.7f * num;
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), color);
    }

    public override void Activate(Vector2 position, params object[] args)
    {
      this._isActive = true;
      this._isLeaving = false;
    }

    public override void Deactivate(params object[] args) => this._isLeaving = true;

    public override void Reset()
    {
      this._opacity = 0.0f;
      this._isActive = false;
    }

    public override bool IsActive() => this._isActive;
  }
}
