using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CrossingLears
{
    [DisallowMultipleComponent]
    public class AutoLatestMenu : MonoBehaviour
    {
        /// <summary>
        /// If not assigned, use singleton
        /// </summary>
        public LatestMenuManager latestMenuManager;
        public string branch = "";

        [FormerlySerializedAs("closeGUI")]
        public bool closeHUD = true;

        private void OnEnable()
        {
            LatestMenuManager lmm = latestMenuManager ?? LatestMenuManager.Instance;
            lmm.ActiveMenus.Add(this);
            lmm.SetCurrentActive(branch, this, closeHUD);
        }

        private void OnDisable()
        {

            LatestMenuManager lmm = latestMenuManager ?? LatestMenuManager.Instance;
            lmm.ActiveMenus.Remove(this);

            var activePanels = lmm.ActiveMenus
                .Where(x => x.closeHUD);

            lmm.SetActiveHUD(!activePanels.Any());
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
