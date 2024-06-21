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


