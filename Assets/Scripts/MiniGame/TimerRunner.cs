using System;
using Zenject;

public class TimerRunner : ITimerRunner, ITickable
{
    private readonly IConfigProvider _configProvider;

    public TimerRunner(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }

    public void Tick()
    {
        if (!IsRunning) return;

        Time -= UnityEngine.Time.deltaTime;

        if (Time < 0)
        {
            Time = 0;
            IsRunning = false;
            OnTimerEnded?.Invoke();
        }
    }

    public void Start()
    {
        IsRunning = true;
        Time = _configProvider.ResolveConfig<CardGameConfig>().Time;
    }

    public void Stop()
    {
        IsRunning = false;
    }

    public Action OnTimerEnded { get; set; }
    public float Time { get; private set; }
    public bool IsRunning { get; private set; }
}