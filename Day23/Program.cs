using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day23
{
    abstract class Instruction
    {
        protected string register;
        protected int offset;

        public abstract int Execute(Dictionary<string, uint> regfile);

        public static Instruction Parse(string x)
        {
            Instruction result = null;

            var parts = x.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

            string opcode = parts[0];
            switch (opcode)
            {
                case "hlf":
                    result = new HalfInstruction
                    {
                        register = parts[1]
                    };
                    break;
                case "tpl":
                    result = new TripleInstruction
                    {
                        register = parts[1]
                    };
                    break;
                case "inc":
                    result = new IncrementInstruction
                    {
                        register = parts[1]
                    };
                    break;
                case "jmp":
                    result = new JumpInstruction
                    {
                        offset = int.Parse(parts[1])
                    };
                    break;
                case "jie":
                    result = new JumpIfEvenInstruction
                    {
                        register = parts[1],
                        offset = int.Parse(parts[2])
                    };
                    break;
                case "jio":
                    result = new JumpIfOneInstruction
                    {
                        register = parts[1],
                        offset = int.Parse(parts[2])
                    };
                    break;
            }

            return result;
        }
    }

    class HalfInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            regfile[register] /= 2;

            return 1;
        }
    }

    class TripleInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            regfile[register] *= 3;

            return 1;
        }
    }

    class IncrementInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            regfile[register]++;

            return 1;
        }
    }

    class JumpInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            return offset;
        }
    }

    class JumpIfEvenInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            if (regfile[register] % 2 == 0)
            {
                return offset;
            }

            return 1;
        }
    }

    class JumpIfOneInstruction : Instruction
    {
        public override int Execute(Dictionary<string, uint> regfile)
        {
            if (regfile[register] == 1)
            {
                return offset;
            }

            return 1;
        }
    }

    class Computer
    {
        public int programCounter;
        public Dictionary<string, uint> registerFile = new Dictionary<string, uint> { { "a", 0 }, { "b", 0 } };
        public List<Instruction> instructionMemory;

        public void Run()
        {
            while (0 <= programCounter && programCounter < instructionMemory.Count)
            {
                var currentInstruction = instructionMemory[programCounter];
                programCounter += currentInstruction.Execute(registerFile);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string input = "jio a, +19\ninc a\ntpl a\ninc a\ntpl a\ninc a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\njmp +23\ntpl a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ninc a\ninc a\ntpl a\ntpl a\ninc a\njio a, +8\ninc b\njie a, +4\ntpl a\ninc a\njmp +2\nhlf a\njmp -7";

            string[] instructionStrings = input.Split('\n');

            List<Instruction> program = instructionStrings.Select(s => Instruction.Parse(s)).ToList();


            var computer1 = new Computer
            {
                instructionMemory = program
            };

            computer1.Run();

            uint answer1 = computer1.registerFile["b"];

            Console.WriteLine("Answer 1: {0}", answer1);


            var computer2 = new Computer
            {
                instructionMemory = program
            };

            computer2.registerFile["a"] = 1;
            computer2.Run();

            uint answer2 = computer2.registerFile["b"];

            Console.WriteLine("Answer 2: {0}", answer2);

            Console.ReadKey();
        }
    }
}
