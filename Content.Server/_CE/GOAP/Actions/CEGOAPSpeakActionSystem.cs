using System;
using Content.Server.Chat.Managers;
using Content.Server.Chat.Systems;
using Content.Shared._CE.GOAP;
using Content.Shared._CE.Health;
using Content.Shared._CE.Health.Components;
using Content.Shared.Actions.Components;
using Content.Shared.Speech;
using Robust.Shared.Prototypes;
using Robust.Shared.Toolshed.Commands.Values;

namespace Content.Server._CE.GOAP.Actions;

public sealed partial class CEGOAPSpeakAction : CEGOAPActionBase<CEGOAPSpeakAction>
{

    [DataField(required: true)]

    public string MessageKey = CEGOAPSpeakActionSystem.SpeakTargetKey;

    [DataField]
    public string Message = string.Empty;

}

public sealed partial class CEGOAPSpeakActionSystem : CEGOAPActionSystem<CEGOAPSpeakAction>
{
    private EntityQuery<EntityTargetActionComponent> _entityTargetQuery;
    private EntityQuery<WorldTargetActionComponent> _worldTargetQuery;
    [Dependency] private readonly CESharedDamageableSystem _damage = default!;
    [Dependency] private readonly ChatSystem _chat = default!;

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
        if (!HasComp<SpeechComponent>(ent))
            args.CanExecute = false;
    }

    protected override void OnActionStartup(Entity<CEGOAPComponent> ent, ref CEGOAPActionStartupEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionStartup(ent, ref args);

        var self = GetTarget(ent, CEGOAPSystem.SelfTargetKey);
        if (self == null)
            return;
        EnsureComp<SpeechComponent>(self.Value, out var comp);
        comp.SpeechVerb = "Cluwne";

    }

    protected override void OnActionUpdate(Entity<CEGOAPComponent> ent, ref CEGOAPActionUpdateEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionUpdate(ent, ref args);
        var target = GetTarget(ent, args.Action.MessageKey);
        if (target == null)
            return;
        if (TryComp<CEDamageableComponent>(ent, out var comp))
            _damage.ChangeDamage((ent, comp), 1, out var damage);

        var argMessage = args.Action.Message;
        var message = Loc.HasString(argMessage) ? Loc.GetString(argMessage) : argMessage;

        _chat.TrySendInGameICMessage(ent, message, Shared.Chat.InGameICChatType.Speak, false);
    }
    protected override void OnActionShutdown(Entity<CEGOAPComponent> ent, ref CEGOAPActionShutdownEvent<CEGOAPSpeakAction> args)
    {
        base.OnActionShutdown(ent, ref args);
    }
}
