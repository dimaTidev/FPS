using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Show_FPS : MonoBehaviour
{
    float deltaTime = 0.0f;
    [SerializeField] Text fpsText = null;
    [SerializeField] bool isShowFullInfo = false;
    [SerializeField] bool isOverrideFPS = false;
    [SerializeField] int overrideFPS = 60;

    StringBuilder sb = new StringBuilder();
    string ms;

    [SerializeField] GUI_Settings guiSettings = new GUI_Settings();

    [System.Serializable]
    public class GUI_Settings
    {
        public bool isDrawGUIText = false;
        public enum HorizontalPosition { Left, Middle, Right }
        public enum VerticalPosition { Up, Middle, Down }

        [SerializeField] HorizontalPosition horizontal = HorizontalPosition.Left;
        [SerializeField] VerticalPosition vertical = VerticalPosition.Up;

        [SerializeField] float padding_x = 10;
        [SerializeField] float padding_y = 10;
        [SerializeField] int fontSize = 40;

        public void OnGUI(string text)
        {
            if (!isDrawGUIText)
                return;

            float x = horizontal == HorizontalPosition.Left ? padding_x : horizontal == HorizontalPosition.Middle ? Screen.width/2 - padding_x : Screen.width - padding_x;
            float y = vertical == VerticalPosition.Up ? padding_y : vertical == VerticalPosition.Middle ? Screen.height/2 - padding_y : Screen.height - padding_y;

            int oriFontSize = GUI.skin.label.fontSize;

            GUI.skin.label.fontSize = fontSize;
            GUI.Label(new Rect(x, y, text.Length * fontSize, fontSize * 1.3f), text);
            GUI.skin.label.fontSize = oriFontSize;
        }
    }

    private void Awake()
    {
        if(isOverrideFPS)
            Application.targetFrameRate = overrideFPS;
    }

    void Update()
    {
        if (Time.timeScale != 1) return;

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f; //deltaTime
        float fps = 1.0f / deltaTime;


        if (isShowFullInfo)
        {
            float msec = deltaTime * 1000.0f;
            msec = (Mathf.RoundToInt(msec * 100)) / 100f;
            sb.Clear();
            sb.Append((int)fps).Append(" fps - ").Append(msec.ToString("0.00")).Append(" ms");
        }
        else
        {
            string ms = ((int)fps).ToString();
            if (!ms.Equals(this.ms))
            {
                this.ms = ms;
                sb.Clear();
                sb.Append((int)fps).Append(" fps");
            }
        }

        if(fpsText)
            fpsText.text = sb.ToString();
    }


    void OnGUI()
    {
        guiSettings.OnGUI(sb.ToString());
    }


    #region ExternalCalls
    //-------------------------------------------------------------------------------------------------
    public void Set_ShowFPSText(bool isEnable)
    {
        if (fpsText)
        {
            fpsText.gameObject.SetActive(isEnable);
            fpsText.enabled = isEnable;
        }
    }

    public void Set_ShowFPSGUI(bool isEnable)
    {
        guiSettings.isDrawGUIText = isEnable;
    }
    //-------------------------------------------------------------------------------------------------
    #endregion
}
