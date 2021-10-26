﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.GolemHeadProgressBar
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Users\mikeyisbaeyt\Downloads\depotdownloader-2.4.5\depots\105601\6707058\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.ID;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class GolemHeadProgressBar : IBigProgressBar
  {
    private float _lifePercentToShow;
    private NPC _referenceDummy;
    private HashSet<int> ValidIds = new HashSet<int>()
    {
      246,
      245
    };

    public GolemHeadProgressBar() => this._referenceDummy = new NPC();

    public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info)
    {
      if (info.npcIndexToAimAt < 0 || info.npcIndexToAimAt > 200)
        return false;
      NPC npc1 = Main.npc[info.npcIndexToAimAt];
      if (!npc1.active && !this.TryFindingAnotherGolemPiece(ref info))
        return false;
      int num1 = 0;
      this._referenceDummy.SetDefaults(245, npc1.GetMatchingSpawnParams());
      int num2 = num1 + this._referenceDummy.lifeMax;
      this._referenceDummy.SetDefaults(246, npc1.GetMatchingSpawnParams());
      int num3 = num2 + this._referenceDummy.lifeMax;
      float num4 = 0.0f;
      for (int index = 0; index < 200; ++index)
      {
        NPC npc2 = Main.npc[index];
        if (npc2.active && this.ValidIds.Contains(npc2.type))
          num4 += (float) npc2.life;
      }
      this._lifePercentToShow = Utils.Clamp<float>(num4 / (float) num3, 0.0f, 1f);
      return true;
    }

    public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
    {
      int bossHeadTexture = NPCID.Sets.BossHeadTextures[246];
      Texture2D texture2D = TextureAssets.NpcHeadBoss[bossHeadTexture].Value;
      Rectangle barIconFrame = texture2D.Frame();
      BigProgressBarHelper.DrawFancyBar(spriteBatch, this._lifePercentToShow, texture2D, barIconFrame);
    }

    private bool TryFindingAnotherGolemPiece(ref BigProgressBarInfo info)
    {
      for (int index = 0; index < 200; ++index)
      {
        NPC npc = Main.npc[index];
        if (npc.active && this.ValidIds.Contains(npc.type))
        {
          info.npcIndexToAimAt = index;
          return true;
        }
      }
      return false;
    }
  }
}
