using UnityEngine;

public class ReloadButton : MonoBehaviour
{
    public void Reload() 
    {
        Helper.ReloadCurrentScene.Reload();
    }
}