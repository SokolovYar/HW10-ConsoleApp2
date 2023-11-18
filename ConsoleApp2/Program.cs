using System.Runtime.Serialization.DataContracts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;

List<Fraction> Fractions = new List<Fraction>();
int FractionSize = 3;

//1. Ввод массива дробей с клавиатуры
for (int i = 0; i < FractionSize; i++)
{
    Console.WriteLine($"Initializing the {i+1} fraction from {FractionSize}");
    Fractions.Add(Fraction.CreateFraction());
}

//2. Сериализация массива дробей
//3. Сохранение сериализованного массива в файл
using (FileStream file = new FileStream("FractionList.json", FileMode.OpenOrCreate))
{
    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Fraction>));
    serializer.WriteObject(file, Fractions);
}


//4. Загрузка сериализованного массива из файла. После загрузки нужно произвести десериализацию
// Можно было сделать в одном потоке, но вывел отдельно для наглядности
List<Fraction> ? DeserializedFractions;
using (FileStream anotherFileStream = new FileStream("FractionList.json", FileMode.Open))
{
    DataContractJsonSerializer  serializer = new DataContractJsonSerializer(typeof(List<Fraction>));
    DeserializedFractions  =  (List < Fraction > ?) serializer.ReadObject(anotherFileStream);
}
Console.WriteLine("\n\nDeserialized List");
foreach (Fraction fraction in Fractions)
    Console.WriteLine(fraction);


[Serializable]
[DataContract]
public class Fraction
{
    [DataMember]
    public int Nom {  get; set; }
    [DataMember]
    public int Denom { get; set; }

    public Fraction(int nom, int denom)
    {
        if (denom == 0) throw new DivideByZeroException("Denominator can`t be a ZERO!");
        Nom = nom;
        Denom = denom;
    }
    public Fraction() 
    {
        Nom = 1;
        Denom = 1;
    }
    public override string ToString()
    {
        return $"{Nom}/{Denom}";
    }

    public static Fraction CreateFraction(int nom, int denom)
    {
        return new Fraction(nom, denom);
    }
    public static Fraction CreateFraction()
    {
        int _nom, _denom;
        bool flag = true;
        Console.Write("Enter the nominator: ");
        do
        {
            flag = true;
            if (!int.TryParse(Console.ReadLine(), out _nom))
            {
                Console.Write("Wrong input! Enter the nominator again: ");
                flag = false;
            }
         } while (!flag);

        Console.Write("Enter the denominator: ");
        do
        {
            flag = true;
            if (!int.TryParse(Console.ReadLine(), out _denom))
            {
                Console.Write("Wrong input! Enter the denominator again: ");
                flag = false;
            }
        } while (!flag);
        return new Fraction(_nom, _denom);
    }
}