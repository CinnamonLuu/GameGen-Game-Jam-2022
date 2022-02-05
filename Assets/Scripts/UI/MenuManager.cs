using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_mainPanel;
    [SerializeField]
    GameObject m_optionsPanel;
    [SerializeField]
    GameObject m_controlsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ChangeStatusOptionsMenu(bool active)
    {
        m_optionsPanel.SetActive(active);
        m_mainPanel.SetActive(!active);
    }

    public void ChangeStatusControlsMenu(bool active)
    {
        m_controlsPanel.SetActive(active);
        m_mainPanel.SetActive(!active);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
