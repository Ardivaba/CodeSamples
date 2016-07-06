using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityLoader
{
    [Plugin]
    public class DeckManagerPlugin : MonoBehaviour
    {
        bool exported = false;
        bool imported = false;
        void OnGUI()
        {
            var deck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
            var ownedCards = CollectionManager.Get().GetOwnedCards();

            if (Input.GetKeyDown(KeyCode.F6))
                ImportDeck();

            if(Input.GetKeyDown(KeyCode.F5))
            {
                List<string> ownedCardsList = new List<string>();
                foreach (var ownedCard in ownedCards)
                {
                    ownedCardsList.Add(ownedCard.Name + ":" + ownedCard.CardDbId + ":" + ownedCard.CardId);
                }

                File.WriteAllLines("ExportedCards.txt", ownedCardsList.ToArray());
                exported = true;
            }
            GUI.Label(new Rect(10, 20, 200, 200), (exported ? "Imported" : "!Imported") + " " + (exported ? "Exported" : "!Exported") + " Total cards: " + CollectionDeckTray.Get().GetCardsContent().GetEditingDeck().GetTotalCardCount());
            return;
            CollectionManager collectionManager = CollectionManager.Get();
            var cards = collectionManager.GetAllCards();

            int i = 0;
            foreach(var card in cards)
            {
                GUI.Label(new Rect(10, 10 + ( 10 * i ), 200, 200), card.Name);
                i++;
            }
        }
        //s
        void ImportDeck()
        {
            string[] deckImportData = File.ReadAllLines("Deck.txt");
            Dictionary<string, int> deckData = new Dictionary<string, int>();
            foreach(string importDataEntry in deckImportData)
            {
                string[] split = importDataEntry.Split(':');
                string count = split[0];
                string card = split[1];

                deckData.Add(card, int.Parse(count));
            }

            var deck = CollectionDeckTray.Get().GetCardsContent().GetEditingDeck();
            var ownedCards = CollectionManager.Get().GetOwnedCards();
            List<string> ownedCardsList = new List<string>();
            foreach (var ownedCard in ownedCards)
            {
                try
                {
                    if (deckData.ContainsKey(ownedCard.Name))
                    {
                        deck.AddCard(ownedCard.CardId, TAG_PREMIUM.NORMAL, false);
                    }
                }
                catch////
                {

                }
            }

            imported = true;
        }
    }
}
