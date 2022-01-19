# Utility
내 유틸리티들

TextWriter사용법
씬에 하나 추가한 후

EX)
    TextWriter.TextWriterSingle textWriterSingle;
    public float msgOutPutSpeed = 1f;
    public TextMeshProUGUI messageText;
    public List<string> textList;
    public int currentIdx = 0;
    public AudioSource audioSource;


    private void Start()
    {
        if(textWriterSingle != null && textWriterSingle.IsActive())
        {
            textWriterSingle.WriteAllAndDestroy();
        }
        else
        {
            textWriterSingle = TextWriter.AddWriter_Static(messageText, textList[currentIdx], msgOutPutSpeed, true, audioSource, () => { currentIdx++; });
        }
    }
  
  
