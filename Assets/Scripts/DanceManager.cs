using System;
using UnityEngine;

public class Dance : MonoBehaviour
{
   [SerializeField]
   private String[] danceNames;
   [SerializeField]
   private Animator character;
   private int currentDanceIndex = 0;

   public void PlayNextDance()
   {
      character.Play(danceNames[currentDanceIndex]);
      currentDanceIndex++;
      if (currentDanceIndex >= danceNames.Length) 
      {
         currentDanceIndex = 0;
      }
       }

}
