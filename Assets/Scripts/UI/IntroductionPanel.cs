using UnityEngine;

public class IntroductionPanel : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(!GameManager.IsRestarting);
    }
}
