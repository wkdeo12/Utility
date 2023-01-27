# Utility
내 유틸리티들

TextWriter사용법

씬에 하나 추가한 후

EX-

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
  
FloatingJoystick

new inputsystem의 플로팅 조이스틱

Image0(이벤트 데이터를 받아올용도)
Image1(조이스틱의 외각모양)
Image2(조이스틱의 Knob)
이렇게 3개가 필요하다.
