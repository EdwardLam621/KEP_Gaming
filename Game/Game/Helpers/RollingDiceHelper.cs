using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Game.Helpers
{
    public class RollingDiceHelper : ContentPage
    {
        // randomize
        private static Random rnd = new Random();

        // roll dice
        public static int RollDice(int rolls, int dice)
        {
            var myReturn = 0;

            for (var i = 0; i < rolls; i++)
            {
                // Add one to the dice, because random is between.  So 1-10 is rnd.Next(1,11)
                myReturn += rnd.Next(1, dice + 1);
            }

            return myReturn;
        }
    }
}