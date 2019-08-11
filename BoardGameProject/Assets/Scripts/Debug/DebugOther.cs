using Assets.Scripts.DataModel;
using Assets.Scripts.DropSites;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class DebugOther
    {
        public static void PrintAllDropSites()
        {
            var dropSites = GameObject.FindObjectsOfType<DropSite>();
            foreach (DropSite ds in dropSites)
            {
                if (ds.name == "Table")
                    UnityEngine.Debug.Log("Table\n" + ds.enabled);
                else
                {
                    string str = ds.transform.parent.name;
                    CardDropSite cds = ds as CardDropSite;
                    if (cds != null)
                    {
                        str += ": " + cds.transform.parent.GetComponentInChildren<PlayingCard>().ToString();

                    }
                    else 
                    {
                        DeckDropSite dds = ds as DeckDropSite;
                        if (dds != null)
                            str += ": " + dds.transform.parent.GetComponentInChildren<PlayingCard>().ToString();
                    }

                    UnityEngine.Debug.Log(str + "\n" + ds.enabled);
                }
            }
        }
    }
}
