using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CocosSharp;
using Match3.Entities;

namespace Match3
{
    public static class ActiveLevel
    {
        public static Level level;
        public static int id;
        public static Candy[,] grid;
    }
}
