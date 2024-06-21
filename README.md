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
