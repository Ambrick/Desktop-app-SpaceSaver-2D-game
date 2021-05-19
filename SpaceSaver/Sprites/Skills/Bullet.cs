﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceSaver
{
    public class Bullet : StaticComponent 
    {
        private Vector2 Direction;

        public Bullet_param Param;

        private Vector2 initial_pos;

        public Bullet(Texture2D texture, Bullet_param param, Vector2 position, string object_type, float angle) : base(texture, position, object_type)
        {
            Texture = texture;
            Rectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Object_type = object_type;
            Angle = angle;

            Param = param;
            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
            initial_pos = Position = position + Direction * 25f;
            Velocity = Direction * Param.MoveSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            IsDead = Math.Sqrt(Math.Pow(Position.X - initial_pos.X, 2) + Math.Pow(Position.Y - initial_pos.Y, 2)) > Param.Range ? true : false;

            //проверка на столкновение со стеной
            foreach (StaticComponent spr2 in Game1.static_objects)
                if (spr2.Object_type == "wall" && Collision_manager.CheckCollision(this, spr2))
                {
                    Game1.shortLifeAnimatedComponents.Add(new ShortLifeAnimatedComponents(new Animation(Game1.textures["explosion"], 6, 0.15f), Position));
                    Game1.sounds["explosion"].Play();
                    IsDead = true;
                    return;
                }

            //проверка на столкновение пули игрока с врагом
            if (Object_type == "player_bullet")
            {
                foreach (Enemy enemy in Game1.enemies)
                {
                    if (Collision_manager.CheckCollision(this, enemy))
                    {
                        enemy.GetHitIsDead(Param.Damage, "bullet_damage_was_dealt", Position);
                        IsDead = true;
                        if (enemy.Object_type == "enemy_shielded")
                        {
                            initial_pos = Position;
                            Angle += (float) Math.Atan(50 + new Random().Next(0, 80)) * 2;
                            Direction = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle));
                            Velocity = Direction * Param.MoveSpeed;
                            Object_type = "enemy_bullet";
                            IsDead = false;
                        }
                        return;
                    }
                }
            }
            //проверка на столкновение вражеской пули с игроком
            if (Object_type == "enemy_bullet")
            {
                foreach (Sword sword in Game1.swords)
                {
                    if (sword.Object_type == "player_sword" && Collision_manager.CheckCollision(this, sword) && sword.Param.IsJedi)
                    {
                        Game1.bullets.Add(new Bullet(Game1.textures["enemy_bullet"], Param, Position, "player_bullet", Game1.player.Angle));
                        IsDead = true;
                        return;
                    }
                }
                if (Collision_manager.CheckCollision(this, Game1.player))
                {
                    Game1.sounds["player_get_hit"].Play();
                    Game1.player.GetHitIsDead(Param.Damage, "bullet_damage_was_dealt", Position);
                    IsDead = true;
                    return;
                }
            };
            //--------------------------------
            Position += Velocity;
        }
    }
}