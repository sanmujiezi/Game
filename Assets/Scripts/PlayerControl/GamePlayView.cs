using QFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayView : BasePanel, IController
{
    private Slider m_slider;
    private Text m_sliderText;

    private PlayerDataModel _model;

    public override void Init()
    {
        m_slider = transform.GetChild(0).Find("m_Slider").GetComponent<Slider>();
        m_sliderText = transform.GetChild(0).Find("m_SliderText").GetComponent<Text>();
        _model = this.GetModel<PlayerDataModel>();

        _model.count.Register(e =>
        {
            UpdatePackageSlider(e);
            UpdatePackageText(e, _model.packageMax.Value);
        });

        UpdatePackageSlider(_model.count.Value);
        UpdatePackageText(_model.count.Value, _model.packageMax.Value);
    }


    private void UpdatePackageSlider(int progress)
    {
        float value = progress / (float)_model.packageMax.Value;
        if (value < 0)
        {
            value = 0;
        }
        m_slider.value = value;
    }

    private void UpdatePackageText(int progress, int max)
    {
        string content = progress + " / " + max;
        m_sliderText.text = content;
    }


    public IArchitecture GetArchitecture()
    {
        return PlayerControlArchitecture.Interface;
    }
}