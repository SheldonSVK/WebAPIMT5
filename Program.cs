using System;
using System.Collections.Generic;
using MetaQuotes.MT5WebAPI;

namespace WebAPIMT5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var web = new MT5WebAPI();
            var server = "";
            var port = 0;
            ulong login = 0;
            var password = "";

            //Connect
            var res = web.Connect(server, port, login, password, MT5WebAPI.EnPumpModes.PUMP_MODE_NONE);
            Console.WriteLine(res);

            //Test MTRetCode
            if (res != MTRetCode.MT_RET_OK)
                throw new Exception($"Login error {res.ToString()}");

            //Ping
            if (web.Ping() != MTRetCode.MT_RET_OK)
                throw new Exception($"Ping error {res.ToString()}");

            //AddPamm
            byte[] answer;
            web.CustomSend("AddPamm", new Dictionary<string, string> {{"kluc0", "100,100,0,"},{"kluc1", "101,101,1,10,"},{"kluc2", "102,102,2,10,"},{"kluc3", "103,103,3,"}}, "", out answer);
            Console.WriteLine(System.Text.Encoding.Default.GetString(answer));
            /*Odpoved
            A d d P a m m | R E T C O D E = 0   D o n e | M e s s a g e = s u c c e s s |
            */

            //GetPamm
            web.CustomSend("GetPamm", new Dictionary<string, string>(), "", out answer);
            Console.WriteLine(System.Text.Encoding.Default.GetString(answer));
            /* Odpoved (Len prezentačne, výpis nemám hotový, urobím tak ako potrebuješ. Môžem meniť len to čo je medzi |. Prvé dva stlpce meniť nemôžem
            G e t P a m m | R E T C O D E = 0   D o n e | k l u c 0 = 1 0 0 , 1 0 0 , 0 | k l u c 1 = 1 0 1 , 1 0 1 , 0 | k l u c 2   1 0 2 , 1 0 2 , 0 | k l u c 3 = 1 0 3 , 1 0 3 , 0 | M e s s a g e = s u c c e s s |
            */

            //DeletePamm
            web.CustomSend("DeletePamm", new Dictionary<string, string> {{"kluc3", ""}}, "", out answer);
            Console.WriteLine(System.Text.Encoding.Default.GetString(answer));
            /* Opoved
            D e l e t e P a m m | R E T C O D E = 0   D o n e | M e s s a g e = s u c c e s s |
            */

            //GetPamm
            web.CustomSend("GetPamm", new Dictionary<string, string>(), "", out answer);
            Console.WriteLine(System.Text.Encoding.Default.GetString(answer));

            //Disconnect
            web.Disconnect();

            Console.ReadKey();
        }
    }
}