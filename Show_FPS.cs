using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show_FPS : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Text fpsText;
    [SerializeField] int targetFPS = 60;

    string tempFPS;

    [SerializeField] GUI_Settings guiSettings;

    [System.Serializable]

    public class GUI_Settings
    {
        [SerializeField] bool isDrawGUIText = false;
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
        Application.targetFrameRate = targetFPS;
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        tempFPS = "FPS: " + Mathf.Ceil(fps).ToString();

        if (fpsText)
            fpsText.text = tempFPS;
    }

    void OnGUI()
    {
        guiSettings.OnGUI(tempFPS);
    }
}
