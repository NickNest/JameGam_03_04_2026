using System;

namespace _Project.Code.Scripts.TimerService
{
    public interface ITimerService
    {
        TimerHandle Start(float duration, Action onComplete);
        void Pause(TimerHandle handle);
        void Resume(TimerHandle handle);
        void Cancel(TimerHandle handle);
        bool IsActive(TimerHandle handle);
    }
}
