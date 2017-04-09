using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColors {
  public Dictionary<string, Color> ColorMap = new Dictionary<string, Color>();
  private List<string> colorList = new List<string>();

  public BallColors () {
    ColorMap.Add("Yellow", GameColor.GargoyleGas);
    ColorMap.Add("Green", GameColor.SheenGreen);
    ColorMap.Add("Red", GameColor.Coquelicot);
    ColorMap.Add("Blue", GameColor.Vividskyblue);
    colorList.AddRange(ColorMap.Keys);
  }

  public string GetRandomColor() {
    return colorList[Random.Range(0, colorList.Count)];
  }
}
