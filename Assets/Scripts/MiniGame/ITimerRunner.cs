using System;

public interface ITimerRunner
{
    public Action OnTimerEnded { get; set; }
    public float Time { get; }
    public bool IsRunning { get; }
    public void Start();
    public void Stop();
}