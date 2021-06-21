using System;
using System.Collections.Generic;

namespace ch_18_sample_KM
{
    class Program
    {
        static void Main(string[] args)
        {

            List<IProduct> products = new List<IProduct>
            {
                new Book(){ Title="坊っちゃん", Date= new DateTime(2021,5,1,10,0,0) },
                new Vegetable(){ Name="かぼちゃ", Date = new DateTime(2021,5,2,18,0,0) },
                new Book(){ Title="吾輩は猫である", Date= new DateTime(2021,5,3,12,0,0) },
                new Book(){ Title="三四郎", Date = new DateTime(2021,5,5,11,0,0) },
                new Vegetable(){ Name="キャベツ", Date= new DateTime(2021,5,6,9,30,0) },
                new Vegetable(){ Name="玉ねぎ", Date = new DateTime(2021,5,20,17,0,0) },
                new Book(){ Title="こころ", Date= new DateTime(2021,5,23,12,0,0) },
                new Vegetable(){ Name="白菜", Date = new DateTime(2021,5,29,10,30,0) },
                new Vegetable(){ Name="なす", Date= new DateTime(2021,6,1,8,0,0) },
                new Book(){ Title="トロッコ", Date = new DateTime(2021,6,2,18,0,0) },
            };

            var history = new PurchaseHistory<IProduct>();
            history.PrintHistory(products);
        }
    }

    public interface IProduct
    {
        void Print();
    }

    public class Book : IProduct
    {

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public void Print()
        {
            Console.WriteLine($"product type: book  product: {Title}  shopping: {Date.ToString("yyyy/MM/dd HH:mm:ss")}");
        }
    }

    public class Vegetable : IProduct
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public void Print()
        {
            Console.WriteLine($"product type: vegetable  product: {Name}  shopping: {Date.ToString("yyyy/MM/dd HH:mm:ss")}");
        }
    }

    public class PurchaseHistory<T> where T : IProduct
    {
        public void PrintHistory(List<T> products)
        {
            foreach (var item in products)
            {
                item.Print();
            }
        }
    }
}
