using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    public class TurnEngine
    {
        private DungeonFighterModel Attacker;
        private DungeonFighterModel Defender;

        private TurnEngine()
        {

        }

        private TurnEngine(DungeonFighterModel attacker)
        {
            Attacker = attacker;
        }

        

    }
}
