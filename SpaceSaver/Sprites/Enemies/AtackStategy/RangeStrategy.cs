﻿using Microsoft.Xna.Framework;

namespace SpaceSaver
{
    public class RangeStrategy : CommonForSkills, IAtackStrategy
    {
        public RangeStrategy(BulletParam param)
        {
            Param = param;
        }

        public string Skill(GameTime gameTime, Vector2 Position, ref float Angle)
        {
            if (!UpdateState(gameTime, Position, ref Angle))
                return "";

            if (CheckTimer())
            {
                Game1.sounds["enemy_shoot"].Play();
                Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Position, "enemy_bullet", Angle, Param));
                timer = Param.CoolDown;
            }
            return "Range_atack";
        }
    }
}
