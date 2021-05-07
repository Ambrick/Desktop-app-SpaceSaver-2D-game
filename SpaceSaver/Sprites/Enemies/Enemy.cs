﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public abstract class Enemy : Minion
    {
        protected IStrategy Strategy;

        protected float Angl90 => (float) Math.Atan(90);

        protected List<Vector2> key_points = new List<Vector2> { };

        protected int State = 0;

        public Enemy(Dictionary<string, Animation> animations, Vector2 position, string object_type, int lvl, IStrategy strategy) : base(animations, position) { }

        protected virtual void Get_path() { }

        protected virtual void Move() { }

        protected override void Action(GameTime gameTime)
        {
            if (Strategy.Skill(gameTime, Position, ref angle))
            {
                AnimationManager.Play(Animations["Action"]);
                return;
            }

            Move();
            AnimationManager.Play(Animations["Move"]);
        }

        public override void Draw(SpriteBatch sprBatch)
        {
            string s = _Minion_Stats.CurrentHealthPoints.ToString();
            sprBatch.DrawString(Game1.font, s, Position+new Vector2(0,-30), Color.White, 0, Vector2.Zero, 0.40f, SpriteEffects.None, 1);
            AnimationManager.Draw(sprBatch, Angle);
        }
        
        protected override void CheckIfDead()
        {
            if (_Minion_Stats.CurrentHealthPoints > 0) return;

            Game1.sounds["enemy_roar2"].Play();
            Game1.explosions.Add(new Explosion(new Dictionary<string, Animation>() { { "Action", new Animation(Game1.textures["explosion"], 6, 0.15f) }, }, Position));
            Game1.static_objects.Add(new Static_Component(Game1.textures["key"], Position, "key"));
            Game1.score++;
            IsDead = true;
        }
    }
}