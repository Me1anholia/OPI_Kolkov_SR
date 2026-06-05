using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Lab_3_OPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // 1. Обязательный endpoint для проверки
            app.MapGet("/health", () => new { status = "ok", message = "API is running perfectly" });

            // 2. Endpoint для Task
            app.MapPost("/api/task", (TaskInput data) => 
            {
                try 
                {
                    Task task = new Task(data.Id, data.Title, data.Description);
                    task.Create();
                    task.Update(data.Title + " (Оновлено через API)", "Виконано рефакторинг");
                    task.CompleteTask();

                    return Results.Ok(new { 
                        status = "success", 
                        message = $"Задача '{data.Title}' успішно оброблена." 
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // 3. Endpoint для Timer
            app.MapPost("/api/timer", (TimerInput data) => 
            {
                try
                {
                    Timer timer = new Timer(data.DurationMinutes);
                    timer.Start(); 

                    return Results.Ok(new { 
                        status = "success", 
                        message = $"Таймер на {data.DurationMinutes} хвилин запущено." 
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // 4. Endpoint для Statistics
            app.MapPost("/api/statistics", (StatsInput data) => 
            {
                try
                {
                    Statistics stats = new Statistics();
                    foreach(var session in data.Sessions)
                    {
                        stats.AddSession(session);
                    }
                    stats.ShowStatistics(); 

                    return Results.Ok(new { 
                        status = "success", 
                        sessions_added = data.Sessions.Count 
                    });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { error = ex.Message });
                }
            });

            // Настройка порта
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
            app.Urls.Add($"http://*:{port}");

            app.Run();
        }
    }

    // Модели данных оставляем внутри namespace, но вне класса Program
    public record TaskInput(int Id, string Title, string Description);
    public record TimerInput(int DurationMinutes);
    public record StatsInput(List<int> Sessions);
}