using System;
using QFramework;
using UnityEngine;

public partial class PlayerControl :IController
{
    private PlayerCountModel _model;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _model = this.GetModel<PlayerCountModel>();
        
        this.RegisterEvent<TraggerInteractEvent>(e =>
        {
            Debug.Log($"触发了 {e._collider.gameObject.name} 的事件");
            _model.count.Value++;
        });
        
        this.GetModel<PlayerCountModel>().count.Register(e =>
        {
            Debug.Log("Count:" + e);
        }).UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    public IArchitecture GetArchitecture()
    {
        return PlayerControlArchitecture.Interface;
    }
}
public struct TriggerEvent{}
public class PlayerControlArchitecture : Architecture<PlayerControlArchitecture>
{
    protected override void Init()
    {
        RegisterModel(new PlayerCountModel());
        RegisterSystem(new PlayerCountSystem());
        
    }
}

public class PlayerCountSystem : AbstractSystem
{
    protected override void OnInit()
    {
       
        
    }
}

public class PlayerCountModel : AbstractModel
{
    public BindableProperty<int> count;
    protected override void OnInit()
    {
        if (count ==null)
        {
            count = new BindableProperty<int>();
        }
    }
}