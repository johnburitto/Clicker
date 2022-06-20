using System.Collections.Generic;

public class ElementalCoef
{
    public List<List<double>> ElementalCoefM;

    public static ElementalCoef Init
    {
        get { return new ElementalCoef(); }
    }

    public ElementalCoef()
    {
        ElementalCoefM = new List<List<double>>() { 
                                                    new List<double> { 0, 1.25, 1.75, 1.25 },
                                                    new List<double> { 1.25, 0, 1.25, 1.25 },
                                                    new List<double> { 1.75, 1.25, 0, 1.50 },
                                                    new List<double> { 1.25, 1.25, 1.50, 0 } };
    }
}
