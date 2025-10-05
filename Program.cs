using System;

namespace pr2_PCS
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ПРАКТИЧЕСКАЯ РАБОТА 2 ===");
                Console.WriteLine("Выберите задание (1-7):");
                Console.WriteLine("1 - Ряды");
                Console.WriteLine("2 - Счастливый билет");
                Console.WriteLine("3 - Сокращение дроби");
                Console.WriteLine("4 - Угадай число");
                Console.WriteLine("5 - Кофейный аппарат");
                Console.WriteLine("6 - Лабораторный опыт");
                Console.WriteLine("7 - Колонизация Марса");
                Console.WriteLine("0 - Выход");
                Console.Write("Ваш выбор: ");
                
                string choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1": Task1.Run(); break;
                    case "2": Task2.Run(); break;
                    case "3": Task3.Run(); break;
                    case "4": Task4.Run(); break;
                    case "5": Task5.Run(); break;
                    case "6": Task6.Run(); break;
                    case "7": Task7.Run(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }
                
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    public static class Task1
{
    public static void Run()
    {
        Console.WriteLine("\n=== Задание 1: Ряды ===");
        Console.WriteLine("Формула: arctg(x) = x - x³/3 + x⁵/5 - x⁷/7 + ... + (-1)ⁿ * x²ⁿ⁺¹/(2n+1)");
        Console.WriteLine("Область сходимости: |x| ≤ 1");
        
        try
        {
            Console.Write("Введите x (|x| ≤ 1): ");
            double x = double.Parse(Console.ReadLine());

            if (Math.Abs(x) > 1)
            {
                Console.WriteLine("Ошибка: |x| должен быть ≤ 1 для сходимости ряда");
                return;
            }

            Console.Write("Введите точность (e < 0.01): ");
            double epsilon = double.Parse(Console.ReadLine());
            
            if (epsilon >= 0.01)
            {
                Console.WriteLine("Точность должна быть меньше 0.01");
                return;
            }

            double result = CalculateArctgSeries(x, epsilon);
            Console.WriteLine($"Значение arctg({x}) с точностью {epsilon}: {result:F6}");
            Console.WriteLine($"Проверка через Math.Atan: {Math.Atan(x):F6}");

            Console.Write("\nВведите номер члена ряда (n, начиная с 0): ");
            int n = int.Parse(Console.ReadLine());
            double nthTerm = GetNthTermArctg(x, n);
            Console.WriteLine($"Значение {n}-го члена ряда: {nthTerm:E6}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // Функция для вычисления n-го члена ряда arctg(x)
    static double GetNthTermArctg(double x, int n)
    {
        // Формула: (-1)ⁿ * x²ⁿ⁺¹/(2n+1)
        double sign = (n % 2 == 0) ? 1 : -1; // (-1)ⁿ
        double numerator = Math.Pow(x, 2 * n + 1); // x²ⁿ⁺¹
        double denominator = 2 * n + 1; // (2n+1)
        
        return sign * numerator / denominator;
    }

    // Функция для вычисления суммы ряда arctg(x) с заданной точностью
    static double CalculateArctgSeries(double x, double epsilon)
    {
        double sum = 0;
        double term;
        int n = 0;

        do
        {
            term = GetNthTermArctg(x, n);
            sum += term;
            n++;

            // Защита от бесконечного цикла
            if (n > 1000) 
            {
                Console.WriteLine("Достигнуто максимальное количество итераций (1000)");
                break;
            }

        } while (Math.Abs(term) > epsilon);

        Console.WriteLine($"Вычислено за {n} итераций");
        return sum;
    }
}

    public static class Task2
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Задание 2: Счастливый билет ===");
            
            try
            {
                Console.Write("Введите шестизначный номер билета: ");
                string ticket = Console.ReadLine();

                if (ticket.Length != 6 || !IsNumber(ticket))
                {
                    Console.WriteLine("Ошибка: введите корректный шестизначный номер");
                    return;
                }

                int[] digits = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    digits[i] = int.Parse(ticket[i].ToString());
                }

                int sumFirst = digits[0] + digits[1] + digits[2];
                int sumLast = digits[3] + digits[4] + digits[5];

                if (sumFirst == sumLast)
                    Console.WriteLine("Билет счастливый!");
                else
                    Console.WriteLine("Билет не счастливый.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static bool IsNumber(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }

    public static class Task3
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Задание 3: Сокращение дроби ===");
            
            try
            {
                Console.Write("Введите числитель M: ");
                int m = int.Parse(Console.ReadLine());

                Console.Write("Введите знаменатель N: ");
                int n = int.Parse(Console.ReadLine());

                if (n == 0)
                {
                    Console.WriteLine("Ошибка: знаменатель не может быть равен нулю");
                    return;
                }

                int gcd = FindGCD(Math.Abs(m), Math.Abs(n));
                int numerator = m / gcd;
                int denominator = n / gcd;

                if (denominator < 0)
                {
                    numerator = -numerator;
                    denominator = -denominator;
                }

                if (denominator == 1)
                    Console.WriteLine($"Несократимая дробь: {numerator}");
                else
                    Console.WriteLine($"Несократимая дробь: {numerator}/{denominator}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static int FindGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }

    public static class Task4
{
    public static void Run()
    {
        Console.WriteLine("\n=== Задание 4: Угадай число ===");
        
        Console.WriteLine("Загадайте число от 0 до 63. Я попробую его угадать.");
        Console.WriteLine("Отвечайте '1' (да) или '0' (нет) на мои вопросы.");

        int guess = BinarySearchGuess();
        Console.WriteLine($"\nВаше число: {guess}");
        Console.WriteLine("Я угадал?");
    }

    static int BinarySearchGuess()
    {
        int low = 0;
        int high = 63;
        int[] questions = { 32, 16, 8, 4, 2, 1 };

        foreach (int step in questions)
        {
            int mid = low + step;
            Console.Write($"Ваше число больше или равно {mid}? (1/0): ");
            string answer = Console.ReadLine();

            if (answer == "1")
            {
                low = mid;
            }
        }

        return low;
    }
}
    public static class Task5
    {
        private const int AMERICANO_WATER = 300;
        private const int LATTE_WATER = 30;
        private const int LATTE_MILK = 270;
        private const int AMERICANO_PRICE = 150;
        private const int LATTE_PRICE = 170;

        private static int water;
        private static int milk;
        private static int americanoCount = 0;
        private static int latteCount = 0;

        public static void Run()
        {
            Console.WriteLine("\n=== Задание 5: Кофейный аппарат ===");
            
            try
            {
                Console.Write("Введите количество воды (мл): ");
                water = int.Parse(Console.ReadLine());

                Console.Write("Введите количество молока (мл): ");
                milk = int.Parse(Console.ReadLine());

                while (true)
                {
                    if (!CanMakeAnyDrink())
                    {
                        GenerateReport();
                        break;
                    }

                    Console.WriteLine("\nВыберите напиток:");
                    Console.WriteLine("1 - Американо");
                    Console.WriteLine("2 - Латте");
                    Console.WriteLine("0 - Завершить работу");
                    Console.Write("Ваш выбор: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": MakeAmericano(); break;
                        case "2": MakeLatte(); break;
                        case "0": GenerateReport(); return;
                        default: Console.WriteLine("Неверный выбор"); break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MakeAmericano()
        {
            if (water >= AMERICANO_WATER)
            {
                water -= AMERICANO_WATER;
                americanoCount++;
                Console.WriteLine($"Готовится Американо. Стоимость: {AMERICANO_PRICE} руб.");
                ShowRemainingResources();
            }
            else
            {
                Console.WriteLine("Недостаточно воды для приготовления Американо");
            }
        }

        static void MakeLatte()
        {
            if (water >= LATTE_WATER && milk >= LATTE_MILK)
            {
                water -= LATTE_WATER;
                milk -= LATTE_MILK;
                latteCount++;
                Console.WriteLine($"Готовится Латте. Стоимость: {LATTE_PRICE} руб.");
                ShowRemainingResources();
            }
            else
            {
                Console.WriteLine("Недостаточно ингредиентов для приготовления Латте");
            }
        }

        static bool CanMakeAnyDrink()
        {
            return (water >= AMERICANO_WATER) || (water >= LATTE_WATER && milk >= LATTE_MILK);
        }

        static void ShowRemainingResources()
        {
            Console.WriteLine($"Осталось воды: {water} мл");
            Console.WriteLine($"Осталось молока: {milk} мл");
        }

        static void GenerateReport()
        {
            Console.WriteLine("\n=== ОТЧЕТ ===");
            Console.WriteLine("Ингредиенты подошли к концу");
            Console.WriteLine($"Остаток воды: {water} мл");
            Console.WriteLine($"Остаток молока: {milk} мл");
            Console.WriteLine($"Приготовлено чашек Американо: {americanoCount}");
            Console.WriteLine($"Приготовлено чашек Латте: {latteCount}");
            
            int totalEarnings = (americanoCount * AMERICANO_PRICE) + (latteCount * LATTE_PRICE);
            Console.WriteLine($"Итоговый заработок: {totalEarnings} руб.");
        }
    }

    public static class Task6
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Задание 6: Лабораторный опыт ===");
            
            try
            {
                Console.Write("Введите количество бактерий (N): ");
                int N = int.Parse(Console.ReadLine());

                Console.Write("Введите количество капель антибиотика (X): ");
                int X = int.Parse(Console.ReadLine());

                int bacteria = N;
                int hours = 0;
                int killPower = 10;

                Console.WriteLine("\nДинамика изменения количества бактерий:");

                while (bacteria > 0 && killPower > 0)
                {
                    hours++;
                    bacteria *= 2;

                    int totalKill = killPower * X;
                    bacteria -= totalKill;

                    killPower--;

                    if (bacteria < 0) bacteria = 0;

                    Console.WriteLine($"Час {hours}: Бактерий = {bacteria}, Мощность антибиотика = {killPower}");

                    if (bacteria == 0 || killPower == 0)
                        break;
                }

                Console.WriteLine($"\nПроцесс завершен через {hours} часов");
                Console.WriteLine($"Конечное количество бактерий: {bacteria}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    public static class Task7
{
    public static void Run()
    {
        Console.WriteLine("\n=== Задание 7: Колонизация Марса ===");
        
        try
        {
            // Пошаговый ввод переменных
            Console.Write("Введите количество модулей (n): ");
            int n = int.Parse(Console.ReadLine());

            Console.Write("Введите длину модуля (a): ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину модуля (b): ");
            int b = int.Parse(Console.ReadLine());

            Console.Write("Введите ширину поля (w): ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Введите высоту поля (h): ");
            int h = int.Parse(Console.ReadLine());

            int maxD = CalculateMaxProtection(n, a, b, w, h);

            if (maxD == -1)
                Console.WriteLine("Размещение модулей невозможно");
            else
                Console.WriteLine($"Максимальная толщина защиты: {maxD}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static int CalculateMaxProtection(int n, int a, int b, int w, int h)
    {
        if (!CanPlaceModules(n, a, b, w, h, 0))
            return -1;

        int left = 0;
        int right = Math.Min(w, h);
        int result = 0;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (CanPlaceModules(n, a, b, w, h, mid))
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }

    static bool CanPlaceModules(int n, int a, int b, int w, int h, int d)
    {
        int aWithD = a + 2 * d;
        int bWithD = b + 2 * d;

        return (w >= aWithD && h >= bWithD && CanFit(n, aWithD, bWithD, w, h)) ||
               (w >= bWithD && h >= aWithD && CanFit(n, bWithD, aWithD, w, h));
    }

    static bool CanFit(int n, int moduleWidth, int moduleHeight, int fieldWidth, int fieldHeight)
    {
        int maxWidth = fieldWidth / moduleWidth;
        int maxHeight = fieldHeight / moduleHeight;
        return maxWidth * maxHeight >= n;
    }
}
}