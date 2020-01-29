using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour {

    private const float _realSecondsPerRevolution = 60f;
    private Text m_TimeText;
    private Transform m_MinuteHandTransform;
    private float seconds;

    private void Awake() {
        m_MinuteHandTransform = transform.Find("MinuteHand");
        m_TimeText = transform.Find("TimeText").GetComponent<Text>();
    }

    private void Update() {
        seconds += Time.deltaTime / _realSecondsPerRevolution;
        float secondsNormalised = seconds % 1f;
        float rotationDegreesPerMinute = 360f;
        m_MinuteHandTransform.eulerAngles = new Vector3(0, 0, -secondsNormalised * rotationDegreesPerMinute);

        float millisecondsPerSecond = 1000f;
        
        string secondsString = Mathf.Floor(secondsNormalised * 60f).ToString("00");
        string millisecondsString = Mathf.Floor( ((secondsNormalised * 1000f) % 1f) * 99f ).ToString("00");
        m_TimeText.text = secondsString + ":" + millisecondsString;
    }

}
