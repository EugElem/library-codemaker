using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace InspectorLib
{
    public class Code
    {
        static private string codeInput ="";
        static private long day;
        static private long month;
        static private long year;
        static private long date;

        static private long num;
        static private long number;
        static private long address;
        static private string inOrEx;

        static private string[] code3 = new string[4];
        static private string district;
        static private string house;
        static private string subject;

        static private long districtInt = 0;
        static private long houseInt;
        static private long subjectInt;



        // функция сеттер
        public List<string> SetGetCode(string codeIn)
        {
            codeInput = codeIn;
            CodeAlg(codeInput);
            return GetCode();
        }


        // функция алгоритма преобразования шифра
        private static void CodeAlg(string code)
        {
            Console.WriteLine("Введи шифр типа: ДДММГГ№№-Р-ЗЗ-ОО-н\n" +
                              "                 123456789012345678\n" +
                              "                 111222333№№нДДММГГ");

            //codeInput = Console.ReadLine();
            //codeInput = "12021903-Е-ФД-ГЕ-в";
            //codeInput = "12021903111н310519";
            Console.WriteLine($"введнный код: {codeInput}");
            //Console.ReadKey();


            Regex regex = new Regex(@"\d{8}-[А-Я]{1}-[А-Я]{2}-[А-Я]{2}-[вн]{1}");

            Regex regexInt = new Regex(@"\d{10}[нв]\d{6}");


            //Console.WriteLine(regex.IsMatch(codeInput));
            if (regex.IsMatch(codeInput))
            {

                // Разделение на подстроки, в качестве разделителя  "-"
                string[] code2 = codeInput.Split('-');

                //парсинг даты и номера
                number = int.Parse(code2[0]);
                year = number / 100 % 100;
                month = number / 10000 % 100;
                day = number / 1000000 % 100;
                Console.WriteLine($"номер договора {number}, год: {year}, месяц: {month}, день: {day}");
                // получение из шифра  района,дома, обьекта и типа проводки
                district = code2[1];
                house = code2[2];
                subject = code2[3];
                inOrEx = code2[4];
                Console.WriteLine("район: {0}, здание: {1}, обьект: {2}, тип проводки: {3}", district, house, subject, inOrEx);
                //Console.ReadKey();

            }
            else if (regexInt.IsMatch(codeInput))
            {
                string[] code2 = codeInput.Split('н');
                //парсинг даты и номера
                //Console.WriteLine($"{ code2[0]}\n{code2[1]}");               
                date = long.Parse(code2[1]);
                year = date % 100;
                month = date / 100 % 100;
                day = date / 10000 % 100;

                Regex regexType = new Regex(@"\D");
                Match m = regexType.Match(codeInput);
                inOrEx = m.Value;
                address = long.Parse(code2[0]);
                num = address % 100;

                Console.WriteLine($"номер договора {num}, год: {year}, месяц: {month}, день: {day}");
                districtInt = address / 100 % 100;
                district = Convert.ToString(Convert.ToChar(1040 + Convert.ToInt16(districtInt)));
                houseInt = address / 100000 % 1000;
                house = Converter(Convert.ToInt16(houseInt));
                subjectInt = address / 100000000;
                subject = Converter(Convert.ToInt16(subjectInt));

                // получение из шифра  района,дома, обьекта и типа проводки                         
                //Console.WriteLine("район: {0}, здание: {1}, обьект: {2}, тип проводки: {3}", districtInt, houseInt, subjectInt, inOrEx);
                //Console.WriteLine("район: {0}, здание: {1}, обьект: {2}, тип проводки: {3}", district, house, subject, inOrEx);
                Console.WriteLine("Код, конвертированный в цифро-буквенный вид: {0} {1} {2} {3}-{4}-{5}-{6}-{7}", day, month, year, num, district, house, subject, inOrEx);

            }
            else
            {
                Console.WriteLine("некорректный ввод");
            }

            //Console.ReadKey();          
        }

        //функция возврата ввиде списка
        public List<string> GetCode()
        {
            //string[] codeOut = new string[7];
            List<string> codeList = new List<string>()
            {
            Convert.ToString(day),
            Convert.ToString(month),
            Convert.ToString(year),
            Convert.ToString(num),
            (Convert.ToString(day)+Convert.ToString(month)+Convert.ToString(year)+Convert.ToString(num)),
            district,
            house,
            subject,
            inOrEx
            };
        
            foreach(string c in codeList) {
                Console.Write(" {c}");
            }

            return codeList;           
        }

        static private string Converter(int intToStr)
        {
            //Console.WriteLine("intToStr: {0}", intToStr);
            int str1, str2;
            char s1, s2;
            string str;
            str1 = intToStr / 33;
            str2 = intToStr % 33;

            // русские буквы в кодировке UTF-16, начинаются с 1040
            // 1040 - буква "А", здесь соответствует нулю ( буквы Ё нет, букв 32)
            s1 = Convert.ToChar(1040 + str1);
            s2 = Convert.ToChar(1040 + str2);
            str = Convert.ToString(s1) + Convert.ToString(s2);
            //Console.WriteLine("str1: {0}, str2: {1}, str{2}", str1, str2, str);

            return str;
        }
    }
}
