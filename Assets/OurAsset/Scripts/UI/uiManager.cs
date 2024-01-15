using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System;

namespace UI.MainMenu
{
    public class NewBehaviourScript : MonoBehaviour
    {

        [Header ("Panels")]
        [SerializeField] GameObject mainPanel;
        [SerializeField] GameObject loadingPanel;
        [SerializeField] GameObject miscPanels;
        [SerializeField] GameObject settingsPanel;

        [Header ("Settings")]
        [SerializeField] GameObject settingsWindows;
        [SerializeField] GameObject activeSettingsWindow;
        [SerializeField] TMP_Text FPSLimitValue;
        [SerializeField] TMP_Text brightnessValue;
        [SerializeField] PostProcessProfile brightness;
        [SerializeField] PostProcessLayer layer;

        [Header("Loading Components")]
        [SerializeField] TMP_Text textLoad;
        [SerializeField] UnityEngine.UI.Image imgLoad;

        #region btMethods


        public void btContinue ()                   // загрузка последнего сохранения
        {
            // когда разберёмся с сохранениями - здесь будет вызов метода загрузки

            mainPanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
        public void btNewGame()                     // запуск новой игры
        {
            StartCoroutine(LoadSceneRoutine(1));    // передаёт индекс первого (по задумке, начального) уровня

            mainPanel.SetActive(false);
            loadingPanel.SetActive(true);
        }
        public void btSwitchPanels(GameObject targetPanel) // переход на другую страницу
        {
            mainPanel.SetActive(false);
            targetPanel.SetActive(true);

            if (targetPanel == settingsPanel)
            {
                foreach (Transform child in settingsWindows.transform)
                {
                    child.gameObject.SetActive(false);
                }
                activeSettingsWindow.SetActive(true);
            }
        }
        public void btGoBack(GameObject activePanel)      // переход на предыдущую страницу
        {
            mainPanel.SetActive(true);
            activePanel.SetActive(false);
        }
        public void btSwitchSettingsWindow(GameObject targetSettingsWindow)
        {
            activeSettingsWindow.SetActive(false);
            targetSettingsWindow.SetActive(true);
            activeSettingsWindow = targetSettingsWindow;
        }
        public void btExit()                              // выход
        {
            Application.Quit();
        }

        #endregion

        #region settingsMethods

        private int screenWidth;
        private int screenHeight;
        private FullScreenMode screenMode;
        private AutoExposure exposure;

        public void changeResolution(TMP_Dropdown dropdown)                         // смена разрешения
        {
            int value = dropdown.value;

            switch (value) {
                default: break;
                case 0: 
                    Screen.SetResolution(1920, 1080, screenMode);
                    screenWidth = 1920;
                    screenHeight = 1080;
                    break;
                case 1: 
                    Screen.SetResolution(1600, 900, screenMode);
                    screenWidth = 1600;
                    screenHeight = 900;
                    break;
                case 2: 
                    Screen.SetResolution(1280, 720, screenMode); 
                    screenWidth = 1280;
                    screenHeight = 720; 
                    break;
                case 3: 
                    Screen.SetResolution(1024, 768, screenMode); 
                    screenWidth = 1024;
                    screenHeight = 768;
                    break;
                case 4: 
                    Screen.SetResolution(800, 600, screenMode); 
                    screenWidth = 800;
                    screenHeight = 600;
                    break;
            }
        }
        public void changeScreenMode(TMP_Dropdown dropdown)                         // смена видеорежима
        {
            int value = dropdown.value;

            switch(value)
            {
                default: break;
                case 0:
                    Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
                    screenMode = FullScreenMode.ExclusiveFullScreen;
                    break;
                case 1:
                    Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.FullScreenWindow);
                    screenMode = FullScreenMode.FullScreenWindow;
                    break;
                case 2:
                    Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
                    screenMode = FullScreenMode.Windowed;
                    break;
            }
        }

        public void VSyncToggle(Toggle toggle)              // VSync
        {
            bool value = toggle.isOn;

            switch (value)
            {   
                case false:
                    QualitySettings.vSyncCount = 0;
                    break;
                case true:
                    QualitySettings.vSyncCount = 1;
                    break;
            }
        }
        public void FPSLimit(Slider slider)                 // ограничитель FPS
        {
            Application.targetFrameRate = (int)slider.value;
            FPSLimitValue.text = Application.targetFrameRate.ToString();
        }
        public void ChangeBrightness(Slider slider)
        {
            float value = slider.value;

            if(value != 0)
            {
                exposure.keyValue.value = value;
            }
            else
            {
                exposure.keyValue.value = .05f;
            }
            brightnessValue.text = (int) (value * 100) + "%";
        }

        #endregion
        IEnumerator LoadSceneRoutine(int sceneIndex)
        {
            yield return null;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
            asyncOperation.allowSceneActivation = false;
            Debug.Log("Pro :" + asyncOperation.progress);

            while(!asyncOperation.isDone)
            {
                textLoad.text = "Loading progress: " + (asyncOperation.progress * 100f) + "%";
                imgLoad.fillAmount = asyncOperation.progress;

                if (asyncOperation.progress >= 0.9f )
                {
                    textLoad.text = "Press any key to continue";

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        asyncOperation.allowSceneActivation = true;
                    }

                    yield return null;
                }
            }

        }


        #region Event Handlers

        // Start is called before the first frame update
        void Start()
        {
            // инициализация параметров разрешения
            screenWidth = 1920;
            screenHeight = 1080;
            screenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.SetResolution(screenWidth, screenHeight, screenMode);

            // инициализация интерфейса
            mainPanel.SetActive(true);
            loadingPanel.SetActive(false);

            foreach (Transform child in miscPanels.transform)
            {
                child.gameObject.SetActive(false);
            }
            brightness.TryGetSettings(out exposure);
        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}

