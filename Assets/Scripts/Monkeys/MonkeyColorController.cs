using System;
using UnityEngine;

public static class MonkeyColorController
{
    private const float _alphaOnDrag = 0.5f;
    private const float _alphaOnScoreLack = 0.4f;

    public static Color MonkeyColorOnDrag = GetMonkeyColorOnDrag();
    public static Color MonkeyColorOnEndDrag = GetMonkeyColorOnEndDrag();
    public static Color CoverageAreaColorOnDrag = GetCoverageAreaColorOnDrag();
    public static Color CoverageAreaWithEnteredTriggersColorOnDrag = GetCoverageAreaWithEnteredTriggersColorOnDrag();

    private static Color GetMonkeyColorOnDrag()
    {
        return new Color(1, 1, 1, 1);
    }

    private static Color GetMonkeyColorOnEndDrag()
    {
        return new Color(1, 1, 1, 1);
    }

    private static Color GetCoverageAreaColorOnDrag()
    {
        Color color = Color.green;
        color.a = _alphaOnDrag;
        return color;
    }

    private static Color GetCoverageAreaWithEnteredTriggersColorOnDrag()
    {
        Color color = Color.red;
        color.a = _alphaOnDrag;
        return color;
    }
}
