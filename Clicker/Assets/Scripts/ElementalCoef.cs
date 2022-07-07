using System.Collections.Generic;

public class ElementalCoef
{
    public List<List<float>> _elementalCoefMatrix;

    public static ElementalCoef Init => new ElementalCoef();

    public float this[int i, int j] => _elementalCoefMatrix[i][j];

    public ElementalCoef()
    {
        _elementalCoefMatrix = new List<List<float>>() { 
                                                    new List<float> { 0, 1.25f, 1.75f, 1.25f },
                                                    new List<float> { 1.25f, 0, 1.25f, 1.25f },
                                                    new List<float> { 1.75f, 1.25f, 0, 1.50f },
                                                    new List<float> { 1.25f, 1.25f, 1.50f, 0 } };
    }
}
