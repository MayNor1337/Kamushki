using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            mainPanel.SetActive(true);
            loadingPanel.SetActive(false);
            foreach(Transform child in miscPanels.transform)
            {
                child.gameObject.SetActive(false);
            }



        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion
    }
}

