using UnityEngine;

public abstract class CreatureEvent //Abstraction and Polymorphism, Shared traits, handles catch events
{
   /*
   To-Do list
        - Handle ALL Catch events
        - Take in player-pressing
        - Determine Catch event type (Player, Catcher)
   */

   public void CatchEvent()
   {
      
   }

   public void CatchProgressCalculator()
   {

   }

   public void RewardCalculator()
   {

   }

   //========================================================
   //player

   public class PlayerCatch: CreatureEvent //individual traits for player events
   {

   }

   //========================================================
   //catcher
   public class StaffCatch: CreatureEvent //individual traits for automated events
   {

   }
}