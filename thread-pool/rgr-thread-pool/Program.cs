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
        Console.WriteLine("Натиснiть будь-яку клавiшу для виходу...");
        Console.ReadKey();
    }

    static void Task(object state)
    {
        int taskNumber = (int)state;
        Console.WriteLine($"Завдання {taskNumber} розпочато у потоцi {Thread.CurrentThread.ManagedThreadId}");

        // Імітація тривалої операції
        Thread.Sleep(1000);

        Console.WriteLine($"Завдання {taskNumber} завершено у потоцi {Thread.CurrentThread.ManagedThreadId}");
    }
}
