using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino_Game_Project___Sonic_Pachinko
{
    public class Ball
    {
        private Texture2D _texture;
        private Rectangle _location;
        private Vector2 _speed;

        //when y = 125 start curved movement

        public Ball(Texture2D texture, int x, int y)
        {
            _texture = texture;
            _location = new Rectangle(x, y, 40, 40);
            _speed = new Vector2();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }
    }
}
