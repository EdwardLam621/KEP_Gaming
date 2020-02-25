using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class BattleMessagesModel
    {
        // Is the player a character or a monster
        public CreatureEnum PlayerType = CreatureEnum.Unknown;

        // The Status of the action
        public HitStatusEnum HitStatus = HitStatusEnum.Unknown;

        // Name of the Attacker
        public string AttackerName = string.Empty;

        // Name of who the target was
        public string TargetName = string.Empty;

        // The status of the Attack
        public string AttackStatus = string.Empty;

        // Turn Message
        public string TurnMessage = string.Empty;

        // Turn Special Message
        public string TurnMessageSpecial = string.Empty;

        // Level Up Message
        public string LevelUpMessage = string.Empty;

        // Amount of Damage
        public int DamageAmount = 0;

        // The Remaining Health Mesage
        public int CurrentHealth = 0;

        public string GetPreamble()
        {
            return AttackerName + " a " + PlayerType + ", prepares to attack " + TargetName + "!";
        }


        /// <summary>
        /// Remaining Health Message
        /// </summary>
        /// <returns></returns>
        public string GetCurrentHealthMessage()
        {
            return " remaining health is " + CurrentHealth.ToString();
        }

        /// <summary>
        /// Return formatted Damage
        /// </summary>
        /// <returns></returns>
        public string GetDamageMessage()
        {
            return string.Format(" for {0} damage ", DamageAmount);
        }

        /// <summary>
        /// Return formatted string
        /// </summary>
        /// <param name="hitStatus"></param>
        /// <returns></returns>
        public string GetSwingResult()
        {
            return HitStatus.ToMessage();
        }

        /// <summary>
        /// Returns the String Attacker Hit Defender
        /// </summary>
        /// <returns></returns>
        public string GetTurnMessage()
        {
            return AttackerName + GetSwingResult() + TargetName;
        }

    }
}
