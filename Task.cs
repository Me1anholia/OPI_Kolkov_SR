using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_3_OPI
{
    class Task
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

        public Task(int id, string title, string description)
        {
            TaskId = id;
            Title = title;
            Description = description;
            Completed = false;
        }

        // Метод створення завдання
        public void Create()
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                throw new Exception("Назва завдання не може бути порожньою!");
            }
            Console.WriteLine($"Завдання '{Title}' створено успішно.");
        }

        // Метод оновлення завдання
        public void Update(string newTitle, string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("Помилка: нова назва порожня.");
                return;
            }

            Title = newTitle;
            Description = newDescription;
            Console.WriteLine("Завдання оновлено.");
        }

        // Метод завершення завдання
        public void CompleteTask()
        {
            if (Completed)
            {
                Console.WriteLine("Завдання вже виконане.");
            }

            else
            {
                Completed = true;
                Console.WriteLine("Завдання позначено як виконане.");
            }
        }
    }

    class Timer
    {
        public int Duration { get; set; }
        public int RemainingTime { get; set; }
        public bool IsRunning { get; set; }

        public Timer(int duration)
        {
            if (duration <= 0)
            {
                throw new Exception("Тривалість таймера повинна бутибільшою за 0.");
            }

            Duration = duration;
            RemainingTime = duration;
            IsRunning = false;
        }

        // Метод запуску таймера
        public void Start()
        {
            if (IsRunning)
            {
                Console.WriteLine("Таймер вже запущений.");
                return;
            }

            IsRunning = true;
            Console.WriteLine("Таймер запущено:\n");

            while (RemainingTime > 0)
            {
                Console.WriteLine($"Залишилось: {RemainingTime} сек.");
                System.Threading.Thread.Sleep(1000);
                RemainingTime--;
            }

            IsRunning = false;
            NotificationService.SendSound();
            Console.WriteLine("Час завершився!");
        }

        // Метод паузи
        public void Pause()
        {
            if (!IsRunning)
            {
                Console.WriteLine("Таймер не запущений.");
            }

            else
            {
                IsRunning = false;
                Console.WriteLine("Таймер поставлено на паузу.");
            }
        }

        // Метод скидання
        public void Reset()
        {
            RemainingTime = Duration;
            IsRunning = false;
            Console.WriteLine("Таймер скинуто.");
        }
    }

    class Statistics
    {
        private List<int> sessions = new List<int>();

        // Метод додавання сесії
        public void AddSession(int minutes)
        {
            if (minutes <= 0)
            {
                Console.WriteLine("Помилка: тривалість повинна бутидодатною.");
                return;
            }

            sessions.Add(minutes);
            Console.WriteLine("Сесію додано до статистики.");
        }

        // Метод підрахунку середнього часу
        public double GetAverageTime()
        {
            if (sessions.Count == 0)
            {
                return 0;
            }

            int sum = 0;
            foreach (int session in sessions)
            {
                sum += session;
            }
            return (double)sum / sessions.Count;
        }

        // Метод виведення статистики
        public void ShowStatistics()
        {
            Console.WriteLine("\nСтатистика сесій:");

            for (int i = 0; i < sessions.Count; i++)
            {
                Console.WriteLine($"Сесія {i + 1}: {sessions[i]} хв.");
            }

            Console.WriteLine($"Середній час: {GetAverageTime()} хв.");
        }
    }
    class NotificationService
    {
        public static void SendSound()
        {
            Console.Beep();
        }
    }
}
