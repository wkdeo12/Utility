using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TextWriter : MonoBehaviour
{
    private static TextWriter instance;
    private List<TextWriterSingle> textWriterSingleList;

    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }
    public static TextWriterSingle AddWriter_Static(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool removeWriterBeforeAdd, AudioSource audioSource,UnityAction endFunc = null)
    {
        if (removeWriterBeforeAdd)
        {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, audioSource,endFunc);
    }

    private static void RemoveWriterStatic(TextMeshProUGUI uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TextMeshProUGUI uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if(textWriterSingleList[i].GetUIText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private TextWriterSingle AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,AudioSource audioSource, UnityAction endFunc)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, audioSource, endFunc);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    private void Update()
    {
        //Debug.Log(textWriterSingleList.Count);
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
        public UnityAction endFucn;


        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter,AudioSource audioSource,UnityAction endFunc)
        {
            textToWrite = textToWrite.Replace("\\n", "\n");
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.audioSource = audioSource;
            this.endFucn = endFunc;
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
                    if (textToWrite[chracterIndex].CompareTo('\n') == 0 || textToWrite[chracterIndex].CompareTo('\\') == 0)
                    {
                    }
                    else
                    {
                        audioSource.Play();
                    }
                    chracterIndex++;
                    uiText.text = textToWrite.Substring(0, chracterIndex);
                    

                    if (chracterIndex >= textToWrite.Length)
                    {
                        uiText = null;
                        if (endFucn != null)
                        {
                            endFucn.Invoke();
                        }
                        return true;
                    }
                }
            }
            
            return false;
        }
        public TextMeshProUGUI GetUIText()
        {
            return uiText;
        }

        public bool IsActive()
        {
            return chracterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy()
        {
            uiText.text = textToWrite;
            chracterIndex = textToWrite.Length;
            TextWriter.RemoveWriterStatic(uiText);
            if (endFucn != null)
            {
                endFucn.Invoke();
            }
        }
    }

    
}
