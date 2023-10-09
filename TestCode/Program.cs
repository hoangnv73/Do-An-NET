static void Main(string[] args)
{
    Console.WriteLine(SumDiscount(100, 10));
    Console.ReadKey();
}

 static double SumDiscount(double price, double? discount)
{
    if (discount != null)
    {
        var sum = price - (price / 100 * discount) ?? 0;

        return sum;
    }

    return price;


}