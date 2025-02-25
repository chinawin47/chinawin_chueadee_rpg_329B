using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private RectTransform selectionBox;
    public RectTransform SelectionBox { get { return selectionBox; } }

    [SerializeField]
    private Toggle togglePauseUnpause;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        togglePauseUnpause.isOn = !togglePauseUnpause.isOn;
    }


    public void ToggleAI(bool isOn)
    {
        foreach (Character member in PartyManager.instance.Members)
        {
            AttackAI ai = member.gameObject.GetComponent<AttackAI>();

            if (ai != null)
                ai.enabled = isOn;
        }
    }

    public void SelectAll()
    {
        foreach (Character member in PartyManager.instance.Members)
        {
          if(member.CurHP > 0)
            {
                member.ToggleRingSelection(true);
                PartyManager.instance.SelectChars.Add(member);
            }
        }
    }

    public void PauseUnpause(bool isOn)
    {
        Time.timeScale = isOn ? 0 : 1;
    }




}
