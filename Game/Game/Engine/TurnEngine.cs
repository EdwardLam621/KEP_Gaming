using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    public class TurnEngine
    {
        public DungeonFighterModel Attacker;
        public DungeonFighterModel Defender;

        public TurnEngine()
        {

        }

        public TurnEngine(DungeonFighterModel attacker)
        {
            Attacker = attacker;
        }

        

    }
}
