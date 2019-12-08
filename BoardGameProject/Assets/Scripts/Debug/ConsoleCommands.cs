using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IngameDebugConsole;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class ConsoleCommands : MonoBehaviour
    {

        private static WACardGenerator cg;

        protected void Awake()
        {
            cg = FindObjectOfType<WACardGenerator>();
        }

        void Start()
        {
            DebugLogConsole.AddCommandStatic("spawncard", "Creates a card at specified position", "CreateCardAt", typeof(ConsoleCommands));
            DebugLogConsole.AddCommandStatic("gencards", "Generates some cards", "GenerateSomeCards", typeof(ConsoleCommands));
        }

        public static void CreateCardAt(long cardId, Vector3 position)
        {
            cg.GenerateCard(cardId, position);
        }

        public static void GenerateSomeCards()
        {
            float increment = 1.3f;
            float xPos = -(increment * cg.cards.Length) / 2;
            foreach (var card in cg.cards)
            {
                cg.GenerateCard(card.id, new Vector3(xPos, 0, 0));
                xPos += increment;
            }
        }
    }
}
