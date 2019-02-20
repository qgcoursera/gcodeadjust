using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCodeAdjust
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showInfo;
            decimal diffX, diffY, diffZ;

            // parse args
            ParseArgs(
                args,
                out showInfo,
                out diffX,
                out diffY,
                out diffZ);

            string input = string.Empty;

            if(!showInfo)
            {
#if !DEBUG
                input = Console.In.ReadToEnd();
#else
                input = @"
;G1 X0a
; G1 X0b
G1 E0
G1 X0
G1 Y0
G1 Z0
G1 X0 Y0
G1 X0 Z0
G1 Y0 Z0
G1 X0 Y0 Z0
G1 E0
G1 X0 E0
G1 Y0 E0
G1 Z0 E0
G1 X0 Y0 E0
G1 X0 Z0 E0
G1 Y0 Z0 E0
G1 X0 Y0 Z0 E0";
#endif

                showInfo = input.Length < 1;
            }

            // display info, or start worker
            if (showInfo)
            {
                Console.WriteLine(
                    "GCodeAdjust [diffx] [diffy] [diffz]");
            }
            else
            {
                Console.WriteLine("input: " + input);

                Console.Out.WriteLine(
                    new Worker(input)
                        .Adjust(
                            diffX,
                            diffY,
                            diffZ));
            }
        }

        private static void ParseArgs(
            string[] args,
            out bool showInfo,
            out decimal diffX,
            out decimal diffY,
            out decimal diffZ)
        {
            diffX = diffY = diffZ = 0M;

            showInfo = args.Length < 1
                || args[0] == "/?";

            if (showInfo)
                return;

            diffX = decimal.Parse(args[0]);
            diffY = decimal.Parse(args[1]);
            diffZ = decimal.Parse(args[2]);
        }
    }
}
