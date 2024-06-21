# dot-net-labs-rgr

# C# Design Patterns

## Factory Method

### UML Diagram

```mermaid
classDiagram
    class Product {
        <<abstract>>
        +Operation() : void
    }

    class ConcreteProductA {
        +Operation() : void
    }

    class ConcreteProductB {
        +Operation() : void
    }

    class Creator {
        <<abstract>>
        +FactoryMethod() : Product
        +SomeOperation() : void
    }

    class ConcreteCreatorA {
        +FactoryMethod() : Product
    }

    class ConcreteCreatorB {
        +FactoryMethod() : Product
    }

    Product <|-- ConcreteProductA
    Product <|-- ConcreteProductB

    Creator <|-- ConcreteCreatorA
    Creator <|-- ConcreteCreatorB

    Creator --> Product : FactoryMethod()
```
### factory-methode c# code
```csharp
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

```
## flyweight

### UML Diagram

```mermaid
classDiagram
    class Flyweight {
        <<abstract>>
        +Operation(extrinsicState : int) : void
    }

    class ConcreteFlyweight {
        -intrinsicState : string
        +Operation(extrinsicState : int) : void
    }

    class UnsharedConcreteFlyweight {
        -allState : string
        +Operation(extrinsicState : int) : void
    }

    class FlyweightFactory {
        -flyweights : Dictionary~string, Flyweight~
        +GetFlyweight(key : string) : Flyweight
        +ListFlyweights() : void
    }

    Flyweight <|-- ConcreteFlyweight
    Flyweight <|-- UnsharedConcreteFlyweight
    FlyweightFactory --> Flyweight : GetFlyweight()
```
### flyweight c# code
```csharp
using System;
using System.Collections.Generic;

// Flyweight
public abstract class Flyweight
{
    public abstract void Operation(int extrinsicState);
}

// ConcreteFlyweight
public class ConcreteFlyweight : Flyweight
{
    private string intrinsicState;

    public ConcreteFlyweight(string intrinsicState)
    {
        this.intrinsicState = intrinsicState;
    }

    public override void Operation(int extrinsicState)
    {
        Console.WriteLine($"ConcreteFlyweight: Intrinsic State = {intrinsicState}, Extrinsic State = {extrinsicState}");
    }
}

// UnsharedConcreteFlyweight
public class UnsharedConcreteFlyweight : Flyweight
{
    private string allState;

    public UnsharedConcreteFlyweight(string allState)
    {
        this.allState = allState;
    }

    public override void Operation(int extrinsicState)
    {
        Console.WriteLine($"UnsharedConcreteFlyweight: All State = {allState}, Extrinsic State = {extrinsicState}");
    }
}

// FlyweightFactory
public class FlyweightFactory
{
    private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

    public Flyweight GetFlyweight(string key)
    {
        if (!flyweights.ContainsKey(key))
        {
            flyweights[key] = new ConcreteFlyweight(key);
        }
        return flyweights[key];
    }

    public void ListFlyweights()
    {
        Console.WriteLine($"FlyweightFactory: {flyweights.Count} flyweights in total.");
        foreach (var key in flyweights.Keys)
        {
            Console.WriteLine(key);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var factory = new FlyweightFactory();

        // Using shared flyweights
        var flyweight1 = factory.GetFlyweight("State1");
        var flyweight2 = factory.GetFlyweight("State2");
        var flyweight3 = factory.GetFlyweight("State1");

        flyweight1.Operation(1);
        flyweight2.Operation(2);
        flyweight3.Operation(3);

        factory.ListFlyweights();

        // Using unshared flyweight
        var unsharedFlyweight = new UnsharedConcreteFlyweight("Unshared State");
        unsharedFlyweight.Operation(4);
    }
}

```
## memento
### UML Diagram
```mermaid
classDiagram
    class Memento {
        +State : string
    }

    class Originator {
        -State : string
        +CreateMemento() : Memento
        +RestoreMemento(memento : Memento) : void
    }

    class Caretaker {
        -mementos : List~Memento~
        -originator : Originator
        +SaveState() : void
        +Undo() : void
        +ShowHistory() : void
    }

    Originator --> Memento : CreateMemento()
    Originator --> Memento : RestoreMemento()
    Caretaker --> Originator : SaveState()
    Caretaker --> Originator : Undo()
    Caretaker --> Originator : ShowHistory()

```
### memento c# code
```csharp
using System;
using System.Collections.Generic;

// Memento
public class Memento
{
    public string State { get; private set; }

    public Memento(string state)
    {
        State = state;
    }
}

// Originator
public class Originator
{
    public string State { get; set; }

    public Memento CreateMemento()
    {
        return new Memento(State);
    }

    public void RestoreMemento(Memento memento)
    {
        State = memento.State;
    }
}

// Caretaker
public class Caretaker
{
    private List<Memento> mementos = new List<Memento>();
    private Originator originator;

    public Caretaker(Originator originator)
    {
        this.originator = originator;
    }

    public void SaveState()
    {
        mementos.Add(originator.CreateMemento());
    }

    public void Undo()
    {
        if (mementos.Count > 0)
        {
            var memento = mementos[mementos.Count - 1];
            originator.RestoreMemento(memento);
            mementos.RemoveAt(mementos.Count - 1);
        }
    }

    public void ShowHistory()
    {
        Console.WriteLine("History of states:");
        foreach (var memento in mementos)
        {
            Console.WriteLine(memento.State);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var originator = new Originator();
        var caretaker = new Caretaker(originator);

        originator.State = "State1";
        caretaker.SaveState();

        originator.State = "State2";
        caretaker.SaveState();

        originator.State = "State3";

        Console.WriteLine($"Current State: {originator.State}");

        caretaker.Undo();
        Console.WriteLine($"Restored State: {originator.State}");

        caretaker.Undo();
        Console.WriteLine($"Restored State: {originator.State}");

        caretaker.ShowHistory();
    }
}

```
## thread-pool

### UML diagram
```mermaid
classDiagram
    class Program {
        +Main(args : string[]) : void
        +Task(state : object) : void
    }

    Program ..> ThreadPool : uses

```

### thread-pool c# code
```csharp
using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Максимальна кількість потоків у пулі
        ThreadPool.SetMaxThreads(5, 5);

        // Виконання 10 завдань у пулі потоків
        for (int i = 0; i < 10; i++)
        {
            int taskNumber = i;
            ThreadPool.QueueUserWorkItem(Task, taskNumber);
        }

        // Зачекати, поки всі завдання завершаться
        Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }

    static void Task(object state)
    {
        int taskNumber = (int)state;
        Console.WriteLine($"Завдання {taskNumber} розпочато у потоці {Thread.CurrentThread.ManagedThreadId}");
        
        // Імітація тривалої операції
        Thread.Sleep(1000);
        
        Console.WriteLine($"Завдання {taskNumber} завершено у потоці {Thread.CurrentThread.ManagedThreadId}");
    }
}

```
