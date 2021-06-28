using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;

    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    public static void AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, AudioSource audioSource)
    {
        instance.AddWriter(uiText, textToWrite, timePerCharacter, audioSource);
    }

    private void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,AudioSource audioSource)
    {
        textWriterSingleList.Add(new TextWriterSingle(uiText, textToWrite, timePerCharacter,audioSource));
    }

    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
           bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance)
            {
                textWriterSingleList.RemoveAt(i);
            }
        }
    }



    public class TextWriterSingle {
        private TextMeshProUGUI uiText;
        private string textToWrite;
        private int chracterIndex;
        private float timePerCharacter;
        private float timer;
        public AudioSource audioSource;
        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,AudioSource audioSource)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.audioSource = audioSource;
            chracterIndex = 0;
        }

        public bool Update()
        {
            if (uiText != null)
            {
                timer -= Time.deltaTime;
                while (timer <= 0f)
                {
                    timer += timePerCharacter;
                    chracterIndex++;
                    uiText.text = textToWrite.Substring(0, chracterIndex);
                    audioSource.Play();

                    if (chracterIndex >= textToWrite.Length)
                    {
                        uiText = null;
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
