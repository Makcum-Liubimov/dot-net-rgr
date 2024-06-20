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
