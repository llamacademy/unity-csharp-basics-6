using UnityEngine;

public class CountDownAutoDestroyText : AutoDestroyText
{
    protected override void Update()
    {
        Text.SetText($"{GetRemainingTime():N2}");

        base.Update();
    }
}