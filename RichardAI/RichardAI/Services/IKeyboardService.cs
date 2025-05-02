using System;

namespace RichardAI.Services;

public interface IKeyboardService : IDisposable
{
    event Action HotKeyPressed;
    void RegisterHotKey();
}