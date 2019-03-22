using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Spaceshooter.Content
{
    class Buster:Basklass
    {
        public Texture2D Texture { get; set; }
        public int rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;
        Vector2 location;
        bool remove = false;
    }
}
