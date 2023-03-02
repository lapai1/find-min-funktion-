
namespace LR
{
    class Program
    {
        static double f(double x)
        {
            return Math.Pow(x, 2) + 6 * x + 12; // заданная функция 
        }

        static void Main(string[] args)
        {
            //double x0 = 1;
            Console.Write("Введите х0: ");
            double x0 = Convert.ToDouble(Console.ReadLine());
            //double t = 3;
            Console.Write("Введите t: ");
            double t = Convert.ToDouble(Console.ReadLine()); // шаг. (2 по условию)
            Console.Write("Введите intervalLeft: ");
            double intervalLeft = Convert.ToDouble(Console.ReadLine());
            //double intervalLeft = -10;
            Console.Write("Введите intervalRight: ");
            double intervalRight = Convert.ToDouble(Console.ReadLine());
            //double intervalRight = 10;
            Console.Write("Введите epsilon: ");
            double epsilon = Convert.ToDouble(Console.ReadLine());
            //double epsilon = 0.001;
            Console.Write("Введите k: ");
            double k = Convert.ToDouble(Console.ReadLine());
            //double k = 0.618;//((Math.Pow(5,05))-1)/2;
            Console.WriteLine($"Искомый интервал по методу Свенна: {Svenn(x0, t)}"); // метод Свенна 
            double xMinDiv = IntervalDivisionMethod(intervalLeft, intervalRight, epsilon, k);
            Console.WriteLine($"Минимум функции по методу деления отрезка пополам: x = {xMinDiv}; f(x) = {f(xMinDiv)}"); // метод деление интервала 
            double xMinGold = GoldenRatio(intervalLeft, intervalRight, epsilon, k);
            Console.WriteLine($"Минимум функции по методу золотого сечения: x = {xMinGold}; f(x) = {f(xMinGold)}"); //метод золотого сечения 
        }


        // Функция метода деления интервалов которую вызываем в Main 
        public static double IntervalDivisionMethod(double intervalLeft, double intervalRight, double epsilon, double k)
        {
            double middlePoint = (intervalLeft + intervalRight) / 2; // берем значения отрезков и делим на 2
            double L = Math.Abs(intervalRight - intervalLeft); //модуль от разницы правого и левого инетервала(интервалы заданы вначале)
            return RecursiveCalculation(intervalLeft, intervalRight, L, middlePoint, epsilon, k);
        }
        


        // рекурсивные рассчеты 
        public static double RecursiveCalculation(double ak, double bk, double L, double middlePoint, double epsilon, double k)
        {
            double yk = ak + L / 4;
            double zk = bk - L / 4;

            double nextA, nextB, nextXc, nextL;

            if (f(yk) < f(middlePoint))
            {
                nextA = ak;
                nextB = middlePoint;
                nextXc = yk;
            }
            else
            {
                if (f(zk) < f(middlePoint))
                {
                    nextA = middlePoint;
                    nextB = bk;
                    nextXc = zk;
                }
                else
                {
                    nextA = yk;
                    nextB = zk;
                    nextXc = middlePoint;
                }
            }
            nextL = Math.Abs(nextB - nextA);
            if (nextL <= epsilon)
                return nextXc;
            k++;
            return RecursiveCalculation(nextA, nextB, nextL, nextXc, epsilon, k);
        }
        // с++
        //double min_f = f(e), min = e;
        //for (double x=s; x<e; x+=EPS) {
       // double val = f(x);
        //if (val<min_f){
         //   min=x;
           // min_f=val


        // метод золотого сечения который вызываем в Main
        public static double GoldenRatio(double intervalLeft, double intervalRight, double epsilon, double k)
        {
            //interval [-10,10]
            double x1 = intervalLeft + (1 - k) * (intervalRight - intervalLeft);
            double y1 = f(x1);
            double x2 = intervalLeft + k * (intervalRight - intervalLeft);
            double y2 = f(x2);

            while (intervalRight - intervalLeft > epsilon)
            {
                if (y1 > y2)
                {
                    intervalLeft = x1;
                    x1 = x2;
                    x2 = intervalLeft + k * (intervalRight - intervalLeft);
                    y1 = y2;
                    y2 = f(x2);
                }
                else
                {
                    intervalRight = x2;
                    x2 = x1;
                    x1 = intervalLeft + (1 - k) * (intervalRight - intervalLeft);
                    y2 = y1;
                    y1 = f(x1);
                }
            }
            return (intervalLeft + intervalRight) / 2;
        }


        
        // метод Свена
        public static string Svenn(double x0, double t)
        {
            double a0 = 0, b0 = 0;
            if (f(x0 - t) >= f(x0) && f(x0) <= f(x0 + t))
            {
                a0 = x0 - t;
                b0 = x0 + t;
                return a0.ToString() + " " + b0.ToString();
            }
            if (f(x0 - t) >= f(x0) && f(x0) >= f(x0 + t))
            {
                return "Не является унимодальной";
            }
            // Унимодальная функция - непрерывная функция, для которой существует
            // не более одного локального экстремума в заданном диапазоне. 

            double delta = 1, x1;
            int k = 1;
            if (f(x0 - t) >= f(x0) && f(x0) >= f(x0 + t))
            {
                delta = t;
                a0 = x0;
                x1 = x0 + t;
                k = 1;
            }
            else
            {
                delta = -t;
                b0 = x0;
                x1 = x0 - t;
                k = 1;
            }


            // бесконечный цикл
            // while(true) ~ for (; ; )
            for (; ; )
            {
                double xF = x0 + Math.Pow(2, k) * delta;
                if (f(xF) < f(x0))
                {
                    if (delta == t)
                    {
                        a0 = x0;
                        x0 = xF;
                    }
                    if (delta == -t)
                    {
                        b0 = x0;
                        x0 = xF;
                    }
                }
                if (f(xF) >= f(x0))
                {
                    if (delta == t)
                    {
                        b0 = xF;
                    }
                    if (delta == -t)
                    {
                        a0 = xF;
                    }
                }
                break;
            }
            return a0.ToString() + ";" + b0.ToString();
        }

    }
}
