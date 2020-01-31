using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    public TextMesh text; 
    private bool m_SpeedIndicator;
    private bool m_JumpIndicator;
    private bool m_FlagIndicator;
    private const string INDICATION_FLAG = "Fire to Drop Flag!\n";
    private const string INDICTION_SPEED = "Speed Boost!\n";
    private const string INDICTION_JUMP = "Jump Boost\n";
    private const string INDICATION_NONE = "\n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string indicators = "";
        indicators += ((m_SpeedIndicator) ? INDICTION_SPEED : INDICATION_NONE);
        indicators += ((m_JumpIndicator) ? INDICTION_JUMP : INDICATION_NONE);
        indicators += ((m_FlagIndicator) ? INDICATION_FLAG : INDICATION_NONE);
        text.text = indicators;
    }

    public void IndicateFlag(bool flagIndicator)
    {
        m_FlagIndicator = flagIndicator;
    }

    public void IndicateBoost(bool speedIndicator, bool jumpIndicator) 
    {
        m_SpeedIndicator = speedIndicator;
        m_JumpIndicator = jumpIndicator;
    }
}
