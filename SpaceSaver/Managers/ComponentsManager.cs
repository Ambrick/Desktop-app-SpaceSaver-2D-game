﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class ComponentsManager
    {
        public void UpdateComponents(GameTime gameTime)
        {
            if (Game1.Map != null) Game1.Map.Update(gameTime);
            Game1.static_objects.RemoveAll(static_object => static_object.IsDead == true);
            Game1.bullets.RemoveAll(bullet => bullet.IsDead == true);
            Game1.bullets.ForEach(bullet => bullet.Update(gameTime));
            Game1.enemies.RemoveAll(enemy => enemy.IsDead == true);
            Game1.enemies.ForEach(enemy => enemy.Update(gameTime));
            Game1.shortLifeAnimatedComponents.RemoveAll(explosion => explosion.IsDead == true);
            Game1.shortLifeAnimatedComponents.ForEach(explosion => explosion.Update(gameTime));
            if (Game1.player != null) Game1.player.Update(gameTime);
            Game1.swords.RemoveAll(sword => sword.IsDead == true);
            Game1.swords.ForEach(sword => sword.Update(gameTime));
        }

        public void DrawComponents(SpriteBatch sprBatch)
        {
            Game1.static_objects.ForEach(static_object => static_object.Draw(sprBatch));
            Game1.bullets.ForEach(bullet => bullet.Draw(sprBatch));
            Game1.enemies.ForEach(enemy => enemy.Draw(sprBatch));
            Game1.shortLifeAnimatedComponents.ForEach(explosion => explosion.Draw(sprBatch));
            if (Game1.player != null) Game1.player.Draw(sprBatch);
            Game1.swords.ForEach(sword => sword.Draw(sprBatch));
        }
    }
}