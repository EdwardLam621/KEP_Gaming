using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;

namespace Game.Engine
{
    /// <summary>
    /// Referee holds the ScoreModel, relays battle messages, 
    /// keeps track of the ItemModel pool, and follows the
    /// players *everywhere*
    /// </summary>
    public class Referee
    {
        public ScoreModel ScoreCard;
        public BattleMessagesModel BattleMessages;
        public List<ItemModel> ItemPool;

        public Referee()
        {

        }

        public Referee(ScoreModel scoreCard, BattleMessagesModel battleMessages)
        {
            ScoreCard = scoreCard;
            BattleMessages = battleMessages;
            ItemPool = new List<ItemModel>();

        }
    }
}
