using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.DataModel;
using Assets.Scripts.Presentation;
using UnityEngine;

namespace Assets.Scripts
{
    public class UIDManager : MonoBehaviour
    {
        private long lastUID;
        private const long DYNAMIC_START = 0;

        /// <summary>
        /// Contains map for registered model and it's object representation in the world.
        /// May contain null if there is no object in the world.
        /// </summary>
        private Dictionary<BaseObject, ModelContainerBehaviour> uidMap;

        public long GenerateUID()
        {
            lastUID++;
            return lastUID;
        }

        public void RegisterUID(BaseObject obj, ModelContainerBehaviour beh)
        {
            if (obj.uid == 0)
            {
                UnityEngine.Debug.Log("Tried to register UID with value 0");
                return;
            }
            uidMap[obj] = beh;
            //UnityEngine.Debug.Log(beh == null ? obj.uid.ToString() : beh.ToString());
        }

        public void UnregisterUID(BaseObject obj)
        {
            uidMap.Remove(obj);
        }

        // public long GetStaticUID(StaticUID uid)
        // {
        //     return (long) uid;
        // }

        protected void Awake()
        {
            uidMap = new Dictionary<BaseObject, ModelContainerBehaviour>();
            lastUID = DYNAMIC_START;
        }

        public ModelContainerBehaviour GetBehaviourByUID(long uid)
        {
            return uidMap[new BaseObject() { uid = uid }];
        }

        /// <summary>
        /// Finds root parent by searching through registered UIDs.
        /// </summary>
        /// <param name="obj">BaseObject to search a root for</param>
        /// <returns>UID of root parent</returns>
        public long GetRootParentUID(BaseObject obj)
        {
            BaseObject curObj = obj;
            long rootUID = curObj.uid;
            while (TryFindModel(curObj.parentUID, out curObj))
            {
                rootUID = curObj.uid;
            }
            return rootUID;
        }

        public long GetRootParentUID(long uid)
        {
            return GetRootParentUID(new BaseObject() {uid = uid});
        }

        private bool TryFindModel(long uid, out BaseObject model)
        {
            model = uidMap.Keys.SingleOrDefault(k => k.uid == uid);
            return model != null;
        }

        /// <summary>
        /// Finds root parent through hierarchy as opposed to finding it through model. May be slow.
        /// </summary>
        /// <param name="beh">ModelContainerBehaviour to search a root for</param>
        /// <returns>UID of root parent</returns>
        public long GetRootParentUIDByHierarchy(ModelContainerBehaviour beh)
        {
            Transform root = beh.transform.root;
            long rootUID = root.GetComponentInChildren<ModelContainerBehaviour>().ModelData.uid;
            return rootUID;
        }
    }

    // public enum StaticUID
    // {
    //     None,
    //     Table,
    //     Hand,
    //     Door1
    // }
}
