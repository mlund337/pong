using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace NewPongCity.Shared
{
    public class PlayerData
    {
        
        public PlayerData()
        {
        }
        
        public int PlayerId { get; set; }
        public Point Location;
        public bool IsPresent { get; set; }

    }
}
