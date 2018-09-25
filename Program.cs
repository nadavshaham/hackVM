using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace hackVM
{
    enum commandType { C_ARITHMETIC, C_PUSH, C_POP, C_LABEL, C_GOTO, C_IF, C_FUNCTION, C_RETURN, C_CALL, ERROR }


    class Program
    {
        public class Parser
        {
            private string currentCommand;
            private System.IO.StreamReader sourceFile;



            public Parser(string sourcePath)
            {
                sourceFile = new System.IO.StreamReader(sourcePath);
            }


            // Are there more commands in the input?
            public Boolean HasMoreCommands()
            {
                string line = "";
                while ((line = sourceFile.ReadLine()) != null)
                {
                    line = line.Trim();

                    // if line STARTS with "//" - next
                    if (line.StartsWith("/"))
                    {
                        continue;
                    }
                    // if line is empty - next
                    if (String.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    //line = line.TrimEnd('/');
                    if (line.Contains("//"))
                    {
                        int foundS1 = line.IndexOf('\u002f');

                        line = line.Remove(foundS1);
                    }
                    // hweaRRYYYY!!! I found a command!!!
                    // damm we still need to trim end // to get a clean command
                    line = line.Trim();
                    //if (sourceFile.Peek() != -1)
                    currentCommand = line;
                    return true;
                }

                return false;
            }
            public commandType CommandType()
            {
                if (currentCommand.StartsWith("function"))
                {
                    return commandType.C_FUNCTION;
                }
                if (currentCommand.StartsWith("push"))
                {
                    return commandType.C_PUSH;
                }
                if (currentCommand.StartsWith("pop"))
                {
                    return commandType.C_POP;
                }
                if (currentCommand.StartsWith("label"))
                {
                    return commandType.C_LABEL;
                }
                if (currentCommand.StartsWith("add") || currentCommand.StartsWith("sub") || currentCommand.StartsWith("and") || currentCommand.StartsWith("or") || currentCommand.StartsWith("it") || currentCommand.StartsWith("eq") || currentCommand.StartsWith("gt") || currentCommand.StartsWith("neg") || currentCommand.StartsWith("not"))
                {
                    return commandType.C_ARITHMETIC;
                }
                if (currentCommand.StartsWith("goto"))
                {
                    return commandType.C_GOTO;
                }
                if (currentCommand.StartsWith("call"))
                {
                    return commandType.C_CALL;
                }
                if (currentCommand.StartsWith("return"))
                {
                    return commandType.C_RETURN;
                }
                if (currentCommand.StartsWith("if"))
                {
                    return commandType.C_IF;
                }
                return commandType.ERROR;
            }
            public string arg1()
            {
                if (CommandType() == commandType.C_ARITHMETIC)
                {
                    Regex arg = new Regex(@"([a-z]+)");
                    Match match = arg.Match(currentCommand);
                    return (match.Value);
                }
                else if (CommandType() == commandType.C_GOTO || CommandType() == commandType.C_IF || CommandType() == commandType.C_LABEL)
                {
                    Regex rx = new Regex(@" (.*)(\d+)");
                    Match match = rx.Match(currentCommand);

                    return (match.Value.Remove(0, 1));
                }
                else
                {
                    Regex rx = new Regex(@" (.*) ");
                    Match match = rx.Match(currentCommand);
                    return (match.Value.Remove(0, 1));
                }
            }
            public int Arg2()
            {
                if (CommandType() == commandType.C_PUSH || CommandType() == commandType.C_POP || CommandType() == commandType.C_FUNCTION || CommandType() == commandType.C_CALL)
                {


                    Regex rx = new Regex(@" (\d+)");
                    Match match = rx.Match(currentCommand);
                    return (int.Parse(match.Value.Remove(0, 1)));
                }
                return (000);
            }



        }
        public class CodeWriter
        {
            private System.IO.StreamReader sourceFile;

            public CodeWriter(string sourcePath)
            {
                sourceFile = new System.IO.StreamReader(sourcePath);
            }

            //writes the assembly code that is the translation of the given trithmetic command
            public string WriteArithmetic(string command)
            {
                return "";
            }
            //write the assembly code that is the translation of the given command' where command is either C_PUSH or C_POP
            public string WritePushPop(commandType command, string segment, int index)
            {
                return "";
            }


        }


        static void Main(string[] args)
        {
            string fileName = "D:\\Nadav\\HDL\\OS\\Array.vm";



            System.IO.StreamWriter destFile = new System.IO.StreamWriter("D:\\Nadav\\HDL\\OS\\Arry.asm");
            Parser Parser = new Parser(fileName);
            while (Parser.HasMoreCommands())
            {
                if (Parser.CommandType() != commandType.C_RETURN)
                {
                    Console.WriteLine(Parser.arg1());
                    Console.WriteLine(Parser.Arg2());
                }

            }
        }
    }
}
