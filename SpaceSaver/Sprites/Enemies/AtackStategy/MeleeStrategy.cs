﻿using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class MeleeStrategy : CommonForSkills, IAtackStrategy
    {
        public MeleeStrategy(SwordParam param)
        {
            Param = param;
        }

        public string Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (!UpdateState(gameTime, Position, ref Angle))
                return "";

            if (CheckTimer())
            {
                Game1.sounds["enemy_sword"].Play();
                Game1.swords.Add(new Sword(Game1.textures["enemy_sword"], Position, "enemy_sword", Angle, Param));
                timer = Param.CoolDown;
            }
            return "Melee_atack";
        }
    }
}