using System;
namespace SnakeGame
{
    class Program
    {
        //Verdiğimiz diziler içerisinde verilen değerlerin olup olmadığını kontrol eden fonksiyon
        static bool contains(int[] arrX, int[] arrY, int x, int y, int size)
        {
            for (int j = 0; j < size; j++)
                if (arrX[j] == x && arrY[j] == y)
                    return true;
            return false;
        }
        static void Main(string[] args)
        {
            /*
             dx: x yönünde gitme (1: sağa, -1: sola),
             dy: y yönünde gitme (1: aşağı, -1: yukarı),
             x,y : imlecin konsol ekranı üzerisindeki konumu,
             x_values - y_values: yılanın her bir parçasının x ve y konularını,
             target_x - target_y: yemin x ve y konumu
             */
            int dx = 1, dy = 0, size = 1;
            int i = size , w = Console.WindowWidth-1, h = Console.WindowHeight-1;
            Random rnd = new Random();
            int x=0, y=0;
            int[] x_values = new int[size], y_values = new int[size];
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("WASD: directions\nQ: exit");
            Console.WriteLine("Press any key to start the game");
            Console.ReadKey(true);
            int target_x=rnd.Next(w), target_y=rnd.Next(h);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(target_x, target_y);
            Console.Write("*");
            //klavye tuş bilgisi
            ConsoleKeyInfo option ;
            Console.CursorVisible = false;
            bool finish = false;

            while(!finish)
            {
                Console.SetCursorPosition(x, y);
                /*
                 klavye de tuşa basıldığında devreye giren Console.KeyAvailable
                 switch yapısı içerisinde basılan tuşa göre aksiyonu belirleme yapılır
                 */
                if (Console.KeyAvailable)
                {
                    option = Console.ReadKey(true);
                    switch (option.Key)
                    {
                        case ConsoleKey.W:
                            if (dy != 1)
                            {
                                dy = -1;
                                dx = 0;
                            }
                            break;
                        case ConsoleKey.A:
                            if (dx != 1)
                            {
                                dy = 0;
                                dx = -1;
                            }
                            break;
                        case ConsoleKey.S:
                            if (dy != -1)
                            {
                                dy = 1;
                                dx = 0;
                            }
                            break;
                        case ConsoleKey.D:
                            if (dx != -1)
                            {
                                dx = 1;
                                dy = 0;
                            }
                            break;
                        case ConsoleKey.Q:
                            finish = true;
                            break;
                        default:
                            break;
                    }
                }
                //x ve y konumlarının atanması
                x += dx;
                if (x > w || x < 0)
                    break;
                y += dy;
                if (y > h || y < 0)
                    break;
                //yeni konumun yılanın x ve y konum dizilerine atanması
                if (i > 0)
                    i--;
                else
                    i = size-1;
                //yeni konumun yılan üzerine yani kendisine çarpma durumunda oyun bitirme
                if (contains(x_values, y_values, x, y, size))
                    finish = true;
                x_values[i] = x;
                y_values[i] = y;
                Console.ForegroundColor = ConsoleColor.Red;
                //yemi yeme durumu ve yani yem konumunun rastgele olarak belirlenmesi
                if (y == target_y && x ==target_x)
                {
                    int temp_x = target_x,  temp_y = target_y;
                    target_x = rnd.Next(w);
                    target_y = rnd.Next(h);
                    while (contains(x_values, y_values, target_x, target_y, size))
                    {
                        target_x = rnd.Next(w);
                        target_y = rnd.Next(h);
                    }
                    Console.SetCursorPosition(target_x, target_y);
                    Console.Write("*");
                    size += 1;
                    int[] temp =(int[]) x_values.Clone();
                    x_values = new int[size];
                    temp.CopyTo(x_values, 0);
                    temp = (int[])y_values.Clone();
                    y_values = new int[size];
                    temp.CopyTo(y_values, 0);
                    x_values[size-1] = temp_x;
                    y_values[size-1] = temp_y;
                }
                else
                {
                    Console.SetCursorPosition(target_x, target_y);
                    Console.Write("*");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                //yılanın tüm parçalarının konsol üzerine yerleştirilmesi
                for (int j = size-1; j >= 0; j--)
                {
                    Console.SetCursorPosition(x_values[j], y_values[j]);
                    Console.Write("*");
                }
                //oyun hız ayarı ve konsol ekrarnının temizlenmesi
                System.Threading.Thread.Sleep(50);
                Console.Clear();
            }
            Console.Clear();
            Console.WriteLine("*************************************************************");
            Console.WriteLine("************************ GAME OVER **************************");
            Console.WriteLine("*************************************************************");
            Console.WriteLine("SCORE: "+size);
        }
    }
}