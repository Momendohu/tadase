using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIGroup : MonoBehaviour {
    public void OnPushRankingButton () {
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushRankingButton ();
    }

    public void OnPushGameRestartButton () {
        AudioManager.Instance.FadeOutBGM ("result");
        AudioManager.Instance.PlaySE ("ohyear", 0.5f);
        UIManager.Instance.ShowTransitionBackground ();
        UIManager.Instance.TransitionIn (() => UIManager.Instance.OnPushGameRestartButton ());
    }

    public void OnPushGameTitleButton () {
        AudioManager.Instance.FadeOutBGM ("result");
        AudioManager.Instance.PlaySE ("button01");
        UIManager.Instance.OnPushTitleButton ();
    }

    public void OnPushTweenButton () {
        AudioManager.Instance.PlaySE ("button01");
        naichilab.UnityRoomTweet.Tweet (
            "extreme-tadashi",
            string.Format ("えっ！「{0}ただし」だって！？そいつぁエクストリームだ！", Model.Instance.hiScore),
            "unityroom",
            "unity1week",
            "エクストリームただし"
        );
    }
}