using System;

// Продукт
public abstract class Product
{
    public abstract void Operation();
}

// Конкретний продукт A
public class ConcreteProductA : Product
{
    public override void Operation()
    {
        Console.WriteLine("Operation of ConcreteProductA");
    }
}

// Конкретний продукт B
public class ConcreteProductB : Product
{
    public override void Operation()
    {
        Console.WriteLine("Operation of ConcreteProductB");
    }
}

// Створювач
public abstract class Creator
{
    public abstract Product FactoryMethod();

    public void SomeOperation()
    {
        // Використання фабричного методу для створення продукту
        var product = FactoryMethod();
        product.Operation();
    }
}

// Конкретний створювач A
public class ConcreteCreatorA : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductA();
    }
}

// Конкретний створювач B
public class ConcreteCreatorB : Creator
{
    public override Product FactoryMethod()
    {
        return new ConcreteProductB();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Використання конкретного створювача A
        Creator creatorA = new ConcreteCreatorA();
        creatorA.SomeOperation();

        // Використання конкретного створювача B
        Creator creatorB = new ConcreteCreatorB();
        creatorB.SomeOperation();
    }
}
