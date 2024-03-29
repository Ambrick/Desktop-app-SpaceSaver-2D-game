﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSaver
{
    public class Hood
    {
        private Texture2D Hood_texture, Bullet_icon, Sword_icon, Stats_icon;

        private Vector2 Hood_pos, b_icon_pos, sw_icon_pos, st_icon_pos, b_lvl_pos, sw_lvl_pos, st_lvl_pos;

        public Hood()
        {
            Vector2 icon_shift = new Vector2(93, 0);

            Hood_texture = Game1.textures["hood"];
            Bullet_icon = Game1.textures["bullet_icon"];
            Sword_icon = Game1.textures["sword_icon"];
            Stats_icon = Game1.textures["stats_icon"];

            Hood_pos = new Vector2(Game1.ScreenWidth / 2 - Hood_texture.Width / 2, Game1.ScreenHeight - Hood_texture.Height - 50);
            //Задаем значения для позиций иконок в UI
            b_icon_pos = Hood_pos + new Vector2(38,5);
            sw_icon_pos = b_icon_pos + icon_shift;
            st_icon_pos = sw_icon_pos + icon_shift;
            //Задаем значения для позиций отрисовки значения уровней в UI
            b_lvl_pos = Hood_pos + new Vector2(106, 39);
            sw_lvl_pos = b_lvl_pos + icon_shift;
            st_lvl_pos = sw_lvl_pos + icon_shift;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Отрисовки "худа"
            spriteBatch.Draw(Hood_texture, Hood_pos, Color.White);

            //Отрисовка иконки "выстрел"
            if (Game1.player._bullet_timer > 0)
            {
                spriteBatch.Draw(Bullet_icon, b_icon_pos, Color.Orchid);
                spriteBatch.DrawString(Game1.font, ((int)(Game1.player._bullet_timer / 0.1)).ToString(), b_icon_pos + new Vector2(21, 15), Color.White);
            }
            else if (Game1.player.RangeBuff)
                spriteBatch.Draw(Bullet_icon, b_icon_pos, new Color(30, 50, 255));
            else
                spriteBatch.Draw(Bullet_icon, b_icon_pos, Color.White);

            //Отрисовка иконки "удар"
            if (Game1.player._sword_timer > 0)
            {
                spriteBatch.Draw(Sword_icon, sw_icon_pos, Color.Orchid);
                spriteBatch.DrawString(Game1.font, ((int)(Game1.player._sword_timer / 0.1)).ToString(), sw_icon_pos + new Vector2(21, 15), Color.White);
            }
            else if (Game1.player.MeleeBuff)
                spriteBatch.Draw(Sword_icon, sw_icon_pos, new Color(30, 50, 255));
            else
                spriteBatch.Draw(Sword_icon, sw_icon_pos, Color.White);

            //Отрисовка иконки "статов"
            spriteBatch.Draw(Stats_icon, st_icon_pos, Color.White);

            //Вывод уровней скиллов
            spriteBatch.DrawString(Game1.font, Game1.player._Bullet_param.SkillLvl.ToString(), b_lvl_pos, Color.White);
            spriteBatch.DrawString(Game1.font, Game1.player._Sword_param.SkillLvl.ToString(), sw_lvl_pos, Color.White);
            spriteBatch.DrawString(Game1.font, Game1.player._Minion_Stats.SkillLvl.ToString(), st_lvl_pos, Color.White);

            //Вывод здоровья
            spriteBatch.DrawString(Game1.font,
                                  ((int)Game1.player._Minion_Stats.CurrentHealthPoints).ToString() + "/" + ((int)Game1.player._Minion_Stats.MaxHealthPoints).ToString(),
                                   Hood_pos + new Vector2(465, 40),
                                   Color.White,
                                   0,
                                   Vector2.Zero,
                                   0.56f,
                                   SpriteEffects.None,1);

            //Вывод информации об игровых ключах
            spriteBatch.DrawString(Game1.font,
                                   Game1.player.key_count.ToString() + "/" + Game1.player.amount_of_keys_on_a_level.ToString(),
                                   Hood_pos + new Vector2(578, 40),
                                   Color.White,
                                   0,
                                   Vector2.Zero,
                                   0.56f,
                                   SpriteEffects.None,
                                   1);

            //Вывод уровня
            spriteBatch.DrawString(Game1.font,
                                   "Ур." + Game1.player.level_system._current_lvl.ToString(),
                                   Hood_pos + new Vector2(343, 35),
                                   Game1.player.level_system._skill_points > 0 ? Color.Yellow : Color.White);

            //Добавить надпись идите к порталу, если собраны все ключи
            //Добавить надпись прокачайтесь
        }
    }
}