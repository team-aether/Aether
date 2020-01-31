using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour
{
    public TextMesh text; 
    private const string SPEED_INDICATION = "Speed Boost!\n";
    private const string JUMP_INDICATION = "Jump Boost\n";
    private const string NO_INDICATION = "\n";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IndicateBoost(bool speedIndicator, bool jumpIndicator) 
    {
        string indicators = "";
        indicators += ((speedIndicator) ? SPEED_INDICATION : NO_INDICATION);
        indicators += ((jumpIndicator) ? JUMP_INDICATION : NO_INDICATION);
        text.text = indicators;
    }
}
