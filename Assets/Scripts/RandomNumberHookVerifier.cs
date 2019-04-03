using ModTool.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class RandomNumberHookVerifier : ModBehaviour
    {
        public Toggle EmptyConstructor, FilledConstructor;
        public GameObject Page1, Page2, Page3;
        public Button Page1Button, Page2Button, Page3Button;
        public MissionHook Hook;

        private void Start()
        {
            Page1Button.onClick.AddListener(Verify);
            Page2Button.onClick.AddListener(Success);
            Page3Button.onClick.AddListener(Fail);
        }

        private void Verify()
        {
            Page1.SetActive(false);
            if (EmptyConstructor.isOn && !FilledConstructor.isOn)
                Page2.SetActive(true);
            else
                Page3.SetActive(true);
        }

        private void Success()
        {
            Hook.RaiseSuccess();
        }

        private void Fail()
        {
            Hook.RaiseFailed();
        }

        private void OnDestroy()
        {
            Page1Button.onClick.RemoveAllListeners();
            Page2Button.onClick.RemoveAllListeners();
            Page3Button.onClick.RemoveAllListeners();
        }
    }
}