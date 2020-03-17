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

        // Remaining player health
        public int AttackerHealth = 0;

        // Remaining target health
        public int TargetHealth = 0;

        // Beginning of the Html Block for html formatting
        public string htmlHead = @"<html><body bgcolor=""#E8D0B6""><p>";

        // Ending of the Html Block for Html formatting
        public string htmlTail = @"</p></body></html>";


        public void ClearMessages()
        {
            PlayerType = CreatureEnum.Unknown;
            HitStatus = HitStatusEnum.Unknown;
            AttackerName = string.Empty;
            TargetName = string.Empty;
            AttackStatus = string.Empty;
            TurnMessage = string.Empty;
            TurnMessageSpecial = string.Empty;
            LevelUpMessage = string.Empty;
        }

        public string GetPreamble()
        {
            return AttackerName + ", a " + PlayerType + " with " + AttackerHealth + "hp" + 
                ", prepares to attack " + TargetName + ", who has " + TargetHealth + "hp remaining!";
        }

        public string GetHitMessage()
        {
            return AttackerName + " studies " + TargetName + " for " + DamageAmount + " damage.";
        }
        
        public string GetCriticalHitMessage()
        {
            return "It's a critical hit! Double damage";
        }

        public string GetMissMessage()
        {
            // Give monsters a different attack message
            if (PlayerType.Equals(CreatureEnum.Monster))
            {
                return AttackerName + " ruins the studying time of " + TargetName + " for "
                    + DamageAmount + " damage!";
            } 

            return AttackerName + " tries to understand " + TargetName + " and fails!";
        }

        /// <summary>
        /// Remaining Health Message
        /// </summary>
        /// <returns></returns>
        public string GetCurrentHealthMessage()
        {
            return TargetName + " has " + TargetHealth + " remaining health.";
        }

        public string GetDeathMessage()
        {
            switch (PlayerType)
            {                
                case CreatureEnum.Character:
                    return TargetName + " is eliminated!";
                default:
                    return TargetName + " passes out from exhaustion!";

            }
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
        /// Returns the entire multiline string output of the turn
        /// Displays on the battle page
        /// </summary>
        /// <returns></returns>
        public string GetTurnMessage()
        {
            StringBuilder msg = new StringBuilder();

            // Change message depending on what happened
            switch (HitStatus) {

                case HitStatusEnum.Miss:
                    msg.AppendLine(GetMissMessage());
                    break;

                case HitStatusEnum.Hit:
                    msg.AppendLine(GetHitMessage());
                    msg.AppendLine(GetCurrentHealthMessage());
                    if (!TurnMessageSpecial.Equals(""))
                    {
                        msg.AppendLine(TurnMessageSpecial);
                    }
                    break;
                default:
                    msg.AppendLine("unknown action");
                    break;
            }
            
            return msg.ToString();
        }

        /// <summary>
        /// Returns a blank HTML page, used for clearing the output window
        /// </summary>
        /// <returns></returns>
        public string GetHTMLBlankMessage()
        {
            var myResult = htmlHead + htmlTail;
            return myResult;
        }

        /// <summary>
        /// Output the Turn as a HTML string
        /// </summary>
        /// <returns></returns>
        public string GetHTMLFormattedTurnMessage()
        {
            var myResult = string.Empty;

            var AttackerStyle = @"<span style=""color:blue"">";
            var DefenderStyle = @"<span style=""color:green"">";

            if (PlayerType == CreatureEnum.Monster)
            {
                // If monster, swap the colors
                DefenderStyle = @"<span style=""color:blue"">";
                AttackerStyle = @"<span style=""color:green"">";
            }

            var SwingResult = string.Empty;
            switch (HitStatus)
            {
                case HitStatusEnum.Miss:
                    SwingResult = @"<span style=""color:yellow"">";
                    break;

                case HitStatusEnum.CriticalMiss:
                    SwingResult = @"<span bold style=""color:yellow; font-weight:bold;"">";
                    break;

                case HitStatusEnum.CriticalHit:
                    SwingResult = @"<span bold style=""color:red; font-weight:bold;"">";
                    break;

                case HitStatusEnum.Hit:
                default:
                    SwingResult = @"<span style=""color:red"">";
                    break;
            }

            var htmlBody = string.Empty;
            htmlBody += string.Format(@"{0}{1}</span>", AttackerStyle, AttackerName);
            htmlBody += string.Format(@"{0}{1}</span>", SwingResult, GetSwingResult());
            htmlBody += string.Format(@"{0}{1}</span>", DefenderStyle, TargetName);
            htmlBody += string.Format(@"<span>{0}</span>", TurnMessageSpecial);

            myResult = htmlHead + htmlBody + htmlTail;
            return myResult;
        }
    }
}
