using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Size = System.Drawing.Size;

namespace Untitled_masterpiece
{
    class Player
    {
        public Image _spritesAnimation;
        public int _x, _y;
        public Size _scale;
        public int currFrame = 1;
        public int currAnimation = 0;
        public int speed;
        public Vector _dir;
        //Vector vector;
        public Player(Size _scale, int _x, int _y, Image _spriteAnimation, Vector _dir)
        {
            this._scale = _scale;
            this._x = _x+64;
            this._y = _y+112;
            this._dir = _dir;
            this._spritesAnimation = _spriteAnimation;
            speed = 6;
        }

        Vector L = new Vector(-3,0);
        Vector R = new Vector(3, 0);
        Vector Down = new Vector(0, 4);
        public void Left()
        {
            //_x -= speed;
            if(Math.Abs(_dir.X)<4)
                _dir += L;
            //playerPic.Location = new Point(playerPic.Location.X - 1, playerPic.Location.Y);
        }

        public void Right()
        {
            //_x +=  speed;
            if (Math.Abs(_dir.X) < 4)
                _dir += R;
            //playerPic.Location = new Point(playerPic.Location.X + 1, playerPic.Location.Y);
        }

        public void Jump(Vector dir)
        {
            //_y -= speed;
            //if ( Math.Abs(_dir.Y) < 4)
            //    _dir += UpR;
            //_dir = UpR;
            //playerPic.Location = new Point(playerPic.Location.X , playerPic.Location.Y-1);

            System.Windows.Point startingpoint = new System.Windows.Point(_x, _y);
            //Vector dir = new Vector(_dir.X, _dir.Y);
            System.Windows.Point pointResult = new System.Windows.Point();

            pointResult = startingpoint + dir;

            _x = (int)pointResult.X;
            _y = (int)pointResult.Y;


        }

        public void Fall()
        {
            if (Math.Abs(_dir.Y) < 8)
                _dir = Down;
            //_y += speed;
            //playerPic.Location = new Point(playerPic.Location.X, playerPic.Location.Y+1);
        }
    }
}
