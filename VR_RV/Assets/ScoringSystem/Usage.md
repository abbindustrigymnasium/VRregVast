# Usage
Make a Unity Event as following:
```csharp
public event Action<int> OnEvent;
```

Add EventListener in ScoreManager that calls relevant method:
```csharp
private void OnEnable()
{
    OnEvent += (int score_to_remove) => {
      Score_Calc_Method(score_to_remove);
    };
}

private void OnDisable()
{
    OnEvent -= (int score_to_remove) => {
      Score_Calc_Method(score_to_remove);
    };
}
```

If you have multiple arguments needed for an event (Note the jump between scripts):
```csharp
//Event Script
public event Action<int, float, ...> OnEvent;

//ScoreManager Script
private void OnEnable()
{
    OnEvent += (int arg1, float arg2, ...) => {
      Score_Calc_Method(arg1, arg2, ...);
    };
}

private void OnDisable()
{
    OnEvent -= (int arg1, float arg2, ...) => {
      Score_Calc_Method(arg1, arg2, ...);
    };
}

//When event happens:
OnEvent?.Invoke(arg1, arg2, ...);
```

Invoke event inside same file as defined:
```csharp
OnEvent?.Invoke(score_penalty);
```
