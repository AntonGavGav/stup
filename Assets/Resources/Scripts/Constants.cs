using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class Constants
{
    public enum PgColor
    {
        WHITE,
        BLACK
    }

    public static Dictionary<PgColor, MaterialSet> colors { get; set; } = new Dictionary<PgColor, MaterialSet>()
    {
        {
            PgColor.BLACK, new MaterialSet
            {
                primary = UnityEngine.Resources.Load<Material>("Coloring/Materials/PigeonMat/PigeonMat"),
                secondary = UnityEngine.Resources.Load<Material>("Coloring/Materials/PigeonMat/PigeonMatRed")
            }
        },
        {
            PgColor.WHITE, new MaterialSet
            {
                primary = UnityEngine.Resources.Load<Material>("Coloring/Materials/PigeonMat/PigeonWhiteMat"),
                secondary = UnityEngine.Resources.Load<Material>("Coloring/Materials/PigeonMat/PigeonWhiteMatRed")
            }
        }
    };



    public static String[] PgNames { get; set; } =
    {
        "Picasso", "Puerto", "Rico", "Potato", "Martini", "Martha", "Rio", "Julia", "Gogi", "Aero", "Joni", "August", "October", "Bit", "Shpun", "Lucinda", "Poly",
        "Moli", "Holi", "Heartbumper", "Jonesa", "Joshua", "Kleinder", "Crocs", "Shaa", "Barton", "Lewis"
    };
}
