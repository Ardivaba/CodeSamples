using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace UnityLoader
{
    [Plugin]
    public class ArenaPlugin : MonoBehaviour
    {
        TierByName tiers;

        void Start()
        {
            tiers = new TierByName();
        }
        void OnGUI()
        {
            DraftCardVisual[] draftCards = GameObject.FindObjectsOfType<DraftCardVisual>();

            int i = 0;
            foreach(DraftCardVisual card in draftCards)
            {
                GUI.Label(new Rect(20, 50 + (20 * i), 200, 200), card.GetActor().GetNameText().Text + " Tier: " + tiers.GetTier(card.GetActor().GetNameText().Text));
                string nameText = card.GetActor().GetNameText().Text;
                if(!nameText.Contains("("))////
                {
                    nameText = nameText + "(" + tiers.GetTier(card.GetActor().GetNameText().Text) + ")";
                    card.GetActor().GetNameText().Text = nameText;
                }
                i++;
            }
        }
    }

    public class TierByName
    {
        string html;
        public TierByName()
        {
            html = GetHtml();
        }

        string GetHtml()
        {
            string url = @"http://www.icy-veins.com/hearthstone/arena-mage-tier-lists-league-of-explorers";
            return new System.Net.WebClient().DownloadString(url);
        }

        public string GetTier(string name)
        {
            int nameIndex = html.IndexOf(name);
            string splitHtml = html.Remove(nameIndex);
            int tierIndex = splitHtml.LastIndexOf("<th colspan=\"4\">");

            string tierPiece = splitHtml.Substring(tierIndex, 50);
            string tierSplit = tierPiece.Split(':')[0];
            string tier = "" + tierSplit[tierSplit.Length - 1];
            return tier;
        }
    }
}
