using System;

public class DefaultAnimator : LayoutAnimator {

    public override void Show(Action complete)
    {
        gameObject.SetActive(true);
        complete?.Invoke();
    }

    public override void Hide(Action complete)
    {
        complete?.Invoke();
        gameObject.SetActive(false);
    }
}
