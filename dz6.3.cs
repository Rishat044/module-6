using System;
using System.Collections.Generic;

public class Product : ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public object Clone()
    {
        return new Product(Name, Price, Quantity);
    }

    public override string ToString()
    {
        return $"{Name} - Цена: {Price}, Количество: {Quantity}";
    }
}

public class Discount : ICloneable
{
    public string Description { get; set; }
    public decimal Percentage { get; set; }

    public Discount(string description, decimal percentage)
    {
        Description = description;
        Percentage = percentage;
    }

    public object Clone()
    {
        return new Discount(Description, Percentage);
    }

    public override string ToString()
    {
        return $"{Description} - Скидка: {Percentage}%";
    }
}

public class Order : ICloneable
{
    public List<Product> Products { get; set; }
    public decimal DeliveryCost { get; set; }
    public Discount Discount { get; set; }
    public string PaymentMethod { get; set; }

    public Order(List<Product> products, decimal deliveryCost, Discount discount, string paymentMethod)
    {
        Products = products;
        DeliveryCost = deliveryCost;
        Discount = discount;
        PaymentMethod = paymentMethod;
    }

    public object Clone()
    {
        var clonedProducts = new List<Product>();
        foreach (var product in Products)
        {
            clonedProducts.Add((Product)product.Clone());
        }

        return new Order(clonedProducts, DeliveryCost, (Discount)Discount.Clone(), PaymentMethod);
    }

    public override string ToString()
    {
        string productsStr = string.Join("\n", Products);
        return $"Заказ:\nТовары:\n{productsStr}\nСтоимость доставки: {DeliveryCost}\n{Discount}\nМетод оплаты: {PaymentMethod}";
    }
}

class Program
{
    static void Main(string[] args)
    {

        Product product1 = new Product("Ноутбук", 50000, 1);
        Product product2 = new Product("Мышь", 1500, 2);

        Discount discount = new Discount("Скидка лояльного клиента", 10);

        Order originalOrder = new Order(new List<Product> { product1, product2 }, 500, discount, "Кредитная карта");

        Console.WriteLine("Оригинальный заказ:");
        Console.WriteLine(originalOrder);

        Order clonedOrder = (Order)originalOrder.Clone();

        clonedOrder.Products[0].Quantity = 2; // Изменяем количество первого товара
        clonedOrder.PaymentMethod = "Наличные";

        Console.WriteLine("\nКлонированный заказ (с изменениями):");
        Console.WriteLine(clonedOrder);

        Console.WriteLine("\nОригинальный заказ (без изменений):");
        Console.WriteLine(originalOrder);
    }
}
