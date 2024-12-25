using System;
using QFramework;
using UnityEngine;

public partial class PlayerControl : IController
{
    private PlayerDataModel _model;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _model = this.GetModel<PlayerDataModel>();

        this.RegisterEvent<TraggerInteractEvent>(e =>
        {
            Debug.Log($"触发了 {e._collider.gameObject.name} 的事件");
            _model.count.Value++;
        });

        this.GetModel<PlayerDataModel>().count.Register(e => { Debug.Log("Count:" + e); })
            .UnRegisterWhenGameObjectDestroyed(gameObject);
    }

    public IArchitecture GetArchitecture()
    {
        return PlayerControlArchitecture.Interface;
    }
}

public struct TriggerEvent
{
}

public class PlayerControlArchitecture : Architecture<PlayerControlArchitecture>
{
    protected override void Init()
    {
        RegisterModel(new PlayerDataModel());
        RegisterSystem(new PlayerCountSystem());
    }
}

public class PlayerCountSystem : AbstractSystem
{
    protected override void OnInit()
    {
    }
}

public class PlayerDataModel : AbstractModel
{
    public BindableProperty<int> count;

    public BindableProperty<int> packageMax;

    protected override void OnInit()
    {
        if (count == null)
        {
            count = new BindableProperty<int>();
        }

        if (packageMax == null)
        {
            packageMax = new BindableProperty<int>(20);
        }

        count.Register(e =>
        {
            if (e < 0)
            {
                count.Value = 0;
            }
        });

        packageMax.Register(e =>
        {
            Debug.Log($"将修背包最大容量修改为了 {e} ");
        });
    }
}