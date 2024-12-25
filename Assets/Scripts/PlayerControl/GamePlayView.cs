
    using QFramework;
    using UnityEngine.UI;

    public class GamePlayView : BasePanel,IController
    {
        private Slider packageSlider;
        private PlayerDataModel _model;
        public override void Init()
        {
            packageSlider = transform.Find("m_Slider").GetComponent<Slider>();
            _model = this.GetModel<PlayerDataModel>();

            _model.count.Register(UpdatePackageSlider);
        }


        private void UpdatePackageSlider(int progress)
        {
            if (progress <= _model.packageMax.Value && progress > 0)
            {
                packageSlider.value = progress/ _model.packageMax.Value;
            }
            else if (progress<0)
            {
                progress = 0;
                packageSlider.value = progress;
            }
        }


        public IArchitecture GetArchitecture()
        {
            return PlayerControlArchitecture.Interface;
        }
    }
