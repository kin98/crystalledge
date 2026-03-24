using System;
using Content.Shared._CE.GOAP;
using Content.Shared.Actions.Components;
using Robust.Shared.Prototypes;

namespace Content.Server._CE.GOAP.Actions;

public sealed partial class CEGOAPSpeakAction : CEGOAPActionBase<CEGOAPSpeakAction>
{

    [DataField(required: true)]

    public string MessageKey = CEGOAPSpeakActionSystem.SpeakTargetKey;

}

public sealed partial class CEGOAPSpeakActionSystem : CEGOAPActionSystem<CEGOAPSpeakAction>
{
    private EntityQuery<EntityTargetActionComponent> _entityTargetQuery;
    private EntityQuery<WorldTargetActionComponent> _worldTargetQuery;

    public const string SpeakTargetKey = "speak";

    public override void Initialize()
    {
        base.Initialize();
        _entityTargetQuery = GetEntityQuery<EntityTargetActionComponent>();
        _worldTargetQuery = GetEntityQuery<WorldTargetActionComponent>();
    }


    protected override void OnCanExecute(Entity<CEGOAPComponent> ent, ref CEGOAPActionCanExecuteEvent<CEGOAPSpeakAction> args)
    {
        base.OnCanExecute(ent, ref args);
    }

    protected override void OnActionStartup(Entity<CEGOAPComponent> ent, ref CEGOAPActionStartupEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionStartup(ent, ref args);

        var target = GetTarget(ent, args.Action.TargetKey);
        if (target == null)
            return;
    }

    protected override void OnActionUpdate(Entity<CEGOAPComponent> ent, ref CEGOAPActionUpdateEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionUpdate(ent, ref args);
    }
    protected override void OnActionShutdown(Entity<CEGOAPComponent> ent, ref CEGOAPActionShutdownEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionShutdown(ent, ref args);
    }
}
