using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float typingDelay;
    public bool isAutoProgress;
    #region
    [SerializeField]
    private BackgroundImage backgroundImage;
    [SerializeField]
    private HeroineImage heroineImage;
    [SerializeField]
    private ScreenOverlay screenOverlay;
    [SerializeField]
    private LikeabilityText likeabilityText;
    [SerializeField]
    private ChoiceButton[] choiceButtons;
    [SerializeField]
    private NameText nameText;
    [SerializeField]
    private DialogText dialogText;
    [SerializeField]
    private ProgressText progressText;
    #endregion

    [SerializeField]
    private AudioClip bgm;

    [Serializable]
    private struct HeroinSprite
    {

        public Sprite Viola;
        public Sprite Smile;
        public Sprite Happy;
        public Sprite Sulky;
        public Sprite Difficult;
        public Sprite Speak;
        public Sprite Down;

    }
    [SerializeField]
    private HeroinSprite heroinSprite;

    [Serializable]
    private struct BackgroundSprite
    {
        public Sprite SchoolBackyard;
        public Sprite BandPracticeRoom;
        public Sprite SchoolRooftop;
    }
    [SerializeField]
    private BackgroundSprite backgroundSprite;

    private int selectedChoiceIndex;
    private int likeability = 0;
    private int requiredLikeability = 4;

    private void Start()
    {
        StartCoroutine(PlayGameScript());   
    }

    private void Update()
    {
        likeabilityText.SetText($"호감도 : {likeability} / {requiredLikeability}");
        if (likeability >= requiredLikeability)
            likeabilityText.SetColor(Color.white);
    }

    private IEnumerator PlayGameScript()
    {
        SoundManager.instance.PlayBGM(bgm);

        #region Day 1:
        SetBackground(backgroundSprite.SchoolBackyard);
        SetHeroineImage(heroinSprite.Viola);
        yield return FadeIn();
        yield return Typing("", "나는 학교 뒤뜰에서 혼자 비올라를 연습 중인 서윤을 우연히 발견한다.");
        yield return Typing("나", "또 연습 중이네. 연습실은 안 쓰고 여기서 해?");
        yield return Typing("서윤", "여기가 더 편해. 조용하잖아. 그리고 넌 왜 여길 돌아다니는 거야?", heroinSprite.Speak);
        yield return Choice(new string[]
        {
            "그냥 걷다가 들렸어. 너 연주하는 거 듣는 게 좋아서.",
            "솔직히 말하면, 너를 더 알고 싶어서.",
            "아무 이유 없어. 그냥 여기 공기가 좋더라고.",
        });
        if (selectedChoiceIndex == 0)
        {
            likeability += 1;
            yield return Typing("서윤", "그렇게 말해줘서 고맙네. 근데 내 연주는 아직 부족해.", heroinSprite.Smile);
        }
        if (selectedChoiceIndex == 1)
        {
            likeability += 2;
            yield return Typing("서윤", "너, 돌려 말하는 거 잘 못하네. 근데 나쁘진 않네.", heroinSprite.Happy);
        }
        if (selectedChoiceIndex == 2)
        {
            likeability += 0;
            yield return Typing("서윤", "그렇구나. 뭐, 여기 머물러도 돼.", heroinSprite.Sulky);
        }

        yield return Typing("", "서윤은 살짝 미소를 짓고 비올라를 내려놓는다.", heroinSprite.Smile);
        yield return Typing("서윤", "가끔 이 시간에 여기 와. 오늘처럼.");
        yield return FadeOut();
        #endregion

        #region Day 2:
        SetBackground(backgroundSprite.BandPracticeRoom);
        SetHeroineImage(heroinSprite.Smile);
        yield return FadeIn();
        yield return Typing("", "나는 서윤의 초대를 받아 밴드부 연습실로 가게 된다.");
        yield return Typing("서윤", "왔네. 오늘은 좀 바쁠 거야. 신경 쓰지 말고 구경해.", heroinSprite.Happy);
        yield return Typing("", "서윤이 앰프를 조정하다가 어려움을 겪는 모습이 보인다.", heroinSprite.Difficult);
        yield return Choice(new string[]
        {
            "이런 건 나한테 맡겨! 내가 해결해 볼게.",
            "옆에서 지켜봐도 돼? 너한테 배우고 싶어서.",
            "너 하는 거 구경할게.",
        });
        if (selectedChoiceIndex == 0)
        {
            likeability += 2;
            yield return Typing("서윤", "오, 의외로 믿음직한데? 잘 부탁할게.", heroinSprite.Smile);
        }
        if (selectedChoiceIndex == 1)
        {
            likeability += 1;
            yield return Typing("서윤", "그럼 이건 이렇게 하는 거야. 잘 봐둬.", heroinSprite.Speak);
        }
        if (selectedChoiceIndex == 2)
        {
            likeability += 0;
            yield return Typing("서윤", "그래, 방해만 안 하면 돼.", heroinSprite.Speak);
        }
        yield return Typing("", "서윤은 비올라를 내려놓고 나를 바라본다.", heroinSprite.Smile);
        yield return Typing("서윤", "덕분에 오늘 연습 좀 잘 됐다. 네가 있어서 다행이네.");
        yield return FadeOut();
        #endregion

        #region Day 3:
        SetBackground(backgroundSprite.SchoolRooftop);
        SetHeroineImage(heroinSprite.Viola);
        yield return FadeIn();
        yield return Typing("", "서윤은 학교 옥상에서 비올라를 연주하며 혼자 시간을 보내고 있다.");
        yield return Typing("나", "여기 있었네. 뭔가 고민 있어 보여.");
        yield return Typing("서윤", "…넌 고민 같은 거 없니? 앞날이 막연하거나, 뭘 해야 할지 모르겠다는 그런 거.", heroinSprite.Down);
        yield return Choice(new string[]
        {
            "나도 그런 고민 많아. 근데 그냥 닥치는 대로 해보는 중이야.",
            "뭐든 너와 함께라면 해볼 수 있을 것 같은데.",
            "글쎄, 고민은 있는데 해결은 잘 안 돼.",
        });
        if (selectedChoiceIndex == 0)
        {
            likeability += 1;
            yield return Typing("서윤", "네 말도 맞아. 가끔은 부딪쳐 보는 게 답이겠지.", heroinSprite.Smile);
        }
        if (selectedChoiceIndex == 1)
        {
            likeability += 2;
            yield return Typing("서윤", "…넌 참 솔직하네. 그런 게 싫지 않아.", heroinSprite.Happy);
        }
        if (selectedChoiceIndex == 2)
        {
            likeability += 0;
            yield return Typing("서윤", "그렇구나. 모두 그런 걸까.", heroinSprite.Down);
        }

        yield return Typing("", "서윤은 의미심장한 미소를 짓는다.", heroinSprite.Smile);
        yield return Typing("서윤", "언젠가 나중에 다시 만나게 되면, 그땐 웃으며 얘기할 수 있길 바랄게.");
        yield return FadeOut();
        #endregion

        #region Ending
        if (likeability >= requiredLikeability)
        {
            SetBackground(null);
            SetHeroineImage(heroinSprite.Down);
            yield return FadeIn();
            yield return Typing("서윤", "사실, 내일이면 유학을 떠나. 네가 조금만 더 늦었으면 말하지 못했을 거야.", heroinSprite.Speak);
            yield return Typing("", "서윤은 내 손을 잡으며 약속한다.", heroinSprite.Smile);
            yield return Typing("서윤", "꼭 다시 만나자. 너와의 추억은 절대 잊지 않을게.", heroinSprite.Happy);
            yield return FadeOut();
            yield return EndingTitle("언젠가 다시, 새로운 시작을");
        }
        else
        {
            SetBackground(null);
            SetHeroineImage(null);
            yield return FadeIn();
            yield return Typing("", "나는 다음 날부터 서윤을 볼 수 없었다. 서윤이 유학 갔다는 소문만 들려온다.");
            yield return Typing("", "나는 뒤늦게 알지만, 연락이 닿지 않아 막막한 감정을 느낀다.");
            yield return EndingTitle("지나간 시간, 멈춘 기억");
        }
        #endregion
    }

    private void SetBackground(Sprite sprite)
    {
        backgroundImage.SetSprite(sprite);
    }

    private void SetHeroineImage(Sprite sprite)
    {
        heroineImage.SetSprite(sprite);
    }

    private IEnumerator FadeIn()
    {
        nameText.SetName("");
        dialogText.SetText("");
        yield return screenOverlay.FadeIn(1f);
    }

    private IEnumerator FadeOut()
    {
        yield return screenOverlay.FadeOut(1f);
    }

    private IEnumerator Choice(params string[] choices)
    {
        foreach (var choiceButton in choiceButtons)
            choiceButton.Hide();

        if (choices.Length == 0)
            yield break;

        for (int i = 0; i < choices.Length; i++)
            choiceButtons[i].Show(choices[i]);

        while (true)
        {
            yield return null;
            int i;
            for (i = 0; i < choices.Length && !choiceButtons[i].IsChoice; i++) ;
            if (i < choices.Length)
            {
                selectedChoiceIndex = i;
                break;
            }
        }

        foreach (var choiceButton in choiceButtons)
            choiceButton.Hide();

        //yield return Typing("나", choices[selectedChoiceIndex]);
    }

    private IEnumerator Typing(string name, string sentence)
    {
        if (name == "")
        {
            nameText.SetName("");
            yield return dialogText.Typing($"({sentence})", typingDelay);
        }
        else
        {
            nameText.SetName($"{name}:");
            yield return dialogText.Typing($"\"{sentence}\"", typingDelay);
        }
        yield return WaitProgress();
    }

    private IEnumerator Typing(string name, string sentence, Sprite heroineSprite)
    {
        SetHeroineImage(heroineSprite);
        yield return Typing(name, sentence);
    }

    private IEnumerator EndingTitle(string endingTitle)
    {
        nameText.SetName("");
        yield return dialogText.Typing($"End. \"{endingTitle}\"", typingDelay);
    }

    private IEnumerator WaitProgress()
    {
        yield return progressText.FadeIn(1f);
        yield return new WaitUntil(() => isAutoProgress || Input.GetMouseButton(0));
        yield return progressText.FadeOut(0f);
    }
}