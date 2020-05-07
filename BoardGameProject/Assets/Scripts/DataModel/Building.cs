using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DataModel
{
    public class Building : BaseObject
    {

        public event Action OnOpen;
        public event Action OnClose;

        public string name;
        public bool isOpen;

        public void Open()
        {
            isOpen = true;
            OnOpen?.Invoke();
        }

        public void Close()
        {
            isOpen = false;
            OnClose?.Invoke();
        }
    }
}
