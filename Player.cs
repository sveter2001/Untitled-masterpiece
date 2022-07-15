using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Untitled_masterpiece
{
    class Player
    {
        public Image _spritesAnimation;
        public int _x, _y;
        public Size _scale;
        public int currFrame = 1;
        public int currAnimation = 0;
        public Image part;
        public int speed;
        public Player(Size _scale, int _x, int _y, Image _spriteAnimation)
        {
            this._scale = _scale;
            this._x = _x;
            this._y = _y;
            this._spritesAnimation = _spriteAnimation;
            speed = 4;
        }
        public void Left()
        {
            _x -= speed;
            //playerPic.Location = new Point(playerPic.Location.X - 1, playerPic.Location.Y);
        }

        public void Right()
        {
            _x +=  speed;
            //playerPic.Location = new Point(playerPic.Location.X + 1, playerPic.Location.Y);
        }

        public void Jump()
        {
            _y -= speed;
            //playerPic.Location = new Point(playerPic.Location.X , playerPic.Location.Y-1);
        }

        public void Fall()
        {
            _y += speed;
            //playerPic.Location = new Point(playerPic.Location.X, playerPic.Location.Y+1);
        }
    }
}
