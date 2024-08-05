using TMPro;
using UnityEngine;
using Zenject;

public class UITimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private ITimerRunner _timerRunner;
    
    [Inject]
    private void Construct(ITimerRunner timerRunner)
    {
        _timerRunner = timerRunner;
    }

    public void Update()
    {
        float minutes = Mathf.FloorToInt(_timerRunner.Time / 60); 
        float seconds = Mathf.FloorToInt(_timerRunner.Time % 60);
        
        _text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}