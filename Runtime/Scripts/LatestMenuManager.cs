using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CrossingLears
{
[DefaultExecutionOrder(-1000)]
public class LatestMenuManager : MonoBehaviour
{
    public static LatestMenuManager Instance { get; private set; }
 
    [FormerlySerializedAs("mainGUI"), SerializeField] private GameObject mainHUD;
    private readonly Dictionary<string, AutoLatestMenu> branchList = new();

    public HashSet<AutoLatestMenu> ActiveMenus = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    public void SetCurrentActive(string branch, AutoLatestMenu toAssign, bool closeMainHUD)
    {
        if (closeMainHUD && mainHUD)
        {
            mainHUD.SetActive(false);
        }

        if (branchList.TryGetValue(branch, out AutoLatestMenu latestActive) && latestActive != toAssign)
        {
            latestActive?.Close();
        }
        
        toAssign.Open();
        branchList[branch] = toAssign;
    }

    public void CloseAllMenuThatHidesTheGUI()
    {
        if (Application.isPlaying)
        {
            var menusToClose = new List<AutoLatestMenu>(ActiveMenus);
            foreach (AutoLatestMenu menu in menusToClose)
            {
                if (menu.closeHUD)
                {
                    menu.Close();
                }
            }
        }
        else
        {
            foreach (AutoLatestMenu item in FindObjectsByType<AutoLatestMenu>(FindObjectsSortMode.None))
            {
                if (item.closeHUD)
                {
                    item.Close();
                }
            }
        }
    }

    public void CloseBranch(string branch, bool openMainHUD)
    {
        SetActiveHUD(openMainHUD);

        if (branchList.TryGetValue(branch, out AutoLatestMenu latestActive) && latestActive != null)
        {
            latestActive.Close();
            branchList.Remove(branch);
        }
    }

    public void SetActiveHUD(bool openMainGUI)
    {       
        if (mainHUD)
        {
            mainHUD.SetActive(openMainGUI);
        }
    }
    
#if UNITY_EDITOR
    private void Reset()
    {
        StartCoroutine(LateReset());
    }

    private System.Collections.IEnumerator LateReset()
    {
        yield return null;
        
        LatestMenuManager[] managers = FindObjectsByType<LatestMenuManager>(FindObjectsSortMode.None);
        if (managers.Length > 1)
        {
            if (UnityEditor.EditorUtility.DisplayDialog(
                    "Multiple LatestMenuManager Instances Detected",
                    "Only one instance of LatestMenuManager is allowed.",
                    "Delete old",
                    "Delete new"))
            {
                foreach (var manager in managers)
                {
                    if (manager != this)
                    {
                        DestroyImmediate(manager);
                    }
                }
            }
            else
            {
                DestroyImmediate(this);
            }
        }
    }
#endif
}
}