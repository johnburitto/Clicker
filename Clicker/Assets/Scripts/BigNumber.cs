using Assets.Extensions;
using System;
using UnityEngine;

[Serializable]
public class BigNumber: IEquatable<BigNumber>, IEquatable<float>
{
    [SerializeField] private float _number;
    [SerializeField] private NumberScale _numberScale;

    private const string TRIPLE_ZERO = "000";

    public float Number
    {
        get { return _number; }
    }

    public int NumberScale
    {
        get { return (int)_numberScale; }
    }

    public string StringInDecimalFormat
    {
        get { return ToStringInDecimalFormat(); }
    }

    public static BigNumber Init
    {
        get { return new BigNumber(); }
    }

    public bool IsZero
    {
        get { return _number <= 0 && _numberScale == global::NumberScale.Units; }
    }

    public bool IsNotZero
    {
        get { return !IsZero; }
    }

    private string Zeros
    {
        get { return TRIPLE_ZERO.Repeat((int)_numberScale - 1); }
    }

    private BigNumber()
    {
        _number = 0;
        _numberScale = global::NumberScale.Units;
    }

    private BigNumber(float number, NumberScale numberScale)
    {
        _number = number;
        _numberScale = numberScale;
    }

    public static BigNumber ValueOf(float number, NumberScale numberScale)
    {
        if (number < 0)
        {
            throw new Exception("Sorry, BigNumbers works only with positive");
        }

        BigNumber newBigNumber = new BigNumber(number, numberScale);

        scaleToLess(newBigNumber);

        return newBigNumber;
    }

    public static BigNumber ValueOf(float number)
    {
        if (number < 0)
        {
            throw new Exception("Sorry, BigNumbers works only with positive");
        }


        BigNumber newBigNumber = new BigNumber(number, global::NumberScale.Units);

        scaleToLess(newBigNumber);

        return newBigNumber;
    }

    public static BigNumber ValueOf(BigNumber number)
    {
        return new BigNumber(number._number, number._numberScale);
    }

    private string ToStringInDecimalFormat()
    {
        if (_numberScale == global::NumberScale.Units)
        {
            return _number.ToString();
        }

        return (Math.Round(_number, 3) * 1000f).ToString() + Zeros;
    }

    public static BigNumber ScaleTo(BigNumber number, NumberScale newNumberScale)
    {
        BigNumber newNumber = ValueOf(number);

        newNumber._number *= (float)Math.Pow(1000, newNumber._numberScale - newNumberScale);
        newNumber._numberScale = newNumberScale;

        return newNumber;
    }

    private static void scaleToLess(BigNumber number)
    {
        while (number._number >= 1000)
        {
            number._number /= 1000;
            number._numberScale += 1;
        }
    }

    private static void scaleToUpper(BigNumber number)
    {
        if (number._number < 0)
        {
            number = ValueOf(0);

            return;
        }

        while (0 < number._number && number._number < 1)
        {
            number._number *= 1000;
            number._numberScale -= 1;
        }
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public bool Equals(BigNumber other)
    {
        return this == other;
    }

    public bool Equals(float other)
    {
        return this == ValueOf(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{_number} {_numberScale}";
    }

    public static BigNumber operator + (BigNumber left, BigNumber right)
    {
        BigNumber newNumber = ValueOf(left);
        BigNumber numberToAdd = ScaleTo(right, left._numberScale);

        newNumber._number += numberToAdd._number;
        scaleToLess(newNumber);

        return newNumber;
    }
    
    public static BigNumber operator - (BigNumber left, BigNumber right)
    {
        BigNumber newNumber = ValueOf(left);
        BigNumber numberToAdd = ScaleTo(right, left._numberScale);

        newNumber._number -= numberToAdd._number;
        scaleToUpper(newNumber);

        return newNumber;
    }

    public static BigNumber operator * (BigNumber left, float right)
    {
        if (right < 0)
        {
            throw new Exception("Sorry, BigNumbers works only with positive");
        }

        BigNumber newNumber = ValueOf(left);

        newNumber._number *= right;
        scaleToLess(newNumber);

        return newNumber;
    }

    public static BigNumber operator / (BigNumber left, float right)
    {
        if (right == 0)
        {
            throw new Exception("You cant divsion on 0");
        }

        BigNumber newNumber = ValueOf(left);

        newNumber._number /= right;
        scaleToUpper(newNumber);

        return newNumber;
    }

    public static bool operator <(BigNumber left, BigNumber right)
    {
        return (left._number < right._number && left._numberScale == right._numberScale) || 
               (left._numberScale < right._numberScale);
    }
    
    public static bool operator >(BigNumber left, BigNumber right)
    {
        return (left._number > right._number && left._numberScale == right._numberScale) || 
               (left._numberScale > right._numberScale);
    }
    
    public static bool operator <(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return (left._number < compareWith._number && left._numberScale == compareWith._numberScale) || 
               (left._numberScale < compareWith._numberScale);
    }
    
    public static bool operator >(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return (left._number > compareWith._number && left._numberScale == compareWith._numberScale) || 
               (left._numberScale > compareWith._numberScale);
    }    
    
    public static bool operator <=(BigNumber left, BigNumber right)
    {
        return (left._number <= right._number && left._numberScale == right._numberScale) || 
               (left._numberScale < right._numberScale);
    }
    
    public static bool operator >=(BigNumber left, BigNumber right)
    {
        return (left._number >= right._number && left._numberScale == right._numberScale) || 
               (left._numberScale > right._numberScale);
    }
    
    public static bool operator <=(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return (left._number <= compareWith._number && left._numberScale == compareWith._numberScale) || 
               (left._numberScale < compareWith._numberScale);
    }
    
    public static bool operator >=(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return (left._number >= compareWith._number && left._numberScale == compareWith._numberScale) || 
               (left._numberScale > compareWith._numberScale);
    }
    
    public static bool operator ==(BigNumber left, BigNumber right)
    {
        return left._number == right._number && left._numberScale == right._numberScale;
    }

    public static bool operator !=(BigNumber left, BigNumber right)
    {
        return left._number != right._number || left._numberScale != right._numberScale;
    }
    
    public static bool operator ==(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return left._number == compareWith._number && left._numberScale == compareWith._numberScale;
    }

    public static bool operator !=(BigNumber left, float right)
    {
        BigNumber compareWith = ValueOf(right);

        return left._number != compareWith._number || left._numberScale != compareWith._numberScale;
    }
}

public enum NumberScale
{
    Units,
    Thousands,
    Millions,
    Billions,
    Trillions,
    Quadrillions,
    Quintillion,
    Sextillion, // Тригер для Прохорова :)
    Septillion,
    Octillion,
    Nonillion,
    Decillion,
    Undecillion,
    Duodecillion,
    Tredecillion,
    Quattuordecillion,
    Quindecillion,
    Sexdecillion, // Ще один :)
    Septendecillion,
    Octodecillion,
    Novemdecillion,
    Vigintillion
}
