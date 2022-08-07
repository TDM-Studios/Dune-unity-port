using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intocable : Quest.QuestGoal
{
    public override string GetDescription()
    {
        return $"Completa el nivel sin recibir daño";
    }

    public override void Initialize()
    {
        base.Initialize();
    }

    private void OnHit()
    {
        currAmount++;
        Evaluate();
    }
}
