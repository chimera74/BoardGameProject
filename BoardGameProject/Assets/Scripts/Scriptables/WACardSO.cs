using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using UnityEngine;

namespace Assets.Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Card", menuName = "WA Card")]
    public class WACardSO : ScriptableObject
    {
        public long id;
        public new string name;
        public string description;
        public Texture2D image;
        public WACardType type;
    }
}
