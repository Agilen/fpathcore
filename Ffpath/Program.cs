using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ffpath
{
    class Program
    {
        public class Foo
        {
            //[Index(0)]
            public string A { get; set; }
            //[Index(1)]
            public string B { get; set; }

        }

        static void Main(string[] args)
        {

            Algor al = new Algor();

            var lines = File.ReadAllLines("Data/G1.csv");
            var list = new List<Foo>();

            foreach (var line in lines) //считываем данные из G1.csv
            {
                var value = line.Split(',');
                var foo = new Foo() { A = value[0], B = value[1] };
                list.Add(foo);
            }


            List<string> IPA = new List<string>();
            List<string> IPB = new List<string>();


            IEnumerable Ip = list.Select(c => c.A).Distinct().OrderBy(c => c);// сортирую и удаляю дубликаты для создания графа
            IEnumerable Ip2 = list.Select(c => c.B).Distinct().OrderBy(c => c);

            foreach (var ip in Ip)// перевожу IEnumerable в  List, что бы было удобнее работать
            {
                IPA.Add(ip.ToString());
            }
            foreach (var ip in Ip)
            {
                IPB.Add(ip.ToString());
            }

            int[,] graf = new int[IPA.Count(), IPB.Count()];// создаю граф сети
            int x = 0;
            int y = 0;
            for (int i = 0; i < list.Count; i++)
            {
                x = 0;
                y = 0;
                if (list[i].A == list[i].B)
                {
                    continue;
                }
                while (IPA[x] != list[i].A)
                {
                    x++;
                }
                while (IPB[y] != list[i].B)
                {
                    y++;
                }
                graf[x, y] = 2;
                graf[y, x] = 2;
            }


            List<string> noCon = new List<string>();// проверка на узлы без соединений

            for (int i = 0; i < IPA.Count(); i++)
            {
                for (int j = 0; j < IPB.Count(); j++)
                {
                    if (graf[i, j] != 0)
                    {
                        break;
                    }
                    else if (j == IPB.Count() - 1 && graf[i, j] == 0)
                    {
                        noCon.Add(IPB[i]);
                    }
                }
            }

            for (int i = 0; i < IPA.Count; i++)// подготавливаю граф для Алгоритма Дейкстра
            {
                for (int j = 0; j < IPB.Count(); j++)
                {
                    if (graf[i, j] == 0)
                    {
                        graf[i, j] = int.MaxValue;
                    }
                }
            }

            Console.WriteLine("1.Хост *192.168.0.5* під керуванням ОС *Debian 10* виконує `ping 192.168.0.1 -c 1 > res.txt`. Що буде записано в файлі `res.txt` ? :)");
            int st = 0;
            int end = 0;

            for (int i = 0; i < IPA.Count(); i++)// 
            {
                if (IPA[i] == "192.168.0.5")
                    st = i;
                else if (IPA[i] == "192.168.0.1")
                    end = i;
            }

            al.Ping(IPA.Count(), st, graf, end, IPA);
            //
            Console.WriteLine("2.Який максимальний час утиліта `ping` може видати в підмережі і між якими вузлами?");

            al.Dijkstra(IPA.Count(), 0, graf, IPA);
            //
            Console.WriteLine("\n3.Чи існуючть вузли, між якими взагалі відсутнє мережеве з'єднання? Якщо так, то які?");

            for (int i = 0; i < noCon.Count(); i++)
            {
                Console.WriteLine(noCon[i]);
            }

            Console.ReadLine();

        }
    }
}
