using Assets.Scripts.DataModel;
using Assets.Scripts.DropZones;
using UnityEngine;

namespace Assets.Scripts.Debug
{
    public class DebugOther
    {
        public static void PrintAllDropSites()
        {
            var dropZones = GameObject.FindObjectsOfType<DropZone>();
            foreach (DropZone dz in dropZones)
            {
                if (dz.name == "Table")
                    UnityEngine.Debug.Log("Table\n" + dz.enabled);
                else
                {
                    string str = dz.transform.parent.name;
                    CardDropZone cdz = dz as CardDropZone;
                    if (cdz != null)
                    {
                        str += ": " + cdz.transform.parent.GetComponentInChildren<PlayingCard>().ToString();

                    }
                    else 
                    {
                        DeckDropZone ddz = dz as DeckDropZone;
                        if (ddz != null)
                            str += ": " + ddz.transform.parent.GetComponentInChildren<PlayingCard>().ToString();
                    }

                    UnityEngine.Debug.Log(str + "\n" + dz.enabled);
                }
            }
        }
    }
}
