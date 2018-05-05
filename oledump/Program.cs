/**************************************
 *
 * Part of OLEWOO - http://www.benf.org
 *
 * CopyLeft, but please credit.
 *
 */

using System;
using Org.Benf.OleWoo.Typelib;

namespace Org.Benf.OleDump
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length < 1) throw new Exception("oledump TLBNAME");
                var tl = new OWTypeLib(args[0]);
                var pi = new PlainIDLFormatter();
                tl.BuildIDLInto(pi);
                System.Console.WriteLine(pi.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine("OleDump:\r\n");
                System.Console.Error.WriteLine("Error : " + e.Message);
            }
            System.Console.ReadKey();
        }
    }
}
