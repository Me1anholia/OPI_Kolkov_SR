using Lab_3_OPI;
using NUnit.Framework;
using System;

namespace Lab_3_OPI
{
    [TestFixture]
    public class TaskTests
    {
        // AAA: Arrange, Act, Assert
        // Техніка: EP (позитивний)
        [Test]
        public void Create_ValidTask_TitleShouldBeCreated()
        {
            // Arrange
            Task task = new Task(1, "ЛР 03", "Реалізувати модуль");
            // Act & Assert
            Assert.DoesNotThrow(() => task.Create());
        }
        // AAA: Arrange, Act, Assert
        // Техніка: EP (негативний)
        [Test]
        public void Create_EmptyTitle_ShouldThrowException()
        {
            // Arrange
            Task task = new Task(1, "", "Опис");
            // Act & Assert
            Assert.Throws<Exception>(() => task.Create());
        }
        // AAA: Arrange, Act, Assert
        // Техніка: EP (позитивний)
        [Test]
        public void CompleteTask_TaskShouldBecomeCompleted()
        {
            // Arrange
            Task task = new Task(1, "Тест", "Опис");
            // Act
            task.CompleteTask();
            // Assert
            Assert.That(task.Completed);
        }
    }
    [TestFixture]
    public class TimerTests
    {
        // AAA: Arrange, Act, Assert
        // Техніка: BVA (граничне значення)
        [Test]
        public void Timer_DurationEqualsOne_ShouldCreateTimer()
        {
            // Arrange, Act
            Timer timer = new Timer(1);
            // Assert
            Assert.That(timer.Duration, Is.EqualTo(1));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: BVA (негативний)
        [Test]
        public void Timer_DurationZero_ShouldThrowException()
        {
            // Arrange, Act & Assert
            Assert.Throws<Exception>(() => new Timer(0));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: EP (позитивний)
        [Test]
        public void Reset_ShouldRestoreRemainingTime()
        {
            // Arrange
            Timer timer = new Timer(10);
            // Act
            timer.Reset();
            // Assert
            Assert.That(timer.RemainingTime, Is.EqualTo(10));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: ЕР (позитивний)
        [Test]
        public void Start_ShouldRunTimerUntilTimeExpires()
        {
            // Arrange
            Timer timer = new Timer(1); // 1 секунда, щоб тест відпрацював швидко

            // Act
            timer.Start();

            // Assert
            Assert.That(timer.IsRunning, Is.False);
            Assert.That(timer.RemainingTime, Is.EqualTo(0));
        }

        // AAA: Arrange, Act, Assert
        // Техніка: ЕР (негативний)
        [Test]
        public void Start_AlreadyRunningTimer_ShouldReturnEarly()
        {
            // Arrange
            Timer timer = new Timer(5);
            timer.IsRunning = true; // Штучно виставляємо, ніби він уже працює

            // Act & Assert
            Assert.DoesNotThrow(() => timer.Start());
        }

        // AAA: Arrange, Act, Assert
        // Техніка: ЕР (позитивний)
        [Test]
        public void Pause_RunningTimer_ShouldStopTimer()
        {
            // Arrange
            Timer timer = new Timer(5);
            timer.IsRunning = true;

            // Act
            timer.Pause();

            // Assert
            Assert.That(timer.IsRunning, Is.False);
        }

        // AAA: Arrange, Act, Assert
        // Техніка: ЕР (негативний)
        [Test]
        public void Pause_NotRunningTimer_ShouldDoNothing()
        {
            // Arrange
            Timer timer = new Timer(5); // IsRunning за замовчуванням false

            // Act
            timer.Pause();

            // Assert
            Assert.That(timer.IsRunning, Is.False);
        }
    }
    [TestFixture]
    public class StatisticsTests
    {
        // AAA: Arrange, Act, Assert
        // Техніка: EP (позитивний)
        [Test]
        public void AddSession_ValidSession_ShouldAddData()
        {
            // Arrange
            Statistics stats = new Statistics();
            // Act
            stats.AddSession(25);
            // Assert
            Assert.That(stats.GetAverageTime(), Is.EqualTo(25));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: BVA (граничне значення)
        [Test]
        public void GetAverageTime_NoSessions_ShouldReturnZero()
        {
            // Arrange
            Statistics stats = new Statistics();
            // Act
            double result = stats.GetAverageTime();
            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: EP (негативний)
        [Test]
        public void AddSession_NegativeValue_ShouldNotAddSession()
        {
            // Arrange
            Statistics stats = new Statistics();
            // Act
            stats.AddSession(-5);
            // Assert
            Assert.That(stats.GetAverageTime(), Is.EqualTo(0));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: ЕР (позитивний) 
        [Test]
        public void Update_ValidData_ShouldUpdateTitleAndDescription()
        {
            // Arrange
            Task task = new Task(1, "Стара назва", "Старий опис");
            string newTitle = "Нова ЛР";
            string newDescription = "Реалізувати програмний модуль";

            // Act
            task.Update(newTitle, newDescription);
            // Assert
            Assert.That(task.Title, Is.EqualTo(newTitle));
            Assert.That(task.Description, Is.EqualTo(newDescription));
        }
        // AAA: Arrange, Act, Assert
        // Техніка: BVA (граничне значення, негативний тест)
        [Test]
        public void AddSession_ZeroMinutes_ShouldNotAddSession()
        {
            // Arrange
            Statistics stats = new Statistics();

            // Act
            stats.AddSession(0);

            // Assert
            Assert.That(stats.GetAverageTime(), Is.EqualTo(0));
        }
    }
}