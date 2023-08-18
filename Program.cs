using System;
using System.ComponentModel.Design;
using System.Reflection.Metadata;

namespace WumpusGame
{
    class Program
    {
        static int[] psPlayer = new int[2];
        static int[] psMonster = new int[2];
        static int[] psTreasure = new int[2];
        static int dimensions;
        static int points = 5000;
        
        static void Main(string[] args)
        {
            
            Console.Clear();
            
            InsertDimensions();
            psPlayer = placeEntity();
            psMonster = placeEntity();
            psTreasure = placeEntity();
            
            
            Console.Clear();
            Menu();
            
        }
        
        static void Menu()
        {
            do
            {
                Console.Clear();
                CreateLevel();
                MoveEntitys();
                if (GameOver())
                {
                    Console.WriteLine("Você foi encontrado pelo monstro! Triste fim garoto...");
                    break;
                }
            } while (!ganhou());

            Console.WriteLine($"Sua pontuação foi de: {points}");
            
        }

        static bool ganhou()
        {
            if (psPlayer[0] == psTreasure[0] & psPlayer[1] == psTreasure[1])
            {
                Console.WriteLine("PARABÉNS VOCÊ GANHOUUUU!!");
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool GameOver()
        {
            if (psPlayer[0] == psMonster[0] & psPlayer[1] == psMonster[1])
            {
                return true;
                points -= 250;
            }
            else
            {
                return false;
            }
        }
        
        static void CreateLevel()
        {
            for (int colunas = 0; colunas < (dimensions+2); colunas++)
            {
                Console.Write("#");
            }
            Console.Write("\n");

            for (int linhas = 0; linhas < dimensions; linhas++)
            {
                Console.Write("#");
                for (int colunas = 0; colunas < dimensions; colunas++)
                {
                    Console.Write(" ");
                }
                Console.Write("#");
                Console.Write("\n");
            }
            
            for (int colunas = 0; colunas < (dimensions+2); colunas++)
            {
                Console.Write("#");
            }
            Console.Write("\n");
            
            Console.SetCursorPosition(psPlayer[0], psPlayer[1]);
            Console.Write("P");
            Console.SetCursorPosition(psMonster[0], psMonster[1]);
            Console.Write("W");
            Console.SetCursorPosition(psTreasure[0], psTreasure[1]);
            Console.Write("G");
            
            Console.SetCursorPosition(0, (dimensions + 4));
        }
        
        static void InsertDimensions()
        {
            int value = 0;
            string? txt;
            
            Console.Write("Informe a dimensão do Tabuleiro (obs: Minimo 5): ");
            
            txt = Console.ReadLine();
            try
            {
                if (txt is not null)
                {
                    value = int.Parse(txt);
                    if (value < 5)
                    {
                        Console.WriteLine("Por favor, insira um valor que seja maior ou igual a 5");
                        InsertDimensions();
                    }

                    dimensions = value;
                }
                else
                {
                    InsertDimensions();
                }
            }
            catch
            {
                Console.WriteLine("Insira um valor válido, por favor!");
                InsertDimensions();
            }
            
        }

        static int[] placeEntity()
        {
            var rand = new Random();
            var value = new int[2]; 
            if (psMonster is not null & psTreasure is null)
            {
                while (true)
                {
                    value[0] = (rand.Next() % (dimensions) + 1);
                    value[1] = (rand.Next() % (dimensions) + 1);

                    if (value[0] != psPlayer[0] & value[1] != psPlayer[0])
                    {
                        break;
                    }
                    
                }
            } else if (psTreasure is not null)
            {
                while (true)
                {
                    value[0] = (rand.Next() % (dimensions) + 1);
                    value[1] = (rand.Next() % (dimensions) + 1);

                    if (value[0] != psPlayer[0] & value[1] != psPlayer[0] & value[0] != psMonster[0] & value[1] != psMonster[1])
                    {
                        break;
                    }
                }
            }
            else
            {
                value[0] = (rand.Next() % (dimensions) + 1);
                value[1] = (rand.Next() % (dimensions) + 1);
            }
            
            return value;
        }

        static void MoveEntitys()
        {
            var randms = new Random();
            Console.WriteLine("Use os direcionais para mover-se: ");
            var key = Console.ReadKey().Key;
            points -= 50;            
            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    if(psPlayer[1]- 1 > 0)
                        psPlayer[1] -= 1;
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    if(psPlayer[1] + 1 <= dimensions)
                        psPlayer[1] += 1;
                    break;
                }
 
                case ConsoleKey.LeftArrow:
                {
                    if(psPlayer[0] - 1 > 0)
                        psPlayer[0] -= 1;
                    break;
                }
                case ConsoleKey.RightArrow:
                {
                    if(psPlayer[0] + 1 <= dimensions)
                        psPlayer[0] += 1;
                    break;
                }
                default: Console.WriteLine("Cuidado para não bater"); MoveEntitys(); break;
            }


            int msmove = (randms.Next() % 4);
            
            switch (msmove)
            {
                case 0:
                {
                    if (psMonster[1] - 1 > 0)
                        psMonster[1] -= 1;
                    break;
                }
                case 1:
                {
                    if(psMonster[1] + 1 <= dimensions)
                        psMonster[1] += 1;
                    break;
                }
 
                case 2:
                {
                    if(psMonster[0] - 1 > 0)
                        psMonster[0] -= 1;
                    break;
                }
                default:
                {
                    if (psMonster[0] + 1 <= dimensions)
                        psMonster[0] += 1;
                    break;
                }
                
            }
            
            
        }
        
        
    }
}